using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EXON.SubData.Services;
using EXON.SubModel.Models;
using EXON.Common;
using EXON.GradedEssay.Report;
using System.Threading;

namespace EXON.GradedEssay.Control
{
    public partial class ucReportKetQuaNN : UserControl
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
        #endregion Service


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

        private int _ContestID { get; set; }
        private int _LocationID { get; set; }
        public ucReportKetQuaNN(int contestID, int LocationID)
        {
            _ContestID = contestID;
            _LocationID = LocationID;
            InitializeComponent();
            //btnPrintResult.Location = new Point(10,Screen.PrimaryScreen.WorkingArea.Width/2);
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
        }

        private void InitializeControl()
        {
            _ContestantService = new ContestantService();

            cbx_LoaiBaoCao.DataSource = new List<string>{"Thi tốt nghiệp Ngoại Ngữ", "Thi A1, A2, B1 Ngoại Ngữ" };

            List<string> lst_units = new List<string>();
            lst_units.Add("-- Tất cả các lớp --");
            lst_units.AddRange((from ds in _ContestantService.GetAll()
                                select ds.Unit).Distinct().ToList());
            cbLop.DataSource = lst_units;
            cbLop.DisplayMember = "TenLop";

            cbx_MonThi.Enabled = false;
        }



        private float KetQuaTong(int contestantShiftID)
        {
            _ContestantTestService = new ContestantTestService();
            CONTESTANTS_TESTS ct = _ContestantTestService.GetByContestantShiftId(contestantShiftID);
            if (ct != null)
            {
                _AnswersheetService = new AnswersheetService();
                _AnswersheetSpeakingService = new AnswersheetSpeakingService();
                _AnswersheetWritingService = new AnswersheetWritingService();
                float result = 0;
                ANSWERSHEET anw = _AnswersheetService.GetByContestantTestID(ct.ContestantTestID);
                if (anw.TestScores != null)
                {
                    ANSWERSHEET_SPEAKING aw = _AnswersheetSpeakingService.GetByAnwsersheetID(anw.AnswerSheetID);
                    ANSWERSHEET_WRITING anwritting = _AnswersheetWritingService.GetByAnwsersheetID(anw.AnswerSheetID);
                    if (aw != null && anwritting != null)
                        if (aw.SpeakingScore != null && anwritting.WritingScore != null)
                        {

                            return (float)(aw.SpeakingScore + anwritting.WritingScore + anw.TestScores);
                        }
                        else if (aw.SpeakingScore == null && anwritting.WritingScore != null)
                        {
                            return (float)(anwritting.WritingScore + anw.TestScores);
                        }
                        else if (aw.SpeakingScore != null && anwritting.WritingScore == null)
                        {
                            if (anw.TestScores == null)
                            {
                                return (float)(aw.SpeakingScore);
                            }

                            return (float)(aw.SpeakingScore + anw.TestScores);
                        }
                        else
                        {
                            if (anw.TestScores == null)
                            {
                                return 0;
                            }
                            return (float)(anw.TestScores);
                        }
                    else if (aw != null && anwritting == null)
                        if (aw.SpeakingScore != null)
                        {

                            return (float)(aw.SpeakingScore + anw.TestScores);
                        }
                        else
                        {
                            if (anw.TestScores == null)
                            {
                                return 0;
                            }
                            return (float)(anw.TestScores);
                        }
                    else if (aw == null && anwritting != null)
                        if (anwritting.WritingScore != null)
                        {

                            return (float)(anwritting.WritingScore + anw.TestScores);
                        }
                        else
                        {
                            if (anw.TestScores == null)
                            {
                                return 0;
                            }
                            return (float)(anw.TestScores);
                        }
                    else if (aw == null && anwritting == null)
                    {
                        if (anw.TestScores == null)
                        {
                            return 0;
                        }
                        return (float)(anw.TestScores);
                    }
                        
                }
                else
                {
                    ANSWERSHEET_SPEAKING aw = _AnswersheetSpeakingService.GetByAnwsersheetID(anw.AnswerSheetID);
                    ANSWERSHEET_WRITING anwritting = _AnswersheetWritingService.GetByAnwsersheetID(anw.AnswerSheetID);
                    if (aw != null && anwritting != null)
                        if (aw.SpeakingScore != null && anwritting.WritingScore != null)
                        {

                            return (float)(aw.SpeakingScore + anwritting.WritingScore + anw.TestScores);
                        }
                        else if (aw.SpeakingScore == null && anwritting.WritingScore != null)
                        {
                            return (float)(anwritting.WritingScore);
                        }
                        else if (aw.SpeakingScore != null && anwritting.WritingScore == null)
                        {
                           
                                return (float)(aw.SpeakingScore);
                           
                        }
                        else
                        {
                                return 0;
                            
                        }
                    else if (aw != null && anwritting == null)
                        if (aw.SpeakingScore != null)
                        {

                            return (float)(aw.SpeakingScore );
                        }
                        else
                        {
                         
                                return 0;
                           
                        }
                    else if (aw == null && anwritting != null)
                        if (anwritting.WritingScore != null)
                        {

                            return (float)(anwritting.WritingScore + anw.TestScores);
                        }
                        else
                        {
                          
                                return 0;
                         
                        }
                    else if (aw == null && anwritting == null)
                    {
                        
                            return 0;
                      
                    }
                }


            }
            return 0;
        }
        private float KetQuaNghe(int contestantShiftID)
        {
            _ContestantTestService = new ContestantTestService();
            CONTESTANTS_TESTS ct = _ContestantTestService.GetByContestantShiftId(contestantShiftID);

            if (ct != null)
            {
                _AnswersheetService = new AnswersheetService();
                _AnswersheetSpeakingService = new AnswersheetSpeakingService();
                _AnswersheetWritingService = new AnswersheetWritingService();
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
                    return SumSCore;

                }


            }

