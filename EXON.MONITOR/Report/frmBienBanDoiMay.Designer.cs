
namespace EXON.MONITOR.Report
{
    partial class frmBienBanDoiMay
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
            this.rpVDoiMay = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ReportDataset = new EXON.MONITOR.Report.ReportDataset();
            ((System.ComponentModel.ISupportInitialize)(this.ReportDataset)).BeginInit();
            this.SuspendLayout();
            // 
            // rpVDoiMay
            // 
            this.rpVDoiMay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpVDoiMay.LocalReport.ReportEmbeddedResource = "EXON.MONITOR.Report.rpLichSuDoiMay.rdlc";
            this.rpVDoiMay.Location = new System.Drawing.Point(0, 0);
            this.rpVDoiMay.Name = "rpVDoiMay";
            this.rpVDoiMay.Size = new System.Drawing.Size(764, 658);
            this.rpVDoiMay.TabIndex = 1;
            // 
            // ReportDataset
            // 
            this.ReportDataset.DataSetName = "ReportDataset";
            this.ReportDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // frmBienBanDoiMay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 658);
            this.Controls.Add(this.rpVDoiMay);
            this.Name = "frmBienBanDoiMay";
            this.Text = "frmBienBanDoiMay";
            this.Load += new System.EventHandler(this.frmBienBanDoiMay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReportDataset)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rpVDoiMay;
        private ReportDataset ReportDataset;
    }
}