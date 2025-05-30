
namespace EXON.MONITOR.Control
{
     partial class ucSubquestion
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
               this.pnTitleOfSubQuestion = new System.Windows.Forms.Panel();
               this.lbNumber = new System.Windows.Forms.Label();
               this.rtbTitleOfSubQuestion = new TXTextControl.TextControl();
               this.label1 = new System.Windows.Forms.Label();
               this.pnTitleOfSubQuestion.SuspendLayout();
               this.SuspendLayout();
               // 
               // pnTitleOfSubQuestion
               // 
               this.pnTitleOfSubQuestion.Controls.Add(this.rtbTitleOfSubQuestion);
               this.pnTitleOfSubQuestion.Controls.Add(this.lbNumber);
               this.pnTitleOfSubQuestion.Location = new System.Drawing.Point(23, 18);
               this.pnTitleOfSubQuestion.Name = "pnTitleOfSubQuestion";
               this.pnTitleOfSubQuestion.Size = new System.Drawing.Size(837, 37);
               this.pnTitleOfSubQuestion.TabIndex = 5;
               // 
               // lbNumber
               // 
               this.lbNumber.Location = new System.Drawing.Point(4, 6);
               this.lbNumber.Name = "lbNumber";
               this.lbNumber.Size = new System.Drawing.Size(80, 21);
               this.lbNumber.TabIndex = 2;
               this.lbNumber.Text = "Câu 999:";
               // 
               // rtbTitleOfSubQuestion
               // 
               this.rtbTitleOfSubQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
               this.rtbTitleOfSubQuestion.AutoControlSize.AutoExpand = TXTextControl.AutoSizeDirection.Both;
               this.rtbTitleOfSubQuestion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
               this.rtbTitleOfSubQuestion.Cursor = System.Windows.Forms.Cursors.Hand;
               this.rtbTitleOfSubQuestion.EditMode = TXTextControl.EditMode.ReadOnly;
               this.rtbTitleOfSubQuestion.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.rtbTitleOfSubQuestion.Location = new System.Drawing.Point(87, 3);
               this.rtbTitleOfSubQuestion.Margin = new System.Windows.Forms.Padding(5);
               this.rtbTitleOfSubQuestion.Name = "rtbTitleOfSubQuestion";
               this.rtbTitleOfSubQuestion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
               this.rtbTitleOfSubQuestion.Size = new System.Drawing.Size(745, 31);
               this.rtbTitleOfSubQuestion.TabIndex = 3;
               this.rtbTitleOfSubQuestion.TextBackColor = System.Drawing.Color.White;
               this.rtbTitleOfSubQuestion.UserNames = null;
               this.rtbTitleOfSubQuestion.ViewMode = TXTextControl.ViewMode.SimpleControl;
               // 
               // label1
               // 
               this.label1.AutoSize = true;
               this.label1.Location = new System.Drawing.Point(879, 24);
               this.label1.Name = "label1";
               this.label1.Size = new System.Drawing.Size(68, 13);
               this.label1.TabIndex = 6;
               this.label1.Text = "Điểm hiện tại";
               // 
               // ucSubquestion
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.Controls.Add(this.label1);
               this.Controls.Add(this.pnTitleOfSubQuestion);
               this.Name = "ucSubquestion";
               this.Size = new System.Drawing.Size(1212, 74);
               this.Load += new System.EventHandler(this.ucSubquestion_Load);
               this.pnTitleOfSubQuestion.ResumeLayout(false);
               this.ResumeLayout(false);
               this.PerformLayout();

          }

          #endregion

          private System.Windows.Forms.Panel pnTitleOfSubQuestion;
          private System.Windows.Forms.Label lbNumber;
          private TXTextControl.TextControl rtbTitleOfSubQuestion;
          private System.Windows.Forms.Label label1;
     }
}
