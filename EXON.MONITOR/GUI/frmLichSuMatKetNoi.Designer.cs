namespace EXON.MONITOR.GUI
{
     partial class frmLichSuMatKetNoi
     {
          private System.ComponentModel.IContainer components = null;

          protected override void Dispose(bool disposing)
          {
               if (disposing && (components != null))
               {
                    components.Dispose();
               }
               base.Dispose(disposing);
          }

          #region Windows Form Designer generated code

          private void InitializeComponent()
          {
                this.pnlAction = new System.Windows.Forms.Panel();
                this.btnXuatFile = new System.Windows.Forms.Button();
                this.btnInBaoCao = new System.Windows.Forms.Button();
                this.dataGridView1 = new System.Windows.Forms.DataGridView();
                this.colSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.colContestantCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.colContestantName = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.colComputerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
               this.colRoomTestName = new System.Windows.Forms.DataGridViewTextBoxColumn();
               this.colDetectionSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.colServerTimeText = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.colLastResponseTimeText = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.colNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
                this.pnlAction.SuspendLayout();
                ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
                this.SuspendLayout();
                // 
                // pnlAction
                // 
                this.pnlAction.Controls.Add(this.btnXuatFile);
                this.pnlAction.Controls.Add(this.btnInBaoCao);
                this.pnlAction.Dock = System.Windows.Forms.DockStyle.Top;
                this.pnlAction.Location = new System.Drawing.Point(20, 60);
                this.pnlAction.Name = "pnlAction";
                this.pnlAction.Size = new System.Drawing.Size(1160, 45);
                this.pnlAction.TabIndex = 0;
                // 
                // btnXuatFile
                // 
                this.btnXuatFile.Location = new System.Drawing.Point(129, 10);
                this.btnXuatFile.Name = "btnXuatFile";
                this.btnXuatFile.Size = new System.Drawing.Size(109, 27);
                this.btnXuatFile.TabIndex = 1;
                this.btnXuatFile.Text = "Xuất CSV";
                this.btnXuatFile.UseVisualStyleBackColor = true;
                this.btnXuatFile.Click += new System.EventHandler(this.btnXuatFile_Click);
                // 
                // btnInBaoCao
                // 
                this.btnInBaoCao.Location = new System.Drawing.Point(14, 10);
                this.btnInBaoCao.Name = "btnInBaoCao";
                this.btnInBaoCao.Size = new System.Drawing.Size(109, 27);
                this.btnInBaoCao.TabIndex = 0;
                this.btnInBaoCao.Text = "In báo cáo";
                this.btnInBaoCao.UseVisualStyleBackColor = true;
                this.btnInBaoCao.Click += new System.EventHandler(this.btnInBaoCao_Click);
                // 
                // dataGridView1
                // 
                this.dataGridView1.AllowUserToAddRows = false;
                this.dataGridView1.AllowUserToDeleteRows = false;
               this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
               this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSTT,
            this.colContestantCode,
            this.colContestantName,
            this.colComputerName,
            this.colRoomTestName,
             this.colDetectionSource,
             this.colServerTimeText,
             this.colLastResponseTimeText,
             this.colNote});
                this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.dataGridView1.Location = new System.Drawing.Point(20, 105);
                this.dataGridView1.Name = "dataGridView1";
                this.dataGridView1.ReadOnly = true;
                this.dataGridView1.RowHeadersWidth = 51;
                this.dataGridView1.Size = new System.Drawing.Size(1160, 455);
                this.dataGridView1.TabIndex = 1;
               // 
               // colSTT
               // 
               this.colSTT.DataPropertyName = "STT";
               this.colSTT.HeaderText = "STT";
               this.colSTT.MinimumWidth = 6;
               this.colSTT.Name = "colSTT";
               this.colSTT.ReadOnly = true;
               // 
               // colContestantCode
               // 
               this.colContestantCode.DataPropertyName = "ContestantCode";
               this.colContestantCode.HeaderText = "Số báo danh";
               this.colContestantCode.MinimumWidth = 6;
               this.colContestantCode.Name = "colContestantCode";
               this.colContestantCode.ReadOnly = true;
               // 
               // colContestantName
               // 
               this.colContestantName.DataPropertyName = "ContestantName";
               this.colContestantName.HeaderText = "Thí sinh";
               this.colContestantName.MinimumWidth = 6;
               this.colContestantName.Name = "colContestantName";
               this.colContestantName.ReadOnly = true;
               // 
               // colComputerName
               // 
               this.colComputerName.DataPropertyName = "ComputerName";
               this.colComputerName.HeaderText = "Máy thi";
               this.colComputerName.MinimumWidth = 6;
               this.colComputerName.Name = "colComputerName";
               this.colComputerName.ReadOnly = true;
               // 
               // colRoomTestName
               // 
               this.colRoomTestName.DataPropertyName = "RoomTestName";
               this.colRoomTestName.HeaderText = "Phòng thi";
               this.colRoomTestName.MinimumWidth = 6;
               this.colRoomTestName.Name = "colRoomTestName";
               this.colRoomTestName.ReadOnly = true;
               // 
                // colDetectionSource
                // 
                this.colDetectionSource.DataPropertyName = "DetectSource";
                this.colDetectionSource.HeaderText = "Nguồn phát hiện";
                this.colDetectionSource.MinimumWidth = 6;
                this.colDetectionSource.Name = "colDetectionSource";
                this.colDetectionSource.ReadOnly = true;
               // 
               // colServerTimeText
               // 
               this.colServerTimeText.DataPropertyName = "ServerTimeText";
               this.colServerTimeText.HeaderText = "Thời gian máy chủ";
               this.colServerTimeText.MinimumWidth = 6;
               this.colServerTimeText.Name = "colServerTimeText";
               this.colServerTimeText.ReadOnly = true;
               // 
               // colLastResponseTimeText
               // 
               this.colLastResponseTimeText.DataPropertyName = "LastResponseTimeText";
               this.colLastResponseTimeText.HeaderText = "Lần phản hồi cuối";
               this.colLastResponseTimeText.MinimumWidth = 6;
               this.colLastResponseTimeText.Name = "colLastResponseTimeText";
               this.colLastResponseTimeText.ReadOnly = true;
               // 
               // colNote
               // 
               this.colNote.DataPropertyName = "Note";
               this.colNote.HeaderText = "Ghi chú";
               this.colNote.MinimumWidth = 6;
               this.colNote.Name = "colNote";
               this.colNote.ReadOnly = true;
               // 
               // frmLichSuMatKetNoi
               // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(1200, 580);
                this.Controls.Add(this.dataGridView1);
                this.Controls.Add(this.pnlAction);
                this.Name = "frmLichSuMatKetNoi";
                this.Padding = new System.Windows.Forms.Padding(20, 60, 20, 20);
                this.Text = "Lịch sử mất kết nối";
                this.pnlAction.ResumeLayout(false);
                ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
                this.ResumeLayout(false);

          }

          #endregion

          private System.Windows.Forms.Panel pnlAction;
          private System.Windows.Forms.Button btnXuatFile;
          private System.Windows.Forms.Button btnInBaoCao;
          private System.Windows.Forms.DataGridView dataGridView1;
          private System.Windows.Forms.DataGridViewTextBoxColumn colSTT;
          private System.Windows.Forms.DataGridViewTextBoxColumn colContestantCode;
          private System.Windows.Forms.DataGridViewTextBoxColumn colContestantName;
          private System.Windows.Forms.DataGridViewTextBoxColumn colComputerName;
          private System.Windows.Forms.DataGridViewTextBoxColumn colRoomTestName;
          private System.Windows.Forms.DataGridViewTextBoxColumn colDetectionSource;
          private System.Windows.Forms.DataGridViewTextBoxColumn colServerTimeText;
          private System.Windows.Forms.DataGridViewTextBoxColumn colLastResponseTimeText;
          private System.Windows.Forms.DataGridViewTextBoxColumn colNote;
     }
}
