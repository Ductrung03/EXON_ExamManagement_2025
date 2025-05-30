
namespace EXON.MONITOR.GUI
{
    partial class frmDoiMay
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
            this.dGVDoiMay = new System.Windows.Forms.DataGridView();
            this.ContestantID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContestantName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oldComputer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.newComputer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time_changecmp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBaoCao = new System.Windows.Forms.Button();
            this.lblCaThi = new System.Windows.Forms.Label();
            this.txtMonThi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKyThi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCaThi = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dGVDoiMay)).BeginInit();
            this.SuspendLayout();
            // 
            // dGVDoiMay
            // 
            this.dGVDoiMay.AllowUserToAddRows = false;
            this.dGVDoiMay.AllowUserToDeleteRows = false;
            this.dGVDoiMay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGVDoiMay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ContestantID,
            this.ContestantName,
            this.oldComputer,
            this.newComputer,
            this.time_changecmp});
            this.dGVDoiMay.Location = new System.Drawing.Point(35, 178);
            this.dGVDoiMay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dGVDoiMay.Name = "dGVDoiMay";
            this.dGVDoiMay.ReadOnly = true;
            this.dGVDoiMay.RowHeadersWidth = 51;
            this.dGVDoiMay.Size = new System.Drawing.Size(992, 373);
            this.dGVDoiMay.TabIndex = 0;
            this.dGVDoiMay.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGVDoiMay_CellContentClick);
            // 
            // ContestantID
            // 
            this.ContestantID.DataPropertyName = "ContestantID";
            this.ContestantID.HeaderText = "ID thí sinh";
            this.ContestantID.MinimumWidth = 6;
            this.ContestantID.Name = "ContestantID";
            this.ContestantID.ReadOnly = true;
            this.ContestantID.Width = 125;
            // 
            // ContestantName
            // 
            this.ContestantName.DataPropertyName = "ContestantName";
            this.ContestantName.HeaderText = "Họ và tên";
            this.ContestantName.MinimumWidth = 6;
            this.ContestantName.Name = "ContestantName";
            this.ContestantName.ReadOnly = true;
            this.ContestantName.Width = 200;
            // 
            // oldComputer
            // 
            this.oldComputer.DataPropertyName = "oldComputer";
            this.oldComputer.HeaderText = "Máy cũ";
            this.oldComputer.MinimumWidth = 6;
            this.oldComputer.Name = "oldComputer";
            this.oldComputer.ReadOnly = true;
            this.oldComputer.Width = 125;
            // 
            // newComputer
            // 
            this.newComputer.DataPropertyName = "newComputer";
            this.newComputer.HeaderText = "Máy mới";
            this.newComputer.MinimumWidth = 6;
            this.newComputer.Name = "newComputer";
            this.newComputer.ReadOnly = true;
            this.newComputer.Width = 125;
            // 
            // time_changecmp
            // 
            this.time_changecmp.DataPropertyName = "time_changecmp";
            this.time_changecmp.HeaderText = "Thời gian đổi máy";
            this.time_changecmp.MinimumWidth = 6;
            this.time_changecmp.Name = "time_changecmp";
            this.time_changecmp.ReadOnly = true;
            this.time_changecmp.Width = 200;
            // 
            // btnBaoCao
            // 
            this.btnBaoCao.Location = new System.Drawing.Point(904, 84);
            this.btnBaoCao.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBaoCao.Name = "btnBaoCao";
            this.btnBaoCao.Size = new System.Drawing.Size(119, 28);
            this.btnBaoCao.TabIndex = 1;
            this.btnBaoCao.Text = "Xuất báo cáo";
            this.btnBaoCao.UseVisualStyleBackColor = true;
            this.btnBaoCao.Click += new System.EventHandler(this.btnBaoCao_Click);
            // 
            // lblCaThi
            // 
            this.lblCaThi.AutoSize = true;
            this.lblCaThi.Location = new System.Drawing.Point(31, 81);
            this.lblCaThi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCaThi.Name = "lblCaThi";
            this.lblCaThi.Size = new System.Drawing.Size(58, 17);
            this.lblCaThi.TabIndex = 2;
            this.lblCaThi.Text = "Môn thi:";
            this.lblCaThi.Click += new System.EventHandler(this.lblCaThi_Click);
            // 
            // txtMonThi
            // 
            this.txtMonThi.Location = new System.Drawing.Point(99, 73);
            this.txtMonThi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMonThi.Name = "txtMonThi";
            this.txtMonThi.ReadOnly = true;
            this.txtMonThi.Size = new System.Drawing.Size(459, 22);
            this.txtMonThi.TabIndex = 3;
            this.txtMonThi.TextChanged += new System.EventHandler(this.txtMonThi_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 116);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Kỳ thi:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtKyThi
            // 
            this.txtKyThi.Location = new System.Drawing.Point(99, 107);
            this.txtKyThi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtKyThi.Name = "txtKyThi";
            this.txtKyThi.ReadOnly = true;
            this.txtKyThi.Size = new System.Drawing.Size(459, 22);
            this.txtKyThi.TabIndex = 5;
            this.txtKyThi.TextChanged += new System.EventHandler(this.txtKyThi_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 154);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Ca Thi:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtCaThi
            // 
            this.txtCaThi.Location = new System.Drawing.Point(99, 145);
            this.txtCaThi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCaThi.Name = "txtCaThi";
            this.txtCaThi.ReadOnly = true;
            this.txtCaThi.Size = new System.Drawing.Size(459, 22);
            this.txtCaThi.TabIndex = 7;
            this.txtCaThi.TextChanged += new System.EventHandler(this.txtCaThi_TextChanged);
            // 
            // frmDoiMay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.txtCaThi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKyThi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMonThi);
            this.Controls.Add(this.lblCaThi);
            this.Controls.Add(this.btnBaoCao);
            this.Controls.Add(this.dGVDoiMay);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmDoiMay";
            this.Padding = new System.Windows.Forms.Padding(27, 74, 27, 25);
            this.Text = "Lịch sử đổi máy";
            this.Load += new System.EventHandler(this.frmDoiMay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGVDoiMay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dGVDoiMay;
        private System.Windows.Forms.Button btnBaoCao;
        private System.Windows.Forms.Label lblCaThi;
        private System.Windows.Forms.TextBox txtMonThi;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContestantID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContestantName;
        private System.Windows.Forms.DataGridViewTextBoxColumn oldComputer;
        private System.Windows.Forms.DataGridViewTextBoxColumn newComputer;
        private System.Windows.Forms.DataGridViewTextBoxColumn time_changecmp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKyThi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCaThi;
    }
}