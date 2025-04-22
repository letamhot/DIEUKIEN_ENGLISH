using DK_Project.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DK_Project.Management
{
    public partial class frmDsDoi : Form
    {
        QuaMienDiSanEntities _entity = new QuaMienDiSanEntities();
        List<ds_doi> _doi = new List<ds_doi>();
        private int id_select = 0;
        public frmDsDoi()
        {
            InitializeComponent();
        }

        private void frmDsDoi_Load(object sender, EventArgs e)
        {
            loadDsDoi();
        }
        private void loadDsDoi()
        {

            _doi = _entity.ds_doi.Where(x => x.vaitro == "TS").ToList();
            dgvDsDoi.DataSource = _doi;
            
        }
        private void ResetData()
        {
            txtTenDoiChoi.Text = "";
            txtNguoiChoi.Text = "";
            cbxVitri.Text = "";
            txtMatKhau.Text = "";
            txtTendangnhap.Text = "";
            txtdcip.Text = "";

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            ds_cuocthi cuocthihientai = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);
            if (txtTenDoiChoi.Text.Trim() == "" || txtNguoiChoi.Text.Trim() == "" || txtTendangnhap.Text.Trim() == "" || txtMatKhau.Text.Trim() == "" || txtdcip.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập!");
                txtTenDoiChoi.Focus();
                return;
            }
            else
            {
                ds_doi dsdoi = new ds_doi();
                dsdoi.cuocthiid = cuocthihientai.cuocthiid;
                dsdoi.tendoi = txtTenDoiChoi.Text;
                dsdoi.tennguoichoi = txtNguoiChoi.Text;
                dsdoi.tendangnhap = txtTendangnhap.Text;
                dsdoi.matkhau = txtMatKhau.Text;
                dsdoi.vitridoi = int.Parse(cbxVitri.SelectedItem.ToString());
                dsdoi.vaitro = "TS";
                _entity.ds_doi.Add(dsdoi);
                _entity.SaveChanges();
                ResetData();
                loadDsDoi();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            ds_cuocthi cuocthihientai = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);
            if (id_select > 0)
            {
                ds_doi dsdoi = _entity.ds_doi.Find(id_select);
                dsdoi.cuocthiid = cuocthihientai.cuocthiid;
                dsdoi.tendoi = txtTenDoiChoi.Text;
                dsdoi.tennguoichoi = txtNguoiChoi.Text;
                dsdoi.tendangnhap = txtTendangnhap.Text;
                dsdoi.matkhau = txtMatKhau.Text;
                dsdoi.vitridoi = int.Parse(cbxVitri.SelectedItem.ToString());
                dsdoi.vaitro = "TS";
                _entity.SaveChanges();
                ResetData();
                loadDsDoi();
            }
        }

        private void dgvDsDoi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (_doi.Count > 0)
                {
                    DataGridViewRow row = dgvDsDoi.Rows[e.RowIndex];
                    id_select = int.Parse(row.Cells[0].Value.ToString());
                    txtTenDoiChoi.Text = row.Cells[1].Value.ToString();
                    txtdcip.Text = row.Cells[2].Value.ToString();
                    txtNguoiChoi.Text = row.Cells[3].Value.ToString();
                    txtTendangnhap.Text = row.Cells[4].Value.ToString();
                    txtMatKhau.Text = row.Cells[5].Value.ToString();
                    cbxVitri.Text = row.Cells[6].Value.ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (id_select > 0)
            {

                ds_doi dsdoi = _entity.ds_doi.Find(id_select);
                _entity.ds_doi.Remove(dsdoi);
                _entity.SaveChanges();
                loadDsDoi();

            }
        }

        private void btnloadForm_Click(object sender, EventArgs e)
        {
            if(id_select > 0)
            {
                id_select = 0;
            }
            loadDsDoi();
        }

        /*private void btnUpload_Click(object sender, EventArgs e)
        {
            // Tạo một OpenFileDialog để chọn file Excel
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                Title = "Select an Excel file"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Đọc file Excel và hiển thị trong DataGridView
                string filePath = openFileDialog.FileName;
                DataTable dt = ReadExcel(filePath);
                dgvDsDoi.DataSource = dt;
                // Lưu dữ liệu vào bảng ds_goicauhoikhampha
                SaveDataToDatabase(dt);
                loadDsDoi();
            }
        }

        private DataTable ReadExcel(string filePath)
        {
            DataTable dt = new DataTable();

            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Lấy sheet đầu tiên
                int colCount = worksheet.Dimension.End.Column;   // Số cột
                int rowCount = worksheet.Dimension.End.Row;      // Số hàng

                // Tạo các cột cho DataTable
                for (int col = 1; col <= colCount; col++)
                {
                    dt.Columns.Add(worksheet.Cells[1, col].Value?.ToString() ?? $"Column {col}");
                }

                // Đọc dữ liệu từ Excel vào DataTable
                for (int row = 2; row <= rowCount; row++)
                {
                    DataRow dr = dt.NewRow();
                    for (int col = 1; col <= colCount; col++)
                    {
                        dr[col - 1] = worksheet.Cells[row, col].Value?.ToString();
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }

        private void SaveDataToDatabase(DataTable dt)
        {
            using (QuaMienDiSanEntities _entity = new QuaMienDiSanEntities())
            {
                foreach (DataRow row in dt.Rows)
                {
                    int? cuocthiid = TryParseInt(row["cuocthiid"]);
                    int? vitridoi = TryParseInt(row["vitridoi"]);
                    ds_doi newRecord = new ds_doi
                    {
                        tendoi = row["tendoi"]?.ToString(),
                        tennguoichoi = string.IsNullOrWhiteSpace(row["tennguoichoi"]?.ToString()) ? null : row["tennguoichoi"].ToString(),
                        tendangnhap = string.IsNullOrWhiteSpace(row["tendangnhap"]?.ToString()) ? null : row["tendangnhap"].ToString(),
                        matkhau = string.IsNullOrWhiteSpace(row["matkhau"]?.ToString()) ? null : row["matkhau"].ToString(),
                        vitridoi = vitridoi ?? 0,
                        vaitro = string.IsNullOrWhiteSpace(row["vaitro"]?.ToString()) ? null : row["vaitro"].ToString(),
                        cuocthiid = cuocthiid ?? 0,


                    };

                    // Thêm đối tượng mới vào context
                    _entity.ds_doi.Add(newRecord);
                }

                // Lưu các thay đổi vào cơ sở dữ liệu
                _entity.SaveChanges();
            }
        }
        private int? TryParseInt(object value)
        {
            if (int.TryParse(value?.ToString(), out int result))
            {
                return result;
            }
            return null;
        }*/

        
    }
}
