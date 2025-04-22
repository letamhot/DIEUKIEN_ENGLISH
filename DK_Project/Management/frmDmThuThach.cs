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
using System.IO;
using OfficeOpenXml;

namespace DK_Project.Management
{
    public partial class frmDmThuThach : Form
    {
        QuaMienDiSanEntities _entity = new QuaMienDiSanEntities();
        List<ds_cauhoithuthach> _kp = new List<ds_cauhoithuthach>();
        private int id_select = 0;

        public frmDmThuThach()
        {
            InitializeComponent();
        }

        private void frmKhamPha_Load(object sender, EventArgs e)
        {
            loadKhamPha();
        }
        private void ResetData()
        {
            txtcauhoi.Text = "";
            txtdapan.Text = "";
            lblDapAnCT.Text = "";
            cbxVitri.Text = "";

        }
        private void loadKhamPha()
        {
            ds_cuocthi cuocthihientai = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);

            _kp = _entity.ds_cauhoithuthach.Where(x => x.cuocthiid == cuocthihientai.cuocthiid).ToList();
            dgvKhamPha.DataSource = _kp;
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

                ds_cauhoithuthach kp = new ds_cauhoithuthach();
                kp.noidung = txtcauhoi.Text;
                kp.dapanABC = txtdapan.Text;
                kp.dapantext = lblDapAnCT.Text;
                if (cbxVitri.Text == null || cbxVitri.Text == "")
                {
                    kp.vitri = 0;
                }
                else
                {
                    kp.vitri = int.Parse(cbxVitri.Text);

                }
                kp.cuocthiid = cuocthihientai.cuocthiid;
                _entity.ds_cauhoithuthach.Add(kp);
                _entity.SaveChanges();
                ResetData();
                loadKhamPha();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                ds_cuocthi cuocthihientai = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);

                if (cuocthihientai == null)
                {
                    MessageBox.Show("Không tìm thấy cuộc thi đang diễn ra!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (id_select > 0)
                {
                    ds_cauhoithuthach kp = _entity.ds_cauhoithuthach.Find(id_select);
                    if (kp != null)
                    {
                        // Kiểm tra dữ liệu đầu vào tránh null hoặc lỗi kiểu dữ liệu
                        if (string.IsNullOrWhiteSpace(txtcauhoi.Text) || string.IsNullOrWhiteSpace(txtdapan.Text))
                        {
                            MessageBox.Show("Nội dung câu hỏi và đáp án không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        kp.noidung = txtcauhoi.Text.Trim();
                        kp.dapanABC = txtdapan.Text.Trim();
                        kp.dapantext = lblDapAnCT.Text.Trim();

                        if (int.TryParse(cbxVitri.Text, out int vitriValue))
                        {
                            kp.vitri = vitriValue;
                        }
                        else
                        {
                            kp.vitri = 0; // Giá trị mặc định nếu không hợp lệ
                        }

                        kp.cuocthiid = cuocthihientai.cuocthiid;

                        _entity.SaveChanges();
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ResetData();
                        loadKhamPha();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy câu hỏi cần sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                // Bắt lỗi và hiển thị chi tiết lỗi
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        MessageBox.Show($"Lỗi: {validationError.ErrorMessage}", "Lỗi Entity Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dgvKhamPha_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_kp.Count > 0)
            {
                DataGridViewRow row = dgvKhamPha.Rows[e.RowIndex];
                id_select = int.Parse(row.Cells[0].Value.ToString());
                txtcauhoi.Text = row.Cells[1].Value.ToString();
                txtdapan.Text = row.Cells[3].Value.ToString();
                lblDapAnCT.Text = row.Cells[2].Value.ToString();
                if (row.Cells[6].Value.ToString() == "" || row.Cells[6].Value.ToString() == null)
                {
                    cbxVitri.Text = "0";
                }
                else
                {
                    cbxVitri.Text = row.Cells[6].Value.ToString();

                }

            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            ds_cuocthi cuocthihientai = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);
            if (id_select > 0)
            {

                ds_cauhoithuthach kp = _entity.ds_cauhoithuthach.Find(id_select);
                _entity.ds_cauhoithuthach.Remove(kp);
                _entity.SaveChanges();
                loadKhamPha();

            }
        }

        private void btnloadForm_Click(object sender, EventArgs e)
        {
            if (id_select > 0)
            {
                id_select = 0;
            }
            loadKhamPha();
        }

        private void btnUpload_Click(object sender, EventArgs e)
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
                dgvKhamPha.DataSource = dt;
                // Lưu dữ liệu vào bảng ds_goicauhoikhampha
                SaveDataToDatabase(dt);
                loadKhamPha();
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
                    ds_cauhoithuthach newRecord = new ds_cauhoithuthach
                    {
                        // Ví dụ: Giả sử các cột đầu tiên là "Câu hỏi" và "Đáp án"
                        noidung = row["noidung"].ToString(),
                        dapantext = row["dapantext"].ToString(),
                        dapanABC = row["dapanABC"].ToString(),
                        ghichu = row["ghichu"].ToString(),
                        cuocthiid = int.Parse(row["cuocthiid"].ToString()),
                        vitri = int.Parse(row["vitri"].ToString()),
                    };

                    _entity.ds_cauhoithuthach.Add(newRecord);
                }

                _entity.SaveChanges(); // Lưu các thay đổi vào cơ sở dữ liệu
            }
        }
    }
}
