
namespace EXON.MONITOR
{
     partial class frmQuickUpdate
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
               this.comboBox1 = new System.Windows.Forms.ComboBox();
               this.textBox1 = new System.Windows.Forms.TextBox();
               this.btnSave = new System.Windows.Forms.Button();
               this.label2 = new System.Windows.Forms.Label();
               this.label3 = new System.Windows.Forms.Label();
               this.SuspendLayout();
               // 
               // comboBox1
               // 
               this.comboBox1.FormattingEnabled = true;
               this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
               this.comboBox1.Location = new System.Drawing.Point(118, 39);
               this.comboBox1.Name = "comboBox1";
               this.comboBox1.Size = new System.Drawing.Size(164, 21);
               this.comboBox1.TabIndex = 0;
               // 
               // textBox1
               // 
               this.textBox1.Location = new System.Drawing.Point(118, 102);
               this.textBox1.Name = "textBox1";
               this.textBox1.Size = new System.Drawing.Size(164, 20);
               this.textBox1.TabIndex = 1;
               // 
               // btnSave
               // 
               this.btnSave.BackColor = System.Drawing.SystemColors.Control;
               this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
               this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
               this.btnSave.Location = new System.Drawing.Point(70, 165);
               this.btnSave.Name = "btnSave";
               this.btnSave.Size = new System.Drawing.Size(153, 43);
               this.btnSave.TabIndex = 130;
               this.btnSave.Text = "Lưu";
               this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
               this.btnSave.UseVisualStyleBackColor = false;
               this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
               // 
               // label2
               // 
               this.label2.AutoSize = true;
               this.label2.Location = new System.Drawing.Point(12, 39);
               this.label2.Name = "label2";
               this.label2.Size = new System.Drawing.Size(33, 13);
               this.label2.TabIndex = 131;
               this.label2.Text = "Level";
               // 
               // label3
               // 
               this.label3.AutoSize = true;
               this.label3.Location = new System.Drawing.Point(2, 105);
               this.label3.Name = "label3";
               this.label3.Size = new System.Drawing.Size(109, 13);
               this.label3.TabIndex = 132;
               this.label3.Text = "Điểm cho mỗi câu hỏi";
               this.label3.Click += new System.EventHandler(this.label3_Click);
               // 
               // frmQuickUpdate
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.ClientSize = new System.Drawing.Size(308, 309);
               this.Controls.Add(this.label2);
               this.Controls.Add(this.label3);
               this.Controls.Add(this.btnSave);
               this.Controls.Add(this.textBox1);
               this.Controls.Add(this.comboBox1);
               this.Name = "frmQuickUpdate";
               this.Text = "frmQuickUpdate";
               this.ResumeLayout(false);
               this.PerformLayout();

          }

          #endregion

          private System.Windows.Forms.ComboBox comboBox1;
          private System.Windows.Forms.TextBox textBox1;
          private System.Windows.Forms.Button btnSave;
          private System.Windows.Forms.Label label2;
          private System.Windows.Forms.Label label3;
     }
}