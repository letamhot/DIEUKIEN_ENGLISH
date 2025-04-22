namespace DK_Project.Management
{
    partial class ChinhDiemThuThach
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbxCauHoi = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDiem = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.dgvDiem = new System.Windows.Forms.DataGridView();
            this.lblDiemID = new System.Windows.Forms.Label();
            this.lblTenThiSinh = new System.Windows.Forms.Label();
            this.txtTenThiSinh = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiem)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Câu hỏi: ";
            // 
            // cbxCauHoi
            // 
            this.cbxCauHoi.FormattingEnabled = true;
            this.cbxCauHoi.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cbxCauHoi.Location = new System.Drawing.Point(141, 25);
            this.cbxCauHoi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxCauHoi.Name = "cbxCauHoi";
            this.cbxCauHoi.Size = new System.Drawing.Size(379, 24);
            this.cbxCauHoi.TabIndex = 2;
            this.cbxCauHoi.SelectedIndexChanged += new System.EventHandler(this.cbxCauHoi_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Điểm số: ";
            // 
            // txtDiem
            // 
            this.txtDiem.Location = new System.Drawing.Point(159, 69);
            this.txtDiem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDiem.Name = "txtDiem";
            this.txtDiem.Size = new System.Drawing.Size(237, 22);
            this.txtDiem.TabIndex = 3;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(59, 101);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(219, 34);
            this.btnUpdate.TabIndex = 7;
            this.btnUpdate.Text = "Cập nhật điểm";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // dgvDiem
            // 
            this.dgvDiem.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiem.Location = new System.Drawing.Point(16, 164);
            this.dgvDiem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvDiem.Name = "dgvDiem";
            this.dgvDiem.RowHeadersWidth = 51;
            this.dgvDiem.Size = new System.Drawing.Size(1035, 375);
            this.dgvDiem.TabIndex = 8;
            this.dgvDiem.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDiem_CellClick);
            // 
            // lblDiemID
            // 
            this.lblDiemID.AutoSize = true;
            this.lblDiemID.Location = new System.Drawing.Point(433, 73);
            this.lblDiemID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiemID.Name = "lblDiemID";
            this.lblDiemID.Size = new System.Drawing.Size(0, 16);
            this.lblDiemID.TabIndex = 4;
            // 
            // lblTenThiSinh
            // 
            this.lblTenThiSinh.AutoSize = true;
            this.lblTenThiSinh.Location = new System.Drawing.Point(543, 73);
            this.lblTenThiSinh.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTenThiSinh.Name = "lblTenThiSinh";
            this.lblTenThiSinh.Size = new System.Drawing.Size(80, 16);
            this.lblTenThiSinh.TabIndex = 5;
            this.lblTenThiSinh.Text = "Tên thí sinh: ";
            // 
            // txtTenThiSinh
            // 
            this.txtTenThiSinh.Enabled = false;
            this.txtTenThiSinh.Location = new System.Drawing.Point(644, 69);
            this.txtTenThiSinh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTenThiSinh.Name = "txtTenThiSinh";
            this.txtTenThiSinh.Size = new System.Drawing.Size(215, 22);
            this.txtTenThiSinh.TabIndex = 6;
            // 
            // ChinhDiemKP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.txtTenThiSinh);
            this.Controls.Add(this.lblTenThiSinh);
            this.Controls.Add(this.lblDiemID);
            this.Controls.Add(this.dgvDiem);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtDiem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbxCauHoi);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ChinhDiemKP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChinhDiemKP";
            this.Load += new System.EventHandler(this.ChinhDiemKP_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxCauHoi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDiem;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridView dgvDiem;
        private System.Windows.Forms.Label lblDiemID;
        private System.Windows.Forms.Label lblTenThiSinh;
        private System.Windows.Forms.TextBox txtTenThiSinh;
    }
}