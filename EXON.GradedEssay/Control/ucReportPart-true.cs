using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EXON.SubData.Services;
using EXON.SubModel.Models;
using EXON.Common;


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
               MTAQuizDbContext db = new MTAQuizDbContext();
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
               MTAQuizDbContext db = new MTAQuizDbContext();
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
                    if (CurrentUnit != string.Empty)
                    {
                         _ContestantService = new ContestantService();
                         List<CONTESTANT> lstct = new List<CONTESTANT>();
                         if (CurrentUnit != "-- Tất cả các lớp --")
                              lstct = _ContestantService.GetMultiByUnit(CurrentUnit).ToList();
                         else
                              lstct = _ContestantService.GetAll().OrderBy(p => p.ContestantCode).ToList();
                         List<KetQuaTongNN> lstkq = new List<KetQuaTongNN>();
                         int stt = 1;
                         MTAQuizDbContext db = new MTAQuizDbContext();
                         
                         foreach (CONTESTANT ct in lstct)
                         {
                              //if (ct.ContestantID == 4088)
                              //{
                                   float TongDiem = 0;
                                   List<SubQuestion2> ListSubQuestion = new List<SubQuestion2>();
                                   _ContestantShiftService = new ContestantShiftService();
                                   List<CONTESTANTS_SHIFTS> lstcs = new List<CONTESTANTS_SHIFTS>();
                                   KetQuaTongNN kq = new KetQuaTongNN();
                                   kq.STT = -1;
                                   lstcs = _ContestantShiftService.GetAllByContestantID(ct.ContestantID).ToList().Where(p => p.SCHEDULE.SUBJECT.SubjectName == CurrentSubject).ToList(); ;
                                   foreach (CONTESTANTS_SHIFTS cs in lstcs)
                                   {
                                        _ContestantShiftService = new ContestantShiftService();
                                        CONTESTANTS_TESTS ctt = _ContestantTestService.GetByContestantShiftId(cs.ContestantShiftID);
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
                                        kq.partOfTests = LayKetQua(ListSubQuestion, ctt.ContestantTestID, LayPart(CurrentSubject));
                                        kq.STT = stt;
                                        kq.HoTen = ct.FullName;
                                        kq.SBD = ct.ContestantCode;
                                        kq.NgaySinh = DatetimeConvert.ConvertUnixToDateTime(ct.DOB.Value).ToString("dd/MM/yyyy");
                                        kq.PhongThi = cs.DIVISION_SHIFTS.ROOMTEST.RoomTestName;
                                        kq.Unit = ct.Unit;

                                   }
                                   if (kq.STT != -1)
                                   {
                                        lstkq.Add(kq);
                                        stt++;
                                   }
                              //}
                         }
                         DataTable dtb = new DataTable();
                         dtb.Columns.Add("STT");
                         dtb.Columns.Add("Hoten");
                         dtb.Columns.Add("SBD");
                         dtb.Columns.Add("NgaySinh");
                         dtb.Columns.Add("PhongThi");
                         dtb.Columns.Add("Unit");
                         List<PART> parts = LayPart(CurrentSubject);
                         for (int i = 0; i < parts.Count(); i++)
                         {
                              dtb.Columns.Add(i.ToString());
                         }
                         for (int i = 0; i < lstkq.Count(); i++)
                         {
                              DataRow row;
                              row = dtb.NewRow();
                              row["STT"] = lstkq[i].STT;
                              row["Hoten"] = lstkq[i].HoTen;
                              row["SBD"] = lstkq[i].SBD;
                              row["NgaySinh"] = lstkq[i].NgaySinh;
                              row["PhongThi"] = lstkq[i].PhongThi;
                              row["Unit"] = lstkq[i].Unit;
                              for (int j = 0; j < lstkq[i].partOfTests.Count(); j++)
                              {
                                   row[j.ToString()] = lstkq[i].partOfTests[j].Score;
                              }
                              dtb.Rows.Add(row);
                         }
                         gvMain.DataSource = dtb;
                    }
               }
               catch(Exception ex)
               {
                    MessageBox.Show("", ex.ToString());
               }
          }
          public List<PART> LayPart(string CurrentSubject)
          {
               _PartService = new PastService();
               List<PartOfTest> parts = new List<PartOfTest>();
               List<PART> lstp = new List<PART>();
               MTAQuizDbContext db = new MTAQuizDbContext();
               lstp = db.PARTS.Where(p => p.SCHEDULE.SUBJECT.SubjectName == CurrentSubject).ToList();
               if (lstp.Count != 0)
               {
                    return lstp;
               }
               else return null;
          }
          public List<PartOfTest> LayKetQua(List<SubQuestion2> ListSubQuestion,int ContestantTestID,List<PART> lstp)
          {
               List<PartOfTest> parts = new List<PartOfTest>();
               List<ANSWERSHEET_DETAILS> lansd = new List<ANSWERSHEET_DETAILS>();
               for (int i=0;i< lstp.Count;i++)
               {
                    float SumSCore = 0;
                    foreach (SubQuestion2 sq2 in ListSubQuestion)
                    {
                         if(sq2.NumberIndex >= lstp[i].OrderOfQuestion && sq2.NumberIndex<= lstp[i+1].OrderOfQuestion)
                         {
                              _AnswersheetService = new AnswersheetService();
                              _AnswersheetSpeakingService = new AnswersheetSpeakingService();
                              _AnswersheetWritingService = new AnswersheetWritingService();
                              ANSWERSHEET anw = _AnswersheetService.GetByContestantTestID(ContestantTestID);
                              if (anw != null)
                              {
                                        int ansID;
                                        _AnswersheetDetailService = new AnswersheetDetailService();
                                        ANSWERSHEET_DETAILS ansd = _AnswersheetDetailService.GetBySubQuestionID(anw.AnswerSheetID, sq2.SubQuestionID);
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
                         }
                    }
                    PartOfTest iPart = new PartOfTest();
                    iPart.PartContent = lstp[i].Name;
                    iPart.Score = SumSCore;
                    parts.Add(iPart);
               }
               return parts;
          }
     }
     public class KetQuaTongNN
     {
          public int STT { get; set; }
          public string SBD { get; set; }
          public string HoTen { get; set; }
          public string PhongThi { get; set; }

          public string NgaySinh { get; set; }
          public string DiemNoi { get; set; }
          public string DiemViet { get; set; }
          public string DiemVietCN { get; set; }
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


