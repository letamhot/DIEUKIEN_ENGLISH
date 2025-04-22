namespace DK_Project.Management
{
    partial class frmDsDoi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDsDoi));
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.dgvDsDoi = new System.Windows.Forms.DataGridView();
            this.cbxVitri = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTenDoiChoi = new System.Windows.Forms.RichTextBox();
            this.txtNguoiChoi = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTendangnhap = new System.Windows.Forms.RichTextBox();
            this.txtMatKhau = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnloadForm = new System.Windows.Forms.Button();
            this.txtdcip = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDsDoi)).BeginInit();
            this.SuspendLayout();
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(554, 267);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 23);
            this.btnXoa.TabIndex = 51;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(473, 268);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(75, 23);
            this.btnSua.TabIndex = 52;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(392, 268);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 23);
            this.btnThem.TabIndex = 53;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // dgvDsDoi
            // 
            this.dgvDsDoi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDsDoi.Location = new System.Drawing.Point(1, 308);
            this.dgvDsDoi.Name = "dgvDsDoi";
            this.dgvDsDoi.Size = new System.Drawing.Size(1024, 271);
            this.dgvDsDoi.TabIndex = 50;
            this.dgvDsDoi.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDsDoi_CellContentClick);
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
            "4"});
            this.cbxVitri.Location = new System.Drawing.Point(208, 196);
            this.cbxVitri.Name = "cbxVitri";
            this.cbxVitri.Size = new System.Drawing.Size(164, 21);
            this.cbxVitri.TabIndex = 49;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(124, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 24);
            this.label5.TabIndex = 48;
            this.label5.Text = "Vị trí:";
            // 
            // txtTenDoiChoi
            // 
            this.txtTenDoiChoi.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtTenDoiChoi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTenDoiChoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenDoiChoi.Location = new System.Drawing.Point(208, 52);
            this.txtTenDoiChoi.Name = "txtTenDoiChoi";
            this.txtTenDoiChoi.Size = new System.Drawing.Size(310, 34);
            this.txtTenDoiChoi.TabIndex = 47;
            this.txtTenDoiChoi.Text = "";
            // 
            // txtNguoiChoi
            // 
            this.txtNguoiChoi.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtNguoiChoi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNguoiChoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNguoiChoi.Location = new System.Drawing.Point(208, 125);
            this.txtNguoiChoi.Name = "txtNguoiChoi";
            this.txtNguoiChoi.Size = new System.Drawing.Size(310, 36);
            this.txtNguoiChoi.TabIndex = 46;
            this.txtNguoiChoi.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 24);
            this.label2.TabIndex = 44;
            this.label2.Text = "Tên người chơi:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(47, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 24);
            this.label1.TabIndex = 45;
            this.label1.Text = "Tên đội chơi:";
            // 
            // txtTendangnhap
            // 
            this.txtTendangnhap.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtTendangnhap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTendangnhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTendangnhap.Location = new System.Drawing.Point(718, 52);
            this.txtTendangnhap.Name = "txtTendangnhap";
            this.txtTendangnhap.Size = new System.Drawing.Size(258, 34);
            this.txtTendangnhap.TabIndex = 57;
            this.txtTendangnhap.Text = "";
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtMatKhau.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMatKhau.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMatKhau.Location = new System.Drawing.Point(718, 125);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new System.Drawing.Size(258, 36);
            this.txtMatKhau.TabIndex = 56;
            this.txtMatKhau.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(591, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 24);
            this.label3.TabIndex = 54;
            this.label3.Text = "Mật khẩu:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(531, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 24);
            this.label4.TabIndex = 55;
            this.label4.Text = "Tên đăng nhập:";
            // 
            // btnloadForm
            // 
            this.btnloadForm.Location = new System.Drawing.Point(678, 267);
            this.btnloadForm.Name = "btnloadForm";
            this.btnloadForm.Size = new System.Drawing.Size(121, 23);
            this.btnloadForm.TabIndex = 58;
            this.btnloadForm.Text = "Load Danh Sách Đội";
            this.btnloadForm.UseVisualStyleBackColor = true;
            this.btnloadForm.Click += new System.EventHandler(this.btnloadForm_Click);
            // 
            // txtdcip
            // 
            this.txtdcip.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtdcip.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtdcip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdcip.Location = new System.Drawing.Point(718, 196);
            this.txtdcip.Name = "txtdcip";
            this.txtdcip.Size = new System.Drawing.Size(258, 36);
            this.txtdcip.TabIndex = 60;
            this.txtdcip.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(591, 196);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 24);
            this.label6.TabIndex = 59;
            this.label6.Text = "Địa chỉ IP:";
            // 
            // frmDsDoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 579);
            this.Controls.Add(this.txtdcip);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnloadForm);
            this.Controls.Add(this.txtTendangnhap);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dgvDsDoi);
            this.Controls.Add(this.cbxVitri);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtTenDoiChoi);
            this.Controls.Add(this.txtNguoiChoi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDsDoi";
            this.Text = "Danh sách đội";
            this.Load += new System.EventHandler(this.frmDsDoi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDsDoi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DataGridView dgvDsDoi;
        private System.Windows.Forms.ComboBox cbxVitri;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox txtTenDoiChoi;
        private System.Windows.Forms.RichTextBox txtNguoiChoi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtTendangnhap;
        private System.Windows.Forms.RichTextBox txtMatKhau;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnloadForm;
        private System.Windows.Forms.RichTextBox txtdcip;
        private System.Windows.Forms.Label label6;
    }
}