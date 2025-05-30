using EXON.SubData.Services;
using EXON.SubModel.Models;
using MetroFramework.Forms;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using EXON.Common;
using Word = Microsoft.Office.Interop.Word;

using System.IO;
using System.Drawing;
using EXON.SubModel.Models;

namespace EXON.GradedEssay.Control
{
     public partial class ucPrintTest : UserControl
     {
          MTAQuizDbContext db = new MTAQuizDbContext();
          private ucLoad Loading = new ucLoad();
          public delegate void SendWorking(int value);
          private List<ListAnwser> listans;
          private List<Question> lstQuestion;
          private Word.Application objApp;
          private Word.Document objDoc;
          private object pathFileForAnswer = Constant.pathTempSpeaking;
          private object pathFileTemp = Constant.pathTempAnswer;
          SendWorking s;
          private object pathFile = Constant.pathTemp;
          //private DIVISION_SHIFTS ds;

          #region Service

          private IContestService _ContestService;
          private IShiftService _ShiftService;
          private IScheduleService _ScheduleService;
          private ISubjectService _SubjectService;
          private IStaffService _StaffService;
          private IDivisionShiftService _DivisionShiftService;
          private IContestantShiftService _ContestantShiftService;
          private ITestNumberService _TestNumberService;
          private IContestantTestService _ContestantTestService;
          private IAnswersheetService _AnswersheetService;
          private IAnswersheetWritingService _AnswersheetWritingService;
          private ITestService _TestService;
          private IRoomTestService _RoomTestService;
          private IAnswersheetDetailService _AnswersheetDetailService;
          private IAnswersheetSpeakingService _AnswersheetSpeakingService;
          private IAnswerService _AnswerService;
          #endregion Service
          private int _ContestID { get; set; }
          private int _LocationID { get; set; }
          private int SubjectID { get; set; }
          private int CurrentTeacherName1
          {
               get
               {
                    try
                    {
                         return int.Parse(cbStaff1.SelectedValue.ToString());
                    }
                    catch { return -1; }
               }
          }
          private int CurrentTeacherName2
          {
               get
               {
                    try
                    {
                         return int.Parse(cbStaff2.SelectedValue.ToString());
                    }
                    catch { return -1; }
               }
          }

          private int CurrentShiftID
          {
               get
               {
                    try
                    {
                         return int.Parse(cbShift.SelectedValue.ToString());
                    }
                    catch { return -1; }
               }
          }
          private int CurrentSubjectID
          {
               get
               {
                    try
                    {
                         return int.Parse(cbSubject.SelectedValue.ToString());
                    }
                    catch { return -1; }
               }
          }
          private int CurrentStaff1
          {
               get
               {
                    try
                    {
                         return int.Parse(cbStaff1.SelectedValue.ToString());
                    }
                    catch { return -1; }
               }
          }
          private int CurrentStaff2
          {
               get
               {
                    try
                    {
                         return int.Parse(cbStaff2.SelectedValue.ToString());
                    }
                    catch { return -1; }
               }
          }
          private int CurrentRoomTestID
          {
               get
               {
                    try
                    {
                         return int.Parse(cbRoomTest.SelectedValue.ToString());
                    }
                    catch { return -1; }
               }
          }


          public ucPrintTest(int contestID, int LocationID)
          {
               _ContestID = contestID;
               _LocationID = LocationID;
               InitializeComponent();
               InitializeService();
               InitializeControl();
               db = new MTAQuizDbContext();
          }
          private void InitializeService()
          {

               _ContestService = new ContestService();
               _ShiftService = new ShiftService();
               _ScheduleService = new ScheduleService();
               _SubjectService = new SubjectService();
               _DivisionShiftService = new DivisionShiftService();
               _ContestantShiftService = new ContestantShiftService();
               _TestNumberService = new TestNumberService();
               _StaffService = new StaffService();
               _ContestantTestService = new ContestantTestService();
               _AnswersheetService = new AnswersheetService();
               _AnswersheetWritingService = new AnswersheetWritingService();
               _TestService = new TestService();
               _RoomTestService = new RoomTestService();
          }

