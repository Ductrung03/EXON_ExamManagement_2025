
namespace EXON.GradedEssay.Control
{
     partial class ucThongKeDapAn
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
               this.btnPrintResult = new System.Windows.Forms.Button();
               this.groupBox3 = new System.Windows.Forms.GroupBox();
               this.gvMain = new System.Windows.Forms.DataGridView();
               this.cTestNumberIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
               this.cSubjectID = new System.Windows.Forms.DataGridViewTextBoxColumn();
               this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
               this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
               this.cbtnXemBaiLam = new System.Windows.Forms.DataGridViewButtonColumn();
               this.OrigiTest = new System.Windows.Forms.DataGridViewButtonColumn();
               this.groupBox2 = new System.Windows.Forms.GroupBox();
               this.groupBox1 = new System.Windows.Forms.GroupBox();
               this.cbx_MonThi = new System.Windows.Forms.ComboBox();
               this.label2 = new System.Windows.Forms.Label();
               this.groupBox3.SuspendLayout();
               ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
               this.groupBox2.SuspendLayout();
               this.groupBox1.SuspendLayout();
               this.SuspendLayout();
               // 
               // btnPrintResult
               // 
               this.btnPrintResult.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.btnPrintResult.Image = global::EXON.GradedEssay.Properties.Resources.print_65_445177;
               this.btnPrintResult.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
               this.btnPrintResult.Location = new System.Drawing.Point(375, 18);
               this.btnPrintResult.Margin = new System.Windows.Forms.Padding(2);
               this.btnPrintResult.Name = "btnPrintResult";
               this.btnPrintResult.Size = new System.Drawing.Size(157, 36);
               this.btnPrintResult.TabIndex = 65;
               this.btnPrintResult.Text = "In Toàn Bộ Đáp Án";
               this.btnPrintResult.UseVisualStyleBackColor = true;
               this.btnPrintResult.Click += new System.EventHandler(this.btnPrintResult_Click);
               // 
               // groupBox3
               // 
               this.groupBox3.Controls.Add(this.gvMain);
               this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
               this.groupBox3.Location = new System.Drawing.Point(0, 102);
               this.groupBox3.Name = "groupBox3";
               this.groupBox3.Size = new System.Drawing.Size(905, 266);
               this.groupBox3.TabIndex = 11;
               this.groupBox3.TabStop = false;
               this.groupBox3.Text = "Danh sách môn thi";
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
            this.cSubjectID,
            this.Column1,
            this.Column2,
            this.cbtnXemBaiLam,
            this.OrigiTest});
               this.gvMain.Dock = System.Windows.Forms.DockStyle.Fill;
               this.gvMain.Location = new System.Drawing.Point(3, 16);
               this.gvMain.Name = "gvMain";
               this.gvMain.Size = new System.Drawing.Size(899, 247);
               this.gvMain.TabIndex = 61;
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
               // cSubjectID
               // 
               this.cSubjectID.DataPropertyName = "SubjectID";
               this.cSubjectID.HeaderText = "Mã Môn Thi";
               this.cSubjectID.Name = "cSubjectID";
               this.cSubjectID.ReadOnly = true;
               this.cSubjectID.Width = 120;
               // 
               // Column1
               // 
               this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
               this.Column1.DataPropertyName = "SubjectName";
               this.Column1.HeaderText = "Tên Môn Thi";
               this.Column1.Name = "Column1";
               this.Column1.ReadOnly = true;
               // 
               // Column2
               // 
               this.Column2.DataPropertyName = "SubjectCode";
               this.Column2.HeaderText = "Mã Môn";
               this.Column2.Name = "Column2";
               this.Column2.ReadOnly = true;
               this.Column2.Width = 120;
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
               // OrigiTest
               // 
               this.OrigiTest.DataPropertyName = "OrigiTest";
               this.OrigiTest.HeaderText = "Đề Thi Gốc";
               this.OrigiTest.Name = "OrigiTest";
               this.OrigiTest.Resizable = System.Windows.Forms.DataGridViewTriState.True;
               this.OrigiTest.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
               // 
               // groupBox2
               // 
               this.groupBox2.Controls.Add(this.btnPrintResult);
               this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
               this.groupBox2.Location = new System.Drawing.Point(0, 368);
               this.groupBox2.Name = "groupBox2";
               this.groupBox2.Size = new System.Drawing.Size(905, 71);
               this.groupBox2.TabIndex = 10;
               this.groupBox2.TabStop = false;
               // 
               // groupBox1
               // 
               this.groupBox1.Controls.Add(this.cbx_MonThi);
               this.groupBox1.Controls.Add(this.label2);
               this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
               this.groupBox1.Location = new System.Drawing.Point(0, 0);
               this.groupBox1.Name = "groupBox1";
               this.groupBox1.Size = new System.Drawing.Size(905, 102);
               this.groupBox1.TabIndex = 9;
               this.groupBox1.TabStop = false;
               this.groupBox1.Text = "Xuất báo cáo";
               // 
               // cbx_MonThi
               // 
               this.cbx_MonThi.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.cbx_MonThi.FormattingEnabled = true;
               this.cbx_MonThi.Location = new System.Drawing.Point(413, 37);
               this.cbx_MonThi.Name = "cbx_MonThi";
               this.cbx_MonThi.Size = new System.Drawing.Size(274, 21);
               this.cbx_MonThi.TabIndex = 96;
               // 
               // label2
               // 
               this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.label2.AutoSize = true;
               this.label2.Location = new System.Drawing.Point(335, 40);
               this.label2.Name = "label2";
               this.label2.Size = new System.Drawing.Size(42, 13);
               this.label2.TabIndex = 95;
               this.label2.Text = "Môn thi";
               // 
               // ucThongKeDapAn
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.Controls.Add(this.groupBox3);
               this.Controls.Add(this.groupBox2);
               this.Controls.Add(this.groupBox1);
               this.Name = "ucThongKeDapAn";
               this.Size = new System.Drawing.Size(905, 439);
               this.groupBox3.ResumeLayout(false);
               ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
               this.groupBox2.ResumeLayout(false);
               this.groupBox1.ResumeLayout(false);
               this.groupBox1.PerformLayout();
               this.ResumeLayout(false);

          }

          #endregion

          private System.Windows.Forms.Button btnPrintResult;
          private System.Windows.Forms.GroupBox groupBox3;
          private System.Windows.Forms.GroupBox groupBox2;
          private System.Windows.Forms.GroupBox groupBox1;
          private System.Windows.Forms.DataGridView gvMain;
          private System.Windows.Forms.ComboBox cbx_MonThi;
          private System.Windows.Forms.Label label2;
          private System.Windows.Forms.DataGridViewTextBoxColumn cTestNumberIndex;
          private System.Windows.Forms.DataGridViewTextBoxColumn cSubjectID;
          private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
          private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
          private System.Windows.Forms.DataGridViewButtonColumn cbtnXemBaiLam;
          private System.Windows.Forms.DataGridViewButtonColumn OrigiTest;
     }
}
