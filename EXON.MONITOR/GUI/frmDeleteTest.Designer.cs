namespace EXON.MONITOR.GUI
{
    partial class frmDeleteTest
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtTestID = new System.Windows.Forms.TextBox();
            this.dgvDeleteTestInfo = new System.Windows.Forms.DataGridView();
            this.colSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTestID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNumOfQuestion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDivisionShiftName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOrderOfQuestion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblQuestionID = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblNumOfTest = new System.Windows.Forms.Label();
            this.btnDeleteTest = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeleteTestInfo)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Sitka Display", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(159, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 28);
            this.label1.TabIndex = 9;
            this.label1.Text = "Mã đề lỗi";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSearch.Font = new System.Drawing.Font("Sitka Display", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(501, 74);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(140, 51);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtTestID
            // 
            this.txtTestID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTestID.BackColor = System.Drawing.SystemColors.Info;
            this.txtTestID.Font = new System.Drawing.Font("Sitka Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTestID.Location = new System.Drawing.Point(319, 64);
            this.txtTestID.Name = "txtTestID";
            this.txtTestID.Size = new System.Drawing.Size(116, 28);
            this.txtTestID.TabIndex = 1;
            this.txtTestID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTestID_KeyDown);
            // 
            // dgvDeleteTestInfo
            // 
            this.dgvDeleteTestInfo.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dgvDeleteTestInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSTT,
            this.colTestID,
            this.colNumOfQuestion,
            this.colSubject,
            this.colDivisionShiftName});
            this.dgvDeleteTestInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDeleteTestInfo.Location = new System.Drawing.Point(0, 0);
            this.dgvDeleteTestInfo.Name = "dgvDeleteTestInfo";
            this.dgvDeleteTestInfo.ReadOnly = true;
            this.dgvDeleteTestInfo.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.dgvDeleteTestInfo.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgvDeleteTestInfo.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Sitka Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDeleteTestInfo.RowTemplate.DefaultCellStyle.Padding = new System.Windows.Forms.Padding(10, 2, 10, 2);
            this.dgvDeleteTestInfo.RowTemplate.Height = 30;
            this.dgvDeleteTestInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDeleteTestInfo.Size = new System.Drawing.Size(800, 234);
            this.dgvDeleteTestInfo.TabIndex = 10;
            // 
            // colSTT
            // 
            this.colSTT.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colSTT.DataPropertyName = "STT";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSTT.DefaultCellStyle = dataGridViewCellStyle1;
            this.colSTT.HeaderText = "Số thứ tự";
            this.colSTT.Name = "colSTT";
            this.colSTT.ReadOnly = true;
            this.colSTT.Width = 75;
            // 
            // colTestID
            // 
            this.colTestID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colTestID.DataPropertyName = "TestID";
            this.colTestID.HeaderText = "Mã đề";
            this.colTestID.Name = "colTestID";
            this.colTestID.ReadOnly = true;
            this.colTestID.Width = 63;
            // 
            // colNumOfQuestion
            // 
            this.colNumOfQuestion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colNumOfQuestion.DataPropertyName = "NumOfQuestion";
            this.colNumOfQuestion.HeaderText = "Số lượng câu hỏi";
            this.colNumOfQuestion.Name = "colNumOfQuestion";
            this.colNumOfQuestion.ReadOnly = true;
            this.colNumOfQuestion.Width = 112;
            // 
            // colSubject
            // 
            this.colSubject.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colSubject.DataPropertyName = "SubjectName";
            this.colSubject.HeaderText = "Môn thi";
            this.colSubject.Name = "colSubject";
            this.colSubject.ReadOnly = true;
            this.colSubject.Width = 67;
            // 
            // colDivisionShiftName
            // 
            this.colDivisionShiftName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDivisionShiftName.DataPropertyName = "DivisionShiftName";
            this.colDivisionShiftName.HeaderText = "Ca thi";
            this.colDivisionShiftName.Name = "colDivisionShiftName";
            this.colDivisionShiftName.ReadOnly = true;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Sitka Heading", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(323, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 35);
            this.label2.TabIndex = 7;
            this.label2.Text = "Xóa đề thi lỗi";
            // 
            // txtOrderOfQuestion
            // 
            this.txtOrderOfQuestion.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtOrderOfQuestion.BackColor = System.Drawing.SystemColors.Info;
            this.txtOrderOfQuestion.Font = new System.Drawing.Font("Sitka Display", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderOfQuestion.Location = new System.Drawing.Point(319, 101);
            this.txtOrderOfQuestion.Name = "txtOrderOfQuestion";
            this.txtOrderOfQuestion.Size = new System.Drawing.Size(116, 28);
            this.txtOrderOfQuestion.TabIndex = 2;
            this.txtOrderOfQuestion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOrderOfQuestion_KeyDown);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Sitka Display", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(159, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 28);
            this.label3.TabIndex = 8;
            this.label3.Text = "Số thứ tự câu hỏi";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblQuestionID);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtOrderOfQuestion);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtTestID);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 168);
            this.panel1.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Sitka Small", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(209, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Danh sách đề có câu hỏi lỗi";
            // 
            // lblQuestionID
            // 
            this.lblQuestionID.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblQuestionID.AutoSize = true;
            this.lblQuestionID.Font = new System.Drawing.Font("Sitka Display", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuestionID.Location = new System.Drawing.Point(345, 136);
            this.lblQuestionID.Name = "lblQuestionID";
            this.lblQuestionID.Size = new System.Drawing.Size(110, 28);
            this.lblQuestionID.TabIndex = 10;
            this.lblQuestionID.Text = "Mã câu hỏi: ";
            this.lblQuestionID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblQuestionID.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblNumOfTest);
            this.panel2.Controls.Add(this.btnDeleteTest);
            this.panel2.Controls.Add(this.btnSelectAll);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 402);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 48);
            this.panel2.TabIndex = 11;
            // 
            // lblNumOfTest
            // 
            this.lblNumOfTest.AutoSize = true;
            this.lblNumOfTest.Font = new System.Drawing.Font("Sitka Small", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumOfTest.Location = new System.Drawing.Point(3, 3);
            this.lblNumOfTest.Name = "lblNumOfTest";
            this.lblNumOfTest.Size = new System.Drawing.Size(189, 20);
            this.lblNumOfTest.TabIndex = 12;
            this.lblNumOfTest.Text = "Tìm thấy x đề có câu hỏi y";
            this.lblNumOfTest.Visible = false;
            // 
            // btnDeleteTest
            // 
            this.btnDeleteTest.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDeleteTest.Font = new System.Drawing.Font("Sitka Display", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteTest.Location = new System.Drawing.Point(403, 3);
            this.btnDeleteTest.Name = "btnDeleteTest";
            this.btnDeleteTest.Size = new System.Drawing.Size(140, 42);
            this.btnDeleteTest.TabIndex = 5;
            this.btnDeleteTest.Text = "Xóa đề thi";
            this.btnDeleteTest.UseVisualStyleBackColor = true;
            this.btnDeleteTest.Click += new System.EventHandler(this.btnDeleteTest_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSelectAll.Font = new System.Drawing.Font("Sitka Display", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectAll.Location = new System.Drawing.Point(257, 3);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(140, 42);
            this.btnSelectAll.TabIndex = 4;
            this.btnSelectAll.Text = "Chọn tất cả";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgvDeleteTestInfo);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 168);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 234);
            this.panel3.TabIndex = 12;
            // 
            // frmDeleteTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmDeleteTest";
            this.Text = "frmDeleteTest";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeleteTestInfo)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtTestID;
        private System.Windows.Forms.DataGridView dgvDeleteTestInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOrderOfQuestion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnDeleteTest;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTestID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumOfQuestion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubject;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDivisionShiftName;
        private System.Windows.Forms.Label lblQuestionID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblNumOfTest;
    }
}