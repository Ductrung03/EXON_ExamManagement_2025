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

namespace EXON.MONITOR
{
     public partial class frmQuickUpdate : Form
     {
          EXON.SubModel.Models.MTAQuizDbContext db = new EXON.SubModel.Models.MTAQuizDbContext();
          public frmQuickUpdate()
          {
               InitializeComponent();
          }

          private void btnSave_Click(object sender, EventArgs e)
          {
               int level = Int32.Parse( comboBox1.Text);
               var lstQuestions = (
                                   from ques in db.QUESTIONS
                                   where ques.Level == level

                                   select new
                                   {
                                        ques.QuestionID,
                                        ques.Level
                                   }).Distinct().ToList();
               foreach (var item in lstQuestions)
               {
                    QUESTION question = db.QUESTIONS.Where(p => p.QuestionID == item.QuestionID).FirstOrDefault();
                    
                    List<SUBQUESTION> lst = db.SUBQUESTIONS.Where(p => p.QuestionID == item.QuestionID).ToList();

                    foreach (var item1 in lst)
                    {
                         item1.Score = Int32.Parse(textBox1.Text);
                    }
                    db.SaveChanges();
               }
               MessageBox.Show("Cập nhật thành công", "Thành Công");
          }

          private void label3_Click(object sender, EventArgs e)
          {

          }
     }
}
