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

namespace DK_Project.Management
{
    public partial class frmDmKhamPhaCS : Form
    {
        QuaMienDiSanEntities _entity = new QuaMienDiSanEntities();
        List<ds_goicaudiscovery> _cp = new List<ds_goicaudiscovery>();
        Dictionary<int, string> teamplayings = new Dictionary<int, string>();

        private int id_select = 0;
        public frmDmKhamPhaCS()
        {
            InitializeComponent();
        }

        private void frmChinhPhuc_Load(object sender, EventArgs e)
        {
            loadChinhphuc();
        }
        private void loadChinhphuc()
        {
            ds_cuocthi cuocthihientai = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);

            _cp = _entity.ds_goicaudiscovery.Where(x => x.cuocthiid == cuocthihientai.cuocthiid).ToList();
            dgvChinhPhuc.DataSource = _cp;
            List<ds_goicaudiscovery> dscp = _entity.ds_goicaudiscovery.Where(x => x.cauhoichaid == null && x.cuocthiid == cuocthihientai.cuocthiid).ToList();
            if (dscp != null)
            {
                cbxIdCha.DataSource = dscp;
                cbxIdCha.DisplayMember = "cauhoiid";

            }
            var dsdoi = _entity.ds_doi.Where(x => x.vaitro == "TS" && x.cuocthiid == cuocthihientai.cuocthiid).ToList();
            for (int i = 0; i < dsdoi.Count; i++)
            {
                if (!teamplayings.ContainsKey(dsdoi[i].doiid))
                {
                    teamplayings.Add(dsdoi[i].doiid,
                        "ĐỘI " + dsdoi[i].vitridoi + " - " + dsdoi[i].tennguoichoi + " - " + dsdoi[i].tendoi);
                    cbxDoiThi.DataSource = new BindingSource(teamplayings, null);
                    cbxDoiThi.DisplayMember = "Value";
                    cbxDoiThi.ValueMember = "Key";
                }
                
            }


        }

        
        private void btnThem_Click(object sender, EventArgs e)
        {
            ds_cuocthi cuocthihientai = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);
            if (lblFileGoc.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập file!");
                lblFileGoc.Focus();
                return;
            }
            else
            {

                ds_goicaudiscovery cp = new ds_goicaudiscovery();
                if (cbxIdCha.Text != "" && cbxIdCha.Text != null)
                {
                    cp.cauhoichaid = Int32.Parse(cbxIdCha.Text);
                    cp.cuocthiid = cuocthihientai.cuocthiid;

                }
                else
                {
                    cp.cuocthiid = cuocthihientai.cuocthiid;

                    cp.cauhoichaid = null;
                }
                if (cbxDoiThi.Text != "" && cbxDoiThi.Text != null)
                {
                    cp.doithiid = int.Parse(cbxDoiThi.SelectedValue.ToString());

                }
                cp.chude = txtcauhoi.Text;
                if(lblFileGoc.Text != "" && lblFileGoc.Text != null)
                {
                    cp.noidungchude = lblFileGoc.Text;

                }
                else
                {
                    cp.noidungchude = "";
                }
                if (lblFileTS.Text != "" && lblFileTS.Text != null)
                {
                    cp.noidungthisinh = lblFileTS.Text;

                }
                else
                {
                    cp.noidungthisinh = "";
                }
                if (cbxVitri.Text != "" && cbxVitri.Text != null)
                {
                    cp.vitri = int.Parse(cbxVitri.Text);

                }
                else
                {
                    cp.vitri = 0;
                }
                if (cbxTrangThaiLat.Text != "" && cbxTrangThaiLat.Text != null)
                {
                    cp.trangthailatAnhPhu = int.Parse(cbxTrangThaiLat.Text);

                }
                else
                {
                    cp.trangthailatAnhPhu = 0;
                }

                if (cbxTrangthaiChinh.Text != "" && cbxTrangthaiChinh.Text != null)
                {
                    cp.trangthai = bool.Parse(cbxTrangthaiChinh.Text);

                }
                else
                {
                    cp.trangthai = null;
                }

                _entity.ds_goicaudiscovery.Add(cp);
                _entity.SaveChanges();
                cbxIdCha.Text = "";
                cbxTrangthaiChinh.Text = "";
                txtcauhoi.Text = "";
                cbxVitri.Text = "";
                lblFileGoc.Text = "";
                lblFileTS.Text = "";
                loadChinhphuc();

            }

        }

        private void dgvChinhPhuc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (_cp.Count > 0)
            {


                DataGridViewRow row = dgvChinhPhuc.Rows[e.RowIndex];

                id_select = int.Parse(row.Cells[0].Value.ToString());
                if (row.Cells[1].Value != null)
                {
                    int doiid;
                    if (int.TryParse(row.Cells[1].Value?.ToString(), out doiid))
                    {
                        var dsdoi = _entity.ds_doi
                            .AsEnumerable() // Chuyển sang xử lý trên bộ nhớ
                            .Where(x => x.vaitro == "TS" && x.doiid == doiid)
                            .ToList();
                        for (int i = 0; i < dsdoi.Count; i++)
                        {
                            if (!teamplayings.ContainsKey(dsdoi[i].doiid))
                            {
                                teamplayings.Add(dsdoi[i].doiid,
                                    "ĐỘI " + dsdoi[i].vitridoi + " - " + dsdoi[i].tennguoichoi + " - " + dsdoi[i].tendoi);
                                cbxDoiThi.DataSource = new BindingSource(teamplayings, null);
                                cbxDoiThi.DisplayMember = "Value";
                                cbxDoiThi.ValueMember = "Key";
                            }
                            

                        }
                        cbxDoiThi.SelectedValue = doiid;

                    }
                }
                else
                {
                    cbxDoiThi.Text = "";
                }
                txtcauhoi.Text = row.Cells[2].Value.ToString();
                if (row.Cells[3].Value != null)
                {
                    lblFileGoc.Text = row.Cells[3].Value.ToString();
                }
                else
                {
                    lblFileGoc.Text = "";
                }
                if (row.Cells[4].Value != null)
                {
                    lblFileTS.Text = row.Cells[4].Value.ToString();
                }
                else
                {
                    lblFileTS.Text = "";
                }
                if (row.Cells[5].Value != null)
                {
                    cbxVitri.Text = row.Cells[5].Value.ToString();
                }
                else
                {
                    cbxVitri.Text = "";
                }
                if (row.Cells[6].Value != null)
                {
                    cbxIdCha.Text = row.Cells[6].Value.ToString();
                }
                else
                {
                    cbxIdCha.Text = "";
                }
                
                
                if (row.Cells[8].Value != null)
                {
                    cbxTrangthaiChinh.Text = row.Cells[8].Value.ToString();
                }
                else
                {
                    cbxTrangthaiChinh.Text = "";
                }
                if (row.Cells[9].Value != null)
                {
                    cbxTrangThaiLat.Text = row.Cells[9].Value.ToString();
                }
                else
                {
                    cbxTrangThaiLat.Text = "";
                }


            }


        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            ds_cuocthi cuocthihientai = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);
            if (id_select > 0)
            {

                ds_goicaudiscovery cp = _entity.ds_goicaudiscovery.Find(id_select);

                if (cbxIdCha.Text != "" && cbxIdCha.Text != null)
                {
                    cp.cauhoichaid = Int32.Parse(cbxIdCha.Text);
                    cp.cuocthiid = cuocthihientai.cuocthiid;

                }
                else
                {
                    cp.cuocthiid = cuocthihientai.cuocthiid;

                    cp.cauhoichaid = null;
                }
                if (cbxDoiThi.Text != "" && cbxDoiThi.Text != null)
                {
                    cp.doithiid = int.Parse(cbxDoiThi.SelectedValue.ToString());

                }
                cp.chude = txtcauhoi.Text;
                if (lblFileGoc.Text != "" && lblFileGoc.Text != null)
                {
                    cp.noidungchude = lblFileGoc.Text;

                }
                else
                {
                    cp.noidungchude = "";
                }
                if (lblFileTS.Text != "" && lblFileTS.Text != null)
                {
                    cp.noidungthisinh = lblFileTS.Text;

                }
                else
                {
                    cp.noidungthisinh = "";
                }
                if (cbxVitri.Text != "" && cbxVitri.Text != null)
                {
                    cp.vitri = int.Parse(cbxVitri.Text);

                }
                else
                {
                    cp.vitri = 0;
                }
                if (cbxTrangThaiLat.Text != "" && cbxTrangThaiLat.Text != null)
                {
                    cp.trangthailatAnhPhu = int.Parse(cbxTrangThaiLat.Text);

                }
                else
                {
                    cp.trangthailatAnhPhu = 0;
                }

                if (cbxTrangthaiChinh.Text != "" && cbxTrangthaiChinh.Text != null)
                {
                    cp.trangthai = bool.Parse(cbxTrangthaiChinh.Text);

                }
                else
                {
                    cp.trangthai = null;
                }

                _entity.SaveChanges();
                cbxIdCha.Text = "";
                cbxDoiThi.Text = "";
                cbxTrangthaiChinh.Text = "";
                txtcauhoi.Text = "";
                cbxVitri.Text = "";
                lblFileGoc.Text = "";
                lblFileTS.Text = "";
                loadChinhphuc();


            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ds_cuocthi cuocthihientai = _entity.ds_cuocthi.FirstOrDefault(x => x.trangthai == true);
            if (id_select > 0)
            {

                ds_goicaudiscovery cp = _entity.ds_goicaudiscovery.Find(id_select);
                _entity.ds_goicaudiscovery.Remove(cp);
                _entity.SaveChanges();
                loadChinhphuc();

            }
        }

        private void cbxIdCha_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxIdCha.SelectedValue == null)
            {
                label6.Visible = false;
                cbxTrangthaiChinh.Visible = false;
            }
            else
            {
                label6.Visible = true;
                cbxTrangthaiChinh.Visible = true;
            }
        }

        private void btnloadForm_Click(object sender, EventArgs e)
        {
            if (id_select > 0)
            {
                id_select = 0;
            }
            loadChinhphuc();
        }

    }
}
