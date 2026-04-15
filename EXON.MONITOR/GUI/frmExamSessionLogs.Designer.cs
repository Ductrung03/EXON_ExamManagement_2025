namespace EXON.MONITOR.GUI
{
    partial class frmExamSessionLogs
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblHeader = new System.Windows.Forms.Label();
            this.tabLogs = new System.Windows.Forms.TabControl();
            this.tabOverview = new System.Windows.Forms.TabPage();
            this.rtbOverview = new System.Windows.Forms.RichTextBox();
            this.tabConnection = new System.Windows.Forms.TabPage();
            this.rtbConnection = new System.Windows.Forms.RichTextBox();
            this.tabActions = new System.Windows.Forms.TabPage();
            this.rtbActions = new System.Windows.Forms.RichTextBox();
            this.tabSubmit = new System.Windows.Forms.TabPage();
            this.rtbSubmit = new System.Windows.Forms.RichTextBox();
            this.tabRuntime = new System.Windows.Forms.TabPage();
            this.rtbRuntime = new System.Windows.Forms.RichTextBox();
            this.tabOthers = new System.Windows.Forms.TabPage();
            this.rtbOthers = new System.Windows.Forms.RichTextBox();
            this.tabLogs.SuspendLayout();
            this.tabOverview.SuspendLayout();
            this.tabConnection.SuspendLayout();
            this.tabActions.SuspendLayout();
            this.tabSubmit.SuspendLayout();
            this.tabRuntime.SuspendLayout();
            this.tabOthers.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Times New Roman", 11F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(20, 60);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblHeader.Size = new System.Drawing.Size(1160, 44);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Logs ca thi";
            // 
            // tabLogs
            // 
            this.tabLogs.Controls.Add(this.tabOverview);
            this.tabLogs.Controls.Add(this.tabConnection);
            this.tabLogs.Controls.Add(this.tabActions);
            this.tabLogs.Controls.Add(this.tabSubmit);
            this.tabLogs.Controls.Add(this.tabRuntime);
            this.tabLogs.Controls.Add(this.tabOthers);
            this.tabLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabLogs.Location = new System.Drawing.Point(20, 104);
            this.tabLogs.Name = "tabLogs";
            this.tabLogs.SelectedIndex = 0;
            this.tabLogs.Size = new System.Drawing.Size(1160, 576);
            this.tabLogs.TabIndex = 1;
            // 
            // tabOverview
            // 
            this.tabOverview.Controls.Add(this.rtbOverview);
            this.tabOverview.Location = new System.Drawing.Point(4, 25);
            this.tabOverview.Name = "tabOverview";
            this.tabOverview.Padding = new System.Windows.Forms.Padding(3);
            this.tabOverview.Size = new System.Drawing.Size(1152, 547);
            this.tabOverview.TabIndex = 0;
            this.tabOverview.Text = "Tổng quan";
            this.tabOverview.UseVisualStyleBackColor = true;
            // 
            // rtbOverview
            // 
            this.rtbOverview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOverview.Font = new System.Drawing.Font("Consolas", 10F);
            this.rtbOverview.Location = new System.Drawing.Point(3, 3);
            this.rtbOverview.Name = "rtbOverview";
            this.rtbOverview.ReadOnly = true;
            this.rtbOverview.Size = new System.Drawing.Size(1146, 541);
            this.rtbOverview.TabIndex = 0;
            this.rtbOverview.Text = "";
            // 
            // tabConnection
            // 
            this.tabConnection.Controls.Add(this.rtbConnection);
            this.tabConnection.Location = new System.Drawing.Point(4, 25);
            this.tabConnection.Name = "tabConnection";
            this.tabConnection.Padding = new System.Windows.Forms.Padding(3);
            this.tabConnection.Size = new System.Drawing.Size(1152, 547);
            this.tabConnection.TabIndex = 1;
            this.tabConnection.Text = "Kết nối";
            this.tabConnection.UseVisualStyleBackColor = true;
            // 
            // rtbConnection
            // 
            this.rtbConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbConnection.Font = new System.Drawing.Font("Consolas", 10F);
            this.rtbConnection.Location = new System.Drawing.Point(3, 3);
            this.rtbConnection.Name = "rtbConnection";
            this.rtbConnection.ReadOnly = true;
            this.rtbConnection.Size = new System.Drawing.Size(1146, 541);
            this.rtbConnection.TabIndex = 0;
            this.rtbConnection.Text = "";
            // 
            // tabActions
            // 
            this.tabActions.Controls.Add(this.rtbActions);
            this.tabActions.Location = new System.Drawing.Point(4, 25);
            this.tabActions.Name = "tabActions";
            this.tabActions.Padding = new System.Windows.Forms.Padding(3);
            this.tabActions.Size = new System.Drawing.Size(1152, 547);
            this.tabActions.TabIndex = 2;
            this.tabActions.Text = "Thao tác";
            this.tabActions.UseVisualStyleBackColor = true;
            // 
            // rtbActions
            // 
            this.rtbActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbActions.Font = new System.Drawing.Font("Consolas", 10F);
            this.rtbActions.Location = new System.Drawing.Point(3, 3);
            this.rtbActions.Name = "rtbActions";
            this.rtbActions.ReadOnly = true;
            this.rtbActions.Size = new System.Drawing.Size(1146, 541);
            this.rtbActions.TabIndex = 0;
            this.rtbActions.Text = "";
            // 
            // tabSubmit
            // 
            this.tabSubmit.Controls.Add(this.rtbSubmit);
            this.tabSubmit.Location = new System.Drawing.Point(4, 25);
            this.tabSubmit.Name = "tabSubmit";
            this.tabSubmit.Padding = new System.Windows.Forms.Padding(3);
            this.tabSubmit.Size = new System.Drawing.Size(1152, 547);
            this.tabSubmit.TabIndex = 3;
            this.tabSubmit.Text = "Nộp bài";
            this.tabSubmit.UseVisualStyleBackColor = true;
            // 
            // rtbSubmit
            // 
            this.rtbSubmit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbSubmit.Font = new System.Drawing.Font("Consolas", 10F);
            this.rtbSubmit.Location = new System.Drawing.Point(3, 3);
            this.rtbSubmit.Name = "rtbSubmit";
            this.rtbSubmit.ReadOnly = true;
            this.rtbSubmit.Size = new System.Drawing.Size(1146, 541);
            this.rtbSubmit.TabIndex = 0;
            this.rtbSubmit.Text = "";
            // 
            // tabRuntime
            // 
            this.tabRuntime.Controls.Add(this.rtbRuntime);
            this.tabRuntime.Location = new System.Drawing.Point(4, 25);
            this.tabRuntime.Name = "tabRuntime";
            this.tabRuntime.Padding = new System.Windows.Forms.Padding(3);
            this.tabRuntime.Size = new System.Drawing.Size(1152, 547);
            this.tabRuntime.TabIndex = 4;
            this.tabRuntime.Text = "Runtime";
            this.tabRuntime.UseVisualStyleBackColor = true;
            // 
            // rtbRuntime
            // 
            this.rtbRuntime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbRuntime.Font = new System.Drawing.Font("Consolas", 10F);
            this.rtbRuntime.Location = new System.Drawing.Point(3, 3);
            this.rtbRuntime.Name = "rtbRuntime";
            this.rtbRuntime.ReadOnly = true;
            this.rtbRuntime.Size = new System.Drawing.Size(1146, 541);
            this.rtbRuntime.TabIndex = 0;
            this.rtbRuntime.Text = "";
            // 
            // tabOthers
            // 
            this.tabOthers.Controls.Add(this.rtbOthers);
            this.tabOthers.Location = new System.Drawing.Point(4, 25);
            this.tabOthers.Name = "tabOthers";
            this.tabOthers.Padding = new System.Windows.Forms.Padding(3);
            this.tabOthers.Size = new System.Drawing.Size(1152, 547);
            this.tabOthers.TabIndex = 5;
            this.tabOthers.Text = "Khác";
            this.tabOthers.UseVisualStyleBackColor = true;
            // 
            // rtbOthers
            // 
            this.rtbOthers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOthers.Font = new System.Drawing.Font("Consolas", 10F);
            this.rtbOthers.Location = new System.Drawing.Point(3, 3);
            this.rtbOthers.Name = "rtbOthers";
            this.rtbOthers.ReadOnly = true;
            this.rtbOthers.Size = new System.Drawing.Size(1146, 541);
            this.rtbOthers.TabIndex = 0;
            this.rtbOthers.Text = "";
            // 
            // frmExamSessionLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.tabLogs);
            this.Controls.Add(this.lblHeader);
            this.Name = "frmExamSessionLogs";
            this.Padding = new System.Windows.Forms.Padding(20, 60, 20, 20);
            this.Text = "Xem logs ca thi";
            this.Load += new System.EventHandler(this.frmExamSessionLogs_Load);
            this.tabLogs.ResumeLayout(false);
            this.tabOverview.ResumeLayout(false);
            this.tabConnection.ResumeLayout(false);
            this.tabActions.ResumeLayout(false);
            this.tabSubmit.ResumeLayout(false);
            this.tabRuntime.ResumeLayout(false);
            this.tabOthers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.TabControl tabLogs;
        private System.Windows.Forms.TabPage tabOverview;
        private System.Windows.Forms.RichTextBox rtbOverview;
        private System.Windows.Forms.TabPage tabConnection;
        private System.Windows.Forms.RichTextBox rtbConnection;
        private System.Windows.Forms.TabPage tabActions;
        private System.Windows.Forms.RichTextBox rtbActions;
        private System.Windows.Forms.TabPage tabSubmit;
        private System.Windows.Forms.RichTextBox rtbSubmit;
        private System.Windows.Forms.TabPage tabRuntime;
        private System.Windows.Forms.RichTextBox rtbRuntime;
        private System.Windows.Forms.TabPage tabOthers;
        private System.Windows.Forms.RichTextBox rtbOthers;
    }
}
