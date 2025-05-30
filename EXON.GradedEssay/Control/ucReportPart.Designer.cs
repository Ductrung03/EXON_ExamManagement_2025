namespace EXON.GradedEssay.Control
{
     partial class ucReportPart
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
               this.groupBox2 = new System.Windows.Forms.GroupBox();
               this.cbLop = new System.Windows.Forms.ComboBox();
               this.label3 = new System.Windows.Forms.Label();
               this.label2 = new System.Windows.Forms.Label();
               this.cbx_MonThi = new System.Windows.Forms.ComboBox();
               this.groupBox1 = new System.Windows.Forms.GroupBox();
               this.gvMain = new System.Windows.Forms.DataGridView();
               this.groupBox3 = new System.Windows.Forms.GroupBox();
               this.btnExportScoreWritting = new System.Windows.Forms.Button();
               this.groupBox2.SuspendLayout();
               this.groupBox1.SuspendLayout();
               ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
               this.groupBox3.SuspendLayout();
               this.SuspendLayout();
               // 
               // btnPrintResult
               // 
               this.btnPrintResult.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.btnPrintResult.Image = global::EXON.GradedEssay.Properties.Resources.print_65_445177;
               this.btnPrintResult.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
               this.btnPrintResult.Location = new System.Drawing.Point(310, 18);
               this.btnPrintResult.Margin = new System.Windows.Forms.Padding(2);
               this.btnPrintResult.Name = "btnPrintResult";
               this.btnPrintResult.Size = new System.Drawing.Size(157, 36);
               this.btnPrintResult.TabIndex = 65;
               this.btnPrintResult.Text = "In kết quả theo môn";
               this.btnPrintResult.UseVisualStyleBackColor = true;
               this.btnPrintResult.Click += new System.EventHandler(this.btnPrintResult_Click);
               // 
               // groupBox2
               // 
               this.groupBox2.Controls.Add(this.btnExportScoreWritting);
               this.groupBox2.Controls.Add(this.btnPrintResult);
               this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
               this.groupBox2.Location = new System.Drawing.Point(0, 368);
               this.groupBox2.Name = "groupBox2";
               this.groupBox2.Size = new System.Drawing.Size(905, 71);
               this.groupBox2.TabIndex = 7;
               this.groupBox2.TabStop = false;
               // 
               // cbLop
               // 
               this.cbLop.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.cbLop.FormattingEnabled = true;
               this.cbLop.Location = new System.Drawing.Point(412, 19);
               this.cbLop.Name = "cbLop";
               this.cbLop.Size = new System.Drawing.Size(274, 21);
               this.cbLop.TabIndex = 96;
               this.cbLop.SelectedIndexChanged += new System.EventHandler(this.cbLop_SelectedIndexChanged);
               // 
               // label3
               // 
               this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.label3.AutoSize = true;
               this.label3.Location = new System.Drawing.Point(334, 22);
               this.label3.Name = "label3";
               this.label3.Size = new System.Drawing.Size(28, 13);
               this.label3.TabIndex = 99;
               this.label3.Text = "Lớp ";
               // 
               // label2
               // 
               this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.label2.AutoSize = true;
               this.label2.Location = new System.Drawing.Point(334, 64);
               this.label2.Name = "label2";
               this.label2.Size = new System.Drawing.Size(42, 13);
               this.label2.TabIndex = 95;
               this.label2.Text = "Môn thi";
               // 
               // cbx_MonThi
               // 
               this.cbx_MonThi.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.cbx_MonThi.FormattingEnabled = true;
               this.cbx_MonThi.Location = new System.Drawing.Point(412, 61);
               this.cbx_MonThi.Name = "cbx_MonThi";
               this.cbx_MonThi.Size = new System.Drawing.Size(274, 21);
               this.cbx_MonThi.TabIndex = 96;
               // 
               // groupBox1
               // 
               this.groupBox1.Controls.Add(this.cbx_MonThi);
               this.groupBox1.Controls.Add(this.label2);
               this.groupBox1.Controls.Add(this.label3);
               this.groupBox1.Controls.Add(this.cbLop);
               this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
               this.groupBox1.Location = new System.Drawing.Point(0, 0);
               this.groupBox1.Name = "groupBox1";
               this.groupBox1.Size = new System.Drawing.Size(905, 102);
               this.groupBox1.TabIndex = 6;
               this.groupBox1.TabStop = false;
               this.groupBox1.Text = "Xuất báo cáo";
               // 
               // gvMain
               // 
               this.gvMain.AllowUserToAddRows = false;
               this.gvMain.AllowUserToDeleteRows = false;
               this.gvMain.AllowUserToResizeColumns = false;
               this.gvMain.AllowUserToResizeRows = false;
               this.gvMain.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
               this.gvMain.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
               this.gvMain.BackgroundColor = System.Drawing.Color.White;
               this.gvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
               this.gvMain.Dock = System.Windows.Forms.DockStyle.Fill;
               this.gvMain.Location = new System.Drawing.Point(3, 16);
               this.gvMain.Name = "gvMain";
               this.gvMain.ReadOnly = true;
               this.gvMain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
               this.gvMain.Size = new System.Drawing.Size(899, 247);
               this.gvMain.TabIndex = 60;
               // 
               // groupBox3
               // 
               this.groupBox3.Controls.Add(this.gvMain);
               this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
               this.groupBox3.Location = new System.Drawing.Point(0, 102);
               this.groupBox3.Name = "groupBox3";
               this.groupBox3.Size = new System.Drawing.Size(905, 266);
               this.groupBox3.TabIndex = 8;
               this.groupBox3.TabStop = false;
               this.groupBox3.Text = "Danh sách thí sinh";
               // 
               // btnExportScoreWritting
               // 
               this.btnExportScoreWritting.Anchor = System.Windows.Forms.AnchorStyles.None;
               this.btnExportScoreWritting.BackColor = System.Drawing.Color.Transparent;
               this.btnExportScoreWritting.Image = global::EXON.GradedEssay.Properties.Resources.export_40_602457;
               this.btnExportScoreWritting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
               this.btnExportScoreWritting.Location = new System.Drawing.Point(547, 19);
               this.btnExportScoreWritting.Name = "btnExportScoreWritting";
               this.btnExportScoreWritting.Size = new System.Drawing.Size(125, 36);
               this.btnExportScoreWritting.TabIndex = 28;
               this.btnExportScoreWritting.Text = "Xuất phiếu điểm";
               this.btnExportScoreWritting.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
               this.btnExportScoreWritting.UseVisualStyleBackColor = false;
               this.btnExportScoreWritting.Click += new System.EventHandler(this.btnExportScoreWritting_Click);
               // 
               // ucReportPart
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.Controls.Add(this.groupBox3);
               this.Controls.Add(this.groupBox2);
               this.Controls.Add(this.groupBox1);
               this.Name = "ucReportPart";
               this.Size = new System.Drawing.Size(905, 439);
               this.groupBox2.ResumeLayout(false);
               this.groupBox1.ResumeLayout(false);
               this.groupBox1.PerformLayout();
               ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
               this.groupBox3.ResumeLayout(false);
               this.ResumeLayout(false);

          }

          #endregion

          private System.Windows.Forms.Button btnPrintResult;
          private System.Windows.Forms.GroupBox groupBox2;
          private System.Windows.Forms.Button btnExportScoreWritting;
          private System.Windows.Forms.ComboBox cbLop;
          private System.Windows.Forms.Label label3;
          private System.Windows.Forms.Label label2;
          private System.Windows.Forms.ComboBox cbx_MonThi;
          private System.Windows.Forms.GroupBox groupBox1;
          private System.Windows.Forms.DataGridView gvMain;
          private System.Windows.Forms.GroupBox groupBox3;
     }
}
