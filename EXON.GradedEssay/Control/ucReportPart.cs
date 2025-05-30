
using EXON.SubData.Services;
using EXON.SubModel.Models;
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


namespace EXON.GradedEssay.Control
{
     public partial class ucReportPart : UserControl
     {
          #region Service
          private IContestService _ContestService;
          private IShiftService _ShiftService;
          private IScheduleService _ScheduleService;
          private ISubjectService _SubjectService;
          private IStaffService _StaffService;
          private IDivisionShiftService _DivisionShiftService;
          private IContestantShiftService _ContestantShiftService;
          private IContestantService _ContestantService;
          private IContestantTestService _ContestantTestService;
          private IAnswersheetService _AnswersheetService;
          private IAnswersheetSpeakingService _AnswersheetSpeakingService;
          private IAnswersheetWritingService _AnswersheetWritingService;
          private IBagOfTestService _BagOfTestService;
          private ITestService _TestService;
          private IRoomTestService _RoomTestService;
          private IAnswersheetDetailService _AnswersheetDetailService;
          private IAnswerService _AnswerService;
          private IPartService _PartService;
          #endregion Service
          private int _ContestID { get; set; }
          private int _LocationID { get; set; }
          MTAQuizDbContext db = new MTAQuizDbContext();
          private string CurrentUnit
          {
               get
               {
                    try
                    {
                         return cbLop.Text.ToString();
                    }
                    catch { return string.Empty; }
               }
          }
          private string CurrentSubject
          {
               get
               {
                    try
                    {
                         return cbx_MonThi.Text.ToString();
                    }
                    catch { return string.Empty; }
               }
          }
          public ucReportPart(int contestID, int LocationID)
          {
               _ContestID = contestID;
               _LocationID = LocationID;
               InitializeComponent();
               InitializeService();              
               InitializeControl();
          }
          private void InitializeService()
          {
               _ContestService = new ContestService();
               _ShiftService = new ShiftService();
               _ScheduleService = new ScheduleService();
               _SubjectService = new SubjectService();
               _DivisionShiftService = new DivisionShiftService();
               _ContestantShiftService = new ContestantShiftService();
               _ContestantService = new ContestantService();
               _StaffService = new StaffService();
               _ContestantTestService = new ContestantTestService();
               _AnswersheetService = new AnswersheetService();
               _AnswersheetSpeakingService = new AnswersheetSpeakingService();
               _BagOfTestService = new BagOfTestService();
               _TestService = new TestService();
               _RoomTestService = new RoomTestService();
               _AnswersheetWritingService = new AnswersheetWritingService();
               _AnswersheetDetailService = new AnswersheetDetailService();
               _AnswerService = new AnswerService();

          }
          private void InitializeControl()
          {
               List<string> lst_units = new List<string>();
               lst_units.Add("-- Tất cả các lớp --");
               lst_units.AddRange((from ds in _ContestantService.GetAll()
                                   select ds.Unit).Distinct().ToList());
               cbLop.DataSource = lst_units;
               cbLop.DisplayMember = "TenLop";
               List<string> lst_Units_Subject = new List<string>();
               if (cbLop.Text != "-- Tất cả các lớp --")
                    lst_Units_Subject = db.CONTESTANTS_SHIFTS.Where(p => p.CONTESTANT.Unit == CurrentUnit).Select(p => p.SCHEDULE.SUBJECT.SubjectName).Distinct().ToList();
               else
                    lst_Units_Subject = db.SUBJECTS.Select(p => p.SubjectName).ToList();
               lst_Units_Subject.Sort();
               cbx_MonThi.DataSource = lst_Units_Subject;
               
          }

