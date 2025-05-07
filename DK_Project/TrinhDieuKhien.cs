using AxWMPLib;
using Dapper;
using DK_Project.Management;
using DK_Project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
delegate void AddMessage(string sNewMessage);

namespace DK_Project
{
    public partial class TrinhDieuKhien : Form
    {
        public static string IP_DB = ConfigurationManager.AppSettings["IP_DB_SERVER"];
        public static string connectionString = "metadata=res://*/Model.mdDiSan.csdl|res://*/Model.mdDiSan.ssdl|res://*/Model.mdDiSan.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=" + IP_DB + ";initial catalog=english;persist security info=True;user id=sa;password=12345;MultipleActiveResultSets=True;App=EntityFramework&quot;\" providerName=\"System.Data.EntityClient";
        private Socket sock;
        private byte[] byBuff = new byte[256];
        private event AddMessage addMessage;
        private server app;
        private int time;
        private int phanThiHienTai = 0, goiKDHienTai = 0;
        private int stt = 0;
        private int currentCau = 0;
        private int goiVDCurrent = 0;
        private int cauhoichudeId = 0;
        string directoryPath = Directory.GetCurrentDirectory();
        private string fullPath = "";
        private ds_cuocthi cuocThiHienTai = null;
        private int id_teamplaying = 0;
        private List<int> trangThaiLatCP = new List<int> { 1, 1, 1, 1, 1 };
        Dictionary<int, string> teamplayings = new Dictionary<int, string>();
        Dictionary<int, string> dVongChoi = new Dictionary<int, string>();
        QuaMienDiSanEntities _entity = new QuaMienDiSanEntities();
        List<ds_goicauhoikhoidong> lsDanhSachCauHoiKhoiDong = new List<ds_goicauhoikhoidong>();
        List<ds_goicaudiscovery> lsCauHoiPhuCP = new List<ds_goicaudiscovery>();
        List<clsDiemDisplay> dsDiemCP = new List<clsDiemDisplay>();
        List<clsDiemDisplay> dsDiemKD = new List<clsDiemDisplay>();
        List<clsDiemDisplay> dsDiemKP = new List<clsDiemDisplay>();
        List<clsDiemDisplay> dsDiemVD = new List<clsDiemDisplay>();
        List<clsDiemDisplay> dsDiemToanCuocThi = new List<clsDiemDisplay>();
        List<ds_goicauhoishining> lsCauHoiVeDich = new List<ds_goicauhoishining>();
        private ds_goicaudiscovery cauHoiPhuCurrent = null;
        SqlDataAccess sqlObject = new SqlDataAccess();
        private List<Socket> listClientSockets = new List<Socket>();

        public TrinhDieuKhien()
        {
            InitializeComponent();
            app = new server();
            connecServer();

            addMessage = new AddMessage(OnAddMessage);
        }

        public TrinhDieuKhien(int cuocThiId)
        {
            InitializeComponent();
            cuocThiHienTai = _entity.ds_cuocthi.Find(cuocThiId);
            lblCuocThi.Text = cuocThiHienTai.tencuocthi;
            app = new server();
            connecServer();

            addMessage = new AddMessage(OnAddMessage);
            dVongChoi.Add(0, "Tổng 4 phần thi");
            dVongChoi.Add(1, "Khởi động");
            dVongChoi.Add(2, "Khám phá và chia sẻ");
            dVongChoi.Add(3, "Thử thách");
            dVongChoi.Add(4, "Tỏa sáng");
            cbBMainVongThi.DataSource = new BindingSource(dVongChoi, null);
            cbBMainVongThi.DisplayMember = "Value";
            cbBMainVongThi.ValueMember = "Key";
            // Lấy danh sách đội và sắp xếp theo vitridoi
            var dsdoi = _entity.ds_doi
                .Where(x => x.vaitro == "TS" && x.cuocthiid == cuocThiHienTai.cuocthiid)
                .OrderBy(x => x.vitridoi)
                .ToList();

            // Tạo Dictionary chứa danh sách đội
            Dictionary<int, string> teamplayings = new Dictionary<int, string>();

            foreach (var doi in dsdoi)
            {
                teamplayings.Add(doi.doiid, $"TEAM {doi.vitridoi} - {doi.tennguoichoi} - {doi.tendoi}");
            }

            // Tạo binding source một lần
            var bindingSource = new BindingSource(teamplayings, null);

            // Gán cho các ComboBox/ListBox
            cbxDoiChoi.DataSource = new BindingSource(bindingSource, null);
            cbxDoiChoi.DisplayMember = "Value";
            cbxDoiChoi.ValueMember = "Key";

            slbDoiChoiCP.DataSource = new BindingSource(bindingSource, null);
            slbDoiChoiCP.DisplayMember = "Value";
            slbDoiChoiCP.ValueMember = "Key";

            cbbDoiChoiVD.DataSource = new BindingSource(bindingSource, null);
            cbbDoiChoiVD.DisplayMember = "Value";
            cbbDoiChoiVD.ValueMember = "Key";

            cbBDoiTraLoiVD.DataSource = new BindingSource(bindingSource, null);
            cbBDoiTraLoiVD.DisplayMember = "Value";
            cbBDoiTraLoiVD.ValueMember = "Key";

        }
        private void connecServer()
        {
            Cursor cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (sock != null && sock.Connected)
                {
                    sock.Shutdown(SocketShutdown.Both);
                    System.Threading.Thread.Sleep(10);
                    sock.Close();
                }
                string server_ip = ConfigurationManager.AppSettings["IP"];
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // IPEndPoint epServer = new IPEndPoint(IPAddress.Parse(app.ip), 399);
                IPEndPoint epServer = new IPEndPoint(IPAddress.Parse(server_ip), 399);
                sock.Blocking = false;
                AsyncCallback onconnect = new AsyncCallback(OnConnect);
                sock.BeginConnect(epServer, onconnect, sock);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Server Connect failed!");
            }
            Cursor.Current = cursor;
        }

