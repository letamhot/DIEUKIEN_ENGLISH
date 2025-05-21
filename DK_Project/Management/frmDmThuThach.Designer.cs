namespace DK_Project.Management
{
    partial class frmDmThuThach
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDmThuThach));
            this.cbxVitri = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtcauhoi = new System.Windows.Forms.RichTextBox();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.dgvKhamPha = new System.Windows.Forms.DataGridView();
            this.txtdapan = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnloadForm = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.lblDapAnCT = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhamPha)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxVitri
            // 
            this.cbxVitri.DisplayMember = "1";
            this.cbxVitri.FormattingEnabled = true;
            this.cbxVitri.Items.AddRange(new object[] {
            "",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.cbxVitri.Location = new System.Drawing.Point(327, 285);
            this.cbxVitri.Name = "cbxVitri";
            this.cbxVitri.Size = new System.Drawing.Size(164, 21);
            this.cbxVitri.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(257, 284);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 24);
            this.label3.TabIndex = 24;
            this.label3.Text = "Vị trí:";
            // 
            // txtcauhoi
            // 
            this.txtcauhoi.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtcauhoi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtcauhoi.Location = new System.Drawing.Point(327, 12);
            this.txtcauhoi.Name = "txtcauhoi";
            this.txtcauhoi.Size = new System.Drawing.Size(695, 98);
            this.txtcauhoi.TabIndex = 23;
            this.txtcauhoi.Text = "";
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(602, 327);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 23);
            this.btnXoa.TabIndex = 20;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click_1);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(521, 328);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(75, 23);
            this.btnSua.TabIndex = 21;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(440, 328);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 23);
            this.btnThem.TabIndex = 22;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // dgvKhamPha
            // 
            this.dgvKhamPha.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKhamPha.Location = new System.Drawing.Point(2, 368);
            this.dgvKhamPha.Name = "dgvKhamPha";
            this.dgvKhamPha.RowHeadersWidth = 51;
            this.dgvKhamPha.Size = new System.Drawing.Size(1346, 360);
            this.dgvKhamPha.TabIndex = 19;
            this.dgvKhamPha.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKhamPha_CellContentClick);
            // 
            // txtdapan
            // 
            this.txtdapan.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtdapan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtdapan.Location = new System.Drawing.Point(327, 126);
            this.txtdapan.Name = "txtdapan";
            this.txtdapan.Size = new System.Drawing.Size(695, 36);
            this.txtdapan.TabIndex = 18;
            this.txtdapan.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(245, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 16;
            this.label2.Text = "Đáp án:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(239, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 24);
            this.label1.TabIndex = 17;
            this.label1.Text = "Câu hỏi:";
            // 
            // btnloadForm
            // 
            this.btnloadForm.Location = new System.Drawing.Point(716, 328);
            this.btnloadForm.Name = "btnloadForm";
            this.btnloadForm.Size = new System.Drawing.Size(94, 23);
            this.btnloadForm.TabIndex = 29;
            this.btnloadForm.Text = "Load Khám Phá";
            this.btnloadForm.UseVisualStyleBackColor = true;
            this.btnloadForm.Click += new System.EventHandler(this.btnloadForm_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(838, 328);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(94, 23);
            this.btnUpload.TabIndex = 29;
            this.btnUpload.Text = "Import File Excel";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // lblDapAnCT
            // 
            this.lblDapAnCT.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblDapAnCT.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblDapAnCT.Location = new System.Drawing.Point(327, 177);
            this.lblDapAnCT.Name = "lblDapAnCT";
            this.lblDapAnCT.Size = new System.Drawing.Size(695, 83);
            this.lblDapAnCT.TabIndex = 31;
            this.lblDapAnCT.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(178, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 24);
            this.label4.TabIndex = 30;
            this.label4.Text = "Đáp án chi tiết:";
            // 
            // frmDmThuThach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.lblDapAnCT);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnloadForm);
            this.Controls.Add(this.cbxVitri);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtcauhoi);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dgvKhamPha);
            this.Controls.Add(this.txtdapan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDmThuThach";
            this.Text = "Danh Mục Thử thách";
            this.Load += new System.EventHandler(this.frmKhamPha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhamPha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxVitri;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox txtcauhoi;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DataGridView dgvKhamPha;
        private System.Windows.Forms.RichTextBox txtdapan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnloadForm;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.RichTextBox lblDapAnCT;
        private System.Windows.Forms.Label label4;
    }
}