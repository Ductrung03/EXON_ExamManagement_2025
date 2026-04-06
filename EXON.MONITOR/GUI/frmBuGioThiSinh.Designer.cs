namespace EXON.MONITOR.GUI
{
     partial class frmBuGioThiSinh
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
               this.label2 = new System.Windows.Forms.Label();
               this.label1 = new System.Windows.Forms.Label();
               this.txtNote = new System.Windows.Forms.TextBox();
               this.mbtnInput = new MetroFramework.Controls.MetroButton();
               this.textBox1 = new System.Windows.Forms.TextBox();
               this.label3 = new System.Windows.Forms.Label();
               this.lblLastResponseValue = new System.Windows.Forms.Label();
               this.SuspendLayout();
               // 
               // label2
               // 
               this.label2.AutoSize = true;
               this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.label2.Location = new System.Drawing.Point(19, 63);
               this.label2.Name = "label2";
               this.label2.Size = new System.Drawing.Size(120, 20);
               this.label2.TabIndex = 10;
               this.label2.Text = "Thời gian bù giờ";
               // 
               // label1
               // 
               this.label1.AutoSize = true;
               this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.label1.Location = new System.Drawing.Point(19, 146);
               this.label1.Name = "label1";
               this.label1.Size = new System.Drawing.Size(205, 20);
               this.label1.TabIndex = 9;
               this.label1.Text = "Nguyên nhân gián đoạn (*): ";
               // 
               // txtNote
               // 
               this.txtNote.Location = new System.Drawing.Point(23, 181);
               this.txtNote.Multiline = true;
               this.txtNote.Name = "txtNote";
               this.txtNote.Size = new System.Drawing.Size(339, 127);
               this.txtNote.TabIndex = 8;
               // 
               // mbtnInput
               // 
               this.mbtnInput.Location = new System.Drawing.Point(151, 314);
               this.mbtnInput.Name = "mbtnInput";
               this.mbtnInput.Size = new System.Drawing.Size(88, 29);
               this.mbtnInput.TabIndex = 6;
               this.mbtnInput.Text = "Nhập";
               this.mbtnInput.UseSelectable = true;
               this.mbtnInput.Click += new System.EventHandler(this.mbtnInput_Click);
               // 
               // textBox1
               // 
               this.textBox1.Location = new System.Drawing.Point(23, 100);
               this.textBox1.Multiline = true;
               this.textBox1.Name = "textBox1";
               this.textBox1.Size = new System.Drawing.Size(231, 30);
               this.textBox1.TabIndex = 11;
               this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
               // 
               // label3
               // 
               this.label3.AutoSize = true;
               this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.label3.Location = new System.Drawing.Point(20, 136);
               this.label3.Name = "label3";
               this.label3.Size = new System.Drawing.Size(156, 17);
               this.label3.TabIndex = 12;
               this.label3.Text = "Lần phản hồi cuối cùng:";
               // 
               // lblLastResponseValue
               // 
               this.lblLastResponseValue.AutoSize = true;
               this.lblLastResponseValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
               this.lblLastResponseValue.Location = new System.Drawing.Point(182, 136);
               this.lblLastResponseValue.Name = "lblLastResponseValue";
               this.lblLastResponseValue.Size = new System.Drawing.Size(103, 17);
               this.lblLastResponseValue.TabIndex = 13;
               this.lblLastResponseValue.Text = "00:00:00";
               // 
               // frmBuGioThiSinh
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.ClientSize = new System.Drawing.Size(416, 361);
               this.Controls.Add(this.lblLastResponseValue);
               this.Controls.Add(this.label3);
               this.Controls.Add(this.textBox1);
               this.Controls.Add(this.label2);
               this.Controls.Add(this.label1);
               this.Controls.Add(this.txtNote);
               this.Controls.Add(this.mbtnInput);
               this.Name = "frmBuGioThiSinh";
               this.Text = "Bù Giờ Thí Sinh";
               this.ResumeLayout(false);
               this.PerformLayout();

          }

        #endregion

        private System.Windows.Forms.Label label2;
         private System.Windows.Forms.Label label1;
         private System.Windows.Forms.TextBox txtNote;
         private MetroFramework.Controls.MetroButton mbtnInput;
         private System.Windows.Forms.TextBox textBox1;
         private System.Windows.Forms.Label label3;
         private System.Windows.Forms.Label lblLastResponseValue;
     }
}