        public void OnConnect(IAsyncResult ar)
        {

            Socket sock = (Socket)ar.AsyncState;


            try
            {
                if (sock.Connected)
                    SetupRecieveCallback(sock);
                else
                    MessageBox.Show(this, "khong cho phep connect den may o xa", "loi ket noi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("loi khi thuc hien connect!");
            }
        }
        //public void SetupRecieveCallback(Socket sock)
        //{
        //    try
        //    {
        //        AsyncCallback recieveData = new AsyncCallback(OnRecievedData);
        //        sock.BeginReceive(byBuff, 0, byBuff.Length, SocketFlags.None, recieveData, sock);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(this, ex.Message, "Setup Recieve Callback failed!");
        //    }
        //}
        public void SetupRecieveCallback(Socket sock)
        {
            try
            {
                AsyncCallback recieveData = new AsyncCallback(OnRecievedData);
                sock.BeginReceive(byBuff, 0, byBuff.Length, SocketFlags.None, recieveData, sock);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Setup Recieve Callback failed!");
            }
        }

        public void OnAddMessage(string sMessage)
        {
            //Cấu trúc tin nhắn nhận được từ client: x,y,z
            //Trong đó x: id đội, y (cli: tin nhắn từ thí sinh, mc, trình chiếu, ser: tin nhắn từ trình điều khiển) z: thao tác (ví dụ connected, playv1, playv2, ....), z: tham số ứng với từng thao tác (ví dụ đối với connected, z = on hoặc off)
            string[] spl = sMessage.Split(',');
            id_teamplaying = int.Parse(spl[0]);
            string src = spl[1];
            string action = spl[2];
            if (src.Equals("cli"))
            {
                if (action.Equals("connected") && spl.Length > 3)
                {
                    if (int.TryParse(spl[0], out int doiId))
                    {
                        var objDoi = _entity.ds_doi.Find(doiId);

                        if (objDoi != null)
                        {
                            if (spl[3] == "on")
                            {
                                accessDataConnected(dgvConnected, true, objDoi);
                            }
                            else
                            {

                                accessDataConnected(dgvConnected, false, objDoi);
                                MessageBox.Show("Thí sinh: " + objDoi.tennguoichoi + " ngắt kết nối!");
                            }
                        }
                    }
                }

                if (action.Equals("playkhoidong"))
                {
                    if (spl[3] == "goi1")
                    {
                        disableGoiCauHoiKhoiDong(1);
                        lsDanhSachCauHoiKhoiDong = getDsCauHoiByGoiId(int.Parse(spl[0]));
                        loadDanhSachCauHoiKD(lsDanhSachCauHoiKhoiDong);
                    }
                }
                if (action.Equals("playthuthach"))
                {
                    updateKetQua(int.Parse(spl[3]));
                }
            }
            Console.WriteLine(sMessage);

        }

        public bool checkStatusDoiInfo(int doiId)
        {
            bool result = false;
            try
            {
                string sql = "SELECT * FROM ds_userlogin where doiid = " + doiId;
                DataTable dt = sqlObject.getDataFromSql(sql, "").Tables[0];
                if (dt != null)
                {
                    int trangThai = int.Parse(dt.Rows[0]["trangthai"].ToString());
                    bool isReconnect = bool.Parse(dt.Rows[0]["isreconnect"].ToString());
                    if (trangThai == 1 && !isReconnect)
                    {
                        result = true;
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public List<ds_goicauhoikhoidong> getDsCauHoiByGoiId(int goiId)
        {
            return _entity.ds_goicauhoikhoidong.Where(x => x.goicauhoiid.Equals(goiId) && x.cuocthiid == cuocThiHienTai.cuocthiid).ToList();
        }

        public void disableGoiCauHoiKhoiDong(int phanthiid)
        {
            switch (phanthiid)
            {
                case 1:
                    btnKDGoi1.Enabled = false;
                    break;
                case 2:
                    btnKDGoi2.Enabled = false;
                    break;
                case 3:
                    btnKDGoi3.Enabled = false;
                    break;
                case 4:
                    btnKDGoi4.Enabled = false;
                    break;
                case 5:
                    btnKDGoi5.Enabled = false;
                    break;
                case 6:
                    btnKDGoi6.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        public void loadDanhSachCauHoiKD(List<ds_goicauhoikhoidong> lsDanhSachCauHoiKhoiDong)
        {
            grvDanhSachCauHoiKD.Rows.Clear();
            grvDanhSachCauHoiKD.Refresh();
            if (lsDanhSachCauHoiKhoiDong != null && lsDanhSachCauHoiKhoiDong.Count > 0)
            {
                for (int i = 0; i < lsDanhSachCauHoiKhoiDong.Count; i++)
                {
                    DataGridViewRow dataGridViewRow = (DataGridViewRow)grvDanhSachCauHoiKD.Rows[0].Clone();
                    dataGridViewRow.Cells[0].Value = lsDanhSachCauHoiKhoiDong[i].noidungcauhoi;
                    dataGridViewRow.Cells[1].Value = lsDanhSachCauHoiKhoiDong[i].dapan;
                    grvDanhSachCauHoiKD.Rows.Add(dataGridViewRow);
                }
            }
        }

        public void accessDataConnected(DataGridView dgv, bool connected, ds_doi objDoi)
        {
            if (connected)
            {
                stt++;
                DataGridViewRow dataGridViewRow = (DataGridViewRow)dgv.Rows[0].Clone();
                dataGridViewRow.Cells[0].Value = objDoi.doiid;
                dataGridViewRow.Cells[1].Value = stt;
                dataGridViewRow.Cells[2].Value = objDoi.tendoi.ToString();
                dataGridViewRow.Cells[3].Value = objDoi.tennguoichoi.ToString();
                dataGridViewRow.Cells[4].Value = "Đã kết nối";
                dgv.Rows.Add(dataGridViewRow);

                if (objDoi.vaitro.Equals("TS"))
                {
                    clsDiemDisplay _diemDisplay = new clsDiemDisplay()
                    {
                        doiid = objDoi.doiid,
                        tenthisinh = objDoi.tennguoichoi,
                        tentruong = objDoi.tendoi,
                        sodiem = 0
                    };
                    dsDiemCP.Add(_diemDisplay);
                    dsDiemVD.Add(_diemDisplay);
                    dsDiemKD.Add(_diemDisplay);
                    dsDiemKP.Add(_diemDisplay);
                }

                if (objDoi.vitridoi == 1)
                {
                    cuocThiHienTai.doi1id = objDoi.doiid;
                }
                else if (objDoi.vitridoi == 2)
                {
                    cuocThiHienTai.doi2id = objDoi.doiid;
                }
                else if (objDoi.vitridoi == 3)
                {
                    cuocThiHienTai.doi3id = objDoi.doiid;
                }
                else if (objDoi.vitridoi == 4)
                {
                    cuocThiHienTai.doi4id = objDoi.doiid;
                }

                _entity.SaveChanges();

                var dsdoi = _entity.ds_doi.Where(x => x.vaitro == "TS" && x.cuocthiid == cuocThiHienTai.cuocthiid).ToList();
                for (int i = 0; i < dsdoi.Count; i++)
                {
                    if (!teamplayings.ContainsKey(dsdoi[i].doiid))
                    {
                        teamplayings.Add(dsdoi[i].doiid, "TEAM " + dsdoi[i].vitridoi + " - " + dsdoi[i].tennguoichoi + " - " + dsdoi[i].tendoi);
                    }
                }

                UpdateDataSources();
            }
            else
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells[0].Value != null && row.Cells[0].Value.ToString().Equals(objDoi.doiid.ToString()))
                    {
                        dgv.Rows.Remove(row);
                    }
                }

                if (stt > 0)
                {
                    stt--;
                }
                var dsdoi = _entity.ds_doi.Where(x => x.vaitro == "TS" && x.cuocthiid == cuocThiHienTai.cuocthiid).ToList();
                for (int i = 0; i < dsdoi.Count; i++)
                {
                    if (!teamplayings.ContainsKey(dsdoi[i].doiid))
                    {
                        teamplayings.Add(dsdoi[i].doiid, "TEAM " + dsdoi[i].vitridoi + " - " + dsdoi[i].tennguoichoi + " - " + dsdoi[i].tendoi);
                    }
                }

                UpdateDataSources();
            }
        }

        private void UpdateDataSources()
        {
            cbxDoiChoi.DataSource = new BindingSource(teamplayings, null);
            cbxDoiChoi.DisplayMember = "Value";
            cbxDoiChoi.ValueMember = "Key";

            slbDoiChoiCP.DataSource = new BindingSource(teamplayings, null);
            slbDoiChoiCP.DisplayMember = "Value";
            slbDoiChoiCP.ValueMember = "Key";

            cbbDoiChoiVD.DataSource = new BindingSource(teamplayings, null);
            cbbDoiChoiVD.DisplayMember = "Value";
            cbbDoiChoiVD.ValueMember = "Key";

            cbBDoiTraLoiVD.DataSource = new BindingSource(teamplayings, null);
            cbBDoiTraLoiVD.DisplayMember = "Value";
            cbBDoiTraLoiVD.ValueMember = "Key";

            //slbDoiTraLoiCP.DataSource = new BindingSource(teamplayings, null);
            //slbDoiTraLoiCP.DisplayMember = "Value";
            //slbDoiTraLoiCP.ValueMember = "Key";
        }

        private void SendEvent(string str)
        {
            // Check we are connected
            if (sock == null || !sock.Connected)
            {
                MessageBox.Show(this, "Must be connected to Send a message");
                return;
            }
            // Read the message from the text box and send it
            try
            {
                // Convert to byte array and send.
                Byte[] byteDateLine = Encoding.ASCII.GetBytes(str.ToCharArray());
                sock.Send(byteDateLine, byteDateLine.Length, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Send lenh dieu khien loi!");
            }
        }
        public void ReconnectFormDisplay(Socket sock)
        {
            try
            {
                // Gửi tín hiệu xác nhận khi form hiển thị mở lại
                byte[] message = Encoding.ASCII.GetBytes("RECONNECT");
                sock.Send(message);
                Console.WriteLine("Đã gửi tín hiệu RECONNECT đến form hiển thị.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi gửi tín hiệu Reconnect: " + ex.Message);
            }
        }

        public void OnRecievedData(IAsyncResult ar)
        {
            Socket socks = (Socket)ar.AsyncState;
            try
            {
                int nBytesRec = socks.EndReceive(ar);
                if (nBytesRec > 0)
                {
                    string sRecieved = Encoding.ASCII.GetString(byBuff, 0, nBytesRec);

                    // Kiểm tra xem form đã được khởi tạo và có handle chưa
                    if (this.IsHandleCreated)
                    {
                        this.BeginInvoke(addMessage, new string[] { sRecieved });
                    }
                    else
                    {
                        Console.WriteLine("Handle chưa được tạo.");
                    }

                    SetupRecieveCallback(socks);
                }
                else
                {
                    Console.WriteLine("Client {0}, disconnected", socks.RemoteEndPoint);
                    socks.Shutdown(SocketShutdown.Both);
                    socks.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Lỗi xảy ra khi nhận kết quả trả về!");
            }
        }
        private void RemoveSocketFromList(Socket sock)
        {
            if (sock == null) return;

            lock (listClientSockets) // Đảm bảo thread-safe
            {
                if (listClientSockets.Contains(sock))
                {
                    listClientSockets.Remove(sock);
                    Console.WriteLine("Đã xóa socket: " + sock.RemoteEndPoint);
                }
                else
                {
                    Console.WriteLine("Socket không tồn tại trong danh sách.");
                }
            }
        }
        private void FormDieuKhien_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var sock in listClientSockets)
            {
                try
                {
                    byte[] msg = Encoding.ASCII.GetBytes("CLOSE");
                    sock.Send(msg);
                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi đóng kết nối: " + ex.Message);
                }
            }
            listClientSockets.Clear();
        }


        private void tabChuongTrinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabChuongTrinh.SelectedIndex)
            {
                case 0:
                    lblThoiGian.Visible = false;
                    SendEvent("0,ser,playgioithieu");
                    tmMain.Enabled = false;
                    //cbxDoiChoi.SelectedIndex = 0;

                    break;
                case 1:
                    phanThiHienTai = 1;
                    SendEvent("0,ser,playkhoidong");
                    time = 60;
                    lblThoiGian.Text = time.ToString();
                    lblThoiGian.Visible = true;
                    tmMain.Enabled = false;
                    //cbxDoiChoi.SelectedIndex = 0;
                    break;
                case 2:
                    phanThiHienTai = 2;
                    time = 180;
                    lblThoiGian.Text = time.ToString();
                    lblThoiGian.Visible = true;
                    tmMain.Enabled = false;
                    SendEvent("0,ser,playkhamphachiase,0," + cuocThiHienTai.cuocthiid);
                    
                    break;
                case 3:
                    phanThiHienTai = 3;
                    time = 30;
                    lblThoiGian.Text = time.ToString();
                    lblThoiGian.Visible = true;
                    tmMain.Enabled = false;
                    SendEvent("0,ser,playthuthach,0," + cuocThiHienTai.cuocthiid);
                    break;
                case 4: 
                    phanThiHienTai = 4;
                    time = 20;
                    lblThoiGian.Text = time.ToString();
                    lblThoiGian.Visible = true;
                    tmMain.Enabled = false;
                    SendEvent("0,ser,playtoasang,"  + cuocThiHienTai.cuocthiid + ",0,0");
                    break;
                case 5:
                    phanThiHienTai = 5;
                    lblThoiGian.Visible = false;
                    tmMain.Enabled = false;
                    SendEvent("0,ser,playkhangia,0," + cuocThiHienTai.cuocthiid);
                    break;
            }
        }

        private void btnGioiThieu_Click(object sender, EventArgs e)
        {
            lblThoiGian.Visible = false;
            SendEvent("0,ser,playgioithieu");
        }

        private void btnStartKD_Click(object sender, EventArgs e)
        {
            //SendEvent(id_teamplaying + ",ser,playkhoidong," + lsDanhSachCauHoiKhoiDong[0].cauhoiid.ToString());
            SendEvent(cbxDoiChoi.SelectedValue.ToString() + ",ser,playkhoidong," + lsDanhSachCauHoiKhoiDong[0].cauhoiid.ToString() + "," + goiKDHienTai + ",start");
            tmMain.Enabled = true;
            txtCauHoiChiTietKD.Text = lsDanhSachCauHoiKhoiDong[0].noidungcauhoi;
            cbxDungSai.Checked = false;
            btnStartKD.Enabled = false;
        }

        private void tmMain_Tick(object sender, EventArgs e)
        {
            if (time > 1)
            {
                time = time - 1;
                lblThoiGian.Text = time.ToString();
            }
            else
            {
                if(phanThiHienTai == 2)
                {
                    time = time - 1;
                    lblThoiGian.Text = time.ToString();
                }
                else
                {
                    tmMain.Enabled = false;
                    lblThoiGian.Text = "END";
                }
                    
            }
        }

        private void btnKDNext_Click(object sender, EventArgs e)
        {
            ds_diem ds_Diem = new ds_diem();
            ds_hienthicautraloi ds_hienthidapan = new ds_hienthicautraloi();
            ds_Diem.cuocthiid = cuocThiHienTai.cuocthiid;
            ds_hienthidapan.cuocthiid = cuocThiHienTai.cuocthiid;
            ds_Diem.doiid = int.Parse(cbxDoiChoi.SelectedValue.ToString());
            ds_hienthidapan.doiid = int.Parse(cbxDoiChoi.SelectedValue.ToString());
            ds_Diem.phanthiid = 1;
            ds_hienthidapan.phanthiid = 1;
            ds_hienthidapan.cauhoi = lsDanhSachCauHoiKhoiDong[0].noidungcauhoi;
            ds_hienthidapan.dapan = lsDanhSachCauHoiKhoiDong[0].dapan;
            ds_hienthidapan.cauhoiid = lsDanhSachCauHoiKhoiDong[0].cauhoiid;
            ds_Diem.cauhoiid = lsDanhSachCauHoiKhoiDong[0].cauhoiid;
            if (cbxDungSai.Checked)
            {
                ds_Diem.sodiem = 20;
                ds_hienthidapan.traloi = true;
            }
            else
            {
                ds_Diem.sodiem = 0;
                ds_hienthidapan.traloi = false;

            }
            _entity.ds_diem.Add(ds_Diem);
            _entity.ds_hienthicautraloi.Add(ds_hienthidapan);
            _entity.SaveChanges();
            grvDanhSachCauHoiKD.Rows.Clear();
            grvDanhSachCauHoiKD.Refresh();
            lsDanhSachCauHoiKhoiDong.RemoveAt(0);
            loadDanhSachCauHoiKD(lsDanhSachCauHoiKhoiDong);
            if (lsDanhSachCauHoiKhoiDong.Count == 1)
            {
                btnKDNext.Enabled = false;

            }
            cbxDungSai.Checked = false;
            txtCauHoiChiTietKD.Text = lsDanhSachCauHoiKhoiDong[0].noidungcauhoi;
            SendEvent(cbxDoiChoi.SelectedValue.ToString() + ",ser,playkhoidong," + lsDanhSachCauHoiKhoiDong[0].cauhoiid.ToString() + "," + goiKDHienTai + ",next");
        }

        private void btnKetThucKD_Click(object sender, EventArgs e)
        {
            ds_diem ds_Diem = new ds_diem();
            ds_hienthicautraloi ds_hienthidapan = new ds_hienthicautraloi();
            ds_Diem.cuocthiid = cuocThiHienTai.cuocthiid;
            ds_hienthidapan.cuocthiid = cuocThiHienTai.cuocthiid;
            ds_Diem.doiid = int.Parse(cbxDoiChoi.SelectedValue.ToString());
            ds_hienthidapan.doiid = int.Parse(cbxDoiChoi.SelectedValue.ToString());
            ds_Diem.phanthiid = 1;
            ds_hienthidapan.phanthiid = 1;
            ds_hienthidapan.cauhoi = lsDanhSachCauHoiKhoiDong[0].noidungcauhoi;
            ds_hienthidapan.dapan = lsDanhSachCauHoiKhoiDong[0].dapan;
            ds_hienthidapan.cauhoiid = lsDanhSachCauHoiKhoiDong[0].cauhoiid;
            ds_Diem.cauhoiid = lsDanhSachCauHoiKhoiDong[0].cauhoiid;
            if (cbxDungSai.Checked)
            {
                ds_Diem.sodiem = 20;
                ds_hienthidapan.traloi = true;
            }
            else
            {
                ds_Diem.sodiem = 0;
                ds_hienthidapan.traloi = false;

            }
            _entity.ds_diem.Add(ds_Diem);
            _entity.ds_hienthicautraloi.Add(ds_hienthidapan);
            _entity.SaveChanges();
            grvDanhSachCauHoiKD.Rows.Clear();
            grvDanhSachCauHoiKD.Refresh();
            lsDanhSachCauHoiKhoiDong.RemoveAt(0);
            loadDanhSachCauHoiKD(lsDanhSachCauHoiKhoiDong);
            btnKDNext.Enabled = true;
            txtCauHoiChiTietKD.Text = "";
            tmMain.Enabled = true;
            cbxDungSai.Checked = false;
            capNhatTongDiem();
            SendEvent(cbxDoiChoi.SelectedValue.ToString() + ",ser,playkhoidong,0," + goiKDHienTai + ",stop");
            btnStartKD.Enabled = true;
            tmMain.Enabled = false;
            //displayPoint(lvBangDiemKD);
            time = 60;
            lblThoiGian.Text = time.ToString();
        }

        private void btnKDGoi1_Click(object sender, EventArgs e)
        {
            tmMain.Enabled = false;
            goiKDHienTai = 1;
            time = 60;
            lblThoiGian.Text = time.ToString();
            disableGoiCauHoiKhoiDong(1);
            SendEvent(cbxDoiChoi.SelectedValue.ToString() + ",ser,playkhoidong,0," + goiKDHienTai + ",ready");
            lsDanhSachCauHoiKhoiDong = getDsCauHoiByGoiId(1);
            loadDanhSachCauHoiKD(lsDanhSachCauHoiKhoiDong);
        }

        private void btnKDGoi2_Click(object sender, EventArgs e)
        {
            tmMain.Enabled = false;
            goiKDHienTai = 2;
            time = 60;
            lblThoiGian.Text = time.ToString();
            disableGoiCauHoiKhoiDong(2);
            SendEvent(cbxDoiChoi.SelectedValue.ToString() + ",ser,playkhoidong,0," + goiKDHienTai + ",ready");

            lsDanhSachCauHoiKhoiDong = getDsCauHoiByGoiId(2);
            loadDanhSachCauHoiKD(lsDanhSachCauHoiKhoiDong);
        }

        private void btnKDGoi3_Click(object sender, EventArgs e)
        {
            tmMain.Enabled = false;
            goiKDHienTai = 3;
            time = 60;
            lblThoiGian.Text = time.ToString();
            disableGoiCauHoiKhoiDong(3);
            SendEvent(cbxDoiChoi.SelectedValue.ToString() + ",ser,playkhoidong,0," + goiKDHienTai + ",ready");

            lsDanhSachCauHoiKhoiDong = getDsCauHoiByGoiId(3);
            loadDanhSachCauHoiKD(lsDanhSachCauHoiKhoiDong);
        }

        private void btnKDGoi4_Click(object sender, EventArgs e)
        {
            tmMain.Enabled = false;
            goiKDHienTai = 4;
            time = 60;
            lblThoiGian.Text = time.ToString();
            disableGoiCauHoiKhoiDong(4);
            SendEvent(cbxDoiChoi.SelectedValue.ToString() + ",ser,playkhoidong,0," + goiKDHienTai + ",ready");

            lsDanhSachCauHoiKhoiDong = getDsCauHoiByGoiId(4);
            loadDanhSachCauHoiKD(lsDanhSachCauHoiKhoiDong);
        }

        private void btnKDGoi5_Click(object sender, EventArgs e)
        {
            tmMain.Enabled = false;
            goiKDHienTai = 5;
            time = 60;
            lblThoiGian.Text = time.ToString();
            disableGoiCauHoiKhoiDong(5);
            SendEvent(cbxDoiChoi.SelectedValue.ToString() + ",ser,playkhoidong,0," + goiKDHienTai + ",ready");

            lsDanhSachCauHoiKhoiDong = getDsCauHoiByGoiId(5);
            loadDanhSachCauHoiKD(lsDanhSachCauHoiKhoiDong);
        }

        private void btnKDGoi6_Click(object sender, EventArgs e)
        {
            tmMain.Enabled = false;
            goiKDHienTai = 6;
            time = 60;
            lblThoiGian.Text = time.ToString();
            disableGoiCauHoiKhoiDong(6);
            SendEvent(cbxDoiChoi.SelectedValue.ToString() + ",ser,playkhoidong,0," + goiKDHienTai + ",ready");

            lsDanhSachCauHoiKhoiDong = getDsCauHoiByGoiId(6);
            loadDanhSachCauHoiKD(lsDanhSachCauHoiKhoiDong);
        }
        public void disableButtonKP(int cauHoiIndex)
        {
            switch (cauHoiIndex)
            {
                case 1:
                    btnCau1KP.Enabled = false;
                    break;
                case 2:
                    btnCau2KP.Enabled = false;
                    break;
                case 3:
                    btnCau3KP.Enabled = false;
                    break;
                case 4:
                    btnCau4KP.Enabled = false;
                    break;
                case 5:
                    btnCauPKP.Enabled = false;
                    break;
                default:
                    break;
            }
        }
        public void disableButtonKG(int cauHoiIndex)
        {
            switch (cauHoiIndex)
            {
                case 1:
                    btnCau1kg.Enabled = false;
                    break;
                case 2:
                    btnCau2kg.Enabled = false;
                    break;
                case 3:
                    btnCau3kg.Enabled = false;
                    break;
                case 4:
                    btnCau4kg.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private void btnStartKP_Click(object sender, EventArgs e)
        {
            SendEvent("0,ser,playthuthach," + currentCau + ",start," + cuocThiHienTai.cuocthiid);
            //axWinMedia.URL = fullPath;
            //Thread.Sleep(1000);
            //axWinMedia.Ctlcontrols.play();
            //axWinMedia.Ctlenabled = false;
            tmMain.Enabled = true;
        }

        private int layDiemThuThach(int thoigian)
        {
            if (thoigian <= 10)
                return 30;
            if (thoigian >= 11 && thoigian <= 20)
                return 20;
            if (thoigian >= 21 && thoigian <= 30)
                return 10;
            return 0;
        }

        private void resetKetQua()
        {
            txtphan2doi1thoigian.ResetText();
            txtphan2doi2thoigian.ResetText();
            txtphan2doi3thoigian.ResetText();
            txtphan2doi4thoigian.ResetText();
            txtphan2doi1traloi.ResetText();
            txtphan2doi2traloi.ResetText();
            txtphan2doi3traloi.ResetText();
            txtphan2doi4traloi.ResetText();
            txtphan2doi1diem.ResetText();
            txtphan2doi2diem.ResetText();
            txtphan2doi3diem.ResetText();
            txtphan2doi4diem.ResetText();
            chkphan2doi1dung.Checked = false;
            chkphan2doi2dung.Checked = false;
            chkphan2doi3dung.Checked = false;
            chkphan2doi4dung.Checked = false;
            //axWinMedia.Ctlcontrols.stop();
        }

        private void updateKetQua(int diemid)
        {
            int doiid = 0;
            string cautraloi = "";
            string thoigiantraloi = "";
            ds_diem diem = _entity.ds_diem.Find(diemid);
            string sql = "select * from ds_diem where diemid = " + diemid;
            DataTable dt = sqlObject.getDataFromSql(sql, "").Tables[0];
            if (dt != null)
            {
                doiid = int.Parse(dt.Rows[0]["doiid"].ToString());
                cautraloi = dt.Rows[0]["cautraloi"].ToString();
                thoigiantraloi = dt.Rows[0]["thoigiantraloi"].ToString();
                ds_doi doi = _entity.ds_doi.Find(doiid);
                if (doi != null)
                {
                    if (doi.vitridoi == 1)
                    {
                        txtphan2doi1traloi.Text = cautraloi;
                        txtphan2doi1thoigian.Text = thoigiantraloi;
                    }
                    if (doi.vitridoi == 2)
                    {
                        txtphan2doi2traloi.Text = cautraloi;
                        txtphan2doi2thoigian.Text = thoigiantraloi;
                    }
                    if (doi.vitridoi == 3)
                    {
                        txtphan2doi3traloi.Text = cautraloi;
                        txtphan2doi3thoigian.Text = thoigiantraloi;
                    }
                    if (doi.vitridoi == 4)
                    {
                        txtphan2doi4traloi.Text = cautraloi;
                        txtphan2doi4thoigian.Text = thoigiantraloi;
                    }
                }
            }
        }
        private void btnCau1KP_Click(object sender, EventArgs e)
        {
            tmMain.Enabled = false;
            time = 30;
            lblThoiGian.Text = time.ToString(); 
            //axWinMedia.Ctlcontrols.stop();
            ds_cauhoithuthach cauHoi = _entity.ds_cauhoithuthach.FirstOrDefault(x => x.vitri == 1 && x.cuocthiid == cuocThiHienTai.cuocthiid);
            if (cauHoi != null)
            {
                currentCau = cauHoi.cauhoiid;
                rTxtCauHoi.Text = cauHoi.noidung;
                lblDapAn.Text = cauHoi.dapantext + "\n" + cauHoi.dapanABC;
                //fullPath = directoryPath + "\\Resources\\Video\\" + cauHoi.urlcauhoi;
                //axWinMedia.URL = fullPath;
            }
            disableButtonKP((int)cauHoi.vitri);
            SendEvent("0,ser,playthuthach," + currentCau + ",ready," + cuocThiHienTai.cuocthiid);
        }

        private void btnCau2KP_Click(object sender, EventArgs e)
        {
            resetKetQua();
            tmMain.Enabled = false;
            //axWinMedia.Ctlcontrols.stop();

            time = 30;
            lblThoiGian.Text = time.ToString();
            ds_cauhoithuthach cauHoi = _entity.ds_cauhoithuthach.FirstOrDefault(x => x.vitri == 2 && x.cuocthiid == cuocThiHienTai.cuocthiid);
            if (cauHoi != null)
            {
                currentCau = cauHoi.cauhoiid;
                rTxtCauHoi.Text = cauHoi.noidung;
                lblDapAn.Text = cauHoi.dapantext + "\n" + cauHoi.dapanABC;
                //fullPath = directoryPath + "\\Resources\\Video\\" + cauHoi.urlcauhoi;
                //axWinMedia.URL = fullPath;
            }
            disableButtonKP((int)cauHoi.vitri);
            SendEvent("0,ser,playthuthach," + currentCau + ",ready," + cuocThiHienTai.cuocthiid);
        }


        private void btnCapNhatDiemKP_Click(object sender, EventArgs e)
        {
            List<ds_diem> lsDiem = _entity.ds_diem.Where(x => x.cuocthiid == cuocThiHienTai.cuocthiid && x.phanthiid == phanThiHienTai && x.cauhoiid == currentCau).ToList();
            if (lsDiem != null && lsDiem.Count > 0)
            {
                for (int i = 0; i < lsDiem.Count; i++)
                {
                    ds_diem diem = lsDiem[i];
                    ds_doi doi = _entity.ds_doi.Find(diem.doiid);
                    if (doi.vitridoi == 1)
                    {
                        if (chkphan2doi1dung.Checked)
                        {
                            int sodiem = layDiemThuThach(int.Parse(txtphan2doi1thoigian.Text));
                            diem.sodiem = sodiem;
                        }
                        else
                        {
                            diem.sodiem = 0;
                        }
                        txtphan2doi1diem.Text = diem.sodiem.ToString();
                    }
                    if (doi.vitridoi == 2)
                    {
                        if (chkphan2doi2dung.Checked)
                        {
                            int sodiem = layDiemThuThach(int.Parse(txtphan2doi2thoigian.Text));
                            diem.sodiem = sodiem;
                        }
                        else
                        {
                            diem.sodiem = 0;
                        }
                        txtphan2doi2diem.Text = diem.sodiem.ToString();
                    }
                    if (doi.vitridoi == 3)
                    {
                        if (chkphan2doi3dung.Checked)
                        {
                            int sodiem = layDiemThuThach(int.Parse(txtphan2doi3thoigian.Text));
                            diem.sodiem = sodiem;
                        }
                        else
                        {
                            diem.sodiem = 0;
                        }
                        txtphan2doi3diem.Text = diem.sodiem.ToString();
                    }
                    if (doi.vitridoi == 4)
                    {
                        if (chkphan2doi4dung.Checked)
                        {
                            int sodiem = layDiemThuThach(int.Parse(txtphan2doi4thoigian.Text));
                            diem.sodiem = sodiem;
                        }
                        else
                        {
                            diem.sodiem = 0;
                        }
                        txtphan2doi4diem.Text = diem.sodiem.ToString();
                    }
                }
                _entity.SaveChanges();
            }
            capNhatTongDiem();
            //displayPoint(lvBangDiemKP);
            SendEvent("0,ser,playthuthach," + currentCau + ",hienthidiemKP," + cuocThiHienTai.cuocthiid);
        }

        private void btnHienThiCauHoiCP_Click(object sender, EventArgs e)
        {
            loadNoiDungCauHoiCP();
            tmMain.Enabled = false;
            time = 180;
            lblThoiGian.Text = time.ToString();
            lblDiemGK1.Text = "0";
            lblDiemGK2.Text = "0";
            lblDiemGK3.Text = "0";
            SendEvent("0,ser,playkhamphachiase," + cauhoichudeId + ",0,ready");
        }

        private void loadNoiDungCauHoiCP()
        {
            string sql = "SELECT * FROM ds_goicaudiscovery WHERE cuocthiid = " + cuocThiHienTai.cuocthiid + " AND doithiid = " + int.Parse(slbDoiChoiCP.SelectedValue.ToString()) + " AND trangthai = 'true'";
            DataTable dt = sqlObject.getDataFromSql(sql, "").Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    cauhoichudeId = int.Parse(dr["cauhoiid"].ToString());
                    lsCauHoiPhuCP = getListCauHoiPhuByChaId(cauhoichudeId);
                    rtbCauHoiChinh.Text = dr["chude"].ToString();

                    // Ẩn các nút
                    btnCPCau1.Enabled = btnCPCau1.Visible = false;
                    btnCPCau2.Enabled = btnCPCau2.Visible = false;
                    btnCPCau3.Enabled = btnCPCau3.Visible = false;
                    btnCPCau4.Enabled = btnCPCau4.Visible = false;
                    btnCPCau5.Enabled = btnCPCau5.Visible = false;
                    btnCPCau6.Enabled = btnCPCau6.Visible = false;

                    // Reset hiển thị
                    pBCauHoiChinhCP.Visible = false;
                    axWindowsMediaPlayer1.Visible = false;

                    string fileName = dr["noidungchude"].ToString();
                    string extension = Path.GetExtension(fileName).ToLower();
                    string fullPath = "";

                    // Phân loại đường dẫn theo loại file
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".bmp" || extension == ".gif")
                    {
                        fullPath = Path.Combine(directoryPath, "Resources", "pic", fileName);

                        // Hiển thị hình ảnh
                        if (File.Exists(fullPath))
                        {
                            pBCauHoiChinhCP.Image = Image.FromFile(fullPath);
                            pBCauHoiChinhCP.SizeMode = PictureBoxSizeMode.StretchImage;
                            pBCauHoiChinhCP.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy hình ảnh: " + fullPath);
                        }
                    }
                    else if (extension == ".mp4" || extension == ".avi" || extension == ".mov" || extension == ".wmv" || extension == ".mkv")
                    {
                        fullPath = Path.Combine(directoryPath, "Resources", "Video", fileName);

                        // Phát video
                        if (File.Exists(fullPath))
                        {
                            axWindowsMediaPlayer1.URL = fullPath;
                            axWindowsMediaPlayer1.settings.autoStart = false; // Không tự động phát
                            axWindowsMediaPlayer1.Ctlcontrols.stop();        // Stop sẵn
                            axWindowsMediaPlayer1.Visible = false;           // Ẩn đi ban đầu
                            axWindowsMediaPlayer1.SendToBack();
                            axWindowsMediaPlayer1.Visible = true;
                            // Cho video nằm dưới
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy video: " + fullPath);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Định dạng file không hỗ trợ: " + extension);
                    }
                }
            }
        }


        private List<ds_goicaudiscovery> getListCauHoiPhuByChaId(int chaID)
        {
            List<ds_goicaudiscovery> lsResult = new List<ds_goicaudiscovery>();
            string sql = "SELECT * from ds_goicaudiscovery where cauhoichaid = " + chaID;

            DataTable dt = sqlObject.getDataFromSql(sql, "").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ds_goicaudiscovery item = new ds_goicaudiscovery();
                    item.cauhoiid = int.Parse(dt.Rows[i]["cauhoiid"].ToString());
                    item.chude = dt.Rows[i]["chude"].ToString();
                    item.vitri = int.Parse(dt.Rows[i]["vitri"].ToString());
                    item.cauhoichaid = int.Parse(dt.Rows[i]["cauhoichaid"].ToString());
                    item.noidungchude = dt.Rows[i]["noidungchude"].ToString();
                    item.cuocthiid = null;
                    lsResult.Add(item);
                }
            }

