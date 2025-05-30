
namespace EXON.MONITOR.GUI
{
    partial class frmInLogGianDoan
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
               this.dGVGianDoan = new System.Windows.Forms.DataGridView();
               this.txtCaThi = new System.Windows.Forms.TextBox();
               this.label2 = new System.Windows.Forms.Label();
               this.txtKyThi = new System.Windows.Forms.TextBox();
               this.label1 = new System.Windows.Forms.Label();
               this.btnBaoCao = new System.Windows.Forms.Button();
               ((System.ComponentModel.ISupportInitialize)(this.dGVGianDoan)).BeginInit();
               this.SuspendLayout();
               // 
               // dGVGianDoan
               // 
               this.dGVGianDoan.AllowUserToAddRows = false;
               this.dGVGianDoan.AllowUserToDeleteRows = false;
               this.dGVGianDoan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
               this.dGVGianDoan.Location = new System.Drawing.Point(18, 137);
               this.dGVGianDoan.Name = "dGVGianDoan";
               this.dGVGianDoan.ReadOnly = true;
               this.dGVGianDoan.RowHeadersWidth = 51;
               this.dGVGianDoan.Size = new System.Drawing.Size(744, 303);
               this.dGVGianDoan.TabIndex = 8;
               // 
               // txtCaThi
               // 
               this.txtCaThi.Location = new System.Drawing.Point(82, 92);
               this.txtCaThi.Name = "txtCaThi";
               this.txtCaThi.ReadOnly = true;
               this.txtCaThi.Size = new System.Drawing.Size(345, 20);
               this.txtCaThi.TabIndex = 15;
               // 
               // label2
               // 
               this.label2.AutoSize = true;
               this.label2.Location = new System.Drawing.Point(32, 99);
               this.label2.Name = "label2";
               this.label2.Size = new System.Drawing.Size(41, 13);
               this.label2.TabIndex = 14;
               this.label2.Text = "Ca Thi:";
               // 
               // txtKyThi
               // 
               this.txtKyThi.Location = new System.Drawing.Point(82, 61);
               this.txtKyThi.Name = "txtKyThi";
               this.txtKyThi.ReadOnly = true;
               this.txtKyThi.Size = new System.Drawing.Size(345, 20);
               this.txtKyThi.TabIndex = 13;
               // 
               // label1
               // 
               this.label1.AutoSize = true;
               this.label1.Location = new System.Drawing.Point(32, 68);
               this.label1.Name = "label1";
               this.label1.Size = new System.Drawing.Size(36, 13);
               this.label1.TabIndex = 12;
               this.label1.Text = "Kỳ thi:";
               // 
               // btnBaoCao
               // 
               this.btnBaoCao.Location = new System.Drawing.Point(670, 61);
               this.btnBaoCao.Name = "btnBaoCao";
               this.btnBaoCao.Size = new System.Drawing.Size(89, 23);
               this.btnBaoCao.TabIndex = 9;
               this.btnBaoCao.Text = "Xuất báo cáo";
               this.btnBaoCao.UseVisualStyleBackColor = true;
               this.btnBaoCao.Click += new System.EventHandler(this.btnBaoCao_Click);
               // 
               // frmInLogGianDoan
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.ClientSize = new System.Drawing.Size(799, 460);
               this.Controls.Add(this.dGVGianDoan);
               this.Controls.Add(this.txtCaThi);
               this.Controls.Add(this.label2);
               this.Controls.Add(this.txtKyThi);
               this.Controls.Add(this.label1);
               this.Controls.Add(this.btnBaoCao);
               this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
               this.Name = "frmInLogGianDoan";
               this.Padding = new System.Windows.Forms.Padding(15, 49, 15, 16);
               this.Text = "Lịch sử bù giờ";
               this.Load += new System.EventHandler(this.frmInLogGianDoan_Load);
               ((System.ComponentModel.ISupportInitialize)(this.dGVGianDoan)).EndInit();
               this.ResumeLayout(false);
               this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dGVGianDoan;
        private System.Windows.Forms.TextBox txtCaThi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKyThi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBaoCao;
    }
}