namespace EXON.MONITOR.GUI
{
     partial class frmLichSuDangNhap
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMaThiSinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContestantName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTenMay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContestantShift = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoomTest = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContestSubject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.cMaThiSinh,
            this.ContestantName,
            this.cTime,
            this.cTenMay,
            this.ContestantShift,
            this.RoomTest,
            this.ContestSubject});
            this.dataGridView1.Location = new System.Drawing.Point(31, 106);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(1185, 397);
            this.dataGridView1.TabIndex = 0;
            // 
            // STT
            // 
            this.STT.DataPropertyName = "STT";
            this.STT.HeaderText = "STT";
            this.STT.MinimumWidth = 6;
            this.STT.Name = "STT";
            this.STT.Width = 50;
            // 
            // cMaThiSinh
            // 
            this.cMaThiSinh.DataPropertyName = "ContestantCode";
            this.cMaThiSinh.HeaderText = "Mã Thí Sinh";
            this.cMaThiSinh.MinimumWidth = 6;
            this.cMaThiSinh.Name = "cMaThiSinh";
            this.cMaThiSinh.Width = 150;
            // 
            // ContestantName
            // 
            this.ContestantName.DataPropertyName = "ContestantName";
            this.ContestantName.HeaderText = "Tên thí sinh";
            this.ContestantName.MinimumWidth = 6;
            this.ContestantName.Name = "ContestantName";
            this.ContestantName.Width = 125;
            // 
            // cTime
            // 
            this.cTime.DataPropertyName = "Time";
            this.cTime.HeaderText = "Thời gian";
            this.cTime.MinimumWidth = 6;
            this.cTime.Name = "cTime";
            this.cTime.Width = 200;
            // 
            // cTenMay
            // 
            this.cTenMay.DataPropertyName = "TenMay";
            this.cTenMay.HeaderText = "Tên Máy";
            this.cTenMay.MinimumWidth = 6;
            this.cTenMay.Name = "cTenMay";
            this.cTenMay.Width = 200;
            // 
            // ContestantShift
            // 
            this.ContestantShift.DataPropertyName = "ContestShift";
            this.ContestantShift.HeaderText = "Ca thi";
            this.ContestantShift.MinimumWidth = 6;
            this.ContestantShift.Name = "ContestantShift";
            this.ContestantShift.Width = 125;
            // 
            // RoomTest
            // 
            this.RoomTest.DataPropertyName = "RoomTest";
            this.RoomTest.HeaderText = "Phòng Thi";
            this.RoomTest.MinimumWidth = 6;
            this.RoomTest.Name = "RoomTest";
            this.RoomTest.Width = 125;
            // 
            // ContestSubject
            // 
            this.ContestSubject.DataPropertyName = "ContestSubject";
            this.ContestSubject.HeaderText = "Môn thi";
            this.ContestSubject.MinimumWidth = 6;
            this.ContestSubject.Name = "ContestSubject";
            this.ContestSubject.Width = 125;
            // 
            // frmLichSuDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1247, 564);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmLichSuDangNhap";
            this.Padding = new System.Windows.Forms.Padding(27, 74, 27, 25);
            this.Text = "Lịch Sử Đăng Nhập";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

          }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn cMaThiSinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContestantName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTenMay;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContestantShift;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoomTest;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContestSubject;
    }
}