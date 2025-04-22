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
    public partial class frmDmKhanGia : Form
    {
        QuaMienDiSanEntities _entity = new QuaMienDiSanEntities();
        List<ds_phanthikhangia> _kg = new List<ds_phanthikhangia>();
        private int id_select = 0;

        public frmDmKhanGia()
        {
            InitializeComponent();
        }

        private void frmKhanGia_Load(object sender, EventArgs e)
        {
            loadKhanGia();
        }
        private void loadKhanGia()
        {
            ds_cuocthi cuocthihientai = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);

            _kg = _entity.ds_phanthikhangia.Where(x => x.cauthiid == cuocthihientai.cuocthiid).ToList();
            dgvKhanGia.DataSource = _kg;
            
        }
        private void ResetData()
        {
            txtcauhoi.Text = "";
            txtdapan.Text = "";
            cbxVitri.Text = "";

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            ds_cuocthi cuocthihientai = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);
            if (txtcauhoi.Text.Trim() == "" || txtdapan.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập câu hỏi!");
                txtcauhoi.Focus();
                return;
            }
            else
            {

                ds_phanthikhangia kg = new ds_phanthikhangia();

                kg.noidungcauhoi = txtcauhoi.Text;
                kg.dapan = txtdapan.Text;
                kg.cauthiid = cuocthihientai.cuocthiid;
                if (cbxVitri.Text == null || cbxVitri.Text == "")
                {
                    cbxVitri.Text = "0";
                }
                else
                {
                    kg.vitri = int.Parse(cbxVitri.Text);

                }
                _entity.ds_phanthikhangia.Add(kg);
                _entity.SaveChanges();
                ResetData();
                loadKhanGia();

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            ds_cuocthi cuocthihientai = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);
            if (id_select > 0)
            {
                ds_phanthikhangia kg = _entity.ds_phanthikhangia.Find(id_select);
                kg.noidungcauhoi = txtcauhoi.Text;
                kg.dapan = txtdapan.Text;
                kg.cauthiid = cuocthihientai.cuocthiid;
                if (cbxVitri.Text == null || cbxVitri.Text == "")
                {
                    cbxVitri.Text = "0";

                }
                else
                {
                    kg.vitri = int.Parse(cbxVitri.Text);

                }
                _entity.SaveChanges();
                ResetData();
                loadKhanGia();

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ds_cuocthi cuocthihientai = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);
            if (id_select > 0)
            {
                ds_phanthikhangia kg = _entity.ds_phanthikhangia.Find(id_select);
                _entity.ds_phanthikhangia.Remove(kg);
                _entity.SaveChanges();
                loadKhanGia();
            }
        }

        private void dgvKhanGia_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_kg.Count > 0)
            {
                DataGridViewRow row = dgvKhanGia.Rows[e.RowIndex];
                id_select = int.Parse(row.Cells[0].Value.ToString());
                txtcauhoi.Text = row.Cells[3].Value.ToString();
                txtdapan.Text = row.Cells[4].Value.ToString();
                if (row.Cells[2].Value.ToString() == "" || row.Cells[2].Value.ToString() == null)
                {
                    cbxVitri.Text = "0";
                }
                else
                {
                    cbxVitri.Text = row.Cells[2].Value.ToString();

                }

            }
        }

        private void btnloadForm_Click(object sender, EventArgs e)
        {
            if (id_select > 0)
            {
                id_select = 0;
            }
            loadKhanGia();
        }
    }
}
