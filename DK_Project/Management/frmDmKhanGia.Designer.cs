namespace DK_Project.Management
{
    partial class frmDmKhanGia
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDmKhanGia));
            this.cbxVitri = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtcauhoi = new System.Windows.Forms.RichTextBox();
            this.txtdapan = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.dgvKhanGia = new System.Windows.Forms.DataGridView();
            this.btnloadForm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhanGia)).BeginInit();
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
            "6",
            "7",
            "8"});
            this.cbxVitri.Location = new System.Drawing.Point(449, 196);
            this.cbxVitri.Name = "cbxVitri";
            this.cbxVitri.Size = new System.Drawing.Size(164, 21);
            this.cbxVitri.TabIndex = 39;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(378, 196);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 24);
            this.label5.TabIndex = 38;
            this.label5.Text = "Vị trí:";
            // 
            // txtcauhoi
            // 
            this.txtcauhoi.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtcauhoi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtcauhoi.Location = new System.Drawing.Point(449, 55);
            this.txtcauhoi.Name = "txtcauhoi";
            this.txtcauhoi.Size = new System.Drawing.Size(498, 44);
            this.txtcauhoi.TabIndex = 35;
            this.txtcauhoi.Text = "";
            // 
            // txtdapan
            // 
            this.txtdapan.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtdapan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtdapan.Location = new System.Drawing.Point(449, 128);
            this.txtdapan.Name = "txtdapan";
            this.txtdapan.Size = new System.Drawing.Size(498, 47);
            this.txtdapan.TabIndex = 34;
            this.txtdapan.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(355, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 32;
            this.label2.Text = "Đáp án:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(351, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 24);
            this.label1.TabIndex = 33;
            this.label1.Text = "Câu hỏi:";
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(686, 258);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 23);
            this.btnXoa.TabIndex = 41;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(605, 259);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(75, 23);
            this.btnSua.TabIndex = 42;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(524, 259);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 23);
            this.btnThem.TabIndex = 43;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // dgvKhanGia
            // 
            this.dgvKhanGia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvKhanGia.Location = new System.Drawing.Point(1, 321);
            this.dgvKhanGia.Name = "dgvKhanGia";
            this.dgvKhanGia.Size = new System.Drawing.Size(1347, 407);
            this.dgvKhanGia.TabIndex = 40;
            this.dgvKhanGia.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKhanGia_CellContentClick);
            // 
            // btnloadForm
            // 
            this.btnloadForm.Location = new System.Drawing.Point(826, 258);
            this.btnloadForm.Name = "btnloadForm";
            this.btnloadForm.Size = new System.Drawing.Size(121, 23);
            this.btnloadForm.TabIndex = 52;
            this.btnloadForm.Text = "Load Khán Giả";
            this.btnloadForm.UseVisualStyleBackColor = true;
            this.btnloadForm.Click += new System.EventHandler(this.btnloadForm_Click);
            // 
            // frmDmKhanGia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.btnloadForm);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dgvKhanGia);
            this.Controls.Add(this.cbxVitri);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtcauhoi);
            this.Controls.Add(this.txtdapan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDmKhanGia";
            this.Text = "Danh Mục Khán Gỉả";
            this.Load += new System.EventHandler(this.frmKhanGia_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhanGia)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxVitri;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox txtcauhoi;
        private System.Windows.Forms.RichTextBox txtdapan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DataGridView dgvKhanGia;
        private System.Windows.Forms.Button btnloadForm;
    }
}