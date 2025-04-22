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
    public partial class frmDmPhanThi : Form
    {
        QuaMienDiSanEntities _entity = new QuaMienDiSanEntities();
        List<ds_phanthi> _phanThi = new List<ds_phanthi>();
        private int id_select = 0;
        public frmDmPhanThi()
        {
            InitializeComponent();
        }

        private void frmPhanThi_Load(object sender, EventArgs e)
        {
            loadPhanThi();
        }

        private void loadPhanThi()
        {
            _phanThi = _entity.ds_phanthi.ToList();
            dgvPhanThi.DataSource = _phanThi;
        }

        private void dgvPhanThi_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_phanThi.Count > 0)
            {
                DataGridViewRow row = dgvPhanThi.Rows[e.RowIndex];
                id_select = int.Parse(row.Cells[0].Value.ToString());
                txtTenPhanThi.Text = row.Cells[1].Value.ToString();
                rtbMoTa.Text = row.Cells[2].Value.ToString();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (id_select > 0)
            {
                ds_phanthi phanThi = _entity.ds_phanthi.Find(id_select);
                if (phanThi != null)
                {
                    phanThi.tenphanthi = txtTenPhanThi.Text;
                    phanThi.mota = rtbMoTa.Text;
                    _entity.SaveChanges();
                    loadPhanThi();
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (id_select == 0)
            {
                ds_phanthi phanThi = new ds_phanthi();
                phanThi.tenphanthi = txtTenPhanThi.Text;
                phanThi.mota = rtbMoTa.Text;
                _entity.ds_phanthi.Add(phanThi);
                _entity.SaveChanges();
                loadPhanThi();
            }
        }

        private void btnloadForm_Click(object sender, EventArgs e)
        {
            if( id_select > 0)
            {
                id_select = 0;
            }
            loadPhanThi();
        }
    }
}