            return 0;
        }
        private float KetQuaNoi(int contestantShiftID)
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
                    ANSWERSHEET_SPEAKING aw = _AnswersheetSpeakingService.GetByAnwsersheetID(anw.AnswerSheetID);

                    if (aw != null)
                    {
                        if (aw.SpeakingScore == null)
                            return 0;
                        return (float)(aw.SpeakingScore);
                    }
                }


            }
            return 0;
        }
        private float KetQuaViet(int contestantShiftID)
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

                    ANSWERSHEET_WRITING anwritting = _AnswersheetWritingService.GetByAnwsersheetID(anw.AnswerSheetID);
                    if (anwritting != null)
                    {
                        if (anwritting.WritingScore == null)
                            return 0;
                        return (float)(anwritting.WritingScore);
                    }
                }


            }
            return 0;
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

        private string DiemLamTron(int contestantShiftID)
        {
            CONTESTANTS_TESTS ct = _ContestantTestService.GetByContestantShiftId(contestantShiftID);
            if (ct != null)
            {
                ANSWERSHEET anw = _AnswersheetService.GetByContestantTestID(ct.ContestantTestID);
                if (anw != null)
                {

                    ANSWERSHEET_SPEAKING aw = _AnswersheetSpeakingService.GetByAnwsersheetID(anw.AnswerSheetID);
                    ANSWERSHEET_WRITING anwritting = _AnswersheetWritingService.GetByAnwsersheetID(anw.AnswerSheetID);
                    if (aw != null && anwritting != null)
                    {
                        float sumscore = (float)(Math.Round(aw.SpeakingScore.Value + anwritting.WritingScore.Value + anw.TestScores.Value, 2));
                        return LamTronSo(sumscore);
                    }
                }


            }
            return string.Empty;
        }
        private string LamTronSo(float sumscore)
        {
            int multiplier = 100;
            float float_value = sumscore;
            int float_result = (int)((float_value - (int)float_value) * multiplier);
            if (float_result >= 0 && float_result < 25)
            {
                int result = (int)float_value;
                return result.ToString();
            }
            else if (float_result >= 25 && float_result < 75)
            {

                float result = (float)((int)float_value + 0.5);
                return result.ToString();
            }
            else
            {
                int result = (int)float_value + 1;

                return result.ToString();
            }


        }
        private void cbLop_SelectedValueChanged(object sender, EventArgs e)
        {
               //try
               //{
               //    if (CurrentUnit != string.Empty && rbUnit.Checked)
               //    {

               //        _ScheduleService = new ScheduleService();
               //        _DivisionShiftService = new DivisionShiftService();
               //        _ContestantShiftService = new ContestantShiftService();
               //        _SubjectService = new SubjectService();
               //        _ContestantService = new ContestantService();


               //        _ContestantService = new ContestantService();
               //        List<CONTESTANT> lstct = new List<CONTESTANT>();
               //        lstct = _ContestantService.GetAll().Where(x => x.Unit == CurrentUnit).ToList();
               //        List<KetQuaTongNNDauRa> lstKQ = new List<KetQuaTongNNDauRa>();
               //        int stt = 1;
               //        foreach (CONTESTANT ct in lstct)
               //        {

               //            float TongDiem = 0;
               //            List<CONTESTANTS_SHIFTS> lstcs = new List<CONTESTANTS_SHIFTS>();
               //            _ContestantShiftService = new ContestantShiftService();
               //            KetQuaTongNNDauRa kq = new KetQuaTongNNDauRa();
               //            lstcs = _ContestantShiftService.GetAllByContestantID(ct.ContestantID).ToList();
               //            foreach (CONTESTANTS_SHIFTS cs in lstcs)
               //            {
               //                if (cs.SCHEDULE.SUBJECT.SubjectCode == "TN20" || cs.SCHEDULE.SUBJECT.SubjectCode == "TTN_TA_CB")
               //                {
               //                    kq.STT = stt;
               //                    kq.HoTen = ct.FullName;
               //                    kq.SBD = ct.ContestantCode;
               //                    kq.NgaySinh = DatetimeConvert.ConvertUnixToDateTime(ct.DOB.Value).ToString("dd/MM/yyyy");
               //                    kq.PhongThi = cs.DIVISION_SHIFTS.ROOMTEST.RoomTestName;
               //                    kq.Unit = ct.Unit;
               //                    kq.DiemDoc = ((KetQuaTracNghiem(cs.ContestantShiftID)) - (KetQuaNghe(cs.ContestantShiftID))).ToString();
               //                    //kq.DiemNghe = KetQuaNghe(cs.ContestantShiftID).ToString();
               //                    kq.DiemNghe = KetQuaNghe(cs.ContestantShiftID);
               //                    kq.DiemNoi = KetQuaNoi(cs.ContestantShiftID).ToString();
               //                    kq.DiemViet = KetQuaViet(cs.ContestantShiftID).ToString();
               //                    TongDiem += KetQuaTong(cs.ContestantShiftID);

               //                }
               //                else
               //                {
               //                    kq.DiemDocCN = KetQuaTracNghiem(cs.ContestantShiftID).ToString();
               //                    kq.DiemVietCN = KetQuaViet(cs.ContestantShiftID).ToString();
               //                    TongDiem += KetQuaTong(cs.ContestantShiftID);

               //                }

               //            }
               //            kq.DiemTongCNCB = TongDiem.ToString();
               //            lstKQ.Add(kq);
               //            stt++;

               //        }
               //        gvMain.DataSource = lstKQ;
               //    }
               //}
               //catch (Exception ex)
               //{
               //    MessageBox.Show(ex.InnerException.Message, "Lỗi");
               //}

               Init_cbxMonThi();
        }
        private string GetScore(int? contestantShiftID)
        {
            if (contestantShiftID.HasValue)
            {
                CONTESTANTS_TESTS ct = _ContestantTestService.GetByContestantShiftId(contestantShiftID.Value);
                if (ct != null)
                {
                    ANSWERSHEET anw = _AnswersheetService.GetByContestantTestID(ct.ContestantTestID);
                    if (anw != null)
                    {
                        return (anw.TestScores).ToString();
                    }

                }
            }
            return string.Empty;
        }
        private string GetScoreSpeaking(int? contestantShiftID)
        {
            if (contestantShiftID.HasValue)
            {
                CONTESTANTS_TESTS ct = _ContestantTestService.GetByContestantShiftId(contestantShiftID.Value);
                if (ct != null)
                {
                    ANSWERSHEET anw = _AnswersheetService.GetByContestantTestID(ct.ContestantTestID);
                    if (anw != null)
                    {
                        ANSWERSHEET_SPEAKING aw = _AnswersheetSpeakingService.GetByAnwsersheetID(anw.AnswerSheetID);

                        if (aw != null)
                        {
                            return (aw.SpeakingScore).ToString();
                        }
                    }

                }
            }
            return string.Empty;
        }
        private string GetScoreWritting(int? contestantShiftID)
        {
            if (contestantShiftID.HasValue)
            {
                CONTESTANTS_TESTS ct = _ContestantTestService.GetByContestantShiftId(contestantShiftID.Value);
                if (ct != null)
                {
                    ANSWERSHEET anw = _AnswersheetService.GetByContestantTestID(ct.ContestantTestID);
                    if (anw != null)
                    {
                        ANSWERSHEET_WRITING aw = _AnswersheetWritingService.GetByAnwsersheetID(anw.AnswerSheetID);

                        if (aw != null)
                        {
                            return (aw.WritingScore).ToString();
                        }
                    }

                }
            }
            return string.Empty;
        }


        private void btnKetQuaLop_Click(object sender, EventArgs e)
        {
            try
            {

                if (CurrentUnit != string.Empty/* && rbUnit.Checked*/)
                {
                    SplashScreenManager.ShowSplashScreen();
                    Thread.Sleep(100);
                    if (cbx_LoaiBaoCao.Text == "Thi tốt nghiệp Ngoại Ngữ")
                    {
                        Report.frmRpKetQuaTongNNDauRaTNNN frm = new Report.frmRpKetQuaTongNNDauRaTNNN(_LocationID, CurrentUnit);
                        SplashScreenManager.CloseForm();
                        frm.ShowDialog();
                    }
                    else if (cbx_LoaiBaoCao.Text == "Thi A1, A2, B1 Ngoại Ngữ")
                    {
                        Report.frmRpKetQuaTongNNDauRa frm = new Report.frmRpKetQuaTongNNDauRa(_LocationID, CurrentUnit, CurrentSubject);
                        SplashScreenManager.CloseForm();
                        frm.ShowDialog();
                    }                    
                    this.Update();
                  
                }
                else
                {
                    MessageBox.Show("Chọn lớp!");
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void cbx_LoaiBaoCao_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbx_LoaiBaoCao.Text == "Thi tốt nghiệp Ngoại Ngữ")
            {
                cbx_MonThi.Enabled = false;
            }
            else if (cbx_LoaiBaoCao.Text == "Thi A1, A2, B1 Ngoại Ngữ")
            {
                cbx_MonThi.Enabled = true;
                Init_cbxMonThi();
            }
        }

        private void Init_cbxMonThi()
        {
            if (cbx_LoaiBaoCao.Text == "Thi A1, A2, B1 Ngoại Ngữ")
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
        }
    }
}
