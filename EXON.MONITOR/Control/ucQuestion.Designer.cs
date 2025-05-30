
namespace EXON.MONITOR.Control
{
     partial class ucQuestion
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
               this.pnTitleOfQuestion = new System.Windows.Forms.Panel();
               this.rtbTitleOfQuestion = new TXTextControl.TextControl();
               this.lbNumber = new System.Windows.Forms.Label();
               this.label2 = new System.Windows.Forms.Label();
               this.label3 = new System.Windows.Forms.Label();
               this.pnTitleOfQuestion.SuspendLayout();
               this.SuspendLayout();
               // 
               // pnTitleOfQuestion
               // 
               this.pnTitleOfQuestion.Controls.Add(this.rtbTitleOfQuestion);
               this.pnTitleOfQuestion.Controls.Add(this.lbNumber);
               this.pnTitleOfQuestion.Location = new System.Drawing.Point(23, 18);
               this.pnTitleOfQuestion.Name = "pnTitleOfQuestion";
               this.pnTitleOfQuestion.Size = new System.Drawing.Size(837, 37);
               this.pnTitleOfQuestion.TabIndex = 4;
               // 
               // rtbTitleOfQuestion
               // 
               this.rtbTitleOfQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
               this.rtbTitleOfQuestion.AutoControlSize.AutoExpand = TXTextControl.AutoSizeDirection.Both;
               this.rtbTitleOfQuestion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
               this.rtbTitleOfQuestion.Cursor = System.Windows.Forms.Cursors.Hand;
               this.rtbTitleOfQuestion.EditMode = TXTextControl.EditMode.ReadOnly;
               this.rtbTitleOfQuestion.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.rtbTitleOfQuestion.Location = new System.Drawing.Point(81, 2);
               this.rtbTitleOfQuestion.Margin = new System.Windows.Forms.Padding(5);
               this.rtbTitleOfQuestion.Name = "rtbTitleOfQuestion";
               this.rtbTitleOfQuestion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
               this.rtbTitleOfQuestion.Size = new System.Drawing.Size(742, 31);
               this.rtbTitleOfQuestion.TabIndex = 0;
               this.rtbTitleOfQuestion.TextBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
               this.rtbTitleOfQuestion.UserNames = null;
               this.rtbTitleOfQuestion.ViewMode = TXTextControl.ViewMode.SimpleControl;
               // 
               // lbNumber
               // 
               this.lbNumber.Location = new System.Drawing.Point(4, 6);
               this.lbNumber.Name = "lbNumber";
               this.lbNumber.Size = new System.Drawing.Size(80, 21);
               this.lbNumber.TabIndex = 2;
               this.lbNumber.Text = "Câu 999:";
               // 
               // label2
               // 
               this.label2.AutoSize = true;
               this.label2.Location = new System.Drawing.Point(878, 18);
               this.label2.Name = "label2";
               this.label2.Size = new System.Drawing.Size(33, 13);
               this.label2.TabIndex = 8;
               this.label2.Text = "Level";
               // 
               // label3
               // 
               this.label3.AutoSize = true;
               this.label3.Location = new System.Drawing.Point(878, 42);
               this.label3.Name = "label3";
               this.label3.Size = new System.Drawing.Size(109, 13);
               this.label3.TabIndex = 9;
               this.label3.Text = "Điểm cho mỗi câu hỏi";
               this.label3.Click += new System.EventHandler(this.label3_Click);
               // 
               // ucQuestion
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.Controls.Add(this.label2);
               this.Controls.Add(this.label3);
               this.Controls.Add(this.pnTitleOfQuestion);
               this.Name = "ucQuestion";
               this.Size = new System.Drawing.Size(1212, 74);
               this.Load += new System.EventHandler(this.ucQuestion_Load);
               this.pnTitleOfQuestion.ResumeLayout(false);
               this.ResumeLayout(false);
               this.PerformLayout();

          }

          #endregion

          private System.Windows.Forms.Panel pnTitleOfQuestion;
          private TXTextControl.TextControl rtbTitleOfQuestion;
          private System.Windows.Forms.Label lbNumber;
          private System.Windows.Forms.Label label2;
          private System.Windows.Forms.Label label3;
     }
}
