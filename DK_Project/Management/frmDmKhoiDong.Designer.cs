namespace DK_Project.Management
{
    partial class frmDmKhoiDong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDmKhoiDong));
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.dgvKhoiDong = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtdapan = new System.Windows.Forms.RichTextBox();
            this.txtcauhoi = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxGoiCauHoi = new System.Windows.Forms.ComboBox();
            this.btnloadForm = new System.Windows.Forms.Button();
            this.cbxViTri = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhoiDong)).BeginInit();
            this.SuspendLayout();
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(640, 217);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 23);
            this.btnXoa.TabIndex = 10;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(559, 218);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(75, 23);
            this.btnSua.TabIndex = 11;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click_1);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(478, 218);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 23);
            this.btnThem.TabIndex = 12;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click_1);
            // 
            // dgvKhoiDong
            // 
            this.dgvKhoiDong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKhoiDong.Location = new System.Drawing.Point(0, 263);
            this.dgvKhoiDong.Name = "dgvKhoiDong";
            this.dgvKhoiDong.RowHeadersWidth = 51;
            this.dgvKhoiDong.Size = new System.Drawing.Size(1642, 466);
            this.dgvKhoiDong.TabIndex = 9;
            this.dgvKhoiDong.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvkhoidong_CellMouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(350, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Đáp án:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(346, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 24);
            this.label1.TabIndex = 6;
            this.label1.Text = "Câu hỏi:";
            // 
            // txtdapan
            // 
            this.txtdapan.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtdapan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtdapan.Location = new System.Drawing.Point(444, 85);
            this.txtdapan.Name = "txtdapan";
            this.txtdapan.Size = new System.Drawing.Size(575, 47);
            this.txtdapan.TabIndex = 8;
            this.txtdapan.Text = "";
            // 
            // txtcauhoi
            // 
            this.txtcauhoi.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtcauhoi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtcauhoi.Location = new System.Drawing.Point(444, 12);
            this.txtcauhoi.Name = "txtcauhoi";
            this.txtcauhoi.Size = new System.Drawing.Size(575, 44);
            this.txtcauhoi.TabIndex = 13;
            this.txtcauhoi.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(311, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 24);
            this.label3.TabIndex = 14;
            this.label3.Text = "Gói câu hỏi:";
            // 
            // cbxGoiCauHoi
            // 
            this.cbxGoiCauHoi.DisplayMember = "1";
            this.cbxGoiCauHoi.FormattingEnabled = true;
            this.cbxGoiCauHoi.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.cbxGoiCauHoi.Location = new System.Drawing.Point(444, 161);
            this.cbxGoiCauHoi.Name = "cbxGoiCauHoi";
            this.cbxGoiCauHoi.Size = new System.Drawing.Size(164, 21);
            this.cbxGoiCauHoi.TabIndex = 15;
            // 
            // btnloadForm
            // 
            this.btnloadForm.Location = new System.Drawing.Point(774, 218);
            this.btnloadForm.Name = "btnloadForm";
            this.btnloadForm.Size = new System.Drawing.Size(121, 23);
            this.btnloadForm.TabIndex = 38;
            this.btnloadForm.Text = "Load Khởi Động";
            this.btnloadForm.UseVisualStyleBackColor = true;
            this.btnloadForm.Click += new System.EventHandler(this.btnloadForm_Click);
            // 
            // cbxViTri
            // 
            this.cbxViTri.DisplayMember = "1";
            this.cbxViTri.FormattingEnabled = true;
            this.cbxViTri.Items.AddRange(new object[] {
            "",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cbxViTri.Location = new System.Drawing.Point(797, 161);
            this.cbxViTri.Name = "cbxViTri";
            this.cbxViTri.Size = new System.Drawing.Size(164, 21);
            this.cbxViTri.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(722, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 24);
            this.label4.TabIndex = 39;
            this.label4.Text = "Vị trí:";
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(951, 217);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(94, 23);
            this.btnUpload.TabIndex = 41;
            this.btnUpload.Text = "Import File Excel";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // frmDmKhoiDong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.cbxViTri);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnloadForm);
            this.Controls.Add(this.cbxGoiCauHoi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtcauhoi);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dgvKhoiDong);
            this.Controls.Add(this.txtdapan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDmKhoiDong";
            this.Text = "Danh Mục Khởi Động";
            this.Load += new System.EventHandler(this.frmKhoiDong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhoiDong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DataGridView dgvKhoiDong;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txtdapan;
        private System.Windows.Forms.RichTextBox txtcauhoi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxGoiCauHoi;
        private System.Windows.Forms.Button btnloadForm;
        private System.Windows.Forms.ComboBox cbxViTri;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnUpload;
    }
}