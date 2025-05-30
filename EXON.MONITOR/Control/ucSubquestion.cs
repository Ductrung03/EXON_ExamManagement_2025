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
     public partial class ucSubquestion : UserControl
     {
          EXON.SubModel.Models.MTAQuizDbContext db = new EXON.SubModel.Models.MTAQuizDbContext();
          int _SubquestionID;
          int _Width;
          public ucSubquestion(int SubquestionID,int Width)
          {
               InitializeComponent();
               _SubquestionID = SubquestionID;
               _Width = Width;
          }

          private void ucSubquestion_Load(object sender, EventArgs e)
          {
               
               SUBQUESTION subques = db.SUBQUESTIONS.Where(p => p.SubQuestionID == _SubquestionID).FirstOrDefault();
               rtbTitleOfSubQuestion.Load(subques.SubQuestionContent, TXTextControl.StringStreamType.RichTextFormat);
               rtbTitleOfSubQuestion.Width = 835;
               rtbTitleOfSubQuestion.Height = (int)subques.HeightToDisplay + 5 ;
               rtbTitleOfSubQuestion.AutoControlSize.AutoExpand = TXTextControl.AutoSizeDirection.Both;
               rtbTitleOfSubQuestion.Update();
               pnTitleOfSubQuestion.Width = 835;
               pnTitleOfSubQuestion.Height = rtbTitleOfSubQuestion.Bottom + 5;
               pnTitleOfSubQuestion.AutoScroll = true;
               pnTitleOfSubQuestion.Update();
               this.Height = pnTitleOfSubQuestion.Bottom + 5;
               lbNumber.Text = _SubquestionID.ToString();
               this.Update();
          }
     }
}
