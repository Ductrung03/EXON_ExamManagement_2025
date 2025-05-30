
namespace EXON.MONITOR.Report
{
    partial class FrmRpLogViolations
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
            this.rpVInLog = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rpVInLog
            // 
            this.rpVInLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpVInLog.LocalReport.ReportEmbeddedResource = "EXON.MONITOR.Report.rpLogViolations.rdlc";
            this.rpVInLog.Location = new System.Drawing.Point(0, 0);
            this.rpVInLog.Margin = new System.Windows.Forms.Padding(4);
            this.rpVInLog.Name = "rpVInLog";
            
            this.rpVInLog.Size = new System.Drawing.Size(946, 583);
            this.rpVInLog.TabIndex = 2;
            // 
            // FrmRpLogViolations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 583);
            this.Controls.Add(this.rpVInLog);
            this.Name = "FrmRpLogViolations";
            this.Text = "FrmRpLogViolations";
            this.Load += new System.EventHandler(this.FrmRpLogViolations_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rpVInLog;
    }
}