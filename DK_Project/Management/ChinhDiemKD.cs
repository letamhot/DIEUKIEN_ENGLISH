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
    public partial class ChinhDiemKD : Form
    {
        private int _doiId, _cuocThiId;
        QuaMienDiSanEntities _entity = new QuaMienDiSanEntities();
        SqlDataAccess _sqlAccess = new SqlDataAccess();
        public ChinhDiemKD()
        {
            InitializeComponent();
        }

        private void ChinhDiemKD_Load(object sender, EventArgs e)
        {
            List<nDiem> listDiem = new List<nDiem>();
            string sql = "SELECT a.diemid, b.noidungcauhoi, a.cauhoiid, a.sodiem from ds_diem a inner join ds_goicauhoikhoidong b on b.cauhoiid = a.cauhoiid where " +
                "a.doiid = " + _doiId + " and a.phanthiid = 1 and a.cuocthiid = " + _cuocThiId;
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
                    listDiem.Add(item);
                }
            }

            //List<ds_diem> lsDiem = _entity.ds_diem.Where(x => x.doiid == _doiId && x.phanthiid == 1 && x.cuocthiid == _cuocThiId).ToList();
            dgvDiem.DataSource = listDiem;
        }

        private void dgvDiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvDiem.Rows[e.RowIndex];
            //MessageBox.Show(row.Cells[0].Value.ToString());
            txtDiem.Text = row.Cells[3].Value.ToString();
            lblID.Text = row.Cells[0].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(lblID.Text != "")
            {
                ds_diem _diem = _entity.ds_diem.Find(int.Parse(lblID.Text));
                if (_diem != null)
                {
                    string sqlUpdate = "UPDATE ds_diem set sodiem = " + int.Parse(txtDiem.Text) + " WHERE diemid = " + int.Parse(lblID.Text);
                    //_diem.sodiem = int.Parse(txtDiem.Text);
                    int cauhoiid = (int)_diem.cauhoiid;
                    ds_hienthicautraloi _hienthi = _entity.ds_hienthicautraloi.FirstOrDefault(x => x.doiid == _doiId && x.cuocthiid == _cuocThiId && x.cauhoiid == cauhoiid);
                    if (_hienthi != null)
                    {
                        if (txtDiem.Text == "0")
                        {
                            _hienthi.traloi = false;
                        }
                        else
                        {
                            _hienthi.traloi = true;
                        }
                        
                    }
                    _sqlAccess.DbCommand(sqlUpdate);
                }
                _entity.SaveChanges();
                ChinhDiemKD_Load(new object(), new EventArgs());
                //dgvDiem.Refresh();

            }
            else
            {
                MessageBox.Show("Chưa chọn ô cần chỉnh sửa!");
            }
        }

        public ChinhDiemKD(int doiId, int cuocThiId)
        {
            InitializeComponent();
            _doiId = doiId;
            _cuocThiId = cuocThiId;
        }


    }
}
