
namespace EXON.MONITOR.Report
{
    partial class FrmRpLogChangeAws
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
            this.components = new System.ComponentModel.Container();
            this.ReportDataset = new EXON.MONITOR.Report.ReportDataset();
            this.InLichThiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboboxkithi = new System.Windows.Forms.ComboBox();
            this.comboboxcathi = new System.Windows.Forms.ComboBox();
            this.comboboxthisinh = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TIMKIEMLOG = new System.Windows.Forms.Button();
            this.INBIENBANLOG = new System.Windows.Forms.Button();
            this.HienThiLogChangeAws = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ReportDataset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InLichThiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HienThiLogChangeAws)).BeginInit();
            this.SuspendLayout();
            // 
            // ReportDataset
            // 
            this.ReportDataset.DataSetName = "ReportDataset";
            this.ReportDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // InLichThiBindingSource
            // 
            this.InLichThiBindingSource.DataMember = "InLichThi";
            this.InLichThiBindingSource.DataSource = this.ReportDataset;
            // 
            // comboboxkithi
            // 
            this.comboboxkithi.FormattingEnabled = true;
            this.comboboxkithi.Location = new System.Drawing.Point(16, 753);
            this.comboboxkithi.Name = "comboboxkithi";
            this.comboboxkithi.Size = new System.Drawing.Size(264, 24);
            this.comboboxkithi.TabIndex = 4;
            // 
            // comboboxcathi
            // 
            this.comboboxcathi.FormattingEnabled = true;
            this.comboboxcathi.Location = new System.Drawing.Point(309, 751);
            this.comboboxcathi.Name = "comboboxcathi";
            this.comboboxcathi.Size = new System.Drawing.Size(264, 24);
            this.comboboxcathi.TabIndex = 5;
            // 
            // comboboxthisinh
            // 
            this.comboboxthisinh.FormattingEnabled = true;
            this.comboboxthisinh.Location = new System.Drawing.Point(602, 751);
            this.comboboxthisinh.Name = "comboboxthisinh";
            this.comboboxthisinh.Size = new System.Drawing.Size(264, 24);
            this.comboboxthisinh.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 721);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Kì Thi ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(316, 721);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Ca Thi";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(607, 721);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Thí Sinh";
            // 
            // TIMKIEMLOG
            // 
            this.TIMKIEMLOG.Location = new System.Drawing.Point(307, 799);
            this.TIMKIEMLOG.Name = "TIMKIEMLOG";
            this.TIMKIEMLOG.Size = new System.Drawing.Size(129, 23);
            this.TIMKIEMLOG.TabIndex = 12;
            this.TIMKIEMLOG.Text = "Tìm Kiếm";
            this.TIMKIEMLOG.UseVisualStyleBackColor = true;
            this.TIMKIEMLOG.Click += new System.EventHandler(this.TIMKIEMLOG_Click);
            // 
            // INBIENBANLOG
            // 
            this.INBIENBANLOG.Location = new System.Drawing.Point(442, 799);
            this.INBIENBANLOG.Name = "INBIENBANLOG";
            this.INBIENBANLOG.Size = new System.Drawing.Size(132, 23);
            this.INBIENBANLOG.TabIndex = 13;
            this.INBIENBANLOG.Text = "In Biên Bản";
            this.INBIENBANLOG.UseVisualStyleBackColor = true;
            this.INBIENBANLOG.Click += new System.EventHandler(this.INBIENBANLOG_Click);
            // 
            // HienThiLogChangeAws
            // 
            this.HienThiLogChangeAws.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.HienThiLogChangeAws.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HienThiLogChangeAws.Location = new System.Drawing.Point(7, 5);
            this.HienThiLogChangeAws.Name = "HienThiLogChangeAws";
            this.HienThiLogChangeAws.RowHeadersWidth = 51;
            this.HienThiLogChangeAws.RowTemplate.Height = 24;
            this.HienThiLogChangeAws.Size = new System.Drawing.Size(874, 698);
            this.HienThiLogChangeAws.TabIndex = 14;
            this.HienThiLogChangeAws.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.HienThiLogChangeAws_CellClick);
            // 
            // FrmRpLogChangeAws
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 858);
            this.Controls.Add(this.HienThiLogChangeAws);
            this.Controls.Add(this.INBIENBANLOG);
            this.Controls.Add(this.TIMKIEMLOG);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboboxthisinh);
            this.Controls.Add(this.comboboxcathi);
            this.Controls.Add(this.comboboxkithi);
            this.Name = "FrmRpLogChangeAws";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log Thay Doi Dap An";
            ((System.ComponentModel.ISupportInitialize)(this.ReportDataset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InLichThiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HienThiLogChangeAws)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource InLichThiBindingSource;
        private ReportDataset ReportDataset;
        private System.Windows.Forms.ComboBox comboboxkithi;
        private System.Windows.Forms.ComboBox comboboxcathi;
        private System.Windows.Forms.ComboBox comboboxthisinh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button TIMKIEMLOG;
        private System.Windows.Forms.Button INBIENBANLOG;
        private System.Windows.Forms.DataGridView HienThiLogChangeAws;
    }
}