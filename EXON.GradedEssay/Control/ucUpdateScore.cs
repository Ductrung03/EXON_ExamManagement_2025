using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EXON_EM.Data.Model;
using EXON_EM.Data.Service;
using EXON.SubModel.Models;
using EXON.SubData.Services;
using MetroFramework;

namespace EXON.GradedEssay.Control
{
     public partial class ucUpdateScore : UserControl
     {
          private ucLoad Loading = new ucLoad();
          public delegate void SendWorking(int value);
          SendWorking s;
          public EXON_DbContext db = new EXON_DbContext();
          public MTAQuizDbContext dbQuiz = new MTAQuizDbContext();
          private EXON.SubModel.Models.SHIFT shift = new EXON.SubModel.Models.SHIFT();
          private List<EXON.SubModel.Models.SHIFT> Shiftinfor = new List<EXON.SubModel.Models.SHIFT>();
          private Timer t = new Timer();
          private IContestantTestService _ContesttantTestService;
          private IAnswersheetDetailService _AnswersheetDetailService;
          private IAnswerService _AnswerService;
          private IAnswersheetService _AnswersheetService;
          private IContestantShiftService _ContestantShiftService;
          public ucUpdateScore(int Contestid, int LocationID)
          {
               InitializeComponent();
               gvMain.DataSource = dbQuiz.SUBQUESTIONS.ToList().Select(p => new
               {
                    SubQuestionID = p.SubQuestionID,
                    Level = p.QUESTION.Level
               }).ToList();
               UpdateScore();
          }
          public bool CheckExistQues()
          {
               var lst_QuestionQuiz = dbQuiz.SUBQUESTIONS.ToList().Select(p => new
               {
                    SubQuestionID = p.SubQuestionID,
                    Level = p.QUESTION.Level
               }).ToList();
               foreach(var item in lst_QuestionQuiz)
               {
                    if( new Subquestion_Service().Find(item.SubQuestionID) == null)
                    {
                         return false;
                    }     
               }
               return true;
          }
          public void UpdateScore()
          {
               if (CheckExistQues())
               {
                    
                         var lst_QuestionQuiz = dbQuiz.SUBQUESTIONS.ToList().Select(p => new
                         {
                              SubQuestionID = p.SubQuestionID,
                              QuestionID= p.QuestionID,
                              Level = p.QUESTION.Level,
                              Score = p.Score
                         }).ToList();
                         int sum = lst_QuestionQuiz.Count();
                         if (sum > 0)
                         {
                              Loading = new ucLoad();
                              Loading.TopMost = true;
                              // Form Loading 
                              s = new SendWorking(Loading.UpdateValue);
                              s(0);

                              Loading.Show();
                              int count = 0;
                              try
                              {
                                   foreach (var item in lst_QuestionQuiz)
                                   {
                                        count++;
                                        float Per = ((float)count / (float)sum) * 90;
                                        s((int)Per);
                                        
                                        EXON.SubModel.Models.SUBQUESTION sub = dbQuiz.SUBQUESTIONS.FirstOrDefault(i => i.SubQuestionID == item.SubQuestionID);
                                        EXON.SubModel.Models.TEST_DETAILS tl = dbQuiz.TEST_DETAILS.FirstOrDefault(i => i.QuestionID == item.QuestionID);
                                        EXON.SubModel.Models.TEST test = dbQuiz.TESTS.FirstOrDefault(i => i.TestID == tl.TestID);
                                        EXON.SubModel.Models.SUBJECT subject = dbQuiz.SUBJECTS.FirstOrDefault(i => i.SubjectID == test.SubjectID);
                                        EXON.SubModel.Models.SCHEDULE b = dbQuiz.SCHEDULES.FirstOrDefault(i => i.SubjectID == subject.SubjectID);

                                        int QuestionID = new Subquestion_Service().Find(item.SubQuestionID).QuestionID;
                                        EXON_EM.Data.Model.QUESTION Question = new Question_Service().Find(QuestionID);
                                        int TopicID = new Topic_Service().Find(Question.TopicID).TopicID;
                                        double Score = new StructureDetail_Service().Find(TopicID, Question.Level,b.ScheduleID).Score;
                                        sub.Score = Score;
                                        dbQuiz.SaveChanges();
                                   }
                                   if (count == sum)
                                   {
                                        s(100);
                                        Loading.Close();
                                        Loading.Dispose();
                                        MessageBox.Show("Cập nhật điểm thành công", "Thành Công");
                                   }
                                   UpdateScoreContestant();
                              
                              }
                              catch (Exception ex)
                              {
                                   MessageBox.Show(ex.ToString(), "Lỗi");
                              }
                         }
                         gvMain.DataSource = dbQuiz.SUBQUESTIONS.ToList().Select(p => new
                         {
                              SubQuestionID = p.SubQuestionID,
                              Level = p.QUESTION.Level,
                              Score = p.Score
                         }).ToList();
                    

               }
               else
               {
                    MetroFramework.MetroMessageBox.Show(this, String.Format("Tồn tại câu hỏi không có trên hệ thống"), "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }
          }
          public void UpdateScoreContestant()
          {
               int ansID;
               _ContesttantTestService = new ContestantTestService();
               _ContestantShiftService = new ContestantShiftService();
               _AnswersheetDetailService = new AnswersheetDetailService();
               _AnswerService = new AnswerService();
               _AnswersheetService = new AnswersheetService();
               List<EXON.SubModel.Models.CONTESTANTS_SHIFTS > lst = new List<EXON.SubModel.Models.CONTESTANTS_SHIFTS>();
               lst = _ContestantShiftService.GetAll().Where(x => x.Status == 3005).ToList();
               // CONTESTANTS_SHIFTS cs = new CONTESTANTS_SHIFTS();
               Loading = new ucLoad();
               Loading.TopMost = true;
               // Form Loading 
               s = new SendWorking(Loading.UpdateValue);
               s(0);

               Loading.Show();
               int count = 0;
               if(lst.Count>0)
               {
                    foreach (EXON.SubModel.Models.CONTESTANTS_SHIFTS cs in lst)
                    {
                         count++;
                         float Per = ((float)count / (float)lst.Count) * 90;
                         s((int)Per);
                         _AnswersheetService = new AnswersheetService();
                         _AnswersheetDetailService = new AnswersheetDetailService();
                         float SumSCore = 0;
                         EXON.SubModel.Models.ANSWERSHEET aws = _ContesttantTestService.GetByContestantShiftId(cs.ContestantShiftID).ANSWERSHEETS.SingleOrDefault();

                         if (aws != null)
                         {
                              List<EXON.SubModel.Models.ANSWERSHEET_DETAILS> lstaws = _AnswersheetDetailService.getAllByAnswerID(aws.AnswerSheetID).ToList();
                              EXON.SubModel.Models.ANSWER aw = new EXON.SubModel.Models.ANSWER();
                              foreach (EXON.SubModel.Models.ANSWERSHEET_DETAILS item in lstaws)
                              {
                                   ansID = item.ChoosenAnswer ?? default(int);
                                   aw = _AnswerService.GetbySubQuestionID(item.SubQuestionID, ansID);
                                   if (aw != null)
                                   {
                                        if (aw.IsCorrect)
                                        {
                                             SumSCore += (float)Math.Round(aw.SUBQUESTION.Score.Value, 2);
                                        }
                                   }
                              }

                              // cập nhập điểm cho thí sinh chưa hoàn thành nhưng có bài làm 
                              aws = _AnswersheetService.GetById(aws.AnswerSheetID);
                              aws.TestScores = Math.Round(SumSCore, 2);
                              _AnswersheetService.Update(aws);
                              _AnswersheetService.Save();

                         }
                         if (count == lst.Count)
                         {
                              s(100);
                              Loading.Close();
                              Loading.Dispose();
                              MessageBox.Show("Cập nhật điểm thành công", "Thành Công");
                         }
                    }
               }
               else
               {
                    Loading.Close();
                    Loading.Dispose();
                    MessageBox.Show("Cập nhật điểm thành công", "Thành Công");
               }
               
          }

          private void gvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
          {

          }
     }
}
