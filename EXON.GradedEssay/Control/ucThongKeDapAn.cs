using EXON.SubData.Services;
using EXON.SubModel.Models;
using EXON.MONITOR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroFramework;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using EXON.Common;
using Word = Microsoft.Office.Interop.Word;

using System.Reflection;
using System.IO;
using System.Threading;
using Excel = Microsoft.Office.Interop.Excel;

namespace EXON.GradedEssay.Control
{
     public partial class ucThongKeDapAn : UserControl
     {

          private int _ContestID { get; set; }
          private int _LocationID { get; set; }
          public Color Black;
          MTAQuizDbContext db = new MTAQuizDbContext();
          public ucThongKeDapAn(int contestID, int LocationID)
          {
               _ContestID = contestID;
               _LocationID = LocationID;
               InitializeComponent();
               InitializeControl();
          }
          private void InitializeControl()
          {
               int index = 1;
               var listSubject = (from sb in db.SUBJECTS.ToList()
                                  select new
                                  {
                                       STT = index++,
                                       SubjectID = sb.SubjectID,
                                       SubjectCode = sb.SubjectCode,
                                       SubjectName = sb.SubjectName,
                                       PrintAnswer = "Thống Kê",
                                       OrigiTest = "Xem đề Thi Gốc"
                                  }).ToList();
               gvMain.DataSource = listSubject;
               cbx_MonThi.DataSource = listSubject;
               cbx_MonThi.DisplayMember = "SubjectName";
          }
          private void gvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
          {
               var senderGrid = (DataGridView)sender;
               try
               {
                    if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                    {
                         if (e.ColumnIndex == 4)
                         {
                              try
                              {
                                   frmProgress frm = new frmProgress(2);
                                   frm.Show();
                                   frm.UpdateValue2(1);
                                   int SubjectID = Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["cSubjectID"].Value.ToString());

                                   // Open printed test file
                                   string SubjectName = db.SUBJECTS.Where(p => p.SubjectID == SubjectID).SingleOrDefault().SubjectName;
                                   string FileNameOutput = System.Windows.Forms.Application.StartupPath + "\\Temp\\" + "\\" + "ThongKeDapAn" + "\\" + SubjectName.Trim() + ".docx";
                                   if (File.Exists(FileNameOutput))
                                        OpenFileAnsewer(SubjectName);
                                   else
                                   {
                                        ThongKeDapAn(SubjectID);
                                        OpenFileAnsewer(SubjectName);
                                   }
                                   frm.UpdateValue2(2);
                                   frm.Close();
                              }
                              catch (Exception ex)
                              {
                                   MetroFramework.MetroMessageBox.Show(this, ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error, 100);
                              }
                         }
                         else if (e.ColumnIndex == 5)
                         {
                              try
                              {
                                   frmProgress frm = new frmProgress(2);
                                   frm.Show();
                                   frm.UpdateValue2(1);
                                   int SubjectID = Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["cSubjectID"].Value.ToString());
                                   TEST originTest = db.TESTS.Where(p => p.SubjectID == SubjectID).FirstOrDefault();
                                   // Print test file
                                   PrintTest(originTest.TestID);
                                   frm.UpdateValue2(2);
                                   // Open printed test file
                                   string SubjectName = originTest.SUBJECT.SubjectName;
                                   string FileNameOutput = System.Windows.Forms.Application.StartupPath + "\\Temp\\" + "\\" + "InDeThi" + "\\" + SubjectName.Trim() + "\\" + originTest.TestID + ".docx";
                                   if (File.Exists(FileNameOutput))
                                        OpenFileAnsewer(originTest.TestID);
                                   frm.Close();
                              }
                              catch (Exception ex)
                              {
                                   MetroFramework.MetroMessageBox.Show(this, ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error, 100);
                              }
                         }
                    }
               }
               catch (Exception ex)
               {
               }
          }
          private void ThongKeDapAn(int SubjectID)
          {
               string SubjectName = db.SUBJECTS.Where(p => p.SubjectID == SubjectID).SingleOrDefault().SubjectName;
               string FileNameOutput = System.Windows.Forms.Application.StartupPath + "\\Temp\\" + "\\" + "ThongKeDapAn" + "\\" + SubjectName.Trim() + ".docx";
               string FolderNameOutput = System.Windows.Forms.Application.StartupPath + "\\Temp\\" + "\\" + "ThongKeDapAn" + "\\" + SubjectName.Trim() + "\\";

               if (!Directory.Exists(Path.Combine(FolderNameOutput.ToString())))
                    Directory.CreateDirectory(Path.Combine(Path.Combine(FolderNameOutput.ToString())));
               if (!File.Exists(FileNameOutput))
               {
                    PrintFile(FolderNameOutput, FileNameOutput, SubjectID);
               }
          }
          private void PrintFile(string FolderNameOutput, string FileNameOutput, int SubjectID)
          {
               CreateFileHead(FolderNameOutput, SubjectID);
               List<TEST> lsttest = db.TESTS.Where(p => p.SubjectID == SubjectID).ToList();
               int FirstTestID = lsttest[0].TestID;
               List<TEST_DETAILS> lstQuestions = db.TEST_DETAILS.Where(p => p.TestID == FirstTestID).ToList();
               int index = 0;
               foreach (var Ques in lstQuestions)
               {
                    QUESTION ques = db.QUESTIONS.Where(p => p.QuestionID == Ques.QuestionID).FirstOrDefault();
                    var lstSubquestions = (
                                      from sub in db.SUBQUESTIONS
                                      where sub.QuestionID == Ques.QuestionID
                                      select new
                                      {
                                           sub.SubQuestionID,
                                           sub.SubQuestionContent,
                                           sub.Score,
                                      }).Distinct().ToList();

                    if (ques.NumberSubQuestion == 1)
                    {
                         foreach (var item in lstSubquestions)
                         {
                              index++;
                              //string FileNameOutput2 = FolderNameOutput + item.SubQuestionID + ".docx";
                              //string FolderNameOutput2 = FolderNameOutput + item.SubQuestionID + "\\";

                              //if (!Directory.Exists(Path.Combine(FolderNameOutput2.ToString())))
                              //     Directory.CreateDirectory(Path.Combine(Path.Combine(FolderNameOutput2.ToString())));
                              string FileNameQA2 = FolderNameOutput + item.SubQuestionID + ".rtf";
                              TXTextControl.TextControl rtfSubQuestion = new TXTextControl.TextControl();
                              this.Controls.Add(rtfSubQuestion);
                              rtfSubQuestion.Font = new System.Drawing.Font("Times New Roman", 13, FontStyle.Bold);
                              float score = (float)Math.Round((double)item.Score, 2);
                              rtfSubQuestion.Text = string.Format("Câu hỏi {0} ({1} điểm): \n", index, score);
                              rtfSubQuestion.Select(rtfSubQuestion.Text.Length, 0);
                              // If question has only 1 subquestion, we print question's content here                             
                              rtfSubQuestion.Select(rtfSubQuestion.Text.Length, 0);
                              rtfSubQuestion.Append(item.SubQuestionContent, TXTextControl.StringStreamType.RichTextFormat, TXTextControl.AppendSettings.None);
                              rtfSubQuestion.Tables.Add(5, 3, 10);
                              rtfSubQuestion.Tables.GridLines = true;
                              TXTextControl.Table itable = rtfSubQuestion.Tables.GetItem(10);
                              rtfSubQuestion.Selection.Start = itable.Cells.GetItem(1, 1).Start - 1;
                              rtfSubQuestion.Selection.Length = itable.Cells.GetItem(1, 3).Start - 1
                                                             + itable.Cells.GetItem(1, 3).Length;
                              rtfSubQuestion.Selection.FontName = "Times New Roman";
                              rtfSubQuestion.Selection.FontSize = 240;
                              rtfSubQuestion.Selection.Baseline = 1;
                              rtfSubQuestion.Selection.ForeColor = System.Drawing.Color.Black;
                              rtfSubQuestion.Selection.ParagraphFormat.Alignment = TXTextControl.HorizontalAlignment.Center;
                              rtfSubQuestion.Selection.Length = 0;
                              itable.Cells.GetItem(1, 1).Text = "ĐÁP ÁN";
                              itable.Cells.GetItem(1, 2).Text = "SỐ THÍ SINH LỰA CHỌN";
                              itable.Cells.GetItem(1, 3).Text = "TỔNG SỐ THÍ SINH";

                              //Word.Application objApp = new Word.Application();
                              //object missing = Missing.Value;
                              //Word.Document objDoc = new Word.Document();
                              //object objMiss = System.Reflection.Missing.Value;
                              //object readOnly = false;
                              //// Path to template word file
                              //object pathfile = Constant.pathTempQuestionTable;
                              //objDoc = objApp.Documents.Open(ref pathfile, ref missing, ref readOnly,
                              //                           ref missing, ref missing, ref missing,
                              //                           ref missing, ref missing, ref missing,
                              //                           ref missing, ref missing, ref missing,
                              //                           ref missing, ref missing, ref missing, ref missing);
                              //objDoc.Activate();

                              List<ANSWER> ans = db.ANSWERS.Where(p => p.SubQuestionID == item.SubQuestionID).ToList();
                              int i = 2;
                              foreach (EXON.SubModel.Models.ANSWER an in ans)
                              {

                                   var lstCountestant = (from schedule in db.SCHEDULES
                                                         where schedule.SubjectID == SubjectID
                                                         from contestant in db.CONTESTANTS_SHIFTS
                                                         where contestant.ScheduleID == schedule.ScheduleID
                                                         select new
                                                         {
                                                              contestant.ContestantShiftID
                                                         }).ToList();
                                   int count = db.ANSWERSHEET_DETAILS.Where(p => p.ChoosenAnswer == an.AnswerID).Count();
                                   //string firstLetter = "";
                                   //switch (i)
                                   //{
                                   //     case 0:
                                   //          firstLetter = "A";
                                   //          break;
                                   //     case 1:
                                   //          firstLetter = "B";
                                   //          break;
                                   //     case 2:
                                   //          firstLetter = "C";
                                   //          break;
                                   //     case 3:
                                   //          firstLetter = "D";
                                   //          break;
                                   //}
                                   TXTextControl.TextControl rtfAnswer = new TXTextControl.TextControl();
                                   this.Controls.Add(rtfAnswer);
                                   rtfAnswer.Font = new System.Drawing.Font("Times New Roman", 13, FontStyle.Regular);
                                   rtfAnswer.Select(rtfAnswer.Text.Length, 0);
                                   rtfAnswer.Append(an.AnswerContent, TXTextControl.StringStreamType.RichTextFormat, TXTextControl.AppendSettings.None);

                                   string s;
                                   rtfAnswer.Save(out s, TXTextControl.StringStreamType.RichTextFormat);
                                   rtfAnswer.Load(s, TXTextControl.StringStreamType.RichTextFormat);

                                   TXTextControl.TableCell myCell1 = itable.Cells.GetItem(i, 1);
                                   myCell1.Select();
                                   myCell1.Text = rtfAnswer.Text;
                                   TXTextControl.TableCell myCell2 = itable.Cells.GetItem(i, 2);
                                   myCell2.Select();
                                   myCell2.Text = count.ToString();
                                   TXTextControl.TableCell myCell3 = itable.Cells.GetItem(i, 3);
                                   myCell3.Select();
                                   myCell3.Text = lstCountestant.Count().ToString();



                                   //string FileNameAnswer = FolderNameOutput2 + "DapAn" + "\\" + firstLetter +".rtf";
                                   //string FolderNameAnswer = FolderNameOutput2 + "DapAn" + "\\";

                                   //if (!Directory.Exists(Path.Combine(FolderNameAnswer.ToString())))
                                   //     Directory.CreateDirectory(Path.Combine(Path.Combine(FolderNameAnswer.ToString())));
                                   //TXTextControl.TextControl rtfAnswer = new TXTextControl.TextControl();
                                   //this.Controls.Add(rtfAnswer);
                                   //rtfAnswer.Select(rtfAnswer.Text.Length, 0);
                                   //rtfAnswer.Append(an.AnswerContent, TXTextControl.StringStreamType.RichTextFormat, TXTextControl.AppendSettings.None);
                                   //rtfAnswer.Save(FileNameAnswer, TXTextControl.StreamType.RichTextFormat);
                                   //Word.Application objAppA = new Word.Application();
                                   //object missingA = Missing.Value;

                                   //Word.Document objDocA = new Word.Document();

                                   //// Path to template word file
                                   //object pathfileA =FileNameAnswer;
                                   //objDocA = objAppA.Documents.Open(ref pathfileA, ref missing, ref readOnly,
                                   //                           ref missing, ref missing, ref missing,
                                   //                           ref missing, ref missing, ref missing,
                                   //                           ref missing, ref missing, ref missing,
                                   //                           ref missing, ref missing, ref missing, ref missing);
                                   //Word.Range oRange = objDocA.Content;
                                   //oRange.Copy();
                                   //object findText = "ANSWER" + firstLetter;
                                   //object replaceWithText = oRange;
                                   //objApp.Selection.Find.Execute(ref findText,
                                   //   ref replaceWithText);
                                   //objDocA.Close();
                                   i++;

                                   //string s;
                                   //rtfAnswer.Save(out s, TXTextControl.StringStreamType.RichTextFormat);

                                   //this.FindAndReplace(objApp, "ANSWER" + firstLetter, s);
                                   //this.FindAndReplace(objApp, "COUNT" + firstLetter, count.ToString());
                                   //this.FindAndReplace(objApp, "COUNT", lstCountestant.Count().ToString());

                              }

                              //object szPath = FolderNameOutput2  + item.SubQuestionID+ ".docx";
                              //objDoc.SaveAs(ref szPath, ref missing, ref missing, ref missing,
                              //   ref missing, ref missing, ref missing,
                              //   ref missing, ref missing, ref missing,
                              //   ref missing, ref missing, ref missing,
                              //   ref missing, ref missing, ref missing);
                              //objDoc.Close();
                              //MergeFile(FileNameOutput2, FolderNameOutput2);
                              //foreach(Process process in Process.GetProcessesByName("WINWORD"))
                              //{
                              //     process.Kill();
                              //}

                              rtfSubQuestion.Save(FileNameQA2, TXTextControl.StreamType.RichTextFormat);
                         }
                    }
                    else
                    {


                         string FileNameQA2 = FolderNameOutput + Ques.QuestionID + ".rtf";
                         TXTextControl.TextControl rtfQuestion = new TXTextControl.TextControl();
                         this.Controls.Add(rtfQuestion);
                         rtfQuestion.Font = new System.Drawing.Font("Times New Roman", 13, FontStyle.Bold);

                         rtfQuestion.Select(rtfQuestion.Text.Length, 0);
                         rtfQuestion.Append(ques.QuestionContent, TXTextControl.StringStreamType.RichTextFormat, TXTextControl.AppendSettings.None);
                         foreach (var item in lstSubquestions)
                         {
                              index++;
                              //string FileNameOutput2 = FolderNameOutput + item.SubQuestionID + ".docx";
                              //string FolderNameOutput2 = FolderNameOutput + item.SubQuestionID + "\\";

                              //if (!Directory.Exists(Path.Combine(FolderNameOutput2.ToString())))
                              //     Directory.CreateDirectory(Path.Combine(Path.Combine(FolderNameOutput2.ToString())));

                              TXTextControl.TextControl rtfSubQuestion = new TXTextControl.TextControl();
                              this.Controls.Add(rtfSubQuestion);
                              rtfSubQuestion.Font = new System.Drawing.Font("Times New Roman", 13, FontStyle.Bold);
                              float score = (float)Math.Round((double)item.Score, 2);
                              rtfSubQuestion.Text = string.Format("Câu hỏi {0} ({1} điểm): \n", index, score);
                              rtfSubQuestion.Select(rtfSubQuestion.Text.Length, 0);
                              // If question has only 1 subquestion, we print question's content here                             
                              rtfSubQuestion.Select(rtfSubQuestion.Text.Length, 0);
                              rtfSubQuestion.Append(item.SubQuestionContent, TXTextControl.StringStreamType.RichTextFormat, TXTextControl.AppendSettings.None);
                              rtfSubQuestion.Tables.Add(5, 3, 10);
                              rtfSubQuestion.Tables.GridLines = true;
                              TXTextControl.Table itable = rtfSubQuestion.Tables.GetItem(10);
                              rtfSubQuestion.Selection.Start = itable.Cells.GetItem(1, 1).Start - 1;
                              rtfSubQuestion.Selection.Length = itable.Cells.GetItem(1, 3).Start - 1
                                                             + itable.Cells.GetItem(1, 3).Length;
                              rtfSubQuestion.Selection.FontName = "Times New Roman";
                              rtfSubQuestion.Selection.FontSize = 240;
                              rtfSubQuestion.Selection.Baseline = 1;
                              rtfSubQuestion.Selection.ForeColor = System.Drawing.Color.Black;
                              rtfSubQuestion.Selection.ParagraphFormat.Alignment = TXTextControl.HorizontalAlignment.Center;
                              rtfSubQuestion.Selection.Length = 0;
                              itable.Cells.GetItem(1, 1).Text = "ĐÁP ÁN";
                              itable.Cells.GetItem(1, 2).Text = "SỐ THÍ SINH LỰA CHỌN";
                              itable.Cells.GetItem(1, 3).Text = "TỔNG SỐ THÍ SINH";

                              //Word.Application objApp = new Word.Application();
                              //object missing = Missing.Value;
                              //Word.Document objDoc = new Word.Document();
                              //object objMiss = System.Reflection.Missing.Value;
                              //object readOnly = false;
                              //// Path to template word file
                              //object pathfile = Constant.pathTempQuestionTable;
                              //objDoc = objApp.Documents.Open(ref pathfile, ref missing, ref readOnly,
                              //                           ref missing, ref missing, ref missing,
                              //                           ref missing, ref missing, ref missing,
                              //                           ref missing, ref missing, ref missing,
                              //                           ref missing, ref missing, ref missing, ref missing);
                              //objDoc.Activate();

                              List<ANSWER> ans = db.ANSWERS.Where(p => p.SubQuestionID == item.SubQuestionID).ToList();
                              int i = 2;
                              foreach (EXON.SubModel.Models.ANSWER an in ans)
                              {

                                   var lstCountestant = (from schedule in db.SCHEDULES
                                                         where schedule.SubjectID == SubjectID
                                                         from contestant in db.CONTESTANTS_SHIFTS
                                                         where contestant.ScheduleID == schedule.ScheduleID
                                                         select new
                                                         {
                                                              contestant.ContestantShiftID
                                                         }).ToList();
                                   int count = db.ANSWERSHEET_DETAILS.Where(p => p.ChoosenAnswer == an.AnswerID).Count();
                                   //string firstLetter = "";
                                   //switch (i)
                                   //{
                                   //     case 0:
                                   //          firstLetter = "A";
                                   //          break;
                                   //     case 1:
                                   //          firstLetter = "B";
                                   //          break;
                                   //     case 2:
                                   //          firstLetter = "C";
                                   //          break;
                                   //     case 3:
                                   //          firstLetter = "D";
                                   //          break;
                                   //}
                                   TXTextControl.TextControl rtfAnswer = new TXTextControl.TextControl();
                                   this.Controls.Add(rtfAnswer);
                                   rtfAnswer.Font = new System.Drawing.Font("Times New Roman", 13, FontStyle.Regular);
                                   rtfAnswer.Select(rtfAnswer.Text.Length, 0);
                                   rtfAnswer.Append(an.AnswerContent, TXTextControl.StringStreamType.RichTextFormat, TXTextControl.AppendSettings.None);

                                   string s;
                                   rtfAnswer.Save(out s, TXTextControl.StringStreamType.RichTextFormat);
                                   rtfAnswer.Load(s, TXTextControl.StringStreamType.RichTextFormat);
                                   TXTextControl.TableCell myCell1 = itable.Cells.GetItem(i, 1);
                                   myCell1.Select();
                                   myCell1.Text = rtfAnswer.Text;
                                   TXTextControl.TableCell myCell2 = itable.Cells.GetItem(i, 2);
                                   myCell2.Select();
                                   myCell2.Text = count.ToString();
                                   TXTextControl.TableCell myCell3 = itable.Cells.GetItem(i, 3);
                                   myCell3.Select();
                                   myCell3.Text = lstCountestant.Count().ToString();



                                   //string FileNameAnswer = FolderNameOutput2 + "DapAn" + "\\" + firstLetter +".rtf";
                                   //string FolderNameAnswer = FolderNameOutput2 + "DapAn" + "\\";

                                   //if (!Directory.Exists(Path.Combine(FolderNameAnswer.ToString())))
                                   //     Directory.CreateDirectory(Path.Combine(Path.Combine(FolderNameAnswer.ToString())));
                                   //TXTextControl.TextControl rtfAnswer = new TXTextControl.TextControl();
                                   //this.Controls.Add(rtfAnswer);
                                   //rtfAnswer.Select(rtfAnswer.Text.Length, 0);
                                   //rtfAnswer.Append(an.AnswerContent, TXTextControl.StringStreamType.RichTextFormat, TXTextControl.AppendSettings.None);
                                   //rtfAnswer.Save(FileNameAnswer, TXTextControl.StreamType.RichTextFormat);
                                   //Word.Application objAppA = new Word.Application();
                                   //object missingA = Missing.Value;

                                   //Word.Document objDocA = new Word.Document();

                                   //// Path to template word file
                                   //object pathfileA =FileNameAnswer;
                                   //objDocA = objAppA.Documents.Open(ref pathfileA, ref missing, ref readOnly,
                                   //                           ref missing, ref missing, ref missing,
                                   //                           ref missing, ref missing, ref missing,
                                   //                           ref missing, ref missing, ref missing,
                                   //                           ref missing, ref missing, ref missing, ref missing);
                                   //Word.Range oRange = objDocA.Content;
                                   //oRange.Copy();
                                   //object findText = "ANSWER" + firstLetter;
                                   //object replaceWithText = oRange;
                                   //objApp.Selection.Find.Execute(ref findText,
                                   //   ref replaceWithText);
                                   //objDocA.Close();
                                   i++;

                                   //string s;
                                   //rtfAnswer.Save(out s, TXTextControl.StringStreamType.RichTextFormat);

                                   //this.FindAndReplace(objApp, "ANSWER" + firstLetter, s);
                                   //this.FindAndReplace(objApp, "COUNT" + firstLetter, count.ToString());
                                   //this.FindAndReplace(objApp, "COUNT", lstCountestant.Count().ToString());

                              }

                              //object szPath = FolderNameOutput2  + item.SubQuestionID+ ".docx";
                              //objDoc.SaveAs(ref szPath, ref missing, ref missing, ref missing,
                              //   ref missing, ref missing, ref missing,
                              //   ref missing, ref missing, ref missing,
                              //   ref missing, ref missing, ref missing,
                              //   ref missing, ref missing, ref missing);
                              //objDoc.Close();
                              //MergeFile(FileNameOutput2, FolderNameOutput2);
                              //foreach(Process process in Process.GetProcessesByName("WINWORD"))
                              //{
                              //     process.Kill();
                              //}
                              string ss;
                              rtfSubQuestion.Save(out ss, TXTextControl.StringStreamType.RichTextFormat);
                              rtfQuestion.Select(rtfQuestion.Text.Length, 0);
                              rtfQuestion.Append(ss, TXTextControl.StringStreamType.RichTextFormat, TXTextControl.AppendSettings.None);
                         }
                         rtfQuestion.Save(FileNameQA2, TXTextControl.StreamType.RichTextFormat);
                    }

               }
               MergeFile(FileNameOutput, FolderNameOutput);
          }
          private void CreateFileHead(string FolderNameOutput, int SubjectID)
          {

               Word.Application objApp = new Word.Application();
               object missing = Missing.Value;
               Word.Document objDoc = new Word.Document();
               object objMiss = System.Reflection.Missing.Value;
               object readOnly = false;
               // Path to template word file
               object pathfile = Constant.pathTempThongKe;
               objDoc = objApp.Documents.Open(ref pathfile, ref missing, ref missing,
                                          ref missing, ref missing, ref missing,
                                          ref missing, ref missing, ref missing,
                                          ref missing, ref missing, ref missing,
                                          ref missing, ref missing, ref missing, ref missing);
               objDoc.Activate();
               string datenow = DatetimeConvert.GetDateTimeServer().ToString(@"\N\g\à\y dd \t\h\á\n\g MM \n\ă\m yyyy");

               SUBJECT subject = db.SUBJECTS.Where(p => p.SubjectID == SubjectID).SingleOrDefault();
               string subjectname = subject.SubjectName;
               string timeoftest = subject.SCHEDULES.FirstOrDefault().TimeOfTest.ToString() + " phút";
               this.FindAndReplace(objApp, "DATENOW", datenow);
               this.FindAndReplace(objApp, "SUBJECTNAME", subjectname.ToUpper());

               object szPath = FolderNameOutput + "\\" + "0000.docx";
               objDoc.SaveAs(ref szPath, ref missing, ref missing, ref missing,
                  ref missing, ref missing, ref missing,
                  ref missing, ref missing, ref missing,
                  ref missing, ref missing, ref missing,
                  ref missing, ref missing, ref missing);
               objDoc.Close();
          }
          private void FindAndReplace(Microsoft.Office.Interop.Word.Application wordApp, object findText, object replaceWithText)
          {
               object matchCase = true;
               object matchWholeWord = true;
               object matchWildCards = false;
               object matchSoundLike = false;
               object nmatchAllForms = false;
               object forward = true;
               object format = false;
               object matchKashida = false;
               object matchDiactitics = false;
               object matchAlefHamza = false;
               object matchControl = false;
               object read_only = false;
               object visible = true;
               object replace = 2;
               object wrap = 1;

               wordApp.Selection.Find.Execute(ref findText,
                           ref matchCase, ref matchWholeWord,
                           ref matchWildCards, ref matchSoundLike,
                           ref nmatchAllForms, ref forward,
                           ref wrap, ref format, ref replaceWithText,
                           ref replace, ref matchKashida,
                           ref matchDiactitics, ref matchAlefHamza,
                           ref matchControl);
          }
          private int MergeFile(string FileNameOutput, string FolderNameOutput)
          {

               object missing = System.Type.Missing;
               object pageBreak = Word.WdBreakType.wdSectionBreakNextPage;
               object outputFile = FileNameOutput;

               Word.Application wordApplication = new Word.Application();

               try
               {

                    Word.Document wordDocument = wordApplication.Documents.Add(
                                                  ref missing
                                                , ref missing
                                                , ref missing
                                                , ref missing);

                    // add header



                    Word.Selection selection = wordApplication.Selection;
                    selection.Font.Name = "Times New Roman";
                    DirectoryInfo info = new DirectoryInfo(FolderNameOutput);
                    string[] filesToMerge = info.GetFiles().OrderBy(p => p.CreationTime).Select(p => p.FullName).ToArray();
                    string[] documentsToMerge = filesToMerge;
                    int documentCount = filesToMerge.Length;


                    int breakStop = 0;
                    int LenghtFile = filesToMerge.Length;
                    int index = 0;

                    // Loop thru each of the Word documents
                    foreach (string file in filesToMerge)
                    {
                         breakStop++;
                         index++;
                         // Insert the files to our template
                         selection.InsertFile(file
                                                 , ref missing
                                                 , ref missing
                                                 , ref missing
                                                 , ref missing);
                    }
                    // Save the document to it's output file.
                    object read = true;


                    //Add footer at the right
                    //foreach (Microsoft.Office.Interop.Word.Section section in wordDocument.Sections)
                    //{

                    //    Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    //    headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
                    //    headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    //    headerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                    //    headerRange.Font.Size = 12;
                    //    headerRange.Text = "Số phách: ";
                    //}
                    //Add the footers into the document

                    foreach (Microsoft.Office.Interop.Word.Section wordSection in wordDocument.Sections)
                    {

                         Microsoft.Office.Interop.Word.Range footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                         footerRange.Fields.Add(footerRange, Type: Word.WdFieldType.wdFieldPage);
                         footerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlack;
                         footerRange.Font.Size = 12;
                         footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    }

                    wordDocument.SaveAs2(ref outputFile, ref missing, ref missing, ref missing,
                       ref missing, ref missing, read,
                       ref missing, ref missing, ref missing,
                       ref missing, ref missing, ref missing,
                       ref missing, ref missing, ref missing);
                    wordDocument.Close();


                    return 1;


               }
               catch (Exception ex)
               {

                    return -1;
               }
          }
          private void OpenFileAnsewer(string SubjectName)
          {

               Word.Application objApp = new Word.Application();
               object missing = Missing.Value;
               objApp.Visible = false;
               Word.Document objDoc = new Word.Document();
               object objMiss = System.Reflection.Missing.Value;

               object readOnly = false;
               if (!Directory.Exists(Path.Combine(System.Windows.Forms.Application.StartupPath + "\\Temp\\", "ThongKeDapAn" + "\\" + SubjectName.Trim().ToString())))
               {
                    MessageBox.Show("Chưa tạo bài làm");
                    return;
               }
               object szPath = System.Windows.Forms.Application.StartupPath + "\\Temp\\" + "\\" + "ThongKeDapAn" + "\\" + SubjectName.Trim() + ".docx";
               objDoc = objApp.Documents.Open(ref szPath, ref missing, ref readOnly,
                                         ref missing, ref missing, ref missing,
                                         ref missing, ref missing, ref missing,
                                         ref missing, ref missing, ref missing,
                                         ref missing, ref missing, ref missing, ref missing);
               objDoc.Activate();

               objApp.Documents.Open(ref szPath, ref missing, ref readOnly,
                                          ref missing, ref missing, ref missing,
                                          ref missing, ref missing, ref missing,
                                          ref missing, ref missing, ref missing,
                                          ref missing, ref missing, ref missing, ref missing);
          }
          private void PrintTest(int testID)
          {
               EXON.SubModel.PrintTestHelper printtesthelper = new SubModel.PrintTestHelper();
               List<EXON.SubModel.Question2Print> lst_ques2print = printtesthelper.GetListQuestion3Print(testID);

               string SubjectName = db.TESTS.Where(p => p.TestID == testID).SingleOrDefault().SUBJECT.SubjectName;
               string FileNameOutput = System.Windows.Forms.Application.StartupPath + "\\Temp\\" + "\\" + "InDeThi" + "\\" + SubjectName.Trim() + "\\" + testID + ".docx";
               string FolderNameOutput = System.Windows.Forms.Application.StartupPath + "\\Temp\\" + "\\" + "InDeThi" + "\\" + SubjectName.Trim() + "\\" + testID + "\\";

               if (!Directory.Exists(Path.Combine(FolderNameOutput.ToString())))
                    Directory.CreateDirectory(Path.Combine(Path.Combine(FolderNameOutput.ToString())));
               if (!File.Exists(FileNameOutput))
               {
                    Print2File(lst_ques2print, FolderNameOutput, FileNameOutput, testID);
               }
          }
          private void Print2File(List<EXON.SubModel.Question2Print> lst_ques2print, string FolderNameOutput, string FileNameOutput, int testID)
          {
               CreateFileHeadTest(FolderNameOutput, testID);
               foreach (EXON.SubModel.Question2Print ques2print in lst_ques2print)
               {
                    string FileNameQA = FolderNameOutput + ques2print.QuestionID + ".rtf";
                    TXTextControl.TextControl rtfQuestion = new TXTextControl.TextControl();
                    this.Controls.Add(rtfQuestion);
                    rtfQuestion.Select(rtfQuestion.Text.Length, 0);
                    if (ques2print.NumberOfSubquestion > 1)
                    {
                         rtfQuestion.Append(ques2print.QuestionContent, TXTextControl.StringStreamType.RichTextFormat, TXTextControl.AppendSettings.None);
                    }
                    // If Question has only 1 subquestion, then we treat it as a subquestion
                    // Otherwise, we print question's content as normals                       
                    foreach (EXON.SubModel.SubQuestion2Print subques2print in ques2print.lst_subquestions)
                    {

                         TXTextControl.TextControl rtfSubQuestion = new TXTextControl.TextControl();
                         this.Controls.Add(rtfSubQuestion);
                         rtfSubQuestion.Font = new System.Drawing.Font("Times New Roman", 13, FontStyle.Bold);
                         float score = (float)Math.Round(subques2print.Score, 2);
                         rtfSubQuestion.Text = string.Format("Câu hỏi {0} ({1} điểm): \n", subques2print.No, score);
                         rtfSubQuestion.Select(rtfSubQuestion.Text.Length, 0);
                         // If question has only 1 subquestion, we print question's content here                             
                         rtfSubQuestion.Select(rtfSubQuestion.Text.Length, 0);
                         rtfSubQuestion.Append(subques2print.SubQuestionContent, TXTextControl.StringStreamType.RichTextFormat, TXTextControl.AppendSettings.None);
                         // If question's type is Writting or Rewriting or Fill or Fill Audio, then we don't print its answers.
                         // Otherwise, we print its answer as normal
                         if (ques2print.Type != (int)QuizTypeEnum.Essay && ques2print.Type != (int)QuizTypeEnum.Fill && ques2print.Type != (int)QuizTypeEnum.FillAudio && ques2print.Type != (int)QuizTypeEnum.ReWritting)
                         {
                              int i = 0;
                              foreach (EXON.SubModel.Answer2Print ans2print in subques2print.lst_answers)
                              {
                                   string firstLetter = "";
                                   switch (i)
                                   {
                                        case 0:
                                             firstLetter = "A.";
                                             break;
                                        case 1:
                                             firstLetter = "B.";
                                             break;
                                        case 2:
                                             firstLetter = "C.";
                                             break;
                                        case 3:
                                             firstLetter = "D.";
                                             break;
                                        default:
                                             firstLetter = "";
                                             break;
                                   }

                                   TXTextControl.TextControl rtfAnswer = new TXTextControl.TextControl();
                                   this.Controls.Add(rtfAnswer);
                                   rtfAnswer.Font = new System.Drawing.Font("Times New Roman", 13, FontStyle.Regular);
                                   rtfAnswer.Select(rtfAnswer.Text.Length, 0);
                                   rtfAnswer.Append(firstLetter, TXTextControl.StringStreamType.PlainText, TXTextControl.AppendSettings.None);
                                   rtfAnswer.Select(rtfAnswer.Text.Length, 0);
                                   rtfAnswer.Append(ans2print.AnswerContent, TXTextControl.StringStreamType.RichTextFormat, TXTextControl.AppendSettings.None);

                                   i++;

                                   rtfSubQuestion.Select(rtfSubQuestion.Text.Length, 0);
                                   string s;
                                   rtfAnswer.Save(out s, TXTextControl.StringStreamType.RichTextFormat);
                                   rtfSubQuestion.Append(s, TXTextControl.StringStreamType.RichTextFormat, TXTextControl.AppendSettings.None);
                              }
                         }
                         if (ques2print.Type == (int)QuizTypeEnum.Essay)

                         {
                              TXTextControl.TextControl rtfAnswer = new TXTextControl.TextControl();
                              this.Controls.Add(rtfAnswer);
                              rtfAnswer.Text = "..............................................................................." +
                                               "..............................................................................." +
                                               "..............................................................................." +
                                               "..............................................................................." +
                                               "..............................................................................." +
                                               "..............................................................................." +
                                               "..............................................................................." +
                                               "..............................................................................." +
                                               "..............................................................................." +
                                               "..............................................................................." +
                                               "..............................................................................." +
                                               "..............................................................................." +
                                               "...............................................";
                              rtfSubQuestion.Select(rtfSubQuestion.Text.Length, 0);
                              string s;
                              rtfAnswer.Save(out s, TXTextControl.StringStreamType.RichTextFormat);
                              rtfSubQuestion.Append(s, TXTextControl.StringStreamType.RichTextFormat, TXTextControl.AppendSettings.None);
                         }

                         rtfQuestion.Select(rtfQuestion.Text.Length, 0);
                         string s1;
                         rtfSubQuestion.Save(out s1, TXTextControl.StringStreamType.RichTextFormat);
                         rtfQuestion.Append(s1, TXTextControl.StringStreamType.RichTextFormat, TXTextControl.AppendSettings.None);
                    }

                    rtfQuestion.Save(FileNameQA, TXTextControl.StreamType.RichTextFormat);


               }
               MergeFile(FileNameOutput, FolderNameOutput);
          }
          private void CreateFileHeadTest(string FolderNameOutput, int testID)
          {

               Word.Application objApp = new Word.Application();
               object missing = Missing.Value;
               Word.Document objDoc = new Word.Document();
               object objMiss = System.Reflection.Missing.Value;
               object readOnly = false;
               // Path to template word file
               object pathfile = Constant.pathTempPrintTest;
               objDoc = objApp.Documents.Open(ref pathfile, ref missing, ref missing,
                                          ref missing, ref missing, ref missing,
                                          ref missing, ref missing, ref missing,
                                          ref missing, ref missing, ref missing,
                                          ref missing, ref missing, ref missing, ref missing);
               objDoc.Activate();
               string datenow = DatetimeConvert.GetDateTimeServer().ToString(@"\N\g\à\y dd \t\h\á\n\g MM \n\ă\m yyyy");
               TEST test = db.TESTS.Where(p => p.TestID == testID).SingleOrDefault();
               string subjectname = test.SUBJECT.SubjectName;
               string contestname = test.BAGOFTEST.DIVISION_SHIFTS.SHIFT.CONTEST.ContestName;
               string timeoftest = test.SUBJECT.SCHEDULES.FirstOrDefault().TimeOfTest.ToString() + " phút";
               this.FindAndReplace(objApp, "DATENOW", datenow);
               this.FindAndReplace(objApp, "SUBJECTNAME", subjectname.ToUpper());
               this.FindAndReplace(objApp, "TESTID", testID.ToString());
               this.FindAndReplace(objApp, "TIMEOFTEST", timeoftest);

               object szPath = FolderNameOutput + "\\" + "0000.docx";
               objDoc.SaveAs(ref szPath, ref missing, ref missing, ref missing,
                  ref missing, ref missing, ref missing,
                  ref missing, ref missing, ref missing,
                  ref missing, ref missing, ref missing,
                  ref missing, ref missing, ref missing);
               objDoc.Close();
          }

          private void OpenFileAnsewer(int testID)
          {

               Word.Application objApp = new Word.Application();
               object missing = Missing.Value;
               objApp.Visible = false;
               Word.Document objDoc = new Word.Document();
               object objMiss = System.Reflection.Missing.Value;

               object readOnly = false;
               string SubjectName = db.TESTS.Where(p => p.TestID == testID).SingleOrDefault().SUBJECT.SubjectName;
               if (!Directory.Exists(Path.Combine(System.Windows.Forms.Application.StartupPath + "\\Temp\\", "InDeThi" + "\\" + SubjectName.Trim().ToString())))
               {
                    MessageBox.Show("Chưa tạo bài làm");
                    return;
               }
               object szPath = System.Windows.Forms.Application.StartupPath + "\\Temp\\" + "InDeThi" + "\\" + SubjectName.Trim() + "\\" + testID + ".docx";
               objDoc = objApp.Documents.Open(ref szPath, ref missing, ref readOnly,
                                         ref missing, ref missing, ref missing,
                                         ref missing, ref missing, ref missing,
                                         ref missing, ref missing, ref missing,
                                         ref missing, ref missing, ref missing, ref missing);
               objDoc.Activate();

               objApp.Documents.Open(ref szPath, ref missing, ref readOnly,
                                          ref missing, ref missing, ref missing,
                                          ref missing, ref missing, ref missing,
                                          ref missing, ref missing, ref missing,
                                          ref missing, ref missing, ref missing, ref missing);
          }

          private void btnPrintResult_Click(object sender, EventArgs e)
          {
               if (cbx_MonThi.Text != "")
               {
                    frmProgress frm = new frmProgress(2);
                    frm.Show();
                    frm.UpdateValue2(1);
                    try
                    {
                         
                         SUBJECT subject = db.SUBJECTS.Where(p => p.SubjectName == cbx_MonThi.Text).FirstOrDefault();
                         if (subject != null)
                         {
                              TEST originTest = db.TESTS.Where(p => p.SubjectID == subject.SubjectID).FirstOrDefault();
                              EXON.SubModel.PrintTestHelper printtesthelper = new SubModel.PrintTestHelper();
                              List<EXON.SubModel.Question2Print> lst_ques2print = printtesthelper.GetListQuestion3Print(originTest.TestID);
                              
                              var lstContestantShift = (from schedule in db.SCHEDULES
                                                    where schedule.SubjectID == subject.SubjectID
                                                    from contestant in db.CONTESTANTS_SHIFTS
                                                    where contestant.ScheduleID == schedule.ScheduleID
                                                    select new
                                                    {
                                                         contestant.CONTESTANT.ContestantCode,
                                                         contestant.ContestantShiftID
                                                    }).ToList();
                              //gvMain.DataSource = null;
                              //gvMain.Rows.Clear();
                              System.Data.DataTable ListAnswerSheet = new System.Data.DataTable();
                              int No = 0;
                              foreach (EXON.SubModel.Question2Print ques2print in lst_ques2print)
                              {
                                   foreach (EXON.SubModel.SubQuestion2Print subques2print in ques2print.lst_subquestions)
                                   {
                                        if (No < subques2print.No)
                                             No = subques2print.No;
                                   } 
                              }
                              DataColumn dc = new DataColumn("SBD", typeof(String));
                              ListAnswerSheet.Columns.Add(dc);
                              for (int i = 1; i<=No; i++)
                              {
                                   DataColumn dc1 = new DataColumn("Câu"+ i.ToString(), typeof(String));
                                   ListAnswerSheet.Columns.Add(dc1);
                              }
                              
                              DataRow drHead = ListAnswerSheet.NewRow();
                              drHead[0] = "Số Báo Danh";
                              for (int i = 1; i <= No; i++)
                              {
                                   drHead[i] = "Câu" + i.ToString();
                              }
                              ListAnswerSheet.Rows.Add(drHead);
                              
                              foreach (var contestant in lstContestantShift)
                              {
                                   
                                        var answersheetID = (from contesttant_test in db.CONTESTANTS_TESTS
                                                             where contesttant_test.ContestantShiftID == contestant.ContestantShiftID
                                                             from anwsheet in db.ANSWERSHEETS
                                                             where anwsheet.ContestantTestID == contesttant_test.ContestantTestID
                                                             select new
                                                             {
                                                                  anwsheet.AnswerSheetID
                                                             }
                                                           ).SingleOrDefault();
                                        List<string> AnswerSheet = new List<string>();
                                   if (answersheetID == null)
                                   {
                                        DataRow dr = ListAnswerSheet.NewRow();
                                        dr[0] = contestant.ContestantCode;
                                        for (int i = 1; i <= No; i++)
                                        {
                                             dr[i] = null;
                                        }
                                        ListAnswerSheet.Rows.Add(dr);
                                   }
                                   else
                                   {
                                        foreach (EXON.SubModel.Question2Print ques2print in lst_ques2print)
                                        {
                                             foreach (EXON.SubModel.SubQuestion2Print subques2print in ques2print.lst_subquestions)
                                             {
                                                  ANSWERSHEET_DETAILS AnswerSheetdt = db.ANSWERSHEET_DETAILS.Where(p => p.AnswerSheetID == answersheetID.AnswerSheetID && p.SubQuestionID == subques2print.SubQuestionID).FirstOrDefault();
                                                  if (AnswerSheetdt != null)
                                                  {
                                                       ANSWER answer = db.ANSWERS.Where(p => p.AnswerID == AnswerSheetdt.ChoosenAnswer).SingleOrDefault();
                                                       EXON.SubModel.Answer2Print answer2 = new SubModel.Answer2Print();
                                                       answer2.AnswerID = answer.AnswerID;
                                                       answer2.AnswerContent = answer.AnswerContent;
                                                       answer2.IsCorrect = answer.IsCorrect;
                                                       List<int> ListID = new List<int>();
                                                       foreach (var item in subques2print.lst_answers)
                                                       {
                                                            ListID.Add(item.AnswerID);
                                                       }
                                                       int IndexID = ListID.IndexOf(answer2.AnswerID);
                                                       switch (IndexID)
                                                       {
                                                            case 0:
                                                                 AnswerSheet.Add("A");
                                                                 break;
                                                            case 1:
                                                                 AnswerSheet.Add("B");
                                                                 break;
                                                            case 2:
                                                                 AnswerSheet.Add("C");
                                                                 break;
                                                            case 3:
                                                                 AnswerSheet.Add("D");
                                                                 break;
                                                            default:
                                                                 AnswerSheet.Add(" ");
                                                                 break;
                                                       }
                                                  }
                                                  else
                                                  {
                                                       AnswerSheet.Add(" ");
                                                  }

                                             }
                                        }
                                        DataRow dr = ListAnswerSheet.NewRow();
                                        dr[0] = contestant.ContestantCode;
                                        for (int i = 1; i <= No; i++)
                                        {
                                             dr[i] = AnswerSheet[i - 1];
                                        }
                                        ListAnswerSheet.Rows.Add(dr);
                                   }
                                        
                                   
                              }
                              Export(ListAnswerSheet, "Thống Kê Đáp Án", "Thống Kê");
                              frm.UpdateValue2(2);
                              frm.Close();

                         }
                         else { MetroFramework.MetroMessageBox.Show(this, "Không tồn tại môn thi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error, 100); }
                    }
                    catch (Exception ex)
                    {
                         MetroFramework.MetroMessageBox.Show(this, ex.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error, 100);
                         frm.UpdateValue2(2);
                         frm.Close();
                    }
               }
               else
               {

                    MetroFramework.MetroMessageBox.Show(this, "Chưa chọn môn thi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error, 100);
               }
          }
          public void Export(System.Data.DataTable dt, string sheetName, string title)
          {

               //Tạo các đối tượng Excel

               Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();

               Microsoft.Office.Interop.Excel.Workbooks oBooks;

               Microsoft.Office.Interop.Excel.Sheets oSheets;

               Microsoft.Office.Interop.Excel.Workbook oBook;

               Microsoft.Office.Interop.Excel.Worksheet oSheet;

               //Tạo mới một Excel WorkBook 

               oExcel.Visible = true;

               oExcel.DisplayAlerts = false;

               oExcel.Application.SheetsInNewWorkbook = 1;

               oBooks = oExcel.Workbooks;

               oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));

               oSheets = oBook.Worksheets;

               oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);

               oSheet.Name = sheetName;

               // Tạo phần đầu nếu muốn

               //Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "C1");

               //head.MergeCells = true;

               //head.Value2 = title;

               //head.Font.Bold = true;

               //head.Font.Name = "Tahoma";

               //head.Font.Size = "18";

               //head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

               // Tạo tiêu đề cột 

               //Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");

               //cl1.Value2 = "Số Báo Danh";

               //cl1.ColumnWidth = 13.5;

               //Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");

               //cl2.Value2 = "Tên đơn vị";

               //cl2.ColumnWidth = 25.0;

               //Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");

               //cl3.Value2 = "Chức năng";

               //cl3.ColumnWidth = 40.0;

               //Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "C3");

               //rowHead.Font.Bold = true;

               // Kẻ viền

               //rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

               //// Thiết lập màu nền

               //rowHead.Interior.ColorIndex = 15;

               //rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

               // Tạo mẳng đối tượng để lưu dữ toàn bồ dữ liệu trong DataTable,

               // vì dữ liệu được được gán vào các Cell trong Excel phải thông qua object thuần.

               object[,] arr = new object[dt.Rows.Count, dt.Columns.Count];

               //Chuyển dữ liệu từ DataTable vào mảng đối tượng

               for (int r = 0; r < dt.Rows.Count; r++)

               {

                    DataRow dr = dt.Rows[r];

                    for (int c = 0; c < dt.Columns.Count; c++)

                    {
                         arr[r, c] = dr[c];
                    }
               }

               //Thiết lập vùng điền dữ liệu

               int rowStart = 1;

               int columnStart = 1;

               int rowEnd = rowStart + dt.Rows.Count-1;

               int columnEnd = dt.Columns.Count;

               // Ô bắt đầu điền dữ liệu

               Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];

               // Ô kết thúc điền dữ liệu

               Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];

               // Lấy về vùng điền dữ liệu

               Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);

               //Điền dữ liệu vào vùng đã thiết lập

               range.Value2 = arr;

               // Kẻ viền

               range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

               // Căn giữa cột STT

               Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnStart];

               Microsoft.Office.Interop.Excel.Range c4 = oSheet.get_Range(c1, c3);

               oSheet.get_Range(c3, c4).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
          }


     }
}
