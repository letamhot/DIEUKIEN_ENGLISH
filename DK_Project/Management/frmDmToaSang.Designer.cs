namespace DK_Project.Management
{
    partial class frmDmToaSang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDmToaSang));
            this.label4 = new System.Windows.Forms.Label();
            this.cbxIsVideo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtcauhoi = new System.Windows.Forms.RichTextBox();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.dgvVeDich = new System.Windows.Forms.DataGridView();
            this.txtdapan = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cbxViTri = new System.Windows.Forms.ComboBox();
            this.labelViTri = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.RichTextBox();
            this.btnloadForm = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVeDich)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(255, 269);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 24);
            this.label4.TabIndex = 42;
            this.label4.Text = "Link File:";
            // 
            // cbxIsVideo
            // 
            this.cbxIsVideo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxIsVideo.DisplayMember = "1";
            this.cbxIsVideo.FormattingEnabled = true;
            this.cbxIsVideo.Items.AddRange(new object[] {
            "True",
            "False"});
            this.cbxIsVideo.Location = new System.Drawing.Point(363, 22);
            this.cbxIsVideo.Name = "cbxIsVideo";
            this.cbxIsVideo.Size = new System.Drawing.Size(164, 21);
            this.cbxIsVideo.TabIndex = 41;
            this.cbxIsVideo.SelectedIndexChanged += new System.EventHandler(this.cbxIsVideo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(275, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 24);
            this.label3.TabIndex = 40;
            this.label3.Text = "IsFile";
            // 
            // txtcauhoi
            // 
            this.txtcauhoi.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtcauhoi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtcauhoi.Location = new System.Drawing.Point(363, 70);
            this.txtcauhoi.Name = "txtcauhoi";
            this.txtcauhoi.Size = new System.Drawing.Size(721, 54);
            this.txtcauhoi.TabIndex = 39;
            this.txtcauhoi.Text = "";
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(573, 333);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 23);
            this.btnXoa.TabIndex = 36;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(492, 334);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(75, 23);
            this.btnSua.TabIndex = 37;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(411, 334);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 23);
            this.btnThem.TabIndex = 38;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // dgvVeDich
            // 
            this.dgvVeDich.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVeDich.Location = new System.Drawing.Point(0, 374);
            this.dgvVeDich.Name = "dgvVeDich";
            this.dgvVeDich.RowHeadersWidth = 51;
            this.dgvVeDich.Size = new System.Drawing.Size(1348, 352);
            this.dgvVeDich.TabIndex = 35;
            this.dgvVeDich.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVeDich_CellContentClick);
            // 
            // txtdapan
            // 
            this.txtdapan.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtdapan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtdapan.Location = new System.Drawing.Point(363, 143);
            this.txtdapan.Name = "txtdapan";
            this.txtdapan.Size = new System.Drawing.Size(721, 61);
            this.txtdapan.TabIndex = 34;
            this.txtdapan.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(268, 168);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 32;
            this.label2.Text = "Đáp án:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(265, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 24);
            this.label1.TabIndex = 33;
            this.label1.Text = "Câu hỏi:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // cbxViTri
            // 
            this.cbxViTri.DisplayMember = "1";
            this.cbxViTri.FormattingEnabled = true;
            this.cbxViTri.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.cbxViTri.Location = new System.Drawing.Point(363, 225);
            this.cbxViTri.Name = "cbxViTri";
            this.cbxViTri.Size = new System.Drawing.Size(164, 21);
            this.cbxViTri.TabIndex = 49;
            // 
            // labelViTri
            // 
            this.labelViTri.AutoSize = true;
            this.labelViTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelViTri.Location = new System.Drawing.Point(280, 225);
            this.labelViTri.Name = "labelViTri";
            this.labelViTri.Size = new System.Drawing.Size(58, 24);
            this.labelViTri.TabIndex = 48;
            this.labelViTri.Text = "Vị trí:";
            // 
            // lblFile
            // 
            this.lblFile.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblFile.Location = new System.Drawing.Point(363, 266);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(256, 34);
            this.lblFile.TabIndex = 50;
            this.lblFile.Text = "";
            // 
            // btnloadForm
            // 
            this.btnloadForm.Location = new System.Drawing.Point(708, 334);
            this.btnloadForm.Name = "btnloadForm";
            this.btnloadForm.Size = new System.Drawing.Size(121, 23);
            this.btnloadForm.TabIndex = 51;
            this.btnloadForm.Text = "Load Về Đích";
            this.btnloadForm.UseVisualStyleBackColor = true;
            this.btnloadForm.Click += new System.EventHandler(this.btnloadForm_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(874, 333);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(94, 23);
            this.btnUpload.TabIndex = 52;
            this.btnUpload.Text = "Import File Excel";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // frmDmToaSang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnloadForm);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.cbxViTri);
            this.Controls.Add(this.labelViTri);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbxIsVideo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtcauhoi);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dgvVeDich);
            this.Controls.Add(this.txtdapan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDmToaSang";
            this.Text = "Danh Mục Về Đích";
            this.Load += new System.EventHandler(this.frmVeDich_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVeDich)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxIsVideo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox txtcauhoi;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DataGridView dgvVeDich;
        private System.Windows.Forms.RichTextBox txtdapan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox cbxViTri;
        private System.Windows.Forms.Label labelViTri;
        private System.Windows.Forms.RichTextBox lblFile;
        private System.Windows.Forms.Button btnloadForm;
        private System.Windows.Forms.Button btnUpload;
    }
}