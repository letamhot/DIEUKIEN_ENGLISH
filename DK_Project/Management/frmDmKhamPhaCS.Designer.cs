namespace DK_Project.Management
{
    partial class frmDmKhamPhaCS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDmKhamPhaCS));
            this.cbxIdCha = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtcauhoi = new System.Windows.Forms.RichTextBox();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.dgvChinhPhuc = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxVitri = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cbxTrangthaiChinh = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblFileGoc = new System.Windows.Forms.RichTextBox();
            this.btnloadForm = new System.Windows.Forms.Button();
            this.lblFileTS = new System.Windows.Forms.RichTextBox();
            this.labelThisinh = new System.Windows.Forms.Label();
            this.cbxDoiThi = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxTrangThaiLat = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChinhPhuc)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxIdCha
            // 
            this.cbxIdCha.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxIdCha.DisplayMember = "1";
            this.cbxIdCha.FormattingEnabled = true;
            this.cbxIdCha.Location = new System.Drawing.Point(392, 31);
            this.cbxIdCha.Name = "cbxIdCha";
            this.cbxIdCha.Size = new System.Drawing.Size(164, 21);
            this.cbxIdCha.TabIndex = 25;
            this.cbxIdCha.SelectedIndexChanged += new System.EventHandler(this.cbxIdCha_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(303, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 24);
            this.label3.TabIndex = 24;
            this.label3.Text = "ID cha:";
            // 
            // txtcauhoi
            // 
            this.txtcauhoi.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtcauhoi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtcauhoi.Location = new System.Drawing.Point(392, 79);
            this.txtcauhoi.Name = "txtcauhoi";
            this.txtcauhoi.Size = new System.Drawing.Size(574, 44);
            this.txtcauhoi.TabIndex = 23;
            this.txtcauhoi.Text = "";
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(574, 340);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(75, 23);
            this.btnXoa.TabIndex = 20;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(494, 341);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(75, 23);
            this.btnSua.TabIndex = 21;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(412, 341);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(75, 23);
            this.btnThem.TabIndex = 22;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // dgvChinhPhuc
            // 
            this.dgvChinhPhuc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChinhPhuc.Location = new System.Drawing.Point(2, 381);
            this.dgvChinhPhuc.Name = "dgvChinhPhuc";
            this.dgvChinhPhuc.RowHeadersWidth = 62;
            this.dgvChinhPhuc.Size = new System.Drawing.Size(1348, 349);
            this.dgvChinhPhuc.TabIndex = 19;
            this.dgvChinhPhuc.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChinhPhuc_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(295, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 24);
            this.label1.TabIndex = 17;
            this.label1.Text = "Chủ đề:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(619, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 24);
            this.label4.TabIndex = 28;
            this.label4.Text = "Link File gốc:";
            // 
            // cbxVitri
            // 
            this.cbxVitri.DisplayMember = "1";
            this.cbxVitri.FormattingEnabled = true;
            this.cbxVitri.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cbxVitri.Location = new System.Drawing.Point(392, 220);
            this.cbxVitri.Name = "cbxVitri";
            this.cbxVitri.Size = new System.Drawing.Size(164, 21);
            this.cbxVitri.TabIndex = 31;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(321, 220);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 24);
            this.label5.TabIndex = 30;
            this.label5.Text = "Vị trí:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // cbxTrangthaiChinh
            // 
            this.cbxTrangthaiChinh.DisplayMember = "1";
            this.cbxTrangthaiChinh.FormattingEnabled = true;
            this.cbxTrangthaiChinh.Items.AddRange(new object[] {
            "",
            "true",
            "false"});
            this.cbxTrangthaiChinh.Location = new System.Drawing.Point(847, 31);
            this.cbxTrangthaiChinh.Name = "cbxTrangthaiChinh";
            this.cbxTrangthaiChinh.Size = new System.Drawing.Size(164, 21);
            this.cbxTrangthaiChinh.TabIndex = 33;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(627, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(222, 24);
            this.label6.TabIndex = 32;
            this.label6.Text = "Trạng thái câu chủ đề:";
            // 
            // lblFileGoc
            // 
            this.lblFileGoc.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblFileGoc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblFileGoc.Location = new System.Drawing.Point(752, 219);
            this.lblFileGoc.Name = "lblFileGoc";
            this.lblFileGoc.Size = new System.Drawing.Size(222, 24);
            this.lblFileGoc.TabIndex = 34;
            this.lblFileGoc.Text = "";
            // 
            // btnloadForm
            // 
            this.btnloadForm.Location = new System.Drawing.Point(701, 340);
            this.btnloadForm.Name = "btnloadForm";
            this.btnloadForm.Size = new System.Drawing.Size(122, 23);
            this.btnloadForm.TabIndex = 37;
            this.btnloadForm.Text = "Load Chinh Phục";
            this.btnloadForm.UseVisualStyleBackColor = true;
            this.btnloadForm.Click += new System.EventHandler(this.btnloadForm_Click);
            // 
            // lblFileTS
            // 
            this.lblFileTS.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblFileTS.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblFileTS.Location = new System.Drawing.Point(467, 272);
            this.lblFileTS.Name = "lblFileTS";
            this.lblFileTS.Size = new System.Drawing.Size(222, 24);
            this.lblFileTS.TabIndex = 39;
            this.lblFileTS.Text = "";
            // 
            // labelThisinh
            // 
            this.labelThisinh.AutoSize = true;
            this.labelThisinh.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelThisinh.Location = new System.Drawing.Point(303, 272);
            this.labelThisinh.Name = "labelThisinh";
            this.labelThisinh.Size = new System.Drawing.Size(168, 24);
            this.labelThisinh.TabIndex = 38;
            this.labelThisinh.Text = "Link File thí sinh:";
            // 
            // cbxDoiThi
            // 
            this.cbxDoiThi.DisplayMember = "1";
            this.cbxDoiThi.FormattingEnabled = true;
            this.cbxDoiThi.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cbxDoiThi.Location = new System.Drawing.Point(392, 154);
            this.cbxDoiThi.Name = "cbxDoiThi";
            this.cbxDoiThi.Size = new System.Drawing.Size(164, 21);
            this.cbxDoiThi.TabIndex = 41;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(295, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 24);
            this.label2.TabIndex = 40;
            this.label2.Text = "Đội thi: ";
            // 
            // cbxTrangThaiLat
            // 
            this.cbxTrangThaiLat.DisplayMember = "1";
            this.cbxTrangThaiLat.FormattingEnabled = true;
            this.cbxTrangThaiLat.Items.AddRange(new object[] {
            "0",
            "1"});
            this.cbxTrangThaiLat.Location = new System.Drawing.Point(829, 154);
            this.cbxTrangThaiLat.Name = "cbxTrangThaiLat";
            this.cbxTrangThaiLat.Size = new System.Drawing.Size(164, 21);
            this.cbxTrangThaiLat.TabIndex = 43;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(589, 154);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(234, 24);
            this.label7.TabIndex = 42;
            this.label7.Text = "Trạng thái lật các mảnh:";
            // 
            // frmDmKhamPhaCS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.cbxTrangThaiLat);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbxDoiThi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblFileTS);
            this.Controls.Add(this.labelThisinh);
            this.Controls.Add(this.btnloadForm);
            this.Controls.Add(this.lblFileGoc);
            this.Controls.Add(this.cbxTrangthaiChinh);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbxVitri);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbxIdCha);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtcauhoi);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dgvChinhPhuc);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDmKhamPhaCS";
            this.Text = "Danh Mục Khám phá Chia sẻ";
            this.Load += new System.EventHandler(this.frmChinhPhuc_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChinhPhuc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxIdCha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox txtcauhoi;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DataGridView dgvChinhPhuc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxVitri;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox cbxTrangthaiChinh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox lblFileGoc;
        private System.Windows.Forms.Button btnloadForm;
        private System.Windows.Forms.RichTextBox lblFileTS;
        private System.Windows.Forms.Label labelThisinh;
        private System.Windows.Forms.ComboBox cbxDoiThi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxTrangThaiLat;
        private System.Windows.Forms.Label label7;
    }
}