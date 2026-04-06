using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EXON.Common;
using EXON.SubModel.Models;

namespace QuanLyHoiDongThiVer2.report
{
    public partial class frmRpKetQuaCaThi : Form
    {
        MTAQuizDbContext db = new MTAQuizDbContext();

        private DIVISION_SHIFTS divisionShift = new DIVISION_SHIFTS();
        public frmRpKetQuaCaThi(int divisionshiftID)
        {
            InitializeComponent();
            divisionShift = db.DIVISION_SHIFTS.Where(x => x.DivisionShiftID == divisionshiftID).SingleOrDefault();

        }
        #region LoadForm

       

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
            catch
            {

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


     

        private int GetTestID(int contestantShiftID)
        {
            db = new MTAQuizDbContext();
            CONTESTANTS_TESTS cts = new CONTESTANTS_TESTS();
            cts = db.CONTESTANTS_TESTS.Where(x => x.ContestantShiftID == contestantShiftID).SingleOrDefault();
            if (cts != null)
            {
                return cts.TestID;
            }
            else
            {
                return -1;
            }
        }

        private CONTESTANTS_TESTS GetContestantTest(int contestantShiftID)
        {
            return db.CONTESTANTS_TESTS.FirstOrDefault(x => x.Status > 0 && x.ContestantShiftID == contestantShiftID);
        }

        private long? ConvertSubmitTimeToMilliseconds(int? submitTime)
        {
            if (!submitTime.HasValue || submitTime.Value <= 0)
            {
                return null;
            }

            return submitTime.Value * 1000L;
        }

        private long? ConvertWorkedTimeToMilliseconds(int? timeWorked)
        {
            if (!timeWorked.HasValue)
            {
                return null;
            }

            return Math.Max(timeWorked.Value, 0) * 1000L;
        }

        private double ParseScore(string scoreText)
        {
            double score;
            return double.TryParse(scoreText, out score) ? score : 0;
        }

        private long GetWorkedTimeForSort(int? timeWorked)
        {
            return timeWorked.HasValue && timeWorked.Value >= 0 ? timeWorked.Value * 1000L : long.MaxValue;
        }
        #endregion

        private void FrmRpKetQuaCaThi_Load(object sender, EventArgs e)
        {
            try
            {
                db = new MTAQuizDbContext();
                // lấy thông tin của kip thi

                LOCATION diadiem = db.LOCATIONS.Where(p => p.LocationID == divisionShift.ROOMTEST.LocationID).FirstOrDefault();
                CONTEST kithi = db.CONTESTS.Where(p => p.ContestID == diadiem.ContestID).FirstOrDefault();
                // Lấy ra danh sách các thí sinh thi c
                List<CONTESTANTS_SHIFTS> listThiSinh = new List<CONTESTANTS_SHIFTS>();
                listThiSinh = db.CONTESTANTS_SHIFTS.Where(x => x.DivisionShiftID == divisionShift.DivisionShiftID && x.Status == 3005).ToList();
                // Lấy ra kết quả thi
                var listKetQua = listThiSinh
                                 .Select(p =>
                                  {
                                      string diemThi = KetQua(p.CONTESTANT);
                                      CONTESTANTS_TESTS contestantTest = GetContestantTest(p.ContestantShiftID);

                                      return new
                                      {
                                          SBD = p.CONTESTANT.ContestantCode,
                                          HoTen = p.CONTESTANT.FullName,
                                          NgaySinh = DateTimeHelpers.ConvertUnixToDateTime((int)p.CONTESTANT.DOB).ToString("dd/MM/yyyy"),
                                          MonThi = p.SCHEDULE.SUBJECT.SubjectName,
                                          DiemThi = diemThi,
                                          MaDe = GetTestID(p.ContestantShiftID),
                                          Unit = p.CONTESTANT.Unit,
                                          SubmitTime = ConvertSubmitTimeToMilliseconds(contestantTest != null ? contestantTest.SubmitTime : (int?)null),
                                          WorkedTime = ConvertWorkedTimeToMilliseconds(p.TimeWorked),
                                          ScoreSort = ParseScore(diemThi),
                                          WorkedTimeSort = GetWorkedTimeForSort(p.TimeWorked)
                                      };
                                  })
                                 .OrderByDescending(p => p.ScoreSort)
                                 .ThenBy(p => p.WorkedTimeSort)
                                 .Select((p, index) => new
                                 {
                                     STT = index + 1,
                                     p.SBD,
                                     p.HoTen,
                                     p.NgaySinh,
                                     p.MonThi,
                                     p.DiemThi,
                                     p.MaDe,
                                     p.Unit,
                                     p.SubmitTime,
                                     p.WorkedTime
                                 })
                                 .ToList();
                ketQuaThiTheoCaThi1BindingSource1.DataSource = listKetQua;
                List<CONTESTANTS_SHIFTS> listThiSinhBoThi = new List<CONTESTANTS_SHIFTS>();
                listThiSinhBoThi = db.CONTESTANTS_SHIFTS.Where(x => x.DivisionShiftID == divisionShift.DivisionShiftID && x.Status == 4001).ToList();
                var lstThiSinhBoThi = listThiSinhBoThi
                                 .Select((p, index) => new
                                  {
                                       STT = index + 1,
                                       SBD = p.CONTESTANT.ContestantCode,
                                       HoTen = p.CONTESTANT.FullName,
                                      NgaySinh = DateTimeHelpers.ConvertUnixToDateTime((int)p.CONTESTANT.DOB).ToString("dd/MM/yyyy"),
                                      CMND = p.CONTESTANT.IdentityCardNumber,
                                      MonThi = p.SCHEDULE.SUBJECT.SubjectName,
                                      Unit = p.CONTESTANT.Unit
                                  })
                                 .ToList();
                thiSinhBoThiBindingSource1.DataSource = lstThiSinhBoThi;

                // add parameter
                ReportParameter[] listPara = new ReportParameter[]{
                    new ReportParameter("ContestName",kithi.ContestName.ToUpper()),
                    new ReportParameter("LocationName",diadiem.LocationName.ToUpper()),
                    new ReportParameter("RoomTestName",divisionShift.ROOMTEST.RoomTestName.ToLower()),
                     new ReportParameter("StartTime",DateTimeHelpers.ConvertUnixToDateTime(divisionShift.SHIFT.StartTime).ToString("HH: mm:ss")),
                     new ReportParameter("EndTime",DateTimeHelpers.ConvertUnixToDateTime(divisionShift.SHIFT.EndTime).ToString("HH: mm:ss")),
                    new ReportParameter("NgayThi",DateTimeHelpers.ConvertUnixToDateTime(divisionShift.SHIFT.ShiftDate).ToString("dd/MM/yyyy")),
                    new ReportParameter("Date",DateTime.Now.ToString(@"\n\g\à\y dd \t\h\á\n\g MM \n\ă\m yyyy"))
                };
                this.rpKetQuaKipThi.LocalReport.SetParameters(listPara);
                this.rpKetQuaKipThi.LocalReport.Refresh();
                this.rpKetQuaKipThi.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.rpKetQuaKipThi.LocalReport.Refresh();
            this.rpKetQuaKipThi.RefreshReport();
            this.rpKetQuaKipThi.RefreshReport();
        }
    }
}