            return lsResult;

        }
        private void btnCPCau1_Click(object sender, EventArgs e)
        {
            var doiChoi = _entity.ds_doi.Find(int.Parse(slbDoiChoiCP.SelectedValue.ToString()));
            ds_goicaudiscovery cauHoiPhu = _entity.ds_goicaudiscovery.FirstOrDefault(x => x.vitri == 1 && x.cuocthiid == cuocThiHienTai.cuocthiid && x.doithiid == doiChoi.doiid);
            cauHoiPhu.trangthailatAnhPhu = 1;
            cauHoiPhuCurrent = cauHoiPhu;
            btnCPCau1.Text = "";
            btnCPCau1.BackgroundImage = Image.FromFile(directoryPath + "\\Resources\\pic\\" + cauHoiPhu.noidungchude);
            _entity.SaveChanges();
            SendEvent("0,ser,playkhamphachiase," + cauhoichudeId + "," + cauHoiPhuCurrent.cauhoiid + ",hienthimanh");
        }



        private void btnCPCau2_Click(object sender, EventArgs e)
        {
            var doiChoi = _entity.ds_doi.Find(int.Parse(slbDoiChoiCP.SelectedValue.ToString()));
            ds_goicaudiscovery cauHoiPhu = _entity.ds_goicaudiscovery.FirstOrDefault(x => x.vitri == 2 && x.cuocthiid == cuocThiHienTai.cuocthiid && x.doithiid == doiChoi.doiid);
            cauHoiPhu.trangthailatAnhPhu = 1;
            cauHoiPhuCurrent = cauHoiPhu;
            btnCPCau2.Text = "";
            btnCPCau2.BackgroundImage = Image.FromFile(directoryPath + "\\Resources\\pic\\" + cauHoiPhu.noidungchude);
            _entity.SaveChanges();
            SendEvent("0,ser,playkhamphachiase," + cauhoichudeId + "," + cauHoiPhuCurrent.cauhoiid + ",hienthimanh");
        }

        private void btnCPCau3_Click(object sender, EventArgs e)
        {
            var doiChoi = _entity.ds_doi.Find(int.Parse(slbDoiChoiCP.SelectedValue.ToString()));
            ds_goicaudiscovery cauHoiPhu = _entity.ds_goicaudiscovery.FirstOrDefault(x => x.vitri == 3 && x.cuocthiid == cuocThiHienTai.cuocthiid && x.doithiid == doiChoi.doiid); 
            cauHoiPhu.trangthailatAnhPhu = 1;
            cauHoiPhuCurrent = cauHoiPhu;
            btnCPCau3.Text = "";
            btnCPCau3.BackgroundImage = Image.FromFile(directoryPath + "\\Resources\\pic\\" + cauHoiPhu.noidungchude);
            _entity.SaveChanges();

            SendEvent("0,ser,playkhamphachiase," + cauhoichudeId + "," + cauHoiPhuCurrent.cauhoiid + ",hienthimanh");
        }

        private void btnCPCau4_Click(object sender, EventArgs e)
        {
            var doiChoi = _entity.ds_doi.Find(int.Parse(slbDoiChoiCP.SelectedValue.ToString()));
            ds_goicaudiscovery cauHoiPhu = _entity.ds_goicaudiscovery.FirstOrDefault(x => x.vitri == 4 && x.cuocthiid == cuocThiHienTai.cuocthiid && x.doithiid == doiChoi.doiid); 
            cauHoiPhu.trangthailatAnhPhu = 1;
            cauHoiPhuCurrent = cauHoiPhu;
            btnCPCau4.Text = "";
            btnCPCau4.BackgroundImage = Image.FromFile(directoryPath + "\\Resources\\pic\\" + cauHoiPhu.noidungchude);
            _entity.SaveChanges();
            SendEvent("0,ser,playkhamphachiase," + cauhoichudeId + "," + cauHoiPhuCurrent.cauhoiid + ",hienthimanh");
        }

        private void btnCPCau5_Click(object sender, EventArgs e)
        {
            var doiChoi = _entity.ds_doi.Find(int.Parse(slbDoiChoiCP.SelectedValue.ToString()));
            ds_goicaudiscovery cauHoiPhu = _entity.ds_goicaudiscovery.FirstOrDefault(x => x.vitri == 5 && x.cuocthiid == cuocThiHienTai.cuocthiid && x.doithiid == doiChoi.doiid); 
            cauHoiPhu.trangthailatAnhPhu = 1;
            cauHoiPhuCurrent = cauHoiPhu;
            btnCPCau5.Text = "";
            btnCPCau5.BackgroundImage = Image.FromFile(directoryPath + "\\Resources\\pic\\" + cauHoiPhu.noidungchude);
            _entity.SaveChanges();
            SendEvent("0,ser,playkhamphachiase," + cauhoichudeId + "," + cauHoiPhuCurrent.cauhoiid + ",hienthimanh");
        }

        private void btnCPCau6_Click(object sender, EventArgs e)
        {
            var doiChoi = _entity.ds_doi.Find(int.Parse(slbDoiChoiCP.SelectedValue.ToString()));
            ds_goicaudiscovery cauHoiPhu = _entity.ds_goicaudiscovery.FirstOrDefault(x => x.vitri == 6 && x.cuocthiid == cuocThiHienTai.cuocthiid && x.doithiid == doiChoi.doiid); 
            cauHoiPhu.trangthailatAnhPhu = 1;
            cauHoiPhuCurrent = cauHoiPhu;
            btnCPCau6.Text = "";
            btnCPCau6.BackgroundImage = Image.FromFile(directoryPath + "\\Resources\\pic\\" + cauHoiPhu.noidungchude);
            _entity.SaveChanges();
            SendEvent("0,ser,playkhamphachiase," + cauhoichudeId + "," + cauHoiPhuCurrent.cauhoiid + ",hienthimanh");
        }

        private void btnCapNhatDiemCP_Click(object sender, EventArgs e)
        {

            tinhDiemChoCauHoiChinh_New();
            capNhatTongDiem();
            //SendEvent("0,ser,playkhamphachiase," + cauhoichudeId + ",0,capnhatTongDiem");

        }

        private int tinhDiemChoCauHoiChinh_New()
        {
            int result = 0;
            int diemGK1 = int.Parse(lblDiemGK1.Text);
            int diemGK2 = int.Parse(lblDiemGK2.Text);
            int diemGK3 = int.Parse(lblDiemGK3.Text);
            result = diemGK1 + diemGK2 + diemGK3;

            int doithiId = int.Parse(slbDoiChoiCP.SelectedValue.ToString());

            // Kiểm tra nếu điểm của đội thi & câu hỏi đã có trong bảng ds_diem
            var diem = _entity.ds_diem.FirstOrDefault(x => x.doiid == doithiId && x.cauhoiid == cauhoichudeId && x.phanthiid == phanThiHienTai && x.cuocthiid == cuocThiHienTai.cuocthiid);

            if (diem != null)
            {
                // Nếu đã có, thì cập nhật điểm tổng
                diem.sodiem = result;
                //diem.thoigiantraloi = int.Parse(lblThoiGian.Text);
            }
            else
            {
                // Nếu chưa có, thì thêm mới
                diem = new ds_diem
                {
                    doiid = doithiId,
                    phanthiid = 2,
                    sodiem = result,
                    cauhoiid = cauhoichudeId,
                    cuocthiid = cuocThiHienTai.cuocthiid,
                    thoigiantraloi = int.Parse(lblThoiGian.Text),
                };
                _entity.ds_diem.Add(diem);
            }

            _entity.SaveChanges(); // Lưu thay đổi

            // Cập nhật điểm chi tiết của giám khảo
            var chiTietDiems = _entity.ds_chitietdiem.Where(x => x.diemid == diem.diemid).ToList();
            if (chiTietDiems.Any())
            {
                // Nếu có, cập nhật điểm của giám khảo
                chiTietDiems[0].sodiem = diemGK1;
                chiTietDiems[1].sodiem = diemGK2;
                chiTietDiems[2].sodiem = diemGK3;
            }
            else
            {
                // Nếu chưa có, thì thêm mới
                chiTietDiems = new List<ds_chitietdiem>
        {
            new ds_chitietdiem { diemid = diem.diemid, sodiem = diemGK1, ghichu = "Giám khảo 1" },
            new ds_chitietdiem { diemid = diem.diemid, sodiem = diemGK2, ghichu = "Giám khảo 2" },
            new ds_chitietdiem { diemid = diem.diemid, sodiem = diemGK3, ghichu = "Giám khảo 3" }
        };
                _entity.ds_chitietdiem.AddRange(chiTietDiems);
            }

            _entity.SaveChanges(); // Lưu thay đổi
            //displayPointCP();

            MessageBox.Show("Cập nhật điểm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return result;
        }

        

        private void capNhatTongDiem()
        {
            //string sql = "SELECT a.doiid, sum(b.sodiem) AS tongdiem FROM ds_doi a INNER JOIN ds_diem b on b.doiid = a.doiid WHERE b.phanthiid = 3 AND b.cuocthiid = " + cuocThiHienTai.cuocthiid + " GROUP BY b.cuocthiid, b.phanthiid, a.doiid";
            string sql = "SELECT doiid, sum(sodiem) as tongdiem from ds_diem WHERE cuocthiid = " + cuocThiHienTai.cuocthiid + " GROUP BY cuocthiid, doiid";
            DataTable dt = sqlObject.getDataFromSql(sql, "").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    ds_doi doiChoi = _entity.ds_doi.Find(int.Parse(dr["doiid"].ToString()));
                    if (doiChoi != null)
                    {
                        if (doiChoi.vitridoi == 1)
                        {
                            cuocThiHienTai.tongdiemdoi1 = int.Parse(dr["tongdiem"].ToString());
                        }
                        if (doiChoi.vitridoi == 2)
                        {
                            cuocThiHienTai.tongdiemdoi2 = int.Parse(dr["tongdiem"].ToString());
                        }
                        if (doiChoi.vitridoi == 3)
                        {
                            cuocThiHienTai.tongdiemdoi3 = int.Parse(dr["tongdiem"].ToString());
                        }
                        if (doiChoi.vitridoi == 4)
                        {
                            cuocThiHienTai.tongdiemdoi4 = int.Parse(dr["tongdiem"].ToString());
                        }
                    }
                }
            }
            _entity.SaveChanges();
        }

        private DataTable getTableDiem(int phanThi)
        {
            SqlDataAccess sqlObject = new SqlDataAccess();
            //string sql = "SELECT a.doiid, sum(b.sodiem) AS tongdiem FROM ds_doi a INNER JOIN ds_diem b on b.doiid = a.doiid WHERE b.phanthiid = 3 AND b.cuocthiid = " + cuocThiHienTai.cuocthiid + " GROUP BY b.cuocthiid, b.phanthiid, a.doiid";
            string sql = "SELECT doiid, sum(sodiem) as tongdiem from ds_diem WHERE phanthiid = " + phanThi + " and cuocthiid = " + cuocThiHienTai.cuocthiid + " GROUP BY cuocthiid, phanthiid, doiid";
            DataTable dt = sqlObject.getDataFromSql(sql, "").Tables[0];
            return dt;
        }

        private void btnKetThucCP_Click(object sender, EventArgs e)
        {
        }

        private void btnGoi1VD_Click(object sender, EventArgs e)
        {
            tmMain.Enabled = false;
            btnCau1VD.Enabled = false;
            time = 20;
            lblThoiGian.Text = time.ToString();
            var cauhoiVD = _entity.ds_goicauhoishining.FirstOrDefault(x => x.vitri == 1 && x.cuocthiid == cuocThiHienTai.cuocthiid);
            currentCau = cauhoiVD.cauhoiid;
            rTxtCauHoiToaSang.Text = cauhoiVD.noidungcauhoi;
            lblDapAnToaSang.Text = cauhoiVD.dapan;
            cauhoiVD.trangThai = 1;
            _entity.SaveChanges();
            _currentQuestionIdForAnswer = currentCau;
            SendEvent("0,ser,playtoasang," + cuocThiHienTai.cuocthiid + "," + currentCau + ",ready");

        }

        private void btnStartVD_Click(object sender, EventArgs e)
        {
            tmMain.Enabled = true;
            if (cbNgoiSaoHiVong.Checked)
            {
                SendEvent("0,ser,playtoasang," + cuocThiHienTai.cuocthiid + "," + currentCau + ",start_x2");

            }
            else
            {
                SendEvent("0,ser,playtoasang," + cuocThiHienTai.cuocthiid + "," + currentCau + ",start");

            }
        }

        private void cBDanhQuyenTraLoi_CheckedChanged(object sender, EventArgs e)
        {
            if (cBDanhQuyenTraLoi.Checked)
            {
                cbBDoiTraLoiVD.Enabled = true;
            }
            else
            {
                cbBDoiTraLoiVD.Enabled = false;
            }
        }
        private int _currentQuestionIdForAnswer = 0;

        private void btnTinhDiemVD_Click(object sender, EventArgs e)
        {
            ds_diem diem_thisinh = new ds_diem();
            diem_thisinh.cuocthiid = cuocThiHienTai.cuocthiid;
            diem_thisinh.phanthiid = 4; //Về đích
            diem_thisinh.doiid = int.Parse(cbbDoiChoiVD.SelectedValue.ToString());
            diem_thisinh.cauhoiid = currentCau;
            diem_thisinh.sodiem = 0;

            ds_diem diem_thisinhdanhtraloi = new ds_diem();
            diem_thisinhdanhtraloi.cuocthiid = cuocThiHienTai.cuocthiid;
            diem_thisinhdanhtraloi.phanthiid = 4; //Về đích
            diem_thisinhdanhtraloi.doiid = int.Parse(cbBDoiTraLoiVD.SelectedValue.ToString());
            diem_thisinhdanhtraloi.cauhoiid = currentCau;
            diem_thisinhdanhtraloi.sodiem = 0;
            _currentQuestionIdForAnswer = currentCau;
            ds_goicauhoishining cauHoiVeDich = _entity.ds_goicauhoishining.Find(currentCau);
            cauHoiVeDich.trangThai = 2;
            _entity.SaveChanges();

            bool showAnswer = false; // Biến kiểm soát hiển thị đáp án

            if (!cBDanhQuyenTraLoi.Checked)
            {
                if (!cbDungSaiVD.Checked)
                {
                    if (!cbNgoiSaoHiVong.Checked)
                    {
                        diem_thisinh.sodiem = -10;
                    }
                    else
                    {
                        diem_thisinh.sodiem = -40;
                    }
                    // Thí sinh 1 trả lời sai, không hiển thị đáp án
                    showAnswer = false;
                }
                else
                {
                    if (!cbNgoiSaoHiVong.Checked)
                    {
                        diem_thisinh.sodiem = 30;
                    }
                    else
                    {
                        diem_thisinh.sodiem = 60;
                    }
                    // Thí sinh 1 trả lời đúng, hiển thị đáp án
                    showAnswer = true;
                }
            }
            else
            {
                if (!cbDungSaiVD.Checked)
                {
                    if (!cbNgoiSaoHiVong.Checked)
                    {
                        diem_thisinhdanhtraloi.sodiem = -5;
                    }
                    else
                    {
                        diem_thisinhdanhtraloi.sodiem = -35;
                    }
                }
                else
                {
                    if (!cbNgoiSaoHiVong.Checked)
                    {
                        diem_thisinhdanhtraloi.sodiem = 20;
                    }
                    else
                    {
                        diem_thisinhdanhtraloi.sodiem = 50;
                    }
                }
                // Thí sinh 2 dành quyền trả lời (dù đúng/sai), hiển thị đáp án
                showAnswer = true;
            }

            _entity.ds_diem.Add(diem_thisinh);
            if (cBDanhQuyenTraLoi.Checked)
            {
                _entity.ds_diem.Add(diem_thisinhdanhtraloi);
            }
            _entity.SaveChanges();
            capNhatTongDiem();

            // Gửi sự kiện với tham số showAnswer
            string eventData = $"{cbbDoiChoiVD.SelectedValue.ToString()},ser,playtoasang,{cuocThiHienTai.cuocthiid},{currentCau},{(showAnswer ? "showanswer" : "noanswer")}";
            SendEvent(eventData);


        }

        private void btnGoi2VD_Click(object sender, EventArgs e)
        {
            tmMain.Enabled = false;
            btnCau2VD.Enabled = false;
            time = 20;
            lblThoiGian.Text = time.ToString();
            var cauhoiVD = _entity.ds_goicauhoishining.FirstOrDefault(x => x.vitri == 2 && x.cuocthiid == cuocThiHienTai.cuocthiid);
            currentCau = cauhoiVD.cauhoiid;
            rTxtCauHoiToaSang.Text = cauhoiVD.noidungcauhoi;
            lblDapAnToaSang.Text = cauhoiVD.dapan;
            cauhoiVD.trangThai = 1;
            _entity.SaveChanges();
            _currentQuestionIdForAnswer = currentCau;

            SendEvent("0,ser,playtoasang," + cuocThiHienTai.cuocthiid + "," + currentCau + ",ready");

 
        }

        private void btnCau3KP_Click(object sender, EventArgs e)
        {
            resetKetQua();
            tmMain.Enabled = false;
            //axWinMedia.Ctlcontrols.stop();
            time = 30;
            lblThoiGian.Text = time.ToString();
            ds_cauhoithuthach cauHoi = _entity.ds_cauhoithuthach.FirstOrDefault(x => x.vitri == 3 && x.cuocthiid == cuocThiHienTai.cuocthiid);
            if (cauHoi != null)
            {
                currentCau = cauHoi.cauhoiid;
                rTxtCauHoi.Text = cauHoi.noidung;
                lblDapAn.Text = cauHoi.dapantext + "\n" + cauHoi.dapanABC;
                //fullPath = directoryPath + "\\Resources\\Video\\" + cauHoi.urlcauhoi;
                //axWinMedia.URL = fullPath;
            }
            disableButtonKP((int)cauHoi.vitri);
            SendEvent("0,ser,playthuthach," + currentCau + ",ready," + cuocThiHienTai.cuocthiid);
        }

        private void btnResetKD_Click(object sender, EventArgs e)
        {
            btnKDGoi1.Enabled = true;
            btnKDGoi2.Enabled = true;
            btnKDGoi3.Enabled = true;
            btnKDGoi4.Enabled = true;
            btnKDGoi5.Enabled = true;
            btnKDGoi6.Enabled = true;
            btnStartKD.Enabled = true;
            btnKetThucKD.Enabled = true;
            cbxDungSai.Checked = false;
            btnKDNext.Enabled = true;
            grvDanhSachCauHoiKD.Rows.Clear();
            grvDanhSachCauHoiKD.Refresh();
        }

        private void btnCapNhatDiemMain_Click(object sender, EventArgs e)
        {
            Dictionary<int, string> temp = new Dictionary<int, string>();
            temp = teamplayings;
            lvBangDiemMain.Items.Clear();
            lvBangDiemMain.Refresh();
            int phanThi = int.Parse(cbBMainVongThi.SelectedValue.ToString());

            if (phanThi > 0)
            {
                DataTable dt = getTableDiem(phanThi);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        int id_team = int.Parse(dr["doiid"].ToString());
                        ds_doi _doi = _entity.ds_doi.Find(id_team);
                        string[] row = { "TEAM " + _doi.vitridoi + " - " + _doi.tennguoichoi + " - " + _doi.tendoi, dr["tongdiem"].ToString() };
                        ListViewItem lvi = new ListViewItem(row);
                        lvBangDiemMain.Items.Add(lvi);
                        if (temp.ContainsKey(id_team))
                        {
                            temp.Remove(id_team);
                        }
                    }
                    foreach (var item in temp)
                    {
                        string[] row = { item.Value.ToString(), "0" };
                        ListViewItem lvi = new ListViewItem(row);
                        lvBangDiemMain.Items.Add(lvi);
                    }
                }
            }
            else
            {
                string sql = "SELECT doiid, sum(sodiem) as tongdiem from ds_diem WHERE cuocthiid = " + cuocThiHienTai.cuocthiid + " GROUP BY cuocthiid, doiid";
                DataTable dt = sqlObject.getDataFromSql(sql, "").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        int id_team = int.Parse(dr["doiid"].ToString());
                        ds_doi _doi = _entity.ds_doi.Find(id_team);
                        string[] row = { "TEAM " + _doi.vitridoi + " - " + _doi.tennguoichoi + " - " + _doi.tendoi, dr["tongdiem"].ToString() };
                        ListViewItem lvi = new ListViewItem(row);
                        lvBangDiemMain.Items.Add(lvi);
                        if (temp.ContainsKey(id_team))
                        {
                            temp.Remove(id_team);
                        }
                    }
                    foreach (var item in temp)
                    {
                        string[] row = { item.Value.ToString(), "0" };
                        ListViewItem lvi = new ListViewItem(row);
                        lvBangDiemMain.Items.Add(lvi);
                    }
                }
            }
            SendEvent("0,ser,tongdiem");

        }

        private void btnHienThiCauTraLoiKP_Click(object sender, EventArgs e)
        {
            SendEvent("0,ser,playthuthach," + currentCau + ",stop," + cuocThiHienTai.cuocthiid);
        }

        private void cbDanhQuyenTraLoiCP_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnCau4KP_Click(object sender, EventArgs e)
        {
            resetKetQua();
            tmMain.Enabled = false;
            time = 30;
            lblThoiGian.Text = time.ToString();
            ds_cauhoithuthach cauHoi = _entity.ds_cauhoithuthach.FirstOrDefault(x => x.vitri == 4 && x.cuocthiid == cuocThiHienTai.cuocthiid);
            if (cauHoi != null)
            {
                currentCau = cauHoi.cauhoiid;
                rTxtCauHoi.Text = cauHoi.noidung;
                lblDapAn.Text = cauHoi.dapantext + "\n" + cauHoi.dapanABC;
            }
            disableButtonKP((int)cauHoi.vitri);
            SendEvent("0,ser,playthuthach," + currentCau + ",ready," + cuocThiHienTai.cuocthiid);
        }

        private void btnGoi3VD_Click(object sender, EventArgs e)
        {
            tmMain.Enabled = false;
            btnCau3VD.Enabled = false;
            time = 20;
            lblThoiGian.Text = time.ToString();
            var cauhoiVD = _entity.ds_goicauhoishining.FirstOrDefault(x => x.vitri == 3 && x.cuocthiid == cuocThiHienTai.cuocthiid);
            currentCau = cauhoiVD.cauhoiid;
            rTxtCauHoiToaSang.Text = cauhoiVD.noidungcauhoi;
            lblDapAnToaSang.Text = cauhoiVD.dapan;
            cauhoiVD.trangThai = 1;
            _entity.SaveChanges();
            _currentQuestionIdForAnswer = currentCau;

            SendEvent("0,ser,playtoasang," + cuocThiHienTai.cuocthiid + "," + currentCau + ",ready");


        }

        private void btnGoi4VD_Click(object sender, EventArgs e)
        {
            tmMain.Enabled = false;
            btnCau4VD.Enabled = false;
            time = 20;
            lblThoiGian.Text = time.ToString();
            var cauhoiVD = _entity.ds_goicauhoishining.FirstOrDefault(x => x.vitri == 4 && x.cuocthiid == cuocThiHienTai.cuocthiid);
            currentCau = cauhoiVD.cauhoiid;
            rTxtCauHoiToaSang.Text = cauhoiVD.noidungcauhoi;
            lblDapAnToaSang.Text = cauhoiVD.dapan;
            cauhoiVD.trangThai = 1;
            _entity.SaveChanges();
            _currentQuestionIdForAnswer = currentCau;

            SendEvent("0,ser,playtoasang," + cuocThiHienTai.cuocthiid + "," + currentCau + ",ready");


        }

        private void btnGoi5VD_Click(object sender, EventArgs e)
        {
            tmMain.Enabled = false;
            btnCau5VD.Enabled = false;
            time = 20;
            lblThoiGian.Text = time.ToString();
            var cauhoiVD = _entity.ds_goicauhoishining.FirstOrDefault(x => x.vitri == 5 && x.cuocthiid == cuocThiHienTai.cuocthiid);
            currentCau = cauhoiVD.cauhoiid;
            rTxtCauHoiToaSang.Text = cauhoiVD.noidungcauhoi;
            lblDapAnToaSang.Text = cauhoiVD.dapan;
            cauhoiVD.trangThai = 1;
            _entity.SaveChanges();
            _currentQuestionIdForAnswer = currentCau;

            SendEvent("0,ser,playtoasang," + cuocThiHienTai.cuocthiid + "," + currentCau + ",ready");


        }

        private void btnGoi6VD_Click(object sender, EventArgs e)
        {
            tmMain.Enabled = false;
            btnCau6VD.Enabled = false;
            time = 20;
            lblThoiGian.Text = time.ToString();
            var cauhoiVD = _entity.ds_goicauhoishining.FirstOrDefault(x => x.vitri == 6 && x.cuocthiid == cuocThiHienTai.cuocthiid);
            currentCau = cauhoiVD.cauhoiid;
            rTxtCauHoiToaSang.Text = cauhoiVD.noidungcauhoi;
            lblDapAnToaSang.Text = cauhoiVD.dapan;
            cauhoiVD.trangThai = 1;
            _entity.SaveChanges();
            _currentQuestionIdForAnswer = currentCau;

            SendEvent("0,ser,playtoasang," + cuocThiHienTai.cuocthiid + "," + currentCau + ",ready");
        }
        //hien thi cau tra loi
        private void btnCautraloi_Click(object sender, EventArgs e)
        {
            ds_cuocthi cuocthi = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);
            lsDanhSachCauHoiKhoiDong = _entity.ds_goicauhoikhoidong.Where(x => x.cuocthiid == cuocthi.cuocthiid && x.goicauhoiid == goiKDHienTai && x.vitri <= 5 && x.vitri >= 1).ToList();
            for (int i = 0; i < lsDanhSachCauHoiKhoiDong.Count; i++)
            {
                currentCau = lsDanhSachCauHoiKhoiDong[i].cauhoiid;
            }
            SendEvent(cbxDoiChoi.SelectedValue.ToString() + ",ser,playkhoidong," + currentCau + "," + goiKDHienTai + ",hienthicautraloi");
        }

        private void mnItemKhoiDong_Click(object sender, EventArgs e)
        {
            frmDmKhoiDong frmKD = new frmDmKhoiDong();
            frmKD.ShowDialog();
        }

        private void mnItemKhamPha_Click(object sender, EventArgs e)
        {
            frmDmThuThach frmKP = new frmDmThuThach();
            frmKP.ShowDialog();
        }

        private void mnItemChinhPhuc_Click(object sender, EventArgs e)
        {
            frmDmKhamPhaCS frmCP = new frmDmKhamPhaCS();
            frmCP.ShowDialog();
        }

        private void mnItemVeDich_Click(object sender, EventArgs e)
        {
            frmDmToaSang frmVeDich = new frmDmToaSang();
            frmVeDich.ShowDialog();
        }

        private void btnCau1kg_Click(object sender, EventArgs e)
        {
            ds_phanthikhangia cauHoi = _entity.ds_phanthikhangia.FirstOrDefault(x => x.vitri == 1 && x.cauthiid == cuocThiHienTai.cuocthiid);
            if (cauHoi != null)
            {
                currentCau = cauHoi.cauhoiid;
                rtbCauHoikg.Text = cauHoi.noidungcauhoi;
                lblDAkg.Text = cauHoi.dapan;
                //axWinMedia.URL = fullPath;
            }
            disableButtonKG((int)cauHoi.vitri);
            SendEvent("0,ser,playkhangia," + currentCau + "," + cuocThiHienTai.cuocthiid + ",ready");
        }

        private void btnCau2kg_Click(object sender, EventArgs e)
        {
            ds_phanthikhangia cauHoi = _entity.ds_phanthikhangia.FirstOrDefault(x => x.vitri == 2 && x.cauthiid == cuocThiHienTai.cuocthiid);
            if (cauHoi != null)
            {
                currentCau = cauHoi.cauhoiid;
                rtbCauHoikg.Text = cauHoi.noidungcauhoi;
                lblDAkg.Text = cauHoi.dapan;
                //axWinMedia.URL = fullPath;
            }
            disableButtonKG((int)cauHoi.vitri);
            SendEvent("0,ser,playkhangia," + currentCau + "," + cuocThiHienTai.cuocthiid + ",ready");
        }

        private void btnCau3kg_Click(object sender, EventArgs e)
        {
            ds_phanthikhangia cauHoi = _entity.ds_phanthikhangia.FirstOrDefault(x => x.vitri == 3 && x.cauthiid == cuocThiHienTai.cuocthiid);
            if (cauHoi != null)
            {
                currentCau = cauHoi.cauhoiid;
                rtbCauHoikg.Text = cauHoi.noidungcauhoi;
                lblDAkg.Text = cauHoi.dapan;
                //axWinMedia.URL = fullPath;
            }
            disableButtonKG((int)cauHoi.vitri);
            SendEvent("0,ser,playkhangia," + currentCau + "," + cuocThiHienTai.cuocthiid + ",ready");
        }

        private void btnCau4kg_Click(object sender, EventArgs e)
        {
            ds_phanthikhangia cauHoi = _entity.ds_phanthikhangia.FirstOrDefault(x => x.vitri == 4 && x.cauthiid == cuocThiHienTai.cuocthiid);
            if (cauHoi != null)
            {
                currentCau = cauHoi.cauhoiid;
                rtbCauHoikg.Text = cauHoi.noidungcauhoi;
                lblDAkg.Text = cauHoi.dapan;
                //axWinMedia.URL = fullPath;
            }
            disableButtonKG((int)cauHoi.vitri);
            SendEvent("0,ser,playkhangia," + currentCau + "," + cuocThiHienTai.cuocthiid + ",ready");
        }
        private void btnHienThiDapAnkg_Click(object sender, EventArgs e)
        {
            SendEvent("0,ser,playkhangia," + currentCau + "," + cuocThiHienTai.cuocthiid + ",hienthidapan");
            /* time = 10;
             lblThoiGian.Text = time.ToString();*/
        }

        private void mnItemKhanGia_Click(object sender, EventArgs e)
        {
            frmDmKhanGia frmKhanGia = new frmDmKhanGia();
            frmKhanGia.ShowDialog();
        }

        private void TrinhDieuKhien_Load(object sender, EventArgs e)
        {

        }

        private void btnTrangThai_Click(object sender, EventArgs e)
        {
            btnCau1KP.Enabled = true;
            btnCau2KP.Enabled = true;
            btnCau3KP.Enabled = true;
            btnCau4KP.Enabled = true;
            btnCauPKP.Enabled = true;
            btnStartKP.Enabled = true;
            txtphan2doi1traloi.Text = "";
            txtphan2doi2traloi.Text = "";
            txtphan2doi3traloi.Text = "";
            txtphan2doi4traloi.Text = "";
            txtphan2doi1thoigian.Text = "0";
            txtphan2doi2thoigian.Text = "0";
            txtphan2doi3thoigian.Text = "0";
            txtphan2doi4thoigian.Text = "0";
            txtphan2doi1diem.Text = "0";
            txtphan2doi2diem.Text = "0";
            txtphan2doi3diem.Text = "0";
            txtphan2doi4diem.Text = "0";
            chkphan2doi1dung.Checked = false;
            chkphan2doi2dung.Checked = false;
            chkphan2doi3dung.Checked = false;
            chkphan2doi4dung.Checked = false;
            lblDapAn.ResetText();
            rTxtCauHoi.Clear();
            rTxtCauHoi.Refresh();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendEvent("0,ser,playkhangia," + currentCau + ",stop," + cuocThiHienTai.cuocthiid);
        }

        private void btnResetKG_Click(object sender, EventArgs e)
        {
            btnCau1kg.Enabled = true;
            btnCau2kg.Enabled = true;
            btnCau3kg.Enabled = true;
            btnCau4kg.Enabled = true;
            rtbCauHoikg.Text = "";
            lblDAkg.Text = "";
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            string message = "Bạn có chắc chắn thoát khỏi cuộc thi không?";
            string title = "Thoát chương trình cuộc thi";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                string sql = "SELECT doiid, sum(sodiem) as tongdiem from ds_diem WHERE cuocthiid = " + cuocThiHienTai.cuocthiid + " GROUP BY cuocthiid,doiid";
                DataTable dt = sqlObject.getDataFromSql(sql, "").Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        ds_doi doiChoi = _entity.ds_doi.Find(int.Parse(dr["doiid"].ToString()));
                        if (doiChoi != null)
                        {
                            if (doiChoi.vitridoi == 1)
                            {
                                cuocThiHienTai.tongdiemdoi1 = int.Parse(dr["tongdiem"].ToString());
                            }
                            if (doiChoi.vitridoi == 2)
                            {
                                cuocThiHienTai.tongdiemdoi2 = int.Parse(dr["tongdiem"].ToString());
                            }
                            if (doiChoi.vitridoi == 3)
                            {
                                cuocThiHienTai.tongdiemdoi3 = int.Parse(dr["tongdiem"].ToString());
                            }
                            if (doiChoi.vitridoi == 4)
                            {
                                cuocThiHienTai.tongdiemdoi4 = int.Parse(dr["tongdiem"].ToString());
                            }
                        }
                    }
                }
                cuocThiHienTai.trangthai = false;
                _entity.SaveChanges();

            }
            else
            {
                // Do something  
            }


        }

        private void mnItemDsDoi_Click(object sender, EventArgs e)
        {
            frmDsDoi frmDsDoi = new frmDsDoi();
            frmDsDoi.ShowDialog();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            List<ds_goicauhoishining> _dsVD = _entity.ds_goicauhoishining.Where(x => x.cuocthiid == cuocThiHienTai.cuocthiid && x.trangThai == 2).ToList();
            if (_dsVD != null && _dsVD.Count > 0)
            {
                for (int i = 0; i < _dsVD.Count; i++)
                {
                    ds_goicauhoishining item = _dsVD[i];
                    item.trangThai = 0;
                }
                _entity.SaveChanges();
            }
            btnCau1VD.Enabled = true;
            btnCau2VD.Enabled = true;
            btnCau3VD.Enabled = true;
            btnCau4VD.Enabled = true;
            btnCau5VD.Enabled = true;
            btnCau6VD.Enabled = true;
            rTxtCauHoiToaSang.Text = "";
            lblDapAnToaSang.Text = "";
            //lvBangDiemVD.Items.Clear();
            //lvBangDiemVD.Refresh();
        }

        private void btnXoaDL_Click(object sender, EventArgs e)
        {
            string message = "Bạn chắc chắn muốn xóa dữ liệu phần thi khởi động?";
            string title = "Xóa dữ liệu Khởi động";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                List<ds_diem> _dsDiem = _entity.ds_diem.Where(x => x.cuocthiid == cuocThiHienTai.cuocthiid && x.phanthiid == 1).ToList();
                if (_dsDiem != null && _dsDiem.Count > 0)
                {
                    for (int i = 0; i < _dsDiem.Count; i++)
                    {
                        ds_diem item = _dsDiem[i];
                        _entity.ds_diem.Remove(item);
                    }
                }
                List<ds_hienthicautraloi> dsCautraloi = _entity.ds_hienthicautraloi.Where(x => x.cuocthiid == cuocThiHienTai.cuocthiid && x.phanthiid == 1).ToList();
                if (dsCautraloi != null && dsCautraloi.Count > 0)
                {
                    for (int i = 0; i < dsCautraloi.Count; i++)
                    {
                        ds_hienthicautraloi item1 = dsCautraloi[i];
                        _entity.ds_hienthicautraloi.Remove(item1);
                    }
                }
                _entity.SaveChanges();
            }

        }

        private void btnXoaKP_Click(object sender, EventArgs e)
        {
            string message = "Bạn chắc chắn muốn xóa dữ liệu phần thi Khám phá?";
            string title = "Xóa dữ liệu Khám phá";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                List<ds_diem> _dsDiem = _entity.ds_diem.Where(x => x.cuocthiid == cuocThiHienTai.cuocthiid && x.phanthiid == 3).ToList();
                if (_dsDiem != null && _dsDiem.Count > 0)
                {
                    for (int i = 0; i < _dsDiem.Count; i++)
                    {
                        ds_diem item = _dsDiem[i];
                        _entity.ds_diem.Remove(item);
                    }
                }
                _entity.SaveChanges();

            }

        }

        private void btnXoaCP_Click(object sender, EventArgs e)
        {
            string message = "Bạn chắc chắn muốn xóa dữ liệu phần thi Chinh phục?";
            string title = "Xóa dữ liệu Chinh phục";
            DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // Lấy danh sách điểm của phần thi Chinh phục
                    var dsDiemList = _entity.ds_diem
                        .Where(x => x.cuocthiid == cuocThiHienTai.cuocthiid && x.phanthiid == 2)
                        .ToList();

                    if (dsDiemList.Any()) // Kiểm tra nếu có dữ liệu cần xóa
                    {
                        // Lấy danh sách ID của ds_diem trước
                        var diemIds = dsDiemList.Select(d => d.diemid).ToList();

                        // Lấy danh sách chi tiết điểm liên quan
                        var dsChiTietDiemList = _entity.ds_chitietdiem
                            .Where(x => diemIds.Contains(x.diemid))
                            .ToList();

                        // Xóa bảng ds_chitietdiem trước
                        if (dsChiTietDiemList.Any())
                        {
                            _entity.ds_chitietdiem.RemoveRange(dsChiTietDiemList);
                        }

                        // Xóa bảng ds_diem
                        _entity.ds_diem.RemoveRange(dsDiemList);

                        // Lưu thay đổi vào database
                        _entity.SaveChanges();

                        MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXoaVD_Click(object sender, EventArgs e)
        {
            string message = "Bạn chắc chắn muốn xóa dữ liệu phần thi Về đích?";
            string title = "Xóa dữ liệu Về đích";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                List<ds_diem> _dsDiem = _entity.ds_diem.Where(x => x.cuocthiid == cuocThiHienTai.cuocthiid && x.phanthiid == 4).ToList();
                if (_dsDiem != null && _dsDiem.Count > 0)
                {
                    for (int i = 0; i < _dsDiem.Count; i++)
                    {
                        ds_diem item = _dsDiem[i];
                        _entity.ds_diem.Remove(item);
                    }
                }
                List<ds_goicauhoishining> _dsVD = _entity.ds_goicauhoishining.Where(y => y.trangThai != 0  && y.cuocthiid == cuocThiHienTai.cuocthiid).ToList();
                if (_dsVD != null && _dsVD.Count > 0)
                {
                    for (int i = 0; i < _dsVD.Count; i++)
                    {
                        ds_goicauhoishining item = _dsVD[i];
                        item.trangThai = 0;
                    }
                }
                _entity.SaveChanges();
            }


        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            Dictionary<int, string> temp = new Dictionary<int, string>();
            temp = teamplayings;
            lvBangDiemMain.Items.Clear();
            lvBangDiemMain.Refresh();
            string sql = "SELECT doiid, sum(sodiem) as tongdiem from ds_diem WHERE cuocthiid = " + cuocThiHienTai.cuocthiid + " GROUP BY cuocthiid, doiid";
            DataTable dt = sqlObject.getDataFromSql(sql, "").Tables[0];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    int id_team = int.Parse(dr["doiid"].ToString());
                    ds_doi _doi = _entity.ds_doi.Find(id_team);
                    string[] row = { "TEAM " + _doi.vitridoi + " - " + _doi.tennguoichoi + " - " + _doi.tendoi, dr["tongdiem"].ToString() };
                    ListViewItem lvi = new ListViewItem(row);
                    lvBangDiemMain.Items.Add(lvi);
                    if (temp.ContainsKey(id_team))
                    {
                        temp.Remove(id_team);
                    }
                    /*clsDiemDisplay item = dsDiemKD.FirstOrDefault(x => x.doiid == int.Parse(dr["doiid"].ToString()));
                    if (item != null)
                    {
                        item.sodiem = int.Parse(dr["tongdiem"].ToString());
                    }*/
                }
                foreach (var item in temp)
                {
                    string[] row = { item.Value.ToString(), "0" };
                    ListViewItem lvi = new ListViewItem(row);
                    lvBangDiemMain.Items.Add(lvi);
                }
            }
        }

        private void btnResetTTCH_Click(object sender, EventArgs e)
        {
            loadNoiDungCauHoiCP();
        }

        private void btnLatCHC_Click(object sender, EventArgs e)
        {
        }

        private void btnCauPKP_Click(object sender, EventArgs e)
        {
            resetKetQua();
            tmMain.Enabled = false;
            //axWinMedia.Ctlcontrols.stop();
            time = 30;
            lblThoiGian.Text = time.ToString();
            ds_cauhoithuthach cauHoi = _entity.ds_cauhoithuthach.FirstOrDefault(x => x.vitri == 5 && x.cuocthiid == cuocThiHienTai.cuocthiid);
            if (cauHoi != null)
            {
                currentCau = cauHoi.cauhoiid;
                rTxtCauHoi.Text = cauHoi.noidung;
                lblDapAn.Text = cauHoi.dapantext + "\n" + cauHoi.dapanABC;
                //fullPath = directoryPath + "\\Resources\\Video\\" + cauHoi.urlcauhoi;
                //axWinMedia.URL = fullPath;
            }
            disableButtonKP((int)cauHoi.vitri);
            SendEvent("0,ser,playthuthach," + currentCau + ",ready," + cuocThiHienTai.cuocthiid);
        }

        private void btnSuaDiemKD_Click(object sender, EventArgs e)
        {
            ChinhDiemKD frmChinhDiem = new ChinhDiemKD(int.Parse(cbxDoiChoi.SelectedValue.ToString()), cuocThiHienTai.cuocthiid);
            frmChinhDiem.ShowDialog();
        }

        private void btnChinhDiemKP_Click(object sender, EventArgs e)
        {
            ChinhDiemThuThach frmChinhDiemKP = new ChinhDiemThuThach(cuocThiHienTai.cuocthiid);
            frmChinhDiemKP.ShowDialog();
        }

        private void btnReady_Click(object sender, EventArgs e)
        {
            //ds_goicauhoivedich ds = _entity.ds_goicauhoivedich.FirstOrDefault(x => x.cuocthiid == cuocThiHienTai.cuocthiid);
            SendEvent("0,ser,playtoasang," + cuocThiHienTai.cuocthiid + ",0,hienthi5NutCauHoi");


        }

        private void btnReadyGoiKD_Click(object sender, EventArgs e)
        {

            SendEvent(cbxDoiChoi.SelectedValue.ToString() + ",ser,playkhoidong,0,0,ready");
        }

        private void dgvConnected_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnUpdateDiemVD_Click(object sender, EventArgs e)
        {
            ChinhDiemToaSang frmChinhDiemVD = new ChinhDiemToaSang(cuocThiHienTai.cuocthiid, goiVDCurrent, int.Parse(cbbDoiChoiVD.SelectedValue.ToString()));
            frmChinhDiemVD.ShowDialog();
        }
        private void loadNoiDungCauHoiThiSinh()
        {
            string sql = "SELECT * from ds_goicaudiscovery WHERE cuocthiid = " + cuocThiHienTai.cuocthiid + " and doithiid = " + int.Parse(slbDoiChoiCP.SelectedValue.ToString()) + " and trangthai = 'true'";
            DataTable dt = sqlObject.getDataFromSql(sql, "").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    pBCauHoiChinhCP.Image = Image.FromFile(directoryPath + "\\Resources\\pic\\" + dr["noidungthisinh"].ToString());

                }
            }
        }
        private void btnHienThiDapAnChung_Click(object sender, EventArgs e)
        {
            SendEvent("0,ser,playthuthach," + currentCau + ",hienthidapanCT," + cuocThiHienTai.cuocthiid);

        }

        private void btnHienThiAnhThiSinh_Click(object sender, EventArgs e)
        {
            loadNoiDungCauHoiThiSinh();
            tmMain.Enabled = false;
            SendEvent("0,ser,playkhamphachiase," + cauhoichudeId + ",0,hienthianhthisinh");
        }

        private void btnDungThoiGian_Click(object sender, EventArgs e)
        {
            tmMain.Enabled = false; // Dừng timer
            SendEvent("0,ser,playkhamphachiase," + cauhoichudeId + ",0,stopTime");

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(rtbCauHoiChinh.Text == "" || rtbCauHoiChinh.Text == null)
            {
                MessageBox.Show("Chưa có nội dung chủ đề");
                return;
            }
            tmMain.Enabled = true;
            time = 180;
            lblThoiGian.Text = time.ToString();
            // Kiểm tra nếu có video thì phát
            if (!string.IsNullOrEmpty(axWindowsMediaPlayer1.URL))
            {
                axWindowsMediaPlayer1.Visible = true; // Hiện player
                axWindowsMediaPlayer1.Ctlcontrols.play(); // Phát video
            }
            else
            {
                axWindowsMediaPlayer1.Visible = false; // Tắt player

                axWindowsMediaPlayer1.Ctlcontrols.stop(); // Tắt video

            }
            SendEvent("0,ser,playkhamphachiase," + cauhoichudeId + ",0,start");
        }

        private void btnResetDL_Click(object sender, EventArgs e)
        {
            // Xóa dữ liệu giao diện
            rtbCauHoiChinh.Text = "";
            btnCPCau1.BackgroundImage = null;
            btnCPCau2.BackgroundImage = null;
            btnCPCau3.BackgroundImage = null;
            btnCPCau4.BackgroundImage = null;
            btnCPCau5.BackgroundImage = null;
            btnCPCau6.BackgroundImage = null;
            pBCauHoiChinhCP.Image = null;
            lblDiemGK1.Text = "0";
            lblDiemGK2.Text = "0";
            lblDiemGK3.Text = "0";
            btnCPCau1.Enabled = true;
            btnCPCau1.Visible = true;
            btnCPCau2.Enabled = true;
            btnCPCau2.Visible = true;
            btnCPCau3.Enabled = true;
            btnCPCau3.Visible = true;
            btnCPCau4.Enabled = true;
            btnCPCau4.Visible = true;
            btnCPCau5.Enabled = true;
            btnCPCau5.Visible = true;
            btnCPCau6.Enabled = true;
            btnCPCau6.Visible = true;
            btnCPCau1.Text = "Câu 1";
            btnCPCau2.Text = "Câu 2";
            btnCPCau3.Text = "Câu 3";
            btnCPCau4.Text = "Câu 4";
            btnCPCau5.Text = "Câu 5";
            btnCPCau6.Text = "Câu 6";
            tmMain.Enabled = false; // Dừng timer

            // Chuyển đổi SelectedValue thành số nguyên trước khi truy vấn
            int doiThiId;
            if (!int.TryParse(slbDoiChoiCP.SelectedValue?.ToString(), out doiThiId))
            {
                MessageBox.Show("Giá trị đội thi không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Thoát nếu không thể chuyển đổi
            }

            // Lấy danh sách câu hỏi discovery
            var ds_Goicaudiscovery = _entity.ds_goicaudiscovery
                .Where(x => x.cuocthiid == cuocThiHienTai.cuocthiid && x.doithiid == doiThiId)
                .ToList(); // Lấy dữ liệu trước khi xử lý

            // Kiểm tra danh sách rỗng
            if (ds_Goicaudiscovery.Any())
            {
                // Cập nhật trạng thái mà không gọi SaveChanges trong vòng lặp
                foreach (var item in ds_Goicaudiscovery)
                {
                    item.trangthailatAnhPhu = 0;
                }

                // Lưu tất cả thay đổi cùng lúc
                _entity.SaveChanges();
            }
            time = 180;
            lblThoiGian.Text = time.ToString();
            // Gửi sự kiện sau khi reset dữ liệu
            //SendEvent("0,ser,playkhamphachiase," + cauhoichudeId + "," + cauHoiPhuCurrent.cauhoiid + ",resetDuLieu");
        }

        private void btnHienThi6Nut_Click(object sender, EventArgs e)
        {
            btnCPCau1.BackgroundImage = null;
            btnCPCau2.BackgroundImage = null;
            btnCPCau3.BackgroundImage = null;
            btnCPCau4.BackgroundImage = null;
            btnCPCau5.BackgroundImage = null;
            btnCPCau6.BackgroundImage = null;
            pBCauHoiChinhCP.Image = null;
            lblDiemGK1.Text = "0";
            lblDiemGK2.Text = "0";
            lblDiemGK3.Text = "0";
            btnCPCau1.Enabled = true;
            btnCPCau1.Visible = true;
            btnCPCau2.Enabled = true;
            btnCPCau2.Visible = true;
            btnCPCau3.Enabled = true;
            btnCPCau3.Visible = true;
            btnCPCau4.Enabled = true;
            btnCPCau4.Visible = true;
            btnCPCau5.Enabled = true;
            btnCPCau5.Visible = true;
            btnCPCau6.Enabled = true;
            btnCPCau6.Visible = true;
            btnCPCau1.Text = "Câu 1";
            btnCPCau2.Text = "Câu 2";
            btnCPCau3.Text = "Câu 3";
            btnCPCau4.Text = "Câu 4";
            btnCPCau5.Text = "Câu 5";
            btnCPCau6.Text = "Câu 6";
            SendEvent("0,ser,playkhamphachiase," + cauhoichudeId + ",0,load6nut");
        }

        private void btnCapNhatDiemCacManHinh_Click(object sender, EventArgs e)
        {
            capNhatTongDiem();
            SendEvent(cbxDoiChoi.SelectedValue.ToString() + ",ser,playkhoidong,0,"+goiKDHienTai+",capNhatDiemManHinh");

        }

        private void btnCapNhatDiemManHinhTT_Click(object sender, EventArgs e)
        {
            capNhatTongDiem();
            SendEvent("0,ser,playthuthach," + currentCau + ",capNhatDienManHinhTT," + cuocThiHienTai.cuocthiid);
        }

        private void btnCapNhatDiemManHinhTS_Click(object sender, EventArgs e)
        {
            capNhatTongDiem();
            SendEvent(cbbDoiChoiVD.SelectedValue.ToString() + ",ser,playtoasang," + cuocThiHienTai.cuocthiid + "," + currentCau + ",capNhatDiemManHinhTS");
        }

        private void slbDoiChoiCP_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDiemGK1.Text = "0";
            lblDiemGK2.Text = "0";
            lblDiemGK3.Text = "0";

        }

        private void btnHienThiDapAn_Click(object sender, EventArgs e)
        {
            if (_currentQuestionIdForAnswer > 0)
            {
                // Gửi sự kiện hiển thị đáp án
                string eventData = $"0,ser,playtoasang,{cuocThiHienTai.cuocthiid},{_currentQuestionIdForAnswer},forceanswer";
                SendEvent(eventData);
            }
            else
            {
                MessageBox.Show("Không có câu hỏi nào để hiển thị đáp án");
            }
        }

        private void cbxDungSai_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbNgoiSaoHiVong_CheckedChanged(object sender, EventArgs e)
        {
            //if (cbNgoiSaoHiVong.Checked)
            //{
            //    // Phát sự kiện "ngôi sao hy vọng" với trạng thái start để phát ngay
            //    SendEvent(cbbDoiChoiVD.SelectedValue.ToString() + ",ser,playtoasang," + cuocThiHienTai.cuocthiid + ",0,start_ngoisaohivong");
            //}

        }

        private void btnNgoiSao_Click(object sender, EventArgs e)
        {
            if (!cbNgoiSaoHiVong.Checked)
            {
                if (_currentQuestionIdForAnswer > 0)
                {
                    // Nếu đã được check thì gửi sự kiện như bình thường
                    SendEvent(cbbDoiChoiVD.SelectedValue.ToString()
                              + ",ser,playtoasang,"
                              + cuocThiHienTai.cuocthiid
                              + "," + _currentQuestionIdForAnswer + ",start_Nongoisaohivong");
                }
            }
            else
            {
                if (_currentQuestionIdForAnswer > 0)
                {
                    // Nếu đã được check thì gửi sự kiện như bình thường
                    SendEvent(cbbDoiChoiVD.SelectedValue.ToString()
                              + ",ser,playtoasang,"
                              + cuocThiHienTai.cuocthiid
                              +","+ _currentQuestionIdForAnswer + ",start_ngoisaohivong");
                }
            }
            

        }


        private void mnItemPhanThi_Click(object sender, EventArgs e)
        {
            frmDmPhanThi frmPhanThi = new frmDmPhanThi();
            frmPhanThi.ShowDialog();
        }
    }
}
