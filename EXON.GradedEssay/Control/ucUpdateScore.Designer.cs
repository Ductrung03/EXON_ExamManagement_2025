
namespace EXON.GradedEssay.Control
{
     partial class ucUpdateScore
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
               this.groupBox1 = new System.Windows.Forms.GroupBox();
               this.groupBox3 = new System.Windows.Forms.GroupBox();
               this.gvMain = new System.Windows.Forms.DataGridView();
               this.QuestionID = new System.Windows.Forms.DataGridViewTextBoxColumn();
               this.TopicID = new System.Windows.Forms.DataGridViewTextBoxColumn();
               this.Score = new System.Windows.Forms.DataGridViewTextBoxColumn();
               this.groupBox2 = new System.Windows.Forms.GroupBox();
               this.groupBox3.SuspendLayout();
               ((System.ComponentModel.ISupportInitialize)(this.gvMain)).BeginInit();
               this.SuspendLayout();
               // 
               // groupBox1
               // 
               this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
               this.groupBox1.Location = new System.Drawing.Point(0, 0);
               this.groupBox1.Name = "groupBox1";
               this.groupBox1.Size = new System.Drawing.Size(1360, 16);
               this.groupBox1.TabIndex = 9;
               this.groupBox1.TabStop = false;
               this.groupBox1.Text = "Cập nhật điểm câu hỏi";
               // 
               // groupBox3
               // 
               this.groupBox3.Controls.Add(this.gvMain);
               this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
               this.groupBox3.Location = new System.Drawing.Point(0, 0);
               this.groupBox3.Name = "groupBox3";
               this.groupBox3.Size = new System.Drawing.Size(1360, 368);
               this.groupBox3.TabIndex = 11;
               this.groupBox3.TabStop = false;
               this.groupBox3.Text = "Danh sách thí sinh";
               // 
               // gvMain
               // 
               this.gvMain.AllowUserToAddRows = false;
               this.gvMain.AllowUserToDeleteRows = false;
               this.gvMain.AllowUserToOrderColumns = true;
               this.gvMain.BackgroundColor = System.Drawing.Color.White;
               this.gvMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
               this.gvMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.QuestionID,
            this.TopicID,
            this.Score});
               this.gvMain.Dock = System.Windows.Forms.DockStyle.Fill;
               this.gvMain.Location = new System.Drawing.Point(3, 16);
               this.gvMain.Name = "gvMain";
               this.gvMain.Size = new System.Drawing.Size(1354, 349);
               this.gvMain.TabIndex = 60;
               this.gvMain.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvMain_CellContentClick);
               // 
               // QuestionID
               // 
               this.QuestionID.DataPropertyName = "SubQuestionID";
               this.QuestionID.HeaderText = "SubQuestionID";
               this.QuestionID.Name = "QuestionID";
               // 
               // TopicID
               // 
               this.TopicID.DataPropertyName = "Level";
               this.TopicID.HeaderText = "Level";
               this.TopicID.Name = "TopicID";
               // 
               // Score
               // 
               this.Score.DataPropertyName = "Score";
               this.Score.HeaderText = "Score";
               this.Score.Name = "Score";
               // 
               // groupBox2
               // 
               this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
               this.groupBox2.Location = new System.Drawing.Point(0, 368);
               this.groupBox2.Name = "groupBox2";
               this.groupBox2.Size = new System.Drawing.Size(1360, 71);
               this.groupBox2.TabIndex = 10;
               this.groupBox2.TabStop = false;
               // 
               // ucUpdateScore
               // 
               this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
               this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
               this.Controls.Add(this.groupBox1);
               this.Controls.Add(this.groupBox3);
               this.Controls.Add(this.groupBox2);
               this.Name = "ucUpdateScore";
               this.Size = new System.Drawing.Size(1360, 439);
               this.groupBox3.ResumeLayout(false);
               ((System.ComponentModel.ISupportInitialize)(this.gvMain)).EndInit();
               this.ResumeLayout(false);

          }

          #endregion

          private System.Windows.Forms.GroupBox groupBox1;
          private System.Windows.Forms.GroupBox groupBox3;
          private System.Windows.Forms.DataGridView gvMain;
          private System.Windows.Forms.GroupBox groupBox2;
          private System.Windows.Forms.DataGridViewTextBoxColumn QuestionID;
          private System.Windows.Forms.DataGridViewTextBoxColumn TopicID;
          private System.Windows.Forms.DataGridViewTextBoxColumn Score;
     }
}
