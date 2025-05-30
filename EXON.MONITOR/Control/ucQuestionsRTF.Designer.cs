
namespace EXONSYSTEM
{
    partial class ucQuestionsRTF
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lbNumber = new System.Windows.Forms.Label();
            this.pnTitleOfQuestion = new System.Windows.Forms.Panel();
            this.rtbTitleOfQuestion = new TXTextControl.TextControl();
            this.mpnAnswers = new System.Windows.Forms.Panel();
            this.mrbAnswerD = new System.Windows.Forms.RadioButton();
            this.mrbAnswerC = new System.Windows.Forms.RadioButton();
            this.mrbAnswerB = new System.Windows.Forms.RadioButton();
            this.mrbAnswerA = new System.Windows.Forms.RadioButton();
            this.rtbAnswerD = new TXTextControl.TextControl();
            this.rtbAnswerC = new TXTextControl.TextControl();
            this.rtbAnswerB = new TXTextControl.TextControl();
            this.rtbAnswerA = new TXTextControl.TextControl();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnTitleOfQuestion.SuspendLayout();
            this.mpnAnswers.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbNumber
            // 
            this.lbNumber.Location = new System.Drawing.Point(4, 6);
            this.lbNumber.Name = "lbNumber";
            this.lbNumber.Size = new System.Drawing.Size(80, 21);
            this.lbNumber.TabIndex = 2;
            this.lbNumber.Text = "Câu 999:";
            // 
            // pnTitleOfQuestion
            // 
            this.pnTitleOfQuestion.Controls.Add(this.rtbTitleOfQuestion);
            this.pnTitleOfQuestion.Controls.Add(this.lbNumber);
            this.pnTitleOfQuestion.Location = new System.Drawing.Point(54, 3);
            this.pnTitleOfQuestion.Name = "pnTitleOfQuestion";
            this.pnTitleOfQuestion.Size = new System.Drawing.Size(636, 37);
            this.pnTitleOfQuestion.TabIndex = 3;
            // 
            // rtbTitleOfQuestion
            // 
            this.rtbTitleOfQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbTitleOfQuestion.AutoControlSize.AutoExpand = TXTextControl.AutoSizeDirection.Both;
            this.rtbTitleOfQuestion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rtbTitleOfQuestion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rtbTitleOfQuestion.EditMode = TXTextControl.EditMode.ReadOnly;
            this.rtbTitleOfQuestion.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbTitleOfQuestion.Location = new System.Drawing.Point(81, 2);
            this.rtbTitleOfQuestion.Margin = new System.Windows.Forms.Padding(5);
            this.rtbTitleOfQuestion.Name = "rtbTitleOfQuestion";
            this.rtbTitleOfQuestion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.rtbTitleOfQuestion.Size = new System.Drawing.Size(541, 31);
            this.rtbTitleOfQuestion.TabIndex = 0;
            //this.toolTip1.SetToolTip(this.rtbTitleOfQuestion, "Bạn có thể nhấp đúp chuột để xem rõ nội dung");
            this.rtbTitleOfQuestion.UserNames = null;
            this.rtbTitleOfQuestion.ViewMode = TXTextControl.ViewMode.SimpleControl;
            this.rtbTitleOfQuestion.DoubleClick += new System.EventHandler(this.rtbTitleOfQuestion_DoubleClick);
            // 
            // mpnAnswers
            // 
            this.mpnAnswers.Controls.Add(this.mrbAnswerD);
            this.mpnAnswers.Controls.Add(this.mrbAnswerC);
            this.mpnAnswers.Controls.Add(this.mrbAnswerB);
            this.mpnAnswers.Controls.Add(this.mrbAnswerA);
            this.mpnAnswers.Controls.Add(this.rtbAnswerD);
            this.mpnAnswers.Controls.Add(this.rtbAnswerC);
            this.mpnAnswers.Controls.Add(this.rtbAnswerB);
            this.mpnAnswers.Controls.Add(this.rtbAnswerA);
            this.mpnAnswers.Location = new System.Drawing.Point(54, 64);
            this.mpnAnswers.Name = "mpnAnswers";
            this.mpnAnswers.Size = new System.Drawing.Size(622, 206);
            this.mpnAnswers.TabIndex = 4;
            // 
            // mrbAnswerD
            // 
            this.mrbAnswerD.AutoSize = true;
            this.mrbAnswerD.Location = new System.Drawing.Point(53, 149);
            this.mrbAnswerD.Margin = new System.Windows.Forms.Padding(5);
            this.mrbAnswerD.Name = "mrbAnswerD";
            this.mrbAnswerD.Size = new System.Drawing.Size(125, 25);
            this.mrbAnswerD.TabIndex = 28;
            this.mrbAnswerD.TabStop = true;
            this.mrbAnswerD.Text = "radioButton4";
            this.mrbAnswerD.UseVisualStyleBackColor = true;
            this.mrbAnswerD.CheckedChanged += new System.EventHandler(this.mrbAnswer_CheckedChanged);
            // 
            // mrbAnswerC
            // 
            this.mrbAnswerC.AutoSize = true;
            this.mrbAnswerC.Location = new System.Drawing.Point(53, 103);
            this.mrbAnswerC.Margin = new System.Windows.Forms.Padding(5);
            this.mrbAnswerC.Name = "mrbAnswerC";
            this.mrbAnswerC.Size = new System.Drawing.Size(125, 25);
            this.mrbAnswerC.TabIndex = 27;
            this.mrbAnswerC.TabStop = true;
            this.mrbAnswerC.Text = "radioButton3";
            this.mrbAnswerC.UseVisualStyleBackColor = true;
            this.mrbAnswerC.CheckedChanged += new System.EventHandler(this.mrbAnswer_CheckedChanged);
            // 
            // mrbAnswerB
            // 
            this.mrbAnswerB.AutoSize = true;
            this.mrbAnswerB.Location = new System.Drawing.Point(53, 62);
            this.mrbAnswerB.Margin = new System.Windows.Forms.Padding(5);
            this.mrbAnswerB.Name = "mrbAnswerB";
            this.mrbAnswerB.Size = new System.Drawing.Size(125, 25);
            this.mrbAnswerB.TabIndex = 26;
            this.mrbAnswerB.TabStop = true;
            this.mrbAnswerB.Text = "radioButton2";
            this.mrbAnswerB.UseVisualStyleBackColor = true;
            this.mrbAnswerB.CheckedChanged += new System.EventHandler(this.mrbAnswer_CheckedChanged);
            // 
            // mrbAnswerA
            // 
            this.mrbAnswerA.AutoSize = true;
            this.mrbAnswerA.Location = new System.Drawing.Point(53, 19);
            this.mrbAnswerA.Margin = new System.Windows.Forms.Padding(5);
            this.mrbAnswerA.Name = "mrbAnswerA";
            this.mrbAnswerA.Size = new System.Drawing.Size(125, 25);
            this.mrbAnswerA.TabIndex = 25;
            this.mrbAnswerA.TabStop = true;
            this.mrbAnswerA.Text = "radioButton1";
            this.mrbAnswerA.UseVisualStyleBackColor = true;
            this.mrbAnswerA.CheckedChanged += new System.EventHandler(this.mrbAnswer_CheckedChanged);
            // 
            // rtbAnswerD
            // 
            this.rtbAnswerD.AutoControlSize.AutoExpand = TXTextControl.AutoSizeDirection.Both;
            this.rtbAnswerD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rtbAnswerD.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rtbAnswerD.EditMode = TXTextControl.EditMode.ReadOnly;
            this.rtbAnswerD.Font = new System.Drawing.Font("Arial", 10F);
            this.rtbAnswerD.HideSelection = false;
            this.rtbAnswerD.Location = new System.Drawing.Point(269, 143);
            this.rtbAnswerD.Margin = new System.Windows.Forms.Padding(5);
            this.rtbAnswerD.Name = "rtbAnswerD";
            this.rtbAnswerD.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.rtbAnswerD.Size = new System.Drawing.Size(314, 31);
            this.rtbAnswerD.TabIndex = 24;
            //this.toolTip1.SetToolTip(this.rtbAnswerD, "Bạn có thể nhấp đúp chuột để xem rõ nội dung");
            this.rtbAnswerD.UserNames = null;
            this.rtbAnswerD.ViewMode = TXTextControl.ViewMode.SimpleControl;
            this.rtbAnswerD.Click += new System.EventHandler(this.RichTexBox_Click);
            this.rtbAnswerD.DoubleClick += new System.EventHandler(this.rtbAnswerD_DoubleClick);
            // 
            // rtbAnswerC
            // 
            this.rtbAnswerC.AutoControlSize.AutoExpand = TXTextControl.AutoSizeDirection.Both;
            this.rtbAnswerC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rtbAnswerC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rtbAnswerC.EditMode = TXTextControl.EditMode.ReadOnly;
            this.rtbAnswerC.Font = new System.Drawing.Font("Arial", 10F);
            this.rtbAnswerC.Location = new System.Drawing.Point(269, 97);
            this.rtbAnswerC.Margin = new System.Windows.Forms.Padding(5);
            this.rtbAnswerC.Name = "rtbAnswerC";
            this.rtbAnswerC.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.rtbAnswerC.Size = new System.Drawing.Size(314, 31);
            this.rtbAnswerC.TabIndex = 23;
            //this.toolTip1.SetToolTip(this.rtbAnswerC, "Bạn có thể nhấp đúp chuột để xem rõ nội dung");
            this.rtbAnswerC.UserNames = null;
            this.rtbAnswerC.ViewMode = TXTextControl.ViewMode.SimpleControl;
            this.rtbAnswerC.Click += new System.EventHandler(this.RichTexBox_Click);
            this.rtbAnswerC.DoubleClick += new System.EventHandler(this.rtbAnswerC_DoubleClick);
            // 
            // rtbAnswerB
            // 
            this.rtbAnswerB.AutoControlSize.AutoExpand = TXTextControl.AutoSizeDirection.Both;
            this.rtbAnswerB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rtbAnswerB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rtbAnswerB.EditMode = TXTextControl.EditMode.ReadOnly;
            this.rtbAnswerB.Font = new System.Drawing.Font("Arial", 10F);
            this.rtbAnswerB.Location = new System.Drawing.Point(269, 56);
            this.rtbAnswerB.Margin = new System.Windows.Forms.Padding(5);
            this.rtbAnswerB.Name = "rtbAnswerB";
            this.rtbAnswerB.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.rtbAnswerB.Size = new System.Drawing.Size(314, 31);
            this.rtbAnswerB.TabIndex = 22;
            //this.toolTip1.SetToolTip(this.rtbAnswerB, "Bạn có thể nhấp đúp chuột để xem rõ nội dung");
            this.rtbAnswerB.UserNames = null;
            this.rtbAnswerB.ViewMode = TXTextControl.ViewMode.SimpleControl;
            this.rtbAnswerB.Click += new System.EventHandler(this.RichTexBox_Click);
            this.rtbAnswerB.DoubleClick += new System.EventHandler(this.rtbAnswerB_DoubleClick);
            // 
            // rtbAnswerA
            // 
            this.rtbAnswerA.AutoControlSize.AutoExpand = TXTextControl.AutoSizeDirection.Both;
            this.rtbAnswerA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.rtbAnswerA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rtbAnswerA.EditMode = TXTextControl.EditMode.ReadOnly;
            this.rtbAnswerA.Font = new System.Drawing.Font("Arial", 10F);
            this.rtbAnswerA.HideSelection = false;
            this.rtbAnswerA.Location = new System.Drawing.Point(269, 13);
            this.rtbAnswerA.Margin = new System.Windows.Forms.Padding(5);
            this.rtbAnswerA.Name = "rtbAnswerA";
            this.rtbAnswerA.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.rtbAnswerA.Size = new System.Drawing.Size(314, 31);
            this.rtbAnswerA.TabIndex = 21;
            //this.toolTip1.SetToolTip(this.rtbAnswerA, "Bạn có thể nhấp đúp chuột để xem rõ nội dung");
            this.rtbAnswerA.UserNames = null;
            this.rtbAnswerA.ViewMode = TXTextControl.ViewMode.SimpleControl;
            this.rtbAnswerA.Click += new System.EventHandler(this.RichTexBox_Click);
            this.rtbAnswerA.DoubleClick += new System.EventHandler(this.rtbAnswerA_DoubleClick);
            // 
            // ucQuestionsRTF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mpnAnswers);
            this.Controls.Add(this.pnTitleOfQuestion);
            this.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ucQuestionsRTF";
            this.Size = new System.Drawing.Size(707, 284);
            this.UseCustomBackColor = true;
            this.Load += new System.EventHandler(this.ucQuestionsRTF_Load);
            this.pnTitleOfQuestion.ResumeLayout(false);
            this.mpnAnswers.ResumeLayout(false);
            this.mpnAnswers.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TXTextControl.TextControl rtbTitleOfQuestion;
        private System.Windows.Forms.Label lbNumber;
        private System.Windows.Forms.Panel pnTitleOfQuestion;
        private System.Windows.Forms.Panel mpnAnswers;
        private System.Windows.Forms.RadioButton mrbAnswerD;
        private System.Windows.Forms.RadioButton mrbAnswerC;
        private System.Windows.Forms.RadioButton mrbAnswerB;
        private System.Windows.Forms.RadioButton mrbAnswerA;
        public TXTextControl.TextControl rtbAnswerD;
        public TXTextControl.TextControl rtbAnswerC;
        public TXTextControl.TextControl rtbAnswerB;
        public TXTextControl.TextControl rtbAnswerA;
        public System.Windows.Forms.ToolTip toolTip1;
    }
}
