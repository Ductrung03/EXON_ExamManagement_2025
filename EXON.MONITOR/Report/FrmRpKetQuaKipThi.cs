using EXON.Common;
using EXON.MONITOR.Common;
using EXON.SubData.Services;
using EXON.SubModel.Models;
using Microsoft.Reporting.WinForms;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EXON.MONITOR.Report
{
    public partial class FrmRpKetQuaKipThi : Form
    {
        MTAQuizDbContext db = new MTAQuizDbContext();

        private DIVISION_SHIFTS divisionShift = new DIVISION_SHIFTS();

        #region Hàm khởi tạo
        public FrmRpKetQuaKipThi(int divisionshiftID)
        {
            InitializeComponent();
            divisionShift = db.DIVISION_SHIFTS.Where(x => x.DivisionShiftID == divisionshiftID).SingleOrDefault();
        }
        #endregion

        #region LoadForm

        private bool ok(int x, DateTime k)
        {
            DateTime a = Common.DatetimeConvert.ConvertUnixToDateTime(x);

            if (a.Year != k.Year) return false;
            if (a.Month != k.Month) return false;
            if (a.Day != k.Day) return false;

            return true;
        }

        private string KetQua(CONTESTANT a)
        {
            try
            {
                CONTESTANTS_SHIFTS cs = db.CONTESTANTS_SHIFTS.Where(p => p.Status > 0 && p.DivisionShiftID == divisionShift.DivisionShiftID && p.ContestantID == a.ContestantID).FirstOrDefault();
                float diem = 0;
                try
                {

                    diem = (float)
                            (
                                from ct in db.CONTESTANTS_TESTS.Where(k => k.Status > 0 && k.ContestantShiftID == cs.ContestantShiftID).ToList()
                                from ans in db.ANSWERSHEETS.Where(k => k.ContestantTestID == ct.ContestantTestID).ToList()
                                select ans
                            )
                            .FirstOrDefault()
                            .TestScores;
                }
                catch
                {
                    diem = 0;
                }

                return diem.ToString();
            }
            catch(Exception ex)
            {
                Log.Instance.WriteErrorLog(Properties.Resources.MSG_LOG_ERROR, string.Format("Expetion : {0}  ", ex.Message));

            }

            return "-";
        }


        private int Conv(string a)
        {
            try
            {
                return Int32.Parse(a);
            }
            catch
            {
                return 0;
            }
        }

        private string DiemTong(string a, string b, string c)
        {
            if (a == "-" && b == "-" && c == "-") return "-";
            return (Conv(a) + Conv(b) + Conv(c)).ToString();
        }

        private CONTESTANTS_TESTS GetContestantTest(int contestantShiftID)
        {
            return db.CONTESTANTS_TESTS.FirstOrDefault(x => x.Status > 0 && x.ContestantShiftID == contestantShiftID);
        }

        private string FormatSubmitTime(int? submitTime)
        {
            if (!submitTime.HasValue || submitTime.Value <= 0)
            {
                return string.Empty;
            }

            return Common.DatetimeConvert.ConvertUnixToDateTime(submitTime.Value).ToString("HH:mm:ss");
        }

        private string FormatWorkedTime(int? timeWorked)
        {
            if (!timeWorked.HasValue)
            {
                return string.Empty;
            }

            int safeSeconds = Math.Max(timeWorked.Value, 0);
            TimeSpan duration = TimeSpan.FromSeconds(safeSeconds);
            int totalHours = (int)duration.TotalHours;
            return string.Format("{0:00}:{1:00}:{2:00}", totalHours, duration.Minutes, duration.Seconds);
        }

        private long GetWorkedTimeForSort(int? timeWorked)
        {
            return timeWorked.HasValue && timeWorked.Value >= 0
                ? timeWorked.Value * 1000L
                : long.MaxValue;
        }

        private double ParseScore(string scoreText)
        {
            double score;

            if (double.TryParse(scoreText, NumberStyles.Float, CultureInfo.InvariantCulture, out score))
            {
                return score;
            }

            if (double.TryParse(scoreText, NumberStyles.Float, CultureInfo.CurrentCulture, out score))
            {
                return score;
            }

            return 0;
        }

        private void FrmRpKetQuaKipThi_Load(object sender, EventArgs e)
        {
            try
            {
                db = new MTAQuizDbContext();
                // lấy thông tin của kip thi
                
                LOCATION diadiem = db.LOCATIONS.Where(p => p.LocationID == divisionShift.ROOMTEST.LocationID).FirstOrDefault();
                CONTEST kithi = db.CONTESTS.Where(p => p.ContestID == diadiem.ContestID).FirstOrDefault();
                // Lấy ra danh sách các thí sinh thi c
                List<CONTESTANTS_SHIFTS> listThiSinh = new List<CONTESTANTS_SHIFTS>();
                listThiSinh = db.CONTESTANTS_SHIFTS.Where(x => x.DivisionShiftID == divisionShift.DivisionShiftID && x.Status==3005).ToList();
                // Lấy ra kết quả thi DiemKhiChuaBonus(p.ContestantShiftID)
                ExamSessionLogService examSessionLogService = new ExamSessionLogService();
                var listKetQua = listThiSinh
                                 .Select(p =>
                                  {
                                      CONTESTANTS_TESTS contestantTest = GetContestantTest(p.ContestantShiftID);
                                      string diemThi = KetQua(p.CONTESTANT);
                                       var audit = examSessionLogService.GetLatestByContestantShiftId(p.ContestantShiftID);
                                       string submitTimeDisplay = !string.IsNullOrWhiteSpace(audit?.Audit?.SubmitTimeText)
                                           ? audit.Audit.SubmitTimeText
                                           : FormatSubmitTime(contestantTest?.SubmitTime);
                                       string workedTimeDisplay = !string.IsNullOrWhiteSpace(audit?.Audit?.TimeWorkedText)
                                           ? audit.Audit.TimeWorkedText
                                           : FormatWorkedTime(p.TimeWorked);

                                      return new
                                      {
                                          p.CONTESTANT.ContestantCode,
                                          p.CONTESTANT.FullName,
                                          NgaySinh = Common.DatetimeConvert.ConvertUnixToDateTime((int)p.CONTESTANT.DOB).ToString("dd/MM/yyyy"),
                                          DiemGoc = DiemKhiChuaBonus(p.ContestantShiftID),
                                          MonThi = p.SCHEDULE.SUBJECT.SubjectName,
                                          DiemThi = diemThi,
                                          MaDe = GetTestID(p.ContestantShiftID),
                                          Unit = p.CONTESTANT.Unit,
                                           SubmitTime = submitTimeDisplay,
                                           WorkedTime = workedTimeDisplay,
                                          ScoreSort = ParseScore(diemThi),
                                          WorkedTimeSort = GetWorkedTimeForSort(p.TimeWorked)
                                      };
                                   })
                                 .OrderByDescending(p => p.ScoreSort)
                                 .ThenBy(p => p.WorkedTimeSort)
                                 .Select((p, index) => new
                                 {
                                     STT = index + 1,
                                     SBD = p.ContestantCode,
                                     HoTen = p.FullName,
                                     p.NgaySinh,
                                     p.DiemGoc,
                                     p.MonThi,
                                     p.DiemThi,
                                     p.MaDe,
                                     p.Unit,
                                     p.SubmitTime,
                                     p.WorkedTime
                                 })
                                 .ToList();
                ketQuaThiTheoCaThiBindingSource.DataSource = listKetQua;
                List<CONTESTANTS_SHIFTS> listThiSinhBoThi = new List<CONTESTANTS_SHIFTS>();
                listThiSinhBoThi = db.CONTESTANTS_SHIFTS.Where(x => x.DivisionShiftID == divisionShift.DivisionShiftID && x.Status == 4001).ToList();
                int stt = 0;
                var lstThiSinhBoThi = listThiSinhBoThi
                                 .Select(p => new
                                  {
                                     STT = ++stt,
                                     SBD = p.CONTESTANT.ContestantCode,
                                     HoTen = p.CONTESTANT.FullName,
                                     NgaySinh = Common.DatetimeConvert.ConvertUnixToDateTime((int)p.CONTESTANT.DOB).ToString("dd/MM/yyyy"),
                                     CMND = p.CONTESTANT.IdentityCardNumber,
                                     MonThi = p.SCHEDULE.SUBJECT.SubjectName,
                                     Unit = p.CONTESTANT.Unit
                                 })
                                 .ToList();
                thiSinhBoThiBindingSource.DataSource = lstThiSinhBoThi;

                // add parameter
                ReportParameter[] listPara = new ReportParameter[]{
                    new ReportParameter("ContestName",kithi.ContestName.ToUpper()),
                    new ReportParameter("LocationName",diadiem.LocationName.ToUpper()),
                    new ReportParameter("RoomTestName",divisionShift.ROOMTEST.RoomTestName.ToLower()),
                     new ReportParameter("StartTime",Common.DatetimeConvert.ConvertUnixToDateTime(divisionShift.SHIFT.StartTime).ToString("HH: mm:ss")),
                     new ReportParameter("EndTime",Common.DatetimeConvert.ConvertUnixToDateTime(divisionShift.SHIFT.EndTime).ToString("HH: mm:ss")),
                    new ReportParameter("NgayThi",Common.DatetimeConvert.ConvertUnixToDateTime(divisionShift.SHIFT.ShiftDate).ToString("dd/MM/yyyy")),
                    new ReportParameter("Date",DateTime.Now.ToString(@"\n\g\à\y dd \t\h\á\n\g MM \n\ă\m yyyy"))
                };
                this.rpKetQuaKipThi.LocalReport.SetParameters(listPara);
                this.rpKetQuaKipThi.LocalReport.Refresh();
                this.rpKetQuaKipThi.RefreshReport();
            }
            catch (Exception ex)
            {
                Log.Instance.WriteErrorLog(Properties.Resources.MSG_LOG_ERROR, string.Format("Expetion : {0}  ", ex.Message));

                MessageBox.Show(ex.Message);
            }
            this.rpKetQuaKipThi.LocalReport.Refresh();
            this.rpKetQuaKipThi.RefreshReport();
            this.rpKetQuaKipThi.RefreshReport();
        }

        //private ContestantTestService _ContestantTestService;
        //private AnswersheetService _AnswersheetService;

        private string DiemKhiChuaBonus(int contestantShiftID)
        {
            // 1) Tìm bài thi
            var ct = db.CONTESTANTS_TESTS
                       .FirstOrDefault(x => x.ContestantShiftID == contestantShiftID);
            if (ct == null) return "0";

            // 2) Tìm phiếu trả lời
            var aw = db.ANSWERSHEETS
                       .FirstOrDefault(x => x.ContestantTestID == ct.ContestantTestID);
            if (aw == null) return "0";

            // 3) Tính tổng điểm các câu đúng
            //    - Join DETAILS -> ANSWERS (điều kiện đúng)
            //    - Join SUBQUESTIONS để lấy Score (có thể null)
            var total = (from d in db.ANSWERSHEET_DETAILS
                         join a in db.ANSWERS on d.ChoosenAnswer equals a.AnswerID
                         join s in db.SUBQUESTIONS on a.SubQuestionID equals s.SubQuestionID into sj
                         from s in sj.DefaultIfEmpty()
                         where d.AnswerSheetID == aw.AnswerSheetID && a.IsCorrect
                         select (double?)(s.Score ?? 0))
                        .Sum() ?? 0.0;

            var totalFillAnswer = (from a in db.ANSWERSHEETS
                                   join d in db.ANSWERSHEET_DETAILS on a.AnswerSheetID equals d.AnswerSheetID
                                   where d.AnswerSheetID == aw.AnswerSheetID
                                   select (double?)(d.Score ?? 0)).Sum() ?? 0.0;

            // Trả về dạng chuỗi
            return (total+ totalFillAnswer).ToString();
        }


        private int GetTestID(int contestantShiftID)
        {
            db = new MTAQuizDbContext();
            CONTESTANTS_TESTS cts = new CONTESTANTS_TESTS();
            cts = db.CONTESTANTS_TESTS.Where(x => x.ContestantShiftID == contestantShiftID).SingleOrDefault();
            if(cts!=null)
            {
                return cts.TestID;
            }
            else
            {
                return -1;
            }
        }
        #endregion
    }
}
