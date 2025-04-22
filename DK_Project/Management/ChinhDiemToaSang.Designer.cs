namespace DK_Project.Management
{
    partial class ChinhDiemToaSang
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
            this.lblDiemID = new System.Windows.Forms.Label();
            this.dgvDiem = new System.Windows.Forms.DataGridView();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtDiem = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTenThiSinh = new System.Windows.Forms.Label();
            this.txtTenThiSinh = new System.Windows.Forms.TextBox();
            this.cbbThiSinh = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiem)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDiemID
            // 
            this.lblDiemID.AutoSize = true;
            this.lblDiemID.Location = new System.Drawing.Point(330, 25);
            this.lblDiemID.Name = "lblDiemID";
            this.lblDiemID.Size = new System.Drawing.Size(0, 13);
            this.lblDiemID.TabIndex = 13;
            // 
            // dgvDiem
            // 
            this.dgvDiem.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiem.Location = new System.Drawing.Point(12, 129);
            this.dgvDiem.Name = "dgvDiem";
            this.dgvDiem.Size = new System.Drawing.Size(776, 305);
            this.dgvDiem.TabIndex = 17;
            this.dgvDiem.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDiem_CellClick);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(44, 78);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(164, 28);
            this.btnUpdate.TabIndex = 16;
            this.btnUpdate.Text = "Cập nhật điểm";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtDiem
            // 
            this.txtDiem.Location = new System.Drawing.Point(124, 22);
            this.txtDiem.Name = "txtDiem";
            this.txtDiem.Size = new System.Drawing.Size(179, 20);
            this.txtDiem.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Điểm số: ";
            // 
            // lblTenThiSinh
            // 
            this.lblTenThiSinh.AutoSize = true;
            this.lblTenThiSinh.Location = new System.Drawing.Point(390, 25);
            this.lblTenThiSinh.Name = "lblTenThiSinh";
            this.lblTenThiSinh.Size = new System.Drawing.Size(85, 13);
            this.lblTenThiSinh.TabIndex = 14;
            this.lblTenThiSinh.Text = "Tên thí sinh cũ: ";
            // 
            // txtTenThiSinh
            // 
            this.txtTenThiSinh.Enabled = false;
            this.txtTenThiSinh.Location = new System.Drawing.Point(488, 22);
            this.txtTenThiSinh.Name = "txtTenThiSinh";
            this.txtTenThiSinh.Size = new System.Drawing.Size(162, 20);
            this.txtTenThiSinh.TabIndex = 15;
            // 
            // cbbThiSinh
            // 
            this.cbbThiSinh.FormattingEnabled = true;
            this.cbbThiSinh.Location = new System.Drawing.Point(488, 48);
            this.cbbThiSinh.Name = "cbbThiSinh";
            this.cbbThiSinh.Size = new System.Drawing.Size(162, 21);
            this.cbbThiSinh.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(330, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Tên thí sinh chỉnh sửa điểm: ";
            // 
            // ChinhDiemVD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbbThiSinh);
            this.Controls.Add(this.txtTenThiSinh);
            this.Controls.Add(this.lblTenThiSinh);
            this.Controls.Add(this.lblDiemID);
            this.Controls.Add(this.dgvDiem);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtDiem);
            this.Controls.Add(this.label2);
            this.Name = "ChinhDiemVD";
            this.Text = "ChinhDiemVD";
            this.Load += new System.EventHandler(this.ChinhDiemVD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblDiemID;
        private System.Windows.Forms.DataGridView dgvDiem;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtDiem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTenThiSinh;
        private System.Windows.Forms.TextBox txtTenThiSinh;
        private System.Windows.Forms.ComboBox cbbThiSinh;
        private System.Windows.Forms.Label label1;
    }
}