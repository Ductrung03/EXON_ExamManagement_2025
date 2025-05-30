using EXON.SubData;
using EXON.SubModel.Models;
using EXONSYSTEM;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Drawing.Html;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using EXON.SubData.Services;
using EXON.SubModel.Models;

namespace EXON.MONITOR.Control
{
     public partial class ucQuestion : UserControl
     {
          EXON.SubModel.Models.MTAQuizDbContext db = new EXON.SubModel.Models.MTAQuizDbContext();
          int _QuestionID;
          int _Width;
          public ucQuestion(int QuestionID,int width)
          {
               InitializeComponent();
               _QuestionID = QuestionID;
               _Width = width;
          }

          private void ucQuestion_Load(object sender, EventArgs e)
          {
               
               QUESTION ques = db.QUESTIONS.Where(p => p.QuestionID == _QuestionID).FirstOrDefault();
               rtbTitleOfQuestion.Width = 835;
               pnTitleOfQuestion.Width = 835;
               pnTitleOfQuestion.Height = rtbTitleOfQuestion.Bottom + 5;
               pnTitleOfQuestion.AutoScroll = true;
               pnTitleOfQuestion.Update();
               if (ques.QuestionContent != null)
               {
                    rtbTitleOfQuestion.Load(ques.QuestionContent, TXTextControl.StringStreamType.RichTextFormat);
                    
                    TRichTextBox.AdvanRichTextBox temp = new TRichTextBox.AdvanRichTextBox();
                    temp.Rtf = ques.QuestionContent;
                    rtbTitleOfQuestion.Height = temp.Height;
               }
               lbNumber.Text = ques.QuestionID.ToString();
               this.Update();
               string comboBoxName = "cb" + _QuestionID.ToString();
               

          }

          private void flpnListOfQuestions_Paint(object sender, PaintEventArgs e)
          {

          }

          private void label3_Click(object sender, EventArgs e)
          {

          }
     }
}