          private void InitializeControl()
          {
               try
               {

                    _ShiftService = new ShiftService();
                    _StaffService = new StaffService();
                    _ScheduleService = new ScheduleService();
                    _SubjectService = new SubjectService();
                    _ContestantShiftService = new ContestantShiftService();
                    _DivisionShiftService = new DivisionShiftService();
                    cbShift.DataSource = (
                                            from ds in _DivisionShiftService.GetAll().Where(x => x.ROOMTEST.LocationID == _LocationID)
                                            from s in _ShiftService.GetAll(_ContestID).Where(x => x.Status > 0)
                                            where ds.ShiftID == s.ShiftID
                                            select s).ToList();
                    cbShift.DisplayMember = "ShiftName";
                    cbShift.ValueMember = "ShiftID";

                    try
                    {
                         List<STAFF> listStaff = _StaffService.GetAll().ToList();

                         cbStaff1.DataSource = listStaff;
                         cbStaff1.DisplayMember = "FullName";
                         cbStaff1.ValueMember = "StaffID";
                         List<STAFF> listStaff2 = _StaffService.GetAll().ToList();
                         cbStaff2.DataSource = listStaff2;
                         cbStaff2.DisplayMember = "FullName";
                         cbStaff2.ValueMember = "StaffID";
                    }
                    catch
                    {
                         MessageBox.Show("Không Lấy Được Dữ Liệu Giáo Viên.", "Thông Báo");

                    }

               }
               catch (Exception ex)
               { }

          }

          private void cbShift_SelectedValueChanged(object sender, EventArgs e)
          {
               _ShiftService = new ShiftService();
               _StaffService = new StaffService();
               _ScheduleService = new ScheduleService();
               _SubjectService = new SubjectService();
               _ContestantShiftService = new ContestantShiftService();
               _DivisionShiftService = new DivisionShiftService();

               cbRoomTest.DataSource = null;
               cbRoomTest.Text = "";
               // commbox roomtest
               cbRoomTest.DataSource = (from r in _RoomTestService.GetAllByLocation(_LocationID)
                                        from ds in _DivisionShiftService.GetAll()
                                        where CurrentShiftID == ds.ShiftID && r.RoomTestID == ds.RoomTestID && r.LocationID == _LocationID
                                        select new
                                        {
                                             RoomTestName = r.RoomTestName,
                                             RoomTestID = r.RoomTestID
                                        }).ToList();
               cbRoomTest.DisplayMember = "RoomTestName";
               cbRoomTest.ValueMember = "RoomTestID";
               if (CurrentRoomTestID == -1)
               {
                    gvMain.DataSource = null;
               }

          }
          private void cbRoomTest_SelectedValueChanged(object sender, EventArgs e)
          {
               if (CurrentShiftID != -1 && CurrentRoomTestID != -1)
               {
                    _ShiftService = new ShiftService();
                    _StaffService = new StaffService();
                    _ScheduleService = new ScheduleService();
                    _SubjectService = new SubjectService();
                    _ContestantShiftService = new ContestantShiftService();
                    _DivisionShiftService = new DivisionShiftService();

                    MTAQuizDbContext db = new MTAQuizDbContext();
                    DIVISION_SHIFTS CurrentDs = new DIVISION_SHIFTS();
                    CurrentDs = _DivisionShiftService.GetByShiftAndRoomTest(CurrentShiftID, CurrentRoomTestID);
                    cbSubject.DataSource = (
                                            from ds in _ContestantShiftService.GetAllByDivisionShiftID(CurrentDs.DivisionShiftID)
                                            select new
                                            {
                                                 SubjectName = ds.SCHEDULE.SUBJECT.SubjectName,
                                                 SubjectID = ds.SCHEDULE.SUBJECT.SubjectID
                                            }).Distinct().ToList();
                    cbSubject.DisplayMember = "SubjectName";
                    cbSubject.ValueMember = "SubjectID";


                    if (CurrentSubjectID == -1)
                    {
                         gvMain.DataSource = null;
                    }

               }

          }

