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
    public partial class ChinhDiemThuThach : Form
    {
        private int _cuocThiId;
        QuaMienDiSanEntities _entity = new QuaMienDiSanEntities();
        List<ds_cauhoithuthach> lsCauHoiKP = new List<ds_cauhoithuthach>();
        public ChinhDiemThuThach()
        {
            InitializeComponent();
        }

        public ChinhDiemThuThach(int cuocThiId)
        {
            InitializeComponent();
            _cuocThiId = cuocThiId;
        }

        private void cbxCauHoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds_cauhoithuthach _cauHoi = new ds_cauhoithuthach();
            switch (cbxCauHoi.SelectedIndex)
            {
                case 0:
                    _cauHoi = lsCauHoiKP.FirstOrDefault(x => x.vitri == 1);
                    break;
                case 1:
                    _cauHoi = lsCauHoiKP.FirstOrDefault(x => x.vitri == 2);
                    break;
                case 2:
                    _cauHoi = lsCauHoiKP.FirstOrDefault(x => x.vitri == 3);
                    break;
                case 3:
                    _cauHoi = lsCauHoiKP.FirstOrDefault(x => x.vitri == 4);
                    break;
                default:
                    break;
            }
            List<ds_diem> ls_diem = _entity.ds_diem.Where(x => x.cuocthiid == _cuocThiId && x.phanthiid == 3 && x.cauhoiid == _cauHoi.cauhoiid).ToList();
            dgvDiem.DataSource = ls_diem;
        }

        private void ChinhDiemKP_Load(object sender, EventArgs e)
        {
            lsCauHoiKP = _entity.ds_cauhoithuthach.Where(x => x.cuocthiid == _cuocThiId).ToList();

        }

        private void dgvDiem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvDiem.Rows[e.RowIndex];
            txtDiem.Text = row.Cells[3].Value.ToString();
            lblDiemID.Text = row.Cells[0].Value.ToString();
            int doiId = int.Parse(row.Cells[1].Value.ToString());
            ds_doi _doi = _entity.ds_doi.Find(doiId);
            if (_doi != null)
            {
                txtTenThiSinh.Text = _doi.tennguoichoi;
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
                }
                _entity.SaveChanges();
                dgvDiem.Refresh();
            }
            else
            {
                MessageBox.Show("Chưa chọn ô cần chỉnh sửa!");
            }
        }
    }
}
