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
          public ucReportPart(int contestID, int LocationID)
          {
               _ContestID = contestID;
               _LocationID = LocationID;
               InitializeComponent();
               InitializeService();

               List<string> lst_units = new List<string>();
               lst_units.Add("-- Tất cả các lớp --");
               lst_units.AddRange((from ds in _ContestantService.GetAll()
                                   select ds.Unit).Distinct().ToList());
               cbLop.DataSource = lst_units;
               cbLop.DisplayMember = "TenLop";
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
               if (CurrentUnit != string.Empty/* && rbUnit.Checked*/)
               {
                    List<CONTESTANT> lstct = new List<CONTESTANT>();
                    //if (CurrentUnit!= "")
                    //     lstct = _ContestantService.GetMultiByUnit(CurrentUnit).ToList();
                    //else
                    //     lstct = _ContestantService.GetAll().OrderBy(p => p.ContestantCode).ToList();
                    if (CurrentUnit == "Rada")
                         lstct = _ContestantService.GetMultiByUnit(CurrentUnit).ToList();
                    gvMain.DataSource = lstct;
               }
               else
               {

               }
          }
          private string KetQuaTracNghiem(int contestantShiftID)
          {

               CONTESTANTS_TESTS ct = _ContestantTestService.GetByContestantShiftId(contestantShiftID);
               if (ct != null)
               {
                    ANSWERSHEET anw = _AnswersheetService.GetByContestantTestID(ct.ContestantTestID);
                    if (anw != null)
                    {

                         if (anw.TestScores == null)
                              return 0.ToString();
                         return (anw.TestScores).ToString();

                    }


               }
               return 0.ToString();
          }
          private string KetQuaNghe(int contestantShiftID)
          {

               CONTESTANTS_TESTS ct = _ContestantTestService.GetByContestantShiftId(contestantShiftID);

               if (ct != null)
               {
                    ANSWERSHEET anw = _AnswersheetService.GetByContestantTestID(ct.ContestantTestID);
                    float SumSCore = 0;
                    if (anw != null)
                    {
                         int ansID;
                         _AnswersheetDetailService = new AnswersheetDetailService();
                         List<ANSWERSHEET_DETAILS> lstaws = _AnswersheetDetailService.getAllByAnswerID(anw.AnswerSheetID).ToList();
                         ANSWER aw = new ANSWER();
                         foreach (ANSWERSHEET_DETAILS item in lstaws)
                         {
                              ansID = item.ChoosenAnswer ?? default(int);
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
                         return SumSCore.ToString();

                    }


               }
               return 0.ToString();
          }
          private string KetQuaViet(int contestantShiftID)
          {

               CONTESTANTS_TESTS ct = _ContestantTestService.GetByContestantShiftId(contestantShiftID);
               if (ct != null)
               {
                    ANSWERSHEET anw = _AnswersheetService.GetByContestantTestID(ct.ContestantTestID);
                    if (anw != null)
                    {

                         ANSWERSHEET_WRITING anwritting = _AnswersheetWritingService.GetByAnwsersheetID(anw.AnswerSheetID);
                         if (anwritting != null)
                         {
                              if (anwritting.WritingScore == null)
                                   return 0.ToString();
                              return (anwritting.WritingScore).ToString();
                         }
                    }


               }
               return 0.ToString();
          }
          private string KetQuaNoi(int contestantShiftID)
          {

               CONTESTANTS_TESTS ct = _ContestantTestService.GetByContestantShiftId(contestantShiftID);
               if (ct != null)
               {
                    ANSWERSHEET anw = _AnswersheetService.GetByContestantTestID(ct.ContestantTestID);
                    if (anw != null)
                    {
                         ANSWERSHEET_SPEAKING aw = _AnswersheetSpeakingService.GetByAnwsersheetID(anw.AnswerSheetID);

                         if (aw != null)
                         {
                              if (aw.SpeakingScore == null)
                                   return 0.ToString();
                              return (aw.SpeakingScore).ToString();
                         }
                    }


               }
               return 0.ToString();
          }

          private void cbLop_SelectedIndexChanged(object sender, EventArgs e)
          {
               if (CurrentUnit != string.Empty/* && rbUnit.Checked*/)
               {
                    List<CONTESTANT> lstct = new List<CONTESTANT>();
                    //if (CurrentUnit!= "")
                    //     lstct = _ContestantService.GetMultiByUnit(CurrentUnit).ToList();
                    //else
                    //     lstct = _ContestantService.GetAll().OrderBy(p => p.ContestantCode).ToList();
                    
                    if (CurrentUnit == "Ra đa")
                         lstct = _ContestantService.GetMultiByUnit(CurrentUnit).ToList();
                    //List<KetQuaTongNNDauRa> lstKQ = new List<KetQuaTongNNDauRa>();
                    //int stt = 1;
                    //foreach (CONTESTANT ct in lstct)
                    //{

                    //     float TongDiem = 0;
                    //     List<CONTESTANTS_SHIFTS> lstcs = new List<CONTESTANTS_SHIFTS>();
                    //     _ContestantShiftService = new ContestantShiftService();
                    //     KetQuaTongNNDauRa kq = new KetQuaTongNNDauRa();
                    //     kq.STT = -1;
                    //     lstcs = _ContestantShiftService.GetAllByContestantID(ct.ContestantID).ToList();
                    //     foreach (CONTESTANTS_SHIFTS cs in lstcs)
                    //     {

                    //               kq.STT = stt;
                    //               kq.HoTen = ct.FullName;
                    //               kq.SBD = ct.ContestantCode;
                    //               kq.NgaySinh = DatetimeConvert.ConvertUnixToDateTime(ct.DOB.Value).ToString("dd/MM/yyyy");
                    //               kq.PhongThi = cs.DIVISION_SHIFTS.ROOMTEST.RoomTestName;
                    //               kq.Unit = ct.Unit;
                    //               //kq.DiemDoc = ((KetQuaTracNghiem(cs.ContestantShiftID)) - (KetQuaNghe(cs.ContestantShiftID))).ToString();
                    //               //kq.DiemNghe = KetQuaNghe(cs.ContestantShiftID).ToString();
                    //               //kq.DiemNghe = KetQuaNghe(cs.ContestantShiftID);
                    //               //kq.DiemDoc = ((KetQuaTracNghiem(cs.ContestantShiftID)) - kq.DiemNghe).ToString();
                    //               //kq.DiemNoi = KetQuaNoi(cs.ContestantShiftID).ToString();
                    //               //kq.DiemViet = KetQuaViet(cs.ContestantShiftID).ToString();
                    //               //TongDiem += KetQuaTong(cs.ContestantShiftID);
                    //     }
                    //     kq.DiemTongCNCB = TongDiem.ToString();
                    //     if (kq.STT != -1)
                    //     {
                    //          lstKQ.Add(kq);
                    //          stt++;
                    //     }

                    //}
                    MTAQuizDbContext db = new MTAQuizDbContext();
                    List<SubQuestion2> ListSubQuestion = new List<SubQuestion2>();
                    foreach (CONTESTANT ct in lstct)
                    {
                         if (ct.ContestantID == 4018)
                         {
                              //List<CONTESTANTS_SHIFTS> lstcs = new List<CONTESTANTS_SHIFTS>();
                              //_ContestantShiftService = new ContestantShiftService();
                              //lstcs = _ContestantShiftService.GetAllByContestantID(ct.ContestantID).ToList();
                              //foreach (CONTESTANTS_SHIFTS cs in lstcs)
                              //{
                              _ContestantShiftService = new ContestantShiftService();
                              CONTESTANTS_SHIFTS cs = _ContestantShiftService.GetFirstByContesttantID(ct.ContestantID);
                              CONTESTANTS_TESTS ctt = _ContestantTestService.GetByContestantShiftId(cs.ContestantShiftID);
                              var List = (from sub in db.SUBQUESTIONS
                                          join Test in db.TEST_DETAILS on sub.QuestionID equals Test.QuestionID
                                          where (Test.TestID == 7574)
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


                              //}
                              gvMain.DataSource = LayKetQua(ListSubQuestion, ctt.ContestantTestID);
                         }
                    }
                    //gvMain.DataSource = ListSubQuestion;
               }
               else
               {

               }
          }
          public List<PartOfTest> LayKetQua(List<SubQuestion2> ListSubQuestion,int ContestantTestID)
          {
               _PartService = new PastService();
               List<PartOfTest> parts = new List<PartOfTest>();
               List<PART> lstp = new List<PART>();
               MTAQuizDbContext db = new MTAQuizDbContext();
               lstp = (from obj in db.PARTS select obj).OrderBy(x => x.OrderInTest).ToList();
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