          private void cbSubject_SelectedValueChanged(object sender, EventArgs e)
          {
               if (CurrentShiftID != -1 && CurrentRoomTestID != -1 && CurrentSubjectID != -1)
               {
                _ShiftService = new ShiftService();
                _StaffService = new StaffService();
                _ScheduleService = new ScheduleService();
                _SubjectService = new SubjectService();
                _ContestantShiftService = new ContestantShiftService();
                _ContestantTestService = new ContestantTestService();
                _DivisionShiftService = new DivisionShiftService();
                _TestService = new TestService();
                DIVISION_SHIFTS CurrentDs = new DIVISION_SHIFTS();
                CurrentDs = _DivisionShiftService.GetByShiftAndRoomTest(CurrentShiftID, CurrentRoomTestID);                    
                int index = 1;
                var listContestantTest = (from cs in _ContestantShiftService.GetAllByDivisionShiftID(CurrentDs.DivisionShiftID)
                                        from ct in _ContestantTestService.GetAll().Where(p => p.ContestantShiftID == cs.ContestantShiftID)
                                        from test in _TestService.GetAll().Where(p => p.TestID == ct.TestID)
                                        where test.SUBJECT.SubjectID == CurrentSubjectID
                                        select new
                                        {
                                            STT = index++,
                                            ContestantCode = cs.CONTESTANT.ContestantCode,                                               
                                            FullName = cs.CONTESTANT.FullName,                                            
                                            DOB = DateTimeHelpers.ConvertUnixToDateTime(cs.CONTESTANT.DOB.Value)
                                            .ToShortDateString(),
                                            TestID = test.TestID,
                                            SubjectName = test.SUBJECT.SubjectName,
                                            PrintAnswer = "In đề thi"
                                        }).ToList();
                    gvMain.DataSource = listContestantTest;
               }
          }

          private void ucWritting_Load(object sender, EventArgs e)
          {

          }

          /// <summary>
          ///  click xem bài làm của thí sinh
          /// </summary>
          /// <param name="sender"></param>
          /// <param name="e"></param>
          private void gvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
          {
               var senderGrid = (DataGridView)sender;
               try
               {
                    if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                    {
                         try
                         {
                                frmProgress frm = new frmProgress(2);
                                frm.Show();
                                frm.UpdateValue2(1);
                                int testID = Convert.ToInt32(senderGrid.Rows[e.RowIndex].Cells["col_TestID"].Value.ToString());
                                // Print test file
                                PrintTest(testID);
                                frm.UpdateValue2(2);
                                // Open printed test file
                                string SubjectName = db.TESTS.Where(p => p.TestID == testID).SingleOrDefault().SUBJECT.SubjectName;
                                string FileNameOutput = System.Windows.Forms.Application.StartupPath + "\\Temp\\" + "\\" + "InDeThi" + "\\" + SubjectName.Trim() + "\\" + testID + ".docx";
                                if (File.Exists(FileNameOutput))
                                    OpenFileAnsewer(testID);
                                frm.Close();
                         }
                         catch (Exception ex)
                         {
                              MetroFramework.MetroMessageBox.Show(this, ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error, 100);
                         }
                    }
               }
               catch (Exception ex)
               {
               }
          }
          
          public void killProcessMSWord()
          {
               foreach (Process process in Process.GetProcessesByName("WINWORD"))
               {

                    process.Kill();

               }
          }