          private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
          {
               List<string> lst_Units_Subject = new List<string>();
               if (cbLop.Text != "-- Tất cả các lớp --")
                    lst_Units_Subject = db.CONTESTANTS_SHIFTS.Where(p => p.CONTESTANT.Unit == CurrentUnit).Select(p => p.SCHEDULE.SUBJECT.SubjectName).Distinct().ToList();
               else
                    lst_Units_Subject = db.SUBJECTS.Select(p => p.SubjectName).ToList();
               lst_Units_Subject.Sort();
               cbx_MonThi.DataSource = lst_Units_Subject;
          }
          private void btnPrintResult_Click(object sender, EventArgs e)
          {
               
               try
               {
                    SplashScreenManager.ShowSplashScreen();
                    Thread.Sleep(100);
                    if (CurrentUnit != string.Empty)
                    {
                         _ContestantService = new ContestantService();
                         List<CONTESTANT> lstct = new List<CONTESTANT>();
                        System.Data.DataTable dtb = new System.Data.DataTable();
                         if (CurrentUnit != "-- Tất cả các lớp --")
                              lstct = _ContestantService.GetMultiByUnit(CurrentUnit).ToList();
                         else
                              lstct = _ContestantService.GetAll().OrderBy(p => p.ContestantCode).ToList();
                         List<KetQuaTongNN> lstkq = new List<KetQuaTongNN>();
                         int stt = 1;

                         if (CheckPart(CurrentSubject))
                         {
                              foreach (CONTESTANT ct in lstct)
                              {
                                  
                                   List<SubQuestion2> ListSubQuestion = new List<SubQuestion2>();
                                   _ContestantShiftService = new ContestantShiftService();
                                   List<CONTESTANTS_SHIFTS> lstcs = new List<CONTESTANTS_SHIFTS>();
                                   KetQuaTongNN kq = new KetQuaTongNN();
                                   kq.STT = -1;
                                   lstcs = _ContestantShiftService.GetAllByContestantID(ct.ContestantID).ToList().Where(p => p.SCHEDULE.SUBJECT.SubjectName == CurrentSubject).ToList();
                                   if (!CheckFinish(lstcs))
                                   {
                                        lstkq = new List<KetQuaTongNN>();
                                        MetroMessageBox.Show(this, "Môn thi chưa kết thúc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, 100);
                                        break;
                                   }
                                   else
                                   {
                                        foreach (CONTESTANTS_SHIFTS cs in lstcs)
                                        {
                                             
                                                  _ContestantShiftService = new ContestantShiftService();
                                                  CONTESTANTS_TESTS ctt = _ContestantTestService.GetByContestantShiftId(cs.ContestantShiftID);
                                                  ANSWERSHEET anw = _AnswersheetService.GetByContestantTestID(ctt.ContestantTestID);
                                                  if (anw != null)
                                                  {
                                                       var List = (from sub in db.SUBQUESTIONS
                                                                   join Test in db.TEST_DETAILS on sub.QuestionID equals Test.QuestionID
                                                                   where (Test.TestID == ctt.TestID)
                                                                   orderby Test.NumberIndex ascending
                                                                   select new
                                                                   {
                                                                        sub.SubQuestionID,
                                                                        sub.Score,
                                                                        Test.NumberIndex,
                                                                        sub.QuestionID,
                                                                   }).ToList();
                                                       int i = 0;
                                                       foreach (var ilist in List)
                                                       {
                                                            i++;
                                                            SubQuestion2 iListSub = new SubQuestion2();
                                                            iListSub.Stt = i;
                                                            iListSub.SubQuestionID = ilist.SubQuestionID;
                                                            iListSub.Score = ilist.Score;
                                                            iListSub.QuestionID = ilist.QuestionID;
                                                            iListSub.NumberIndex = ilist.NumberIndex;
                                                            ListSubQuestion.Add(iListSub);
                                                       }
                                                       kq.partOfTests = LayKetQua(ListSubQuestion, ctt.ContestantTestID, anw.AnswerSheetID, LayPart(CurrentSubject));
                                                       kq.STT = stt;
                                                       kq.HoTen = ct.FullName;
                                                       //kq.SBD = ct.ContestantCode;
                                                       //kq.NgaySinh = DatetimeConvert.ConvertUnixToDateTime(ct.DOB.Value).ToString("dd/MM/yyyy");
                                                       //kq.PhongThi = cs.DIVISION_SHIFTS.ROOMTEST.RoomTestName;
                                                       kq.Unit = ct.Unit;
                                                       kq.DiemNoi = KetQuaNoi(anw.AnswerSheetID).ToString();
                                                       kq.DiemViet = KetQuaViet(anw.AnswerSheetID).ToString();
                                                       kq.DiemVietCN = KetQuaVietCN(anw.AnswerSheetID).ToString();
                                                       kq.DiemTong = KetQuaTong(kq).ToString();
                                                  }
                                                  else
                                                  {
                                                       kq.STT = stt;
                                                       kq.HoTen = ct.FullName;
                                                       //kq.SBD = ct.ContestantCode;
                                                       //kq.NgaySinh = DatetimeConvert.ConvertUnixToDateTime(ct.DOB.Value).ToString("dd/MM/yyyy");
                                                       //kq.PhongThi = cs.DIVISION_SHIFTS.ROOMTEST.RoomTestName;
                                                       kq.Unit = ct.Unit;
                                                       kq.partOfTests = LayPartNull(LayPart(CurrentSubject));
                                                       kq.DiemNoi = null;
                                                       kq.DiemViet = null;
                                                       kq.DiemVietCN = null;
                                                       kq.DiemTong = null;
                                                  }
                                            
                                        }
                                        if (kq.STT != -1)
                                        {
                                             lstkq.Add(kq);
                                             stt++;
                                        }
                                   }
                              }
                              
                              dtb.Columns.Add("STT");
                              dtb.Columns.Add("Họ Và Tên");
                              //dtb.Columns.Add("SBD");
                              //dtb.Columns.Add("NgaySinh");
                              //dtb.Columns.Add("PhongThi");
                              dtb.Columns.Add("Lớp");
                              List<PART> parts = LayPart(CurrentSubject);
                              for (int i = 0; i < parts.Count(); i++)
                              {
                                   dtb.Columns.Add(ConvertName(parts[i].Name));
                              }
                              dtb.Columns.Add("Điểm nói");
                              dtb.Columns.Add("Điểm viết");
                              dtb.Columns.Add("Điểm viết CN");
                              dtb.Columns.Add("Tổng");
                              for (int i = 0; i < lstkq.Count(); i++)
                              {
                                   DataRow row;
                                   row = dtb.NewRow();
                                   row["STT"] = lstkq[i].STT;
                                   row["Họ Và Tên"] = lstkq[i].HoTen;
                                   //row["SBD"] = lstkq[i].SBD;
                                   //row["NgaySinh"] = lstkq[i].NgaySinh;
                                   //row["PhongThi"] = lstkq[i].PhongThi;
                                   row["Lớp"] = lstkq[i].Unit;
                                   for (int j = 0; j < lstkq[i].partOfTests.Count(); j++)
                                   {
                                        row[ConvertName(lstkq[i].partOfTests[j].PartContent)] = lstkq[i].partOfTests[j].Score;
                                   }
                                   row["Điểm nói"] = lstkq[i].DiemNoi;
                                   row["Điểm viết"] = lstkq[i].DiemViet;
                                   row["Điểm viết CN"] = lstkq[i].DiemVietCN;
                                   row["Tổng"] = lstkq[i].DiemTong;
                                   dtb.Rows.Add(row);
                              }
                         }
                         else
                         {
                              foreach (CONTESTANT ct in lstct)
                              {
                                   List<SubQuestion2> ListSubQuestion = new List<SubQuestion2>();
                                   
                                   _ContestantShiftService = new ContestantShiftService();
                                   List<CONTESTANTS_SHIFTS> lstcs = new List<CONTESTANTS_SHIFTS>();
                                   KetQuaTongNN kq = new KetQuaTongNN();
                                   kq.STT = -1;
                                   lstcs = _ContestantShiftService.GetAllByContestantID(ct.ContestantID).ToList().Where(p => p.SCHEDULE.SUBJECT.SubjectName == CurrentSubject).ToList();
                                   if (!CheckFinish(lstcs))
                                   {
                                        lstkq = new List<KetQuaTongNN>();
                                        MetroMessageBox.Show(this, "Môn thi chưa kết thúc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, 100);
                                        break;
                                   }
                                   else
                                   {
                                        foreach (CONTESTANTS_SHIFTS cs in lstcs)
                                        {
                                            
                                                  _ContestantShiftService = new ContestantShiftService();
                                                  CONTESTANTS_TESTS ctt = _ContestantTestService.GetByContestantShiftId(cs.ContestantShiftID);
                                                  ANSWERSHEET anw = _AnswersheetService.GetByContestantTestID(ctt.ContestantTestID);
                                                  if (anw != null)
                                                  {
                                                       kq.STT = stt;
                                                       kq.HoTen = ct.FullName;
                                                       //kq.SBD = ct.ContestantCode;
                                                       //kq.NgaySinh = DatetimeConvert.ConvertUnixToDateTime(ct.DOB.Value).ToString("dd/MM/yyyy");
                                                       //kq.PhongThi = cs.DIVISION_SHIFTS.ROOMTEST.RoomTestName;
                                                       kq.Unit = ct.Unit;
                                                       kq.DiemTN = KetQuaTracNghiem(cs.ContestantShiftID).ToString();
                                                       kq.DiemNoi = KetQuaNoi(anw.AnswerSheetID).ToString();
                                                       kq.DiemViet = KetQuaViet(anw.AnswerSheetID).ToString();
                                                       kq.DiemVietCN = KetQuaVietCN(anw.AnswerSheetID).ToString();
                                                       kq.DiemTong = KetQuaTong(kq).ToString();
                                                  }
                                                  else
                                                  {
                                                       kq.STT = stt;
                                                       kq.HoTen = ct.FullName;
                                                       kq.SBD = ct.ContestantCode;
                                                       //kq.NgaySinh = DatetimeConvert.ConvertUnixToDateTime(ct.DOB.Value).ToString("dd/MM/yyyy");
                                                       //kq.PhongThi = cs.DIVISION_SHIFTS.ROOMTEST.RoomTestName;
                                                       //kq.Unit = ct.Unit;
                                                       kq.DiemTN = null;
                                                       kq.DiemNoi = null;
                                                       kq.DiemViet = null;
                                                       kq.DiemVietCN = null;
                                                       kq.DiemTong = null;
                                                  }
                                             
                                        }
                                        if (kq.STT != -1)
                                        {
                                             lstkq.Add(kq);
                                             stt++;
                                        }
                                   }
                              }
                              dtb.Columns.Add("STT");
                              dtb.Columns.Add("Họ Và Tên");
                              //dtb.Columns.Add("SBD");
                              //dtb.Columns.Add("NgaySinh");
                              //dtb.Columns.Add("PhongThi");
                              dtb.Columns.Add("Lớp");
                              dtb.Columns.Add("Điểm TN");
                              dtb.Columns.Add("Điểm nói");
                              dtb.Columns.Add("Điểm viết");
                              dtb.Columns.Add("Điểm viết CN");
                              dtb.Columns.Add("Tổng");
                              for (int i = 0; i < lstkq.Count(); i++)
                              {
                                   DataRow row;
                                   row = dtb.NewRow();
                                   row["STT"] = lstkq[i].STT;
                                   row["Họ Và Tên"] = lstkq[i].HoTen;
                                   //row["SBD"] = lstkq[i].SBD;
                                   //row["NgaySinh"] = lstkq[i].NgaySinh;
                                   //row["PhongThi"] = lstkq[i].PhongThi;
                                   row["Lớp"] = lstkq[i].Unit;
                                   row["Điểm TN"] = lstkq[i].DiemTN;
                                   row["Điểm nói"] = lstkq[i].DiemNoi;
                                   row["Điểm viết"] = lstkq[i].DiemViet;
                                   row["Điểm viết CN"] = lstkq[i].DiemVietCN;
                                   row["Tổng"] = lstkq[i].DiemTong;
                                   dtb.Rows.Add(row);
                              }
                         }
                         
                         gvMain.DataSource = dtb;         
                    }
                    SplashScreenManager.CloseForm();
               }
               catch
               {
                    SplashScreenManager.CloseForm();
                    MetroMessageBox.Show(this, "Chọn môn không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error, 100);
               }
               
          }
          private float KetQuaTracNghiem(int contestantShiftID)
          {
               _ContestantTestService = new ContestantTestService();

               CONTESTANTS_TESTS ct = _ContestantTestService.GetByContestantShiftId(contestantShiftID);
               if (ct != null)
               {
                    _AnswersheetService = new AnswersheetService();
                    _AnswersheetSpeakingService = new AnswersheetSpeakingService();
                    _AnswersheetWritingService = new AnswersheetWritingService();
                    ANSWERSHEET anw = _AnswersheetService.GetByContestantTestID(ct.ContestantTestID);
                    if (anw != null)
                    {

                         if (anw.TestScores == null)
                              return 0;
                         return (float)(anw.TestScores);

                    }
               }
               return 0;
          }
          private float KetQuaViet(int AnswerSheetID)
          {   
                ANSWERSHEET_WRITING anwritting = _AnswersheetWritingService.GetByAnwsersheetID(AnswerSheetID);
                if (anwritting != null)
                {
                     if (anwritting.WritingScore == null)
                          return 0;
                     return (float)(anwritting.WritingScore);
                }
                return 0;
          }
          private float KetQuaVietCN(int AnswerSheetID)
          {
               ANSWERSHEET anws = _AnswersheetService.GetById(AnswerSheetID);
               if (anws != null)
               {
                    if (anws.EssayPoints == null)
                         return 0;
                    return (float)(anws.EssayPoints);
               }
               return 0;
          }
          private float KetQuaNoi(int AnswerSheetID)
          {
               
               ANSWERSHEET_SPEAKING aw = _AnswersheetSpeakingService.GetByAnwsersheetID(AnswerSheetID);
               if (aw != null)
               {
                    if (aw.SpeakingScore == null)
                         return 0;
                    return (float)(aw.SpeakingScore);
               }
               else return 0;
                    

          }
          private float KetQuaTong(KetQuaTongNN kq)
          {
               if (kq.partOfTests == null)
               {
                    return (float)(Convert.ToDouble(kq.DiemTN) + Convert.ToDouble(kq.DiemViet) + Convert.ToDouble(kq.DiemVietCN) + Convert.ToDouble(kq.DiemNoi));
               }
               else
               {
                    float Sum=0;
                    for(int i = 0;i < kq.partOfTests.Count();i++)
                    {
                         Sum += (float)Convert.ToDouble(kq.partOfTests[i].Score);
                    }
                    Sum += (float)(Convert.ToDouble(kq.DiemViet) + Convert.ToDouble(kq.DiemVietCN) + Convert.ToDouble(kq.DiemNoi));
                    return Sum;
               }
          }
          public List<PART> LayPart(string CurrentSubject)
          {
               _PartService = new PastService();
               List<PartOfTest> parts = new List<PartOfTest>();
               List<PART> lstp = new List<PART>();
               lstp = db.PARTS.Where(p => p.SCHEDULE.SUBJECT.SubjectName == CurrentSubject).OrderBy(p => p.OrderInTest).ToList();
               if (lstp.Count != 0)
               {
                    return lstp;
               }
               else return null;
          }
          public List<PartOfTest> LayPartNull(List<PART> lstp)
          {
               List<PartOfTest> parts = new List<PartOfTest>();
               for (int i = 0; i < lstp.Count; i++)
               {                    
                    PartOfTest iPart = new PartOfTest();
                    iPart.PartContent = lstp[i].Name;
                    iPart.Score = null;
                    parts.Add(iPart);
               }
               return parts;
          } 
          public bool CheckPart(string CurrentSubject)
          {
               _PartService = new PastService();
               List<PartOfTest> parts = new List<PartOfTest>();
               List<PART> lstp = new List<PART>();
               lstp = db.PARTS.Where(p => p.SCHEDULE.SUBJECT.SubjectName == CurrentSubject).OrderBy(p => p.OrderInTest).ToList();
               if (lstp.Count != 0)
               {
                    return true;
               }
               else return false;
          }
          public bool CheckFinish(List<CONTESTANTS_SHIFTS> lstcs)
          {
               bool kq = true;
               foreach (CONTESTANTS_SHIFTS cs in lstcs)
               {
                    if (cs.DIVISION_SHIFTS.Status == 8)
                    {
                         kq = true;
                    }
                    else kq = false;
               }
               return kq;
          }
          public string ConvertName(string s)
          {
               TXTextControl.TextControl NamePart = new TXTextControl.TextControl();
               this.Controls.Add(NamePart);
               NamePart.Select(NamePart.Text.Length, 0);
               NamePart.Append(s, TXTextControl.StringStreamType.RichTextFormat, TXTextControl.AppendSettings.None);
               if (NamePart.Text.Length > 30)
               {
                    string a = NamePart.Text.ToString().Substring(0,30);
                    return a + "...........";
               }
               else return NamePart.Text.ToString();
          }
          public List<PartOfTest> LayKetQua(List<SubQuestion2> ListSubQuestion,int ContestantTestID,int AnswerID,List<PART> lstp)
          {
               List<PartOfTest> parts = new List<PartOfTest>();
               List<ANSWERSHEET_DETAILS> lansd = new List<ANSWERSHEET_DETAILS>();
               for (int i=0;i< lstp.Count;i++)
               {
                    float SumSCore = 0;                    
                    foreach (SubQuestion2 sq2 in ListSubQuestion)
                    {
                         if (i!= (lstp.Count - 1) && sq2.Stt >= lstp[i].OrderOfQuestion && sq2.Stt < lstp[i + 1].OrderOfQuestion  )
                         {
                              QUESTION ques = db.QUESTIONS.Where(p => p.QuestionID == sq2.QuestionID).FirstOrDefault();
                              if (ques.Type == 0)
                              {                 
                                   int ansID;
                                   _AnswersheetDetailService = new AnswersheetDetailService();
                                   ANSWERSHEET_DETAILS ansd = _AnswersheetDetailService.GetBySubQuestionID(AnswerID, sq2.SubQuestionID);
                                   if (ansd != null)
                                   {
                                        ANSWER aw = new ANSWER();
                                        ansID = ansd.ChoosenAnswer ?? default(int);
                                        _AnswerService = new AnswerService();
                                        aw = _AnswerService.GetById(ansID);
                                        if (aw != null)
                                        {
                                             if (aw.IsCorrect)
                                             {
                                                  SumSCore += (float)Math.Round(aw.SUBQUESTION.Score.Value, 2);
                                             }
                                        }
                                   }
                                   else SumSCore += 0;
                              }
                              else if (ques.Type == 1)
                              {                                                        
                                   int ansID;
                                   _AnswersheetDetailService = new AnswersheetDetailService();
                                   ANSWERSHEET_DETAILS ansd = _AnswersheetDetailService.GetBySubQuestionID(AnswerID, sq2.SubQuestionID);
                                   if (ansd != null)
                                   {
                                        ANSWER aw = new ANSWER();
                                   ansID = ansd.ChoosenAnswer ?? default(int);
                                   _AnswerService = new AnswerService();
                                   aw = _AnswerService.GetById(ansID);
                                   if (aw != null)
                                   {
                                        if (aw.IsCorrect && aw.SUBQUESTION.QUESTION.Audio != null)
                                        {
                                             SumSCore += (float)Math.Round(aw.SUBQUESTION.Score.Value, 2);
                                        }
                                   }
                                   }
                                   else SumSCore += 0;
                              }
                              else if (ques.Type == 4)
                              {
                                   
                                   SumSCore += 0;
                              }

                         }
                         else if (i == (lstp.Count-1) && sq2.Stt >= lstp[lstp.Count - 1].OrderOfQuestion && sq2.Stt <= ListSubQuestion.Count)
                         {
                              QUESTION ques = db.QUESTIONS.Where(p => p.QuestionID == sq2.QuestionID).FirstOrDefault();
                              if (ques.Type == 1)
                              {
                                   int ansID;
                                   _AnswersheetDetailService = new AnswersheetDetailService();
                                   ANSWERSHEET_DETAILS ansd = _AnswersheetDetailService.GetBySubQuestionID(AnswerID, sq2.SubQuestionID);
                                   if (ansd != null)
                                   {
                                        ANSWER aw = new ANSWER();
                                        ansID = ansd.ChoosenAnswer ?? default(int);
                                        _AnswerService = new AnswerService();
                                        aw = _AnswerService.GetById(ansID);
                                        if (aw != null)
                                        {
                                             if (aw.IsCorrect && aw.SUBQUESTION.QUESTION.Audio != null)
                                             {
                                                  SumSCore += (float)Math.Round(aw.SUBQUESTION.Score.Value, 2);
                                             }
                                        }
                                   }
                                   else SumSCore += 0;
                              }
                              if (ques.Type == 0)
                              {
                                   int ansID;
                                   _AnswersheetDetailService = new AnswersheetDetailService();
                                   ANSWERSHEET_DETAILS ansd = _AnswersheetDetailService.GetBySubQuestionID(AnswerID, sq2.SubQuestionID);
                                   if (ansd != null)
                                   {
                                        ANSWER aw = new ANSWER();
                                        ansID = ansd.ChoosenAnswer ?? default(int);
                                        _AnswerService = new AnswerService();
                                        aw = _AnswerService.GetById(ansID);
                                        if (aw != null)
                                        {
                                             if (aw.IsCorrect)
                                             {
                                                  SumSCore += (float)Math.Round(aw.SUBQUESTION.Score.Value, 2);
                                             }
                                        }
                                   }
                                   else SumSCore += 0;
                              }
                              else if (ques.Type == 4)
                              {

                                   SumSCore += 0;
                              }
                         }
                    }
                    PartOfTest iPart = new PartOfTest();
                    iPart.PartContent = lstp[i].Name;
                    iPart.Score = SumSCore;
                    parts.Add(iPart);
               }
               return parts;
          }

        private void btnExportScoreWritting_Click(object sender, EventArgs e)
        {
               SplashScreenManager.ShowSplashScreen();
               Thread.Sleep(100);
               string FileNameOutput = System.Windows.Forms.Application.StartupPath + "\\Temp\\" + "\\" + "InPhieuDiem" + "\\" + CurrentSubject.Trim() + "\\" + CurrentUnit.Trim() + ".docx";
               string FolderNameOutput = System.Windows.Forms.Application.StartupPath + "\\Temp\\" + "\\" + "InPhieuDiem" + "\\" + CurrentSubject.Trim() + "\\" + CurrentUnit.Trim() + "\\";
               if (!Directory.Exists(Path.Combine(FolderNameOutput.ToString())))
                    Directory.CreateDirectory(Path.Combine(Path.Combine(FolderNameOutput.ToString())));
               if (!File.Exists(FileNameOutput))
               {
                    PrintResult(gvMain, FolderNameOutput, FileNameOutput);
                    foreach (Process process in Process.GetProcessesByName("WINWORD"))
                    {

                         process.Kill();

                    }
               }
               OpenFileAnsewer(FileNameOutput);
               SplashScreenManager.CloseForm();
          }
        private void PrintResult(DataGridView DGV, string FolderNameOutput,string FileNameOutput)
        {
               CreateFileHead(FolderNameOutput);
               Word.Application objApp = new Word.Application();
               object missing = Missing.Value;
               Word.Document objDoc = new Word.Document();
               object objMiss = System.Reflection.Missing.Value;
               object readOnly = false;
               // Path to template word file
               object pathfile = Constant.pathTempPrintResultN;
               objDoc = objApp.Documents.Open(ref pathfile, ref missing, ref missing,
                                          ref missing, ref missing, ref missing,
                                          ref missing, ref missing, ref missing,
                                          ref missing, ref missing, ref missing,
                                          ref missing, ref missing, ref missing, ref missing);
               objDoc.Activate();

               if (DGV.Rows.Count != 0)
               {
                    int RowCount = DGV.Rows.Count;
                    int ColumnCount = DGV.Columns.Count;
                    Object[,] DataArray = new object[RowCount + 1, ColumnCount + 1];

                    //add rows
                    int r = 0;
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                         for (r = 0; r <= RowCount - 1; r++)
                         {
                              DataArray[r, c] = DGV.Rows[r].Cells[c].Value;
                         } //end row loop
                    } //end column loop


                    objDoc.Application.Visible = true;

                    //page orintation
                    objDoc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;


                    dynamic oRange = objDoc.Content.Application.Selection.Range;
                    string oTemp = "";
                    for (r = 0; r <= RowCount - 1; r++)
                    {
                         for (int c = 0; c <= ColumnCount - 1; c++)
                         {
                              oTemp = oTemp + DataArray[r, c] + "\t";

                         }
                    }

                    //table format
                    oRange.Text = oTemp;

                    object Separator = Word.WdTableFieldSeparator.wdSeparateByTabs;
                    object ApplyBorders = true;
                    object AutoFit = true;
                    object AutoFitBehavior = Word.WdAutoFitBehavior.wdAutoFitWindow;
                    
                    oRange.ConvertToTable(ref Separator, ref RowCount, ref ColumnCount,
                                          Type.Missing, Type.Missing, ref ApplyBorders,
                                          Type.Missing, Type.Missing, Type.Missing,
                                          Type.Missing, Type.Missing, Type.Missing,
                                          Type.Missing, ref AutoFit, ref AutoFitBehavior, Type.Missing);

                    oRange.Select();

                    objDoc.Application.Selection.Tables[1].Select();
                    objDoc.Application.Selection.Tables[1].Rows.AllowBreakAcrossPages = 0;
                    objDoc.Application.Selection.Tables[1].Rows.Alignment = 0;
                    objDoc.Application.Selection.Tables[1].Rows[1].Select();
                    objDoc.Application.Selection.InsertRowsAbove(1);
                    objDoc.Application.Selection.Tables[1].Rows[1].Select();
                    objDoc.Application.Selection.Tables[1].Borders.Enable = 1;

                    //header row style
                    objDoc.Application.Selection.Tables[1].Rows[1].Range.Bold = 0;
                    objDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Name = "Times New Roman";
                    objDoc.Application.Selection.Tables[1].Rows[1].Range.Font.Size = 12;

                    //add header row manually
                    for (int c = 0; c <= ColumnCount - 1; c++)
                    {
                         objDoc.Application.Selection.Tables[1].Cell(1, c + 1).Range.Text = DGV.Columns[c].HeaderText;
                    }

                    //table style 
                    //objDoc.Application.Selection.Tables[1].set_Style("Grid Table 4 - Accent 5");
                    objDoc.Application.Selection.Tables[1].Rows[1].Select();
                    objDoc.Application.Selection.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    //header text
                    //foreach (Word.Section section in objDoc.Application.ActiveDocument.Sections)
                    //{
                    //     Word.Range headerRange = section.Headers[Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    //     headerRange.Fields.Add(headerRange, Word.WdFieldType.wdFieldPage);
                    //     headerRange.Text = "your header text";
                    //     headerRange.Font.Size = 16;
                    //     headerRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    //}

                    //save the file


               }
               object SzPath = FolderNameOutput + "\\" + "0001.docx";
               objDoc.SaveAs(ref SzPath, ref missing, ref missing, ref missing,
               ref missing, ref missing, ref missing,
               ref missing, ref missing, ref missing,
               ref missing, ref missing, ref missing,
               ref missing, ref missing, ref missing);
               objDoc.Close();
               MergeFile(FileNameOutput, FolderNameOutput);
               
          }
          private void CreateFileHead(string FolderNameOutput)
          {
               try
               {
                    Word.Application objApp = new Word.Application();
                    object missing = Missing.Value;
                    Word.Document objDoc = new Word.Document();
                    object objMiss = System.Reflection.Missing.Value;
                    object readOnly = false;
                    // Path to template word file
                    object pathfile = Constant.pathTempPrintResult;
                    objDoc = objApp.Documents.Open(ref pathfile, ref missing, ref missing,
                                               ref missing, ref missing, ref missing,
                                               ref missing, ref missing, ref missing,
                                               ref missing, ref missing, ref missing,
                                               ref missing, ref missing, ref missing, ref missing);
                    objDoc.Activate();
                    string datenow = DatetimeConvert.GetDateTimeServer().ToString(@"\N\g\à\y dd \t\h\á\n\g MM \n\ă\m yyyy");

                    this.FindAndReplace(objApp, "DATENOW", datenow);
                    this.FindAndReplace(objApp, "SUBJECTNAME", CurrentSubject);
                    object szPath = FolderNameOutput + "\\" + "0000.docx";
                    objDoc.SaveAs(ref szPath, ref missing, ref missing, ref missing,
                       ref missing, ref missing, ref missing,
                       ref missing, ref missing, ref missing,
                       ref missing, ref missing, ref missing,
                       ref missing, ref missing, ref missing);
                    objDoc.Close();
               }
               catch (Exception ex)
               {
                    MessageBox.Show(ex.ToString());
               }
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

                    wordDocument.SaveAs(ref outputFile, ref missing, ref missing, ref missing,
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
          private void OpenFileAnsewer(string FileNameOutput)
          {

               Word.Application objApp = new Word.Application();
               object missing = Missing.Value;
               objApp.Visible = false;
               Word.Document objDoc = new Word.Document();
               object objMiss = System.Reflection.Missing.Value;        
               object readOnly = true;
               if (!File.Exists(FileNameOutput))
               {
                    MessageBox.Show("Chưa tạo phiếu điểm");
                    return;
               }
               object szPath = FileNameOutput;
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
     }
    public class KetQuaTongNN
     {
          public int STT { get; set; }
          public string SBD { get; set; }
          public string HoTen { get; set; }
          public string PhongThi { get; set; }
          public string NgaySinh { get; set; }
          public string DiemTN { get; set; }
          public string DiemNoi { get; set; }
          public string DiemViet { get; set; }
          public string DiemVietCN { get; set; }
          public string DiemTong { get; set; }
          public string MonThi { get; set; }
          public string Unit { get; set; }
          public List<PartOfTest> partOfTests;
     }
     public class PartOfTest
     {

          public string PartContent { get; set; }
          public int OrderOfIndex { get; set; }
          public int OrderOfQuestion { get; set; }
          public double? Score { get; set; }
          public PartOfTest() { }
          public PartOfTest(string _PartContent, int _Oindex, int _OQuestion,double? _Score)
          {
               PartContent = _PartContent;
               OrderOfIndex = _Oindex;
               OrderOfQuestion = _OQuestion;
               Score = _Score;
          }
     }
     public class SubQuestion2
     {
          public int Stt { get; set; }
          public int SubQuestionID { get; set; }
          public double? Score { get; set; }
          public int QuestionID { get; set; }
          public int NumberIndex { get; set; }
          public SubQuestion2() { }
          public SubQuestion2(int _SubQuestionID, double? _Score, int _QuestionID, int _NumberIndex)
          {
               SubQuestionID = _SubQuestionID;
               Score = _Score;
               QuestionID = _QuestionID;
               NumberIndex = _NumberIndex;
          }
     }
         
}


