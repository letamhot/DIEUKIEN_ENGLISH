using DK_Project.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DK_Project.Management
{
    public partial class ChinhDiemToaSang : Form
    {
        private int _cuocThiId;
        private int _goicauhoi;
        private int _doiId;
        QuaMienDiSanEntities _entity = new QuaMienDiSanEntities();
        List<ds_goicauhoishining> lsCauHoiVD = new List<ds_goicauhoishining>();
        SqlDataAccess _sqlAccess = new SqlDataAccess();
        Dictionary<int, string> teamplayings = new Dictionary<int, string>();


        public ChinhDiemToaSang()
        {
            InitializeComponent();
        }
        public ChinhDiemToaSang(int cuocthiId, int goicauhoi, int doiId)
        {
            InitializeComponent();
            _cuocThiId = cuocthiId;
            _goicauhoi = goicauhoi;
            _doiId = doiId;
            var dsdoi = _entity.ds_doi.Where(x => x.vaitro == "TS" && x.cuocthiid == cuocthiId).ToList();
            for (int i = 0; i < dsdoi.Count; i++)
            {
                teamplayings.Add(dsdoi[i].doiid, "ĐỘI " + dsdoi[i].vitridoi + " - " + dsdoi[i].tennguoichoi + " - " + dsdoi[i].tendoi);
                
                cbbThiSinh.DataSource = new BindingSource(teamplayings, null);
                cbbThiSinh.DisplayMember = "Value";
                cbbThiSinh.ValueMember = "Key";
            }

        }

        private void cbxCauHoi_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
        }

        private void ChinhDiemVD_Load(object sender, EventArgs e)
        {
            //lsCauHoiVD = _entity.ds_goicauhoivedich.Where(x => x.cuocthiid == _cuocThiId && x.goicauhoiid == _goicauhoi).ToList();
            List<nDiem> listDiem = new List<nDiem>();
            string sql = "SELECT a.diemid, b.noidungcauhoi, a.cauhoiid, a.sodiem, a.doiid from ds_diem a inner join ds_goicauhoishining b on b.cauhoiid = a.cauhoiid where " +
                " a.phanthiid = 4 and a.cuocthiid = " + _cuocThiId;
            DataTable dt = _sqlAccess.getDataFromSql(sql, "").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    nDiem item = new nDiem();
                    item.diemId = int.Parse(dt.Rows[i]["diemid"].ToString());
                    item.cauHoi = dt.Rows[i]["noidungcauhoi"].ToString();
                    item.cauhoiid = int.Parse(dt.Rows[i]["cauhoiid"].ToString());
                    item.soDiem = int.Parse(dt.Rows[i]["sodiem"].ToString());
                    item.doiid = int.Parse(dt.Rows[i]["doiid"].ToString());
                    listDiem.Add(item);
                }
            }
            //List<ds_diem> ls_diem = _entity.ds_diem.Where(x => x.cuocthiid == _cuocThiId && x.phanthiid == 4 && x.cauhoiid == _cauHoi.cauhoiid).ToList();
            dgvDiem.DataSource = listDiem;
        }

        private void dgvDiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra xem hàng được click có hợp lệ không
            {
                DataGridViewRow row = dgvDiem.Rows[e.RowIndex];

                // Hiển thị sodiem từ cột thứ 3 vào TextBox txtDiem
                txtDiem.Text = row.Cells[3].Value?.ToString() ?? "";

                // Hiển thị diemId từ cột đầu tiên vào Label lblDiemID
                lblDiemID.Text = row.Cells[0].Value?.ToString() ?? "";

                // Kiểm tra và lấy giá trị doiid từ cột thứ 4 (Cells[4] chứa doiid trong mã trước đó)
                if (int.TryParse(row.Cells[4].Value?.ToString(), out int _doiId))
                {
                    // Tìm đội chơi trong danh sách ds_doi
                    ds_doi _doi = _entity.ds_doi.Find(_doiId);
                    if (_doi != null)
                    {
                        // Hiển thị thông tin người chơi vào TextBox và ComboBox
                        txtTenThiSinh.Text = _doi.tennguoichoi;

                        cbbThiSinh.SelectedItem = _doiId;
                        cbbThiSinh.Text = "TEAM " + _doi.vitridoi + " - " + _doi.tennguoichoi + " - " + _doi.tendoi;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy đội chơi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Giá trị doiid không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lblDiemID.Text != "")
            {
                ds_diem _diem = _entity.ds_diem.Find(int.Parse(lblDiemID.Text));
                if (_diem != null)
                {
                    _diem.sodiem = int.Parse(txtDiem.Text);
                    _diem.doiid = int.Parse(cbbThiSinh.SelectedValue.ToString());

                }
                _entity.SaveChanges();
                ChinhDiemVD_Load(new object(), new EventArgs());
                dgvDiem.Refresh();
            }
            else
            {
                MessageBox.Show("Chưa chọn ô cần chỉnh sửa!");
            }
        }
    }
}
