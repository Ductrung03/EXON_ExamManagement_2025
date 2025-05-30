
namespace EXON.MONITOR.GUI
{
     partial class frmUpdateQuestion
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
               this.flpnListOfQuestions = new System.Windows.Forms.FlowLayoutPanel();
               this.label1 = new System.Windows.Forms.Label();
               this.cbMonthi = new System.Windows.Forms.ComboBox();
               this.btnSave = new System.Windows.Forms.Button();
               this.button1 = new System.Windows.Forms.Button();
               this.SuspendLayout();
               // 
               // flpnListOfQuestions
               // 
               this.flpnListOfQuestions.AutoScroll = true;
               this.flpnListOfQuestions.AutoSize = true;
               this.flpnListOfQuestions.Location = new System.Drawing.Point(195, 25);
               this.flpnListOfQuestions.Name = "flpnListOfQuestions";
               this.flpnListOfQuestions.Padding = new System.Windows.Forms.Padding(0, 0, 0, 30);
               this.flpnListOfQuestions.Size = new System.Drawing.Size(272, 100);
               this.flpnListOfQuestions.TabIndex = 3;
               // 
               // label1
               // 
               this.label1.AutoSize = true;
               this.label1.Location = new System.Drawing.Point(52, 25);
               this.label1.Name = "label1";
               this.label1.Size = new System.Drawing.Size(46, 13);
               this.label1.TabIndex = 4;
               this.label1.Text = "Môn Thi";
               // 
               // cbMonthi
               // 
               this.cbMonthi.FormattingEnabled = true;
               this.cbMonthi.Location = new System.Drawing.Point(12, 52);
               this.cbMonthi.Name = "cbMonthi";
               this.cbMonthi.Size = new System.Drawing.Size(153, 21);
               this.cbMonthi.TabIndex = 5;
               this.cbMonthi.SelectedValueChanged += new System.EventHandler(this.cbMonthi_SelectedValueChanged);
               this.cbMonthi.TextChanged += new System.EventHandler(this.cbMonthi_TextChanged);
               // 
               // btnSave
               // 
               this.btnSave.BackColor = System.Drawing.SystemColors.Control;
               this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
               this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
               this.btnSave.Location = new System.Drawing.Point(12, 95);
               this.btnSave.Name = "btnSave";
               this.btnSave.Size = new System.Drawing.Size(153, 43);
               this.btnSave.TabIndex = 129;
               this.btnSave.Text = "Lưu";
               this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
               this.btnSave.UseVisualStyleBackColor = false;
               this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
               // 
               // button1
               // 
               this.button1.BackColor = System.Drawing.SystemColors.Control;
               this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
               this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
               this.button1.Location = new System.Drawing.Point(12, 149);
               this.button1.Name = "button1";
               this.button1.Size = new System.Drawing.Size(153, 43);
               this.button1.TabIndex = 130;
               this.button1.Text = "Cập nhật nhanh";
               this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
               this.button1.UseVisualStyleBackColor = false;
               this.button1.Click += new System.EventHandler(this.button1_Click);
               // 
               // frmUpdateQuestion
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.ClientSize = new System.Drawing.Size(411, 204);
               this.Controls.Add(this.button1);
               this.Controls.Add(this.btnSave);
               this.Controls.Add(this.cbMonthi);
               this.Controls.Add(this.label1);
               this.Controls.Add(this.flpnListOfQuestions);
               this.Name = "frmUpdateQuestion";
               this.Text = "frmUpdateQuestion";
               this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
               this.ResumeLayout(false);
               this.PerformLayout();

          }

          #endregion

          private System.Windows.Forms.FlowLayoutPanel flpnListOfQuestions;
          private System.Windows.Forms.Label label1;
          private System.Windows.Forms.ComboBox cbMonthi;
          private System.Windows.Forms.Button btnSave;
          private System.Windows.Forms.Button button1;
     }
}