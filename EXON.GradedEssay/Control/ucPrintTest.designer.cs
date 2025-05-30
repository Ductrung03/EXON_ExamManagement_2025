namespace EXON.GradedEssay.Control
{
    partial class ucPrintTest
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
               this.label1 = new System.Windows.Forms.Label();
               this.cbShift = new System.Windows.Forms.ComboBox();
               this.label2 = new System.Windows.Forms.Label();
               this.cbRoomTest = new System.Windows.Forms.ComboBox();
               this.label5 = new System.Windows.Forms.Label();
               this.cbSubject = new System.Windows.Forms.ComboBox();
               this.groupBox1 = new System.Windows.Forms.GroupBox();
               this.btnPrintResult = new System.Windows.Forms.Button();
               this.groupBox2 = new System.Windows.Forms.GroupBox();
               this.gvMain = new System.Windows.Forms.DataGridView();
               this.cTestNumberIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
               this.cMTS = new System.Windows.Forms.DataGridViewTextBoxColumn();
               this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
               this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
               this.col_TestID = new System.Windows.Forms.DataGridViewTextBoxColumn();
               this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
               this.cbtnXemBaiLam = new System.Windows.Forms.DataGridViewButtonColumn();
               this.groupBox3 = new System.Windows.Forms.GroupBox();
               this.label3 = new System.Windows.Forms.Label();
               this.cbStaff2 = new System.Windows.Forms.ComboBox();
               this.cbStaff1 = new System.Windows.Forms.ComboBox();
               this.label4 = new System.Windows.Forms.Label();
               this.label6 = new System.Windows.Forms.Label();
               this.txtDivisionShiftID = new System.Windows.Forms.TextBox();
               this.groupBox1.SuspendLayout();
               this.groupBox2.SuspendLayout();
               ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
               this.groupBox3.SuspendLayout();
               this.SuspendLayout();
               // 
               // label1
               // 
               this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.label1.AutoSize = true;
               this.label1.Location = new System.Drawing.Point(109, 26);
               this.label1.Name = "label1";
               this.label1.Size = new System.Drawing.Size(38, 13);
               this.label1.TabIndex = 13;
               this.label1.Text = "Ca Thi";
               this.label1.Click += new System.EventHandler(this.label1_Click);
               // 
               // cbShift
               // 
               this.cbShift.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.cbShift.FormattingEnabled = true;
               this.cbShift.Location = new System.Drawing.Point(197, 18);
               this.cbShift.Name = "cbShift";
               this.cbShift.Size = new System.Drawing.Size(292, 21);
               this.cbShift.TabIndex = 15;
               this.cbShift.SelectedIndexChanged += new System.EventHandler(this.cbShift_SelectedIndexChanged);
               this.cbShift.SelectedValueChanged += new System.EventHandler(this.cbShift_SelectedValueChanged);
               // 
               // label2
               // 
               this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.label2.AutoSize = true;
               this.label2.Location = new System.Drawing.Point(536, 21);
               this.label2.Name = "label2";
               this.label2.Size = new System.Drawing.Size(52, 13);
               this.label2.TabIndex = 16;
               this.label2.Text = "Phòng thi";
               this.label2.Click += new System.EventHandler(this.label2_Click);
               // 
               // cbRoomTest
               // 
               this.cbRoomTest.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.cbRoomTest.FormattingEnabled = true;
               this.cbRoomTest.Location = new System.Drawing.Point(636, 18);
               this.cbRoomTest.Name = "cbRoomTest";
               this.cbRoomTest.Size = new System.Drawing.Size(193, 21);
               this.cbRoomTest.TabIndex = 17;
               this.cbRoomTest.SelectedIndexChanged += new System.EventHandler(this.cbRoomTest_SelectedIndexChanged);
               this.cbRoomTest.SelectedValueChanged += new System.EventHandler(this.cbRoomTest_SelectedValueChanged);
               // 
               // label5
               // 
               this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.label5.AutoSize = true;
               this.label5.Location = new System.Drawing.Point(875, 21);
               this.label5.Name = "label5";
               this.label5.Size = new System.Drawing.Size(42, 13);
               this.label5.TabIndex = 81;
               this.label5.Text = "Môn thi";
               this.label5.Click += new System.EventHandler(this.label5_Click);
               // 
               // cbSubject
               // 
               this.cbSubject.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.cbSubject.FormattingEnabled = true;
               this.cbSubject.Location = new System.Drawing.Point(943, 18);
               this.cbSubject.Name = "cbSubject";
               this.cbSubject.Size = new System.Drawing.Size(309, 21);
               this.cbSubject.TabIndex = 82;
               this.cbSubject.SelectedIndexChanged += new System.EventHandler(this.cbSubject_SelectedIndexChanged);
               this.cbSubject.SelectedValueChanged += new System.EventHandler(this.cbSubject_SelectedValueChanged);
               // 
               // groupBox1
               // 
               this.groupBox1.Controls.Add(this.txtDivisionShiftID);
               this.groupBox1.Controls.Add(this.label6);
               this.groupBox1.Controls.Add(this.cbSubject);
               this.groupBox1.Controls.Add(this.label5);
               this.groupBox1.Controls.Add(this.cbStaff2);
               this.groupBox1.Controls.Add(this.label3);
               this.groupBox1.Controls.Add(this.cbStaff1);
               this.groupBox1.Controls.Add(this.label4);
               this.groupBox1.Controls.Add(this.cbRoomTest);
               this.groupBox1.Controls.Add(this.label2);
               this.groupBox1.Controls.Add(this.cbShift);
               this.groupBox1.Controls.Add(this.label1);
               this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
               this.groupBox1.Location = new System.Drawing.Point(0, 0);
               this.groupBox1.Name = "groupBox1";
               this.groupBox1.Size = new System.Drawing.Size(1360, 96);
               this.groupBox1.TabIndex = 3;
               this.groupBox1.TabStop = false;
               this.groupBox1.Text = "Chấm thi viết";
               // 
               // btnPrintResult
               // 
               this.btnPrintResult.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.btnPrintResult.Image = global::EXON.GradedEssay.Properties.Resources.print_65_445177;
               this.btnPrintResult.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
               this.btnPrintResult.Location = new System.Drawing.Point(636, 18);
               this.btnPrintResult.Margin = new System.Windows.Forms.Padding(2);
               this.btnPrintResult.Name = "btnPrintResult";
               this.btnPrintResult.Size = new System.Drawing.Size(125, 36);
               this.btnPrintResult.TabIndex = 28;
               this.btnPrintResult.Text = "In toàn bộ";
               this.btnPrintResult.UseVisualStyleBackColor = true;
               this.btnPrintResult.Click += new System.EventHandler(this.btnPrintResult_Click);
               // 
               // groupBox2
               // 
               this.groupBox2.Controls.Add(this.btnPrintResult);
               this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
               this.groupBox2.Location = new System.Drawing.Point(0, 368);
               this.groupBox2.Name = "groupBox2";
               this.groupBox2.Size = new System.Drawing.Size(1360, 71);
               this.groupBox2.TabIndex = 4;
               this.groupBox2.TabStop = false;
               this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
               // 
               // gvMain
               // 
               this.gvMain.AllowUserToAddRows = false;
               this.gvMain.AllowUserToDeleteRows = false;
               this.gvMain.AllowUserToOrderColumns = true;
               this.gvMain.BackgroundColor = System.Drawing.Color.White;
               this.gvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
               this.gvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cTestNumberIndex,
            this.cMTS,
            this.Column1,
            this.Column2,
            this.col_TestID,
            this.Column4,
            this.cbtnXemBaiLam});
               this.gvMain.Dock = System.Windows.Forms.DockStyle.Fill;
               this.gvMain.Location = new System.Drawing.Point(3, 16);
               this.gvMain.Name = "gvMain";
               this.gvMain.Size = new System.Drawing.Size(1354, 253);
               this.gvMain.TabIndex = 60;
               this.gvMain.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvMain_CellContentClick);
               // 
               // cTestNumberIndex
               // 
               this.cTestNumberIndex.DataPropertyName = "STT";
               this.cTestNumberIndex.HeaderText = "STT";
               this.cTestNumberIndex.Name = "cTestNumberIndex";
               this.cTestNumberIndex.ReadOnly = true;
               this.cTestNumberIndex.Width = 50;
               // 
               // cMTS
               // 
               this.cMTS.DataPropertyName = "ContestantCode";
               this.cMTS.HeaderText = "Mã Thí Sinh";
               this.cMTS.Name = "cMTS";
               this.cMTS.ReadOnly = true;
               this.cMTS.Width = 120;
               // 
               // Column1
               // 
               this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
               this.Column1.DataPropertyName = "FullName";
               this.Column1.HeaderText = "Họ Tên";
               this.Column1.Name = "Column1";
               this.Column1.ReadOnly = true;
               // 
               // Column2
               // 
               this.Column2.DataPropertyName = "DOB";
               this.Column2.HeaderText = "Ngày Sinh";
               this.Column2.Name = "Column2";
               this.Column2.ReadOnly = true;
               this.Column2.Width = 120;
               // 
               // col_TestID
               // 
               this.col_TestID.DataPropertyName = "TestID";
               this.col_TestID.HeaderText = "Mã Đề";
               this.col_TestID.Name = "col_TestID";
               // 
               // Column4
               // 
               this.Column4.DataPropertyName = "SubjectName";
               this.Column4.HeaderText = "Môn thi";
               this.Column4.Name = "Column4";
               // 
               // cbtnXemBaiLam
               // 
               this.cbtnXemBaiLam.DataPropertyName = "PrintAnswer";
               this.cbtnXemBaiLam.HeaderText = "Tác vụ";
               this.cbtnXemBaiLam.Name = "cbtnXemBaiLam";
               this.cbtnXemBaiLam.Resizable = System.Windows.Forms.DataGridViewTriState.True;
               this.cbtnXemBaiLam.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
               this.cbtnXemBaiLam.Width = 120;
               // 
               // groupBox3
               // 
               this.groupBox3.Controls.Add(this.gvMain);
               this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
               this.groupBox3.Location = new System.Drawing.Point(0, 96);
               this.groupBox3.Name = "groupBox3";
               this.groupBox3.Size = new System.Drawing.Size(1360, 272);
               this.groupBox3.TabIndex = 5;
               this.groupBox3.TabStop = false;
               this.groupBox3.Text = "Danh sách thí sinh";
               this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
               // 
               // label3
               // 
               this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.label3.AutoSize = true;
               this.label3.Location = new System.Drawing.Point(875, 65);
               this.label3.Name = "label3";
               this.label3.Size = new System.Drawing.Size(61, 13);
               this.label3.TabIndex = 79;
               this.label3.Text = "Giáo viên 2";
               this.label3.Click += new System.EventHandler(this.label3_Click);
               // 
               // cbStaff2
               // 
               this.cbStaff2.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.cbStaff2.FormattingEnabled = true;
               this.cbStaff2.Location = new System.Drawing.Point(942, 60);
               this.cbStaff2.Name = "cbStaff2";
               this.cbStaff2.Size = new System.Drawing.Size(193, 21);
               this.cbStaff2.TabIndex = 80;
               this.cbStaff2.SelectedIndexChanged += new System.EventHandler(this.cbStaff2_SelectedIndexChanged);
               // 
               // cbStaff1
               // 
               this.cbStaff1.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.cbStaff1.FormattingEnabled = true;
               this.cbStaff1.Location = new System.Drawing.Point(636, 60);
               this.cbStaff1.Name = "cbStaff1";
               this.cbStaff1.Size = new System.Drawing.Size(177, 21);
               this.cbStaff1.TabIndex = 78;
               this.cbStaff1.SelectedIndexChanged += new System.EventHandler(this.cbStaff1_SelectedIndexChanged);
               // 
               // label4
               // 
               this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.label4.AutoSize = true;
               this.label4.Location = new System.Drawing.Point(536, 68);
               this.label4.Name = "label4";
               this.label4.Size = new System.Drawing.Size(61, 13);
               this.label4.TabIndex = 77;
               this.label4.Text = "Giáo viên 1";
               this.label4.Click += new System.EventHandler(this.label4_Click);
               // 
               // label6
               // 
               this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.label6.AutoSize = true;
               this.label6.Location = new System.Drawing.Point(110, 70);
               this.label6.Name = "label6";
               this.label6.Size = new System.Drawing.Size(37, 13);
               this.label6.TabIndex = 85;
               this.label6.Text = "Mã ca";
               this.label6.Click += new System.EventHandler(this.label6_Click);
               // 
               // txtDivisionShiftID
               // 
               this.txtDivisionShiftID.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.txtDivisionShiftID.Enabled = false;
               this.txtDivisionShiftID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.txtDivisionShiftID.Location = new System.Drawing.Point(197, 60);
               this.txtDivisionShiftID.Name = "txtDivisionShiftID";
               this.txtDivisionShiftID.Size = new System.Drawing.Size(100, 23);
               this.txtDivisionShiftID.TabIndex = 86;
               this.txtDivisionShiftID.TextChanged += new System.EventHandler(this.txtDivisionShiftID_TextChanged);
               // 
               // ucPrintTest
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.Controls.Add(this.groupBox3);
               this.Controls.Add(this.groupBox2);
               this.Controls.Add(this.groupBox1);
               this.Name = "ucPrintTest";
               this.Size = new System.Drawing.Size(1360, 439);
               this.Load += new System.EventHandler(this.ucWritting_Load);
               this.groupBox1.ResumeLayout(false);
               this.groupBox1.PerformLayout();
               this.groupBox2.ResumeLayout(false);
               ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
               this.groupBox3.ResumeLayout(false);
               this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbShift;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbRoomTest;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbSubject;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPrintResult;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView gvMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTestNumberIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMTS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TestID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewButtonColumn cbtnXemBaiLam;
        private System.Windows.Forms.GroupBox groupBox3;
          private System.Windows.Forms.TextBox txtDivisionShiftID;
          private System.Windows.Forms.Label label6;
          private System.Windows.Forms.ComboBox cbStaff2;
          private System.Windows.Forms.Label label3;
          private System.Windows.Forms.ComboBox cbStaff1;
          private System.Windows.Forms.Label label4;
     }
}