          private void btnPrintResult_Click(object sender, EventArgs e)
          {
               try
               {
                    List<int> lst_testID = new List<int>();
                    for (int i = 0; i < gvMain.Rows.Count; i++)
                    {
                        int testID = Convert.ToInt32(gvMain.Rows[i].Cells["col_TestID"].Value.ToString());
                        lst_testID.Add(testID);
                    }
                    frmProgress frm = new frmProgress(lst_testID.Count);
                    frm.Show();
                    int j = 0;
                    foreach (int id in lst_testID)
                    {
                        PrintTest(id);
                        frm.UpdateValue2(++j);
                    }
                    frm.Close();
                    if (lst_testID.Count > 0)
                    {
                        string SubjectName = cbSubject.SelectedText;
                        string FolderNameOutput = System.Windows.Forms.Application.StartupPath + "\\Temp\\" + "\\" + "InDeThi" + "\\" + SubjectName.Trim() + "\\";
                        MetroFramework.MetroMessageBox.Show(this, "In đề thi thành công. Xem đề trong thư mục:\n" + FolderNameOutput, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, 200);
                    }                    
               }
               catch (Exception ex)
               {
                    MessageBox.Show("Không có dữ liệu!");

               }
          }

        private void PrintTest(int testID)
        {
            EXON.SubModel.PrintTestHelper printtesthelper = new SubModel.PrintTestHelper();
            List<EXON.SubModel.Question2Print> lst_ques2print = printtesthelper.GetListQuestion2Print(testID);

            string SubjectName = db.TESTS.Where(p => p.TestID == testID).SingleOrDefault().SUBJECT.SubjectName;
            string FileNameOutput = System.Windows.Forms.Application.StartupPath + "\\Temp\\" + "\\" + "InDeThi" + "\\" + SubjectName.Trim() + "\\" + testID + ".docx";
            string FolderNameOutput = System.Windows.Forms.Application.StartupPath + "\\Temp\\" + "\\" + "InDeThi" + "\\" + SubjectName.Trim() + "\\" + testID + "\\";

            if (!Directory.Exists(Path.Combine(FolderNameOutput.ToString())))
                Directory.CreateDirectory(Path.Combine(Path.Combine(FolderNameOutput.ToString())));
            if (!File.Exists(FileNameOutput))
            {
                if (CheckDecrypt(lst_ques2print))
                    Print2File(lst_ques2print, FolderNameOutput, FileNameOutput, testID);
                else
                    MetroFramework.MetroMessageBox.Show(this, "Đề thi chưa được giải mã", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error, 100);
            }            
        }

        private bool CheckDecrypt(List<EXON.SubModel.Question2Print> list)
        {
            foreach (EXON.SubModel.Question2Print ques in list)
            {
                if (ques.QuestionContent == null)
                {
                    foreach (EXON.SubModel.SubQuestion2Print subques in ques.lst_subquestions)
                    {
                        if (subques.SubQuestionContent == null)
                            return false;
                    }
                }
            }
            return true;
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

        private void CreateFileHead(string FolderNameOutput, int testID)
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

        private void Print2File(List<EXON.SubModel.Question2Print> lst_ques2print, string FolderNameOutput, string FileNameOutput, int testID)
        {
            CreateFileHead(FolderNameOutput, testID);
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

         

          private void txtDivisionShiftID_TextChanged(object sender, EventArgs e)
          {

          }

          private void label4_Click(object sender, EventArgs e)
          {

          }

          private void cbShift_SelectedIndexChanged(object sender, EventArgs e)
          {

          }

          private void cbStaff2_SelectedIndexChanged(object sender, EventArgs e)
          {

          }

          private void groupBox3_Enter(object sender, EventArgs e)
          {

          }

          private void label5_Click(object sender, EventArgs e)
          {

          }

          private void label1_Click(object sender, EventArgs e)
          {

          }

          private void cbSubject_SelectedIndexChanged(object sender, EventArgs e)
          {

          }

          private void label6_Click(object sender, EventArgs e)
          {

          }

          private void label3_Click(object sender, EventArgs e)
          {

          }

          private void label2_Click(object sender, EventArgs e)
          {

          }

          private void groupBox2_Enter(object sender, EventArgs e)
          {

          }

          private void cbRoomTest_SelectedIndexChanged(object sender, EventArgs e)
          {

          }

          private void cbStaff1_SelectedIndexChanged(object sender, EventArgs e)
          {

          }
     }
}


