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

namespace DK_Project
{
    public partial class frmCuocThi : Form
    {
        QuaMienDiSanEntities quaMienDiSanEntities = new QuaMienDiSanEntities();
        SqlDataAccess SqlDataAccess = new SqlDataAccess();
        public frmCuocThi()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtTenCuocThi.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên cuộc thi!");
                txtTenCuocThi.Focus();
                return;
            }
            var doithi = quaMienDiSanEntities.ds_doi.Where(x => x.vaitro == "TS").ToList();
            ds_cuocthi cuocthi = new ds_cuocthi();
            cuocthi.tencuocthi = txtTenCuocThi.Text;
            cuocthi.trangthai = true;
            cuocthi.doi1id = 1;
            cuocthi.doi2id = 2;
            cuocthi.doi3id = 3;
            cuocthi.doi4id = 4;
            cuocthi.tongdiemdoi1 = 0;
            cuocthi.tongdiemdoi2 = 0;
            cuocthi.tongdiemdoi3 = 0;
            cuocthi.tongdiemdoi4 = 0;
            cuocthi.motacuocthi = txtMoTa.Text;
            quaMienDiSanEntities.ds_cuocthi.Add(cuocthi);
            quaMienDiSanEntities.SaveChanges();
            Hide();
            TrinhDieuKhien frmTrinhDieuKhien = new TrinhDieuKhien(cuocthi.cuocthiid);
            frmTrinhDieuKhien.ShowDialog();
            Close();
        }

        private void frmCuocThi_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM ds_cuocthi WHERE trangthai = 1";
            DataSet ds = SqlDataAccess.getDataFromSql(sql, "");

            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                Hide();
                DataTable dt = ds.Tables[0];
                string sql_update = "update ds_cuocthi set tongdiemdoi1 = 0, tongdiemdoi2 = 0, tongdiemdoi3 = 0, tongdiemdoi4 = 0 where cuocthiid = " + dt.Rows[0]["cuocthiid"].ToString();
                SqlDataAccess.DbCommand(sql_update);
                TrinhDieuKhien frmTrinhDieuKhien = new TrinhDieuKhien(int.Parse(dt.Rows[0]["cuocthiid"].ToString()));
                frmTrinhDieuKhien.ShowDialog();
                Close();
            }
        }
    }
}
