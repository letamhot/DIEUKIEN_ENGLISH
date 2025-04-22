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
    public partial class frmDmToaSang : Form
    {
        QuaMienDiSanEntities _entity = new QuaMienDiSanEntities();
        List<ds_goicauhoishining> _vd = new List<ds_goicauhoishining>();
        private int id_select = 0;
        string urlcauhoi = null;
        string directoryPath = Directory.GetCurrentDirectory();
        public frmDmToaSang()
        {
            InitializeComponent();
        }

        private void frmVeDich_Load(object sender, EventArgs e)
        {
            loadVeDich();
            
        }
        private void ResetData()
        {
            txtcauhoi.Text = "";
            txtdapan.Text = "";
            cbxIsVideo.Text = "";
            cbxViTri.Text = "";
            lblFile.Text = "";

        }
        private void loadVeDich()
        {
            ds_cuocthi cuocthihientai = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);

            _vd = _entity.ds_goicauhoishining.Where(x => x.cuocthiid == cuocthihientai.cuocthiid).ToList();
            dgvVeDich.DataSource = _vd;
        }

       /* private void btnUpload_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;

                string[] f = file.Split('\\');
                // to get the only file name
                string fn = f[(f.Length) - 1];
                string dest = directoryPath + "\\Resources\\" + fn;
                //to copy the file to the destination folder
                File.Copy(file, dest, true);
                urlcauhoi = fn;


            }
        }*/

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

                ds_goicauhoishining vd = new ds_goicauhoishining();
                if (bool.Parse(cbxIsVideo.Text) == true)
                {
                    vd.isvideo = bool.Parse(cbxIsVideo.Text);
                    vd.noidungcauhoi = txtcauhoi.Text;
                    vd.dapan = txtdapan.Text;
                    vd.cuocthiid = cuocthihientai.cuocthiid;
                    vd.urlhinhanh = lblFile.Text;
                    if (cbxViTri.Text == null || cbxViTri.Text == "")
                    {
                        vd.vitri = 0;
                    }
                    else
                    {
                        vd.vitri = int.Parse(cbxViTri.Text);

                    }
                    vd.trangThai = 0;
                    vd.sodiem = 30;
                    _entity.ds_goicauhoishining.Add(vd);
                    _entity.SaveChanges();
                    ResetData();
                    loadVeDich();
                }
                else
                {
                    vd.isvideo = bool.Parse(cbxIsVideo.Text);
                    vd.noidungcauhoi = txtcauhoi.Text;
                    vd.dapan = txtdapan.Text;
                    vd.cuocthiid = cuocthihientai.cuocthiid;
                    vd.sodiem = 30;
                    if (cbxViTri.Text == null || cbxViTri.Text == "0")
                    {
                        cbxViTri.Text = "0";
                    }
                    else
                    {
                        vd.vitri = int.Parse(cbxViTri.Text);

                    }
                    vd.trangThai = 0;
                    _entity.ds_goicauhoishining.Add(vd);
                    _entity.SaveChanges();
                    ResetData();
                    loadVeDich();
                }

            }
        }

        private void dgvVeDich_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_vd.Count > 0)
            {

                DataGridViewRow row = dgvVeDich.Rows[e.RowIndex];
                id_select = int.Parse(row.Cells[0].Value.ToString());
                txtcauhoi.Text = row.Cells[1].Value.ToString();
                txtdapan.Text = row.Cells[2].Value.ToString();
                if(row.Cells[8].Value.ToString() == null || row.Cells[8].Value.ToString() == "")
                {
                    cbxViTri.Text = row.Cells[8].Value.ToString();
                }
                else
                {
                    cbxViTri.Text = row.Cells[8].Value.ToString();

                }
                cbxIsVideo.Text = row.Cells[6].Value.ToString();
                if(row.Cells[7].Value != null)
                {
                    lblFile.Text = row.Cells[7].Value.ToString();

                }
                else
                {
                    lblFile.Text = "";
                }

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            ds_cuocthi cuocthihientai = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);
            if (id_select > 0)
            {

                ds_goicauhoishining vd = _entity.ds_goicauhoishining.Find(id_select);
                if (bool.Parse(cbxIsVideo.Text) == true)
                {
                    vd.isvideo = bool.Parse(cbxIsVideo.Text);
                    vd.noidungcauhoi = txtcauhoi.Text;
                    vd.dapan = txtdapan.Text;
                    vd.cuocthiid = cuocthihientai.cuocthiid;
                    vd.urlhinhanh = lblFile.Text;
                    vd.sodiem = 30;
                    if (cbxViTri.Text == null || cbxViTri.Text == "")
                    {
                        cbxViTri.Text = "0";
                    }
                    else
                    {
                        vd.vitri = int.Parse(cbxViTri.Text);

                    }
                    vd.trangThai = 0;
                    _entity.SaveChanges();
                    ResetData();
                    loadVeDich();
                }
                else
                {
                    vd.isvideo = bool.Parse(cbxIsVideo.Text);
                    vd.noidungcauhoi = txtcauhoi.Text;
                    vd.dapan = txtdapan.Text;
                    vd.cuocthiid = cuocthihientai.cuocthiid;
                    vd.sodiem = 30;
                    if (cbxViTri.Text == null || cbxViTri.Text == "")
                    {
                        cbxViTri.Text = "0";
                    }
                    else
                    {
                        vd.vitri = int.Parse(cbxViTri.Text);

                    }
                    vd.trangThai = 0;
                    _entity.SaveChanges();
                    ResetData();
                    loadVeDich();
                }

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ds_cuocthi cuocthihientai = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);
            if (id_select > 0)
            {

                ds_goicauhoishining vd = _entity.ds_goicauhoishining.Find(id_select);
                _entity.ds_goicauhoishining.Remove(vd);
                _entity.SaveChanges();
                loadVeDich();

            }
        }

        private void cbxIsVideo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bool.Parse(cbxIsVideo.Text) == true)
            {
                lblFile.Visible = true;
                label4.Visible = true;
            }
            else
            {
                lblFile.Visible = false;
                label4.Visible = false;
            }
        }

        private void btnloadForm_Click(object sender, EventArgs e)
        {
            if (id_select > 0)
            {
                id_select = 0;
            }
            loadVeDich();
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
                dgvVeDich.DataSource = dt;
                // Lưu dữ liệu vào bảng ds_goicauhoikhampha
                SaveDataToDatabase(dt);
                loadVeDich();
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
                    int? vitri = TryParseInt(row["vitri"]);
                    int? sodiem = TryParseInt(row["sodiem"]);
                    bool? isvideo = TryParseBool(row["isvideo"]);
                    bool? trangthai = TryParseBool(row["trangthai"]);

                    string urlhinhanh = null;

                    // Kiểm tra nếu cột 'urlhinhanh' tồn tại trong DataTable
                    if (dt.Columns.Contains("urlhinhanh"))
                    {
                        urlhinhanh = string.IsNullOrWhiteSpace(row["urlhinhanh"]?.ToString()) ? null : row["urlhinhanh"].ToString();
                    }

                    ds_goicauhoishining newRecord = new ds_goicauhoishining
                    {
                        //goicauhoiid = goicauhoiid ?? 0,
                        //noidungcauhoi = string.IsNullOrWhiteSpace(row["noidungcauhoi"]?.ToString()) ? null : row["noidungcauhoi"].ToString(),
                        //dapan = string.IsNullOrWhiteSpace(row["dapan"]?.ToString()) ? null : row["dapan"].ToString(),
                        //sodiem = sodiem ?? 0,
                        //isvideo = isvideo ?? false,
                        //urlhinhanh = urlhinhanh,
                        //cuocthiid = cuocthiid ?? 0,
                        //trangthai = trangthai ?? false,
                        //vitri = vitri ?? 0
                    };

                    // Thêm đối tượng mới vào context
                    _entity.ds_goicauhoishining.Add(newRecord);
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
        }

        private bool? TryParseBool(object value)
        {
            if (bool.TryParse(value?.ToString(), out bool result))
            {
                return result;
            }
            return null;
        }
    }
}
