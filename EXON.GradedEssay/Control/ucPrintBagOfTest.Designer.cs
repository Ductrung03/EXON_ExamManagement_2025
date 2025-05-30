namespace EXON.GradedEssay.Control
{
     partial class ucPrintBagOfTest
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
               this.cbSubject = new System.Windows.Forms.ComboBox();
               this.label5 = new System.Windows.Forms.Label();
               this.cbRoomTest = new System.Windows.Forms.ComboBox();
               this.label2 = new System.Windows.Forms.Label();
               this.label1 = new System.Windows.Forms.Label();
               this.groupBox3 = new System.Windows.Forms.GroupBox();
               this.gvMain = new System.Windows.Forms.DataGridView();
               this.btnPrintResult = new System.Windows.Forms.Button();
               this.groupBox2 = new System.Windows.Forms.GroupBox();
               this.cbShift = new System.Windows.Forms.ComboBox();
               this.groupBox1 = new System.Windows.Forms.GroupBox();
               this.cTestNumberIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
               this.cBagOfTestID = new System.Windows.Forms.DataGridViewTextBoxColumn();
               this.cTestID = new System.Windows.Forms.DataGridViewTextBoxColumn();
               this.cStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
               this.cbtnXemBaiLam = new System.Windows.Forms.DataGridViewButtonColumn();
               this.groupBox3.SuspendLayout();
               ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
               this.groupBox2.SuspendLayout();
               this.groupBox1.SuspendLayout();
               this.SuspendLayout();
               // 
               // cbSubject
               // 
               this.cbSubject.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.cbSubject.FormattingEnabled = true;
               this.cbSubject.Location = new System.Drawing.Point(943, 18);
               this.cbSubject.Name = "cbSubject";
               this.cbSubject.Size = new System.Drawing.Size(309, 21);
               this.cbSubject.TabIndex = 82;
               this.cbSubject.SelectedValueChanged += new System.EventHandler(this.cbSubject_SelectedValueChanged);
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
               // 
               // cbRoomTest
               // 
               this.cbRoomTest.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.cbRoomTest.FormattingEnabled = true;
               this.cbRoomTest.Location = new System.Drawing.Point(636, 18);
               this.cbRoomTest.Name = "cbRoomTest";
               this.cbRoomTest.Size = new System.Drawing.Size(193, 21);
               this.cbRoomTest.TabIndex = 17;
               this.cbRoomTest.SelectedValueChanged += new System.EventHandler(this.cbRoomTest_SelectedValueChanged);
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
               // 
               // groupBox3
               // 
               this.groupBox3.Controls.Add(this.gvMain);
               this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
               this.groupBox3.Location = new System.Drawing.Point(0, 96);
               this.groupBox3.Name = "groupBox3";
               this.groupBox3.Size = new System.Drawing.Size(1360, 272);
               this.groupBox3.TabIndex = 8;
               this.groupBox3.TabStop = false;
               this.groupBox3.Text = "Danh sách thí sinh";
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
            this.cBagOfTestID,
            this.cTestID,
            this.cStatus,
            this.cbtnXemBaiLam});
               this.gvMain.Dock = System.Windows.Forms.DockStyle.Fill;
               this.gvMain.Location = new System.Drawing.Point(3, 16);
               this.gvMain.Name = "gvMain";
               this.gvMain.Size = new System.Drawing.Size(1354, 253);
               this.gvMain.TabIndex = 60;
               this.gvMain.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvMain_CellContentClick);
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
               this.groupBox2.TabIndex = 7;
               this.groupBox2.TabStop = false;
               // 
               // cbShift
               // 
               this.cbShift.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.cbShift.FormattingEnabled = true;
               this.cbShift.Location = new System.Drawing.Point(197, 18);
               this.cbShift.Name = "cbShift";
               this.cbShift.Size = new System.Drawing.Size(292, 21);
               this.cbShift.TabIndex = 15;
               this.cbShift.SelectedValueChanged += new System.EventHandler(this.cbShift_SelectedValueChanged);
               // 
               // groupBox1
               // 
               this.groupBox1.Controls.Add(this.cbSubject);
               this.groupBox1.Controls.Add(this.label5);
               this.groupBox1.Controls.Add(this.cbRoomTest);
               this.groupBox1.Controls.Add(this.label2);
               this.groupBox1.Controls.Add(this.cbShift);
               this.groupBox1.Controls.Add(this.label1);
               this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
               this.groupBox1.Location = new System.Drawing.Point(0, 0);
               this.groupBox1.Name = "groupBox1";
               this.groupBox1.Size = new System.Drawing.Size(1360, 96);
               this.groupBox1.TabIndex = 6;
               this.groupBox1.TabStop = false;
               this.groupBox1.Text = "Chấm thi viết";
               // 
               // cTestNumberIndex
               // 
               this.cTestNumberIndex.DataPropertyName = "STT";
               this.cTestNumberIndex.HeaderText = "STT";
               this.cTestNumberIndex.Name = "cTestNumberIndex";
               this.cTestNumberIndex.ReadOnly = true;
               this.cTestNumberIndex.Width = 50;
               // 
               // cBagOfTestID
               // 
               this.cBagOfTestID.DataPropertyName = "BagOfTestID";
               this.cBagOfTestID.HeaderText = "Mã Túi Đề Thi";
               this.cBagOfTestID.Name = "cBagOfTestID";
               this.cBagOfTestID.ReadOnly = true;
               this.cBagOfTestID.Width = 320;
               // 
               // cTestID
               // 
               this.cTestID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
               this.cTestID.DataPropertyName = "TestID";
               this.cTestID.HeaderText = "Mã Đề";
               this.cTestID.Name = "cTestID";
               this.cTestID.ReadOnly = true;
               // 
               // cStatus
               // 
               this.cStatus.DataPropertyName = "Status";
               this.cStatus.HeaderText = "Status";
               this.cStatus.Name = "cStatus";
               this.cStatus.ReadOnly = true;
               this.cStatus.Width = 320;
               // 
               // cbtnXemBaiLam
               // 
               this.cbtnXemBaiLam.DataPropertyName = "PrintAnswer";
               this.cbtnXemBaiLam.HeaderText = "Tác vụ";
               this.cbtnXemBaiLam.Name = "cbtnXemBaiLam";
               this.cbtnXemBaiLam.Resizable = System.Windows.Forms.DataGridViewTriState.True;
               this.cbtnXemBaiLam.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
               this.cbtnXemBaiLam.Width = 220;
               // 
               // ucPrintBagOfTest
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.Controls.Add(this.groupBox3);
               this.Controls.Add(this.groupBox2);
               this.Controls.Add(this.groupBox1);
               this.Name = "ucPrintBagOfTest";
               this.Size = new System.Drawing.Size(1360, 439);
               this.Load += new System.EventHandler(this.ucPrintBagOfTest_Load);
               this.groupBox3.ResumeLayout(false);
               ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
               this.groupBox2.ResumeLayout(false);
               this.groupBox1.ResumeLayout(false);
               this.groupBox1.PerformLayout();
               this.ResumeLayout(false);

          }

        #endregion
        private System.Windows.Forms.ComboBox cbSubject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbRoomTest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView gvMain;
        private System.Windows.Forms.Button btnPrintResult;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbShift;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTestNumberIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBagOfTestID;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTestID;
        private System.Windows.Forms.DataGridViewTextBoxColumn cStatus;
        private System.Windows.Forms.DataGridViewButtonColumn cbtnXemBaiLam;
    }
}
