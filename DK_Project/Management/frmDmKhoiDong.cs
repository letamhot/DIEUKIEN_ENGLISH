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
    public partial class frmDmKhoiDong : Form
    {
        QuaMienDiSanEntities _entity = new QuaMienDiSanEntities();
        List<ds_goicauhoikhoidong> _kd = new List<ds_goicauhoikhoidong>();
        private int id_select = 0;

        public frmDmKhoiDong()
        {
            InitializeComponent();
        }

        private void frmKhoiDong_Load(object sender, EventArgs e)
        {
            if (id_select > 0)
            {
                id_select = 0;
            }
            loadKhoiDong();
        }
        private void loadKhoiDong()
        {
            ds_cuocthi cuocthihientai = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);

            _kd = _entity.ds_goicauhoikhoidong.Where(x => x.cuocthiid == cuocthihientai.cuocthiid).ToList();
            dgvKhoiDong.DataSource = _kd;
        }
        private void ResetData()
        {
            txtcauhoi.Text = "";
            txtdapan.Text = "";
            cbxGoiCauHoi.Text = "";
            cbxViTri.Text = "";

        }


        private void btnThem_Click_1(object sender, EventArgs e)
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
                ds_goicauhoikhoidong kd = new ds_goicauhoikhoidong();
                kd.noidungcauhoi = txtcauhoi.Text;
                kd.dapan = txtdapan.Text;
                kd.goicauhoiid = int.Parse(cbxGoiCauHoi.SelectedItem.ToString());
                if (cbxViTri.Text == null || cbxViTri.Text == "0")
                {
                    cbxViTri.Text = "0";
                }
                else
                {
                    kd.vitri = int.Parse(cbxViTri.Text);

                }
                kd.cuocthiid = cuocthihientai.cuocthiid;
                _entity.ds_goicauhoikhoidong.Add(kd);
                _entity.SaveChanges();
                ResetData();
                loadKhoiDong();
            }
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            ds_cuocthi cuocthihientai = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);

            if (id_select > 0)
            {
                ds_goicauhoikhoidong kd = _entity.ds_goicauhoikhoidong.Find(id_select);
                if (kd != null)
                {
                    kd.noidungcauhoi = txtcauhoi.Text;
                    kd.dapan = txtdapan.Text;
                    kd.goicauhoiid = int.Parse(cbxGoiCauHoi.SelectedItem.ToString());
                    if (cbxViTri.Text == null || cbxViTri.Text == "0")
                    {
                        cbxViTri.Text = "0";
                    }
                    else
                    {
                        kd.vitri = int.Parse(cbxViTri.Text);

                    }
                    kd.cuocthiid = cuocthihientai.cuocthiid;

                    _entity.SaveChanges();
                    ResetData();
                    loadKhoiDong();
                }
            }
        }

        private void dgvkhoidong_CellMouseClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_kd.Count > 0)
            {
                DataGridViewRow row = dgvKhoiDong.Rows[e.RowIndex];
                id_select = int.Parse(row.Cells[0].Value.ToString());
                cbxGoiCauHoi.Text = row.Cells[1].Value.ToString();
                txtcauhoi.Text = row.Cells[2].Value.ToString();
                txtdapan.Text = row.Cells[3].Value.ToString();
                if(row.Cells[5].Value.ToString() == "")
                {
                    cbxViTri.Text = "0";
                }
                else
                {
                    cbxViTri.Text = row.Cells[5].Value.ToString();
                }
                
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ds_cuocthi cuocthihientai = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);
            if (id_select > 0)
            {

                ds_goicauhoikhoidong kd = _entity.ds_goicauhoikhoidong.Find(id_select);
                _entity.ds_goicauhoikhoidong.Remove(kd);
                _entity.SaveChanges();
                loadKhoiDong();

            }
        }

        private void btnloadForm_Click(object sender, EventArgs e)
        {
            if(id_select > 0)
            {
                id_select = 0;
            }
            loadKhoiDong();
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
                dgvKhoiDong.DataSource = dt;
                // Lưu dữ liệu vào bảng ds_goicauhoikhampha
                SaveDataToDatabase(dt);
                loadKhoiDong();
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
                    int goicauhoiid, cuocthiid, vitri;

                    // Kiểm tra và chuyển đổi giá trị của goicauhoiid
                    if (!int.TryParse(row["goicauhoiid"].ToString(), out goicauhoiid))
                    {
                        // Nếu không thể chuyển đổi, bỏ qua hàng này hoặc gán giá trị mặc định
                        continue; // Bỏ qua hàng này và tiếp tục với hàng tiếp theo
                    }

                    // Kiểm tra và chuyển đổi giá trị của cuocthiid
                    if (!int.TryParse(row["cuocthiid"].ToString(), out cuocthiid))
                    {
                        continue; // Bỏ qua hàng này nếu không thể chuyển đổi
                    }

                    // Kiểm tra và chuyển đổi giá trị của vitri
                    if (!int.TryParse(row["vitri"].ToString(), out vitri))
                    {
                        continue; // Bỏ qua hàng này nếu không thể chuyển đổi
                    }

                    // Tạo đối tượng ds_goicauhoikhoidong mới
                    ds_goicauhoikhoidong newRecord = new ds_goicauhoikhoidong
                    {
                        goicauhoiid = goicauhoiid,
                        noidungcauhoi = row["noidungcauhoi"].ToString(),
                        dapan = row["dapan"].ToString(),
                        cuocthiid = cuocthiid,
                        vitri = vitri
                    };

                    // Thêm đối tượng mới vào context
                    _entity.ds_goicauhoikhoidong.Add(newRecord);
                }

                // Lưu các thay đổi vào cơ sở dữ liệu
                _entity.SaveChanges();
            }
        }
    }
}
