using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EXON.SubModel.Models;
using EXON.MONITOR.Common;
using EXON.Common;
using Microsoft.Reporting.WinForms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace EXON.MONITOR.Report
{
    public partial class frmShowRPLogChangeAws : Form
    {
        private DIVISION_SHIFTS dv = new DIVISION_SHIFTS();
        private MTAQuizDbContext db;
        string SBD;
        string made;
        public frmShowRPLogChangeAws()
        {
            InitializeComponent();
            MTAQuizDbContext db = new MTAQuizDbContext();
        }

        public frmShowRPLogChangeAws(DIVISION_SHIFTS tg,string SBD)
        {
            InitializeComponent();
            dv = tg;
            MTAQuizDbContext db = new MTAQuizDbContext();
            this.SBD = SBD;
        }

        private void frmShowRPLogChangeAws_Load(object sender, EventArgs e)
        {
            try
            {
                EXON.MONITOR.Report.ReportDataset dataSet = new EXON.MONITOR.Report.ReportDataset();
                dataSet.Tables.Clear();
                db = new MTAQuizDbContext();

                ROOMTEST roomTest = db.ROOMTESTS.Where(p => p.RoomTestID == dv.RoomTestID).FirstOrDefault();
                LOCATION location = db.LOCATIONS.Where(p => p.LocationID == roomTest.LocationID).FirstOrDefault();
                CONTEST kithi = db.CONTESTS.Where(p => p.ContestID == location.ContestID).FirstOrDefault();
                SHIFT shift = db.SHIFTS.Where(p => p.ShiftID == dv.ShiftID).FirstOrDefault();
                STAFF staff = db.STAFFS.Where(p => p.StaffID == kithi.CreatedStaffID).FirstOrDefault();
                CONTESTANT sv = db.CONTESTANTS.Where(p => p.ContestantID.ToString() == SBD).FirstOrDefault();

                //
                int i = 1;
                List<VIOLATION> listviolation = db.VIOLATIONS.Where(p => p.Level == 8004).ToList();
                var logchangeaws = (
                from lt in listviolation.Where(pz => pz.Level == 8004).ToList()
                select new
                {
                    STT = ((string)JObject.Parse(lt.Description)["contestantid"] == SBD) ? i++ : 0,
                    MADE = ((string)JObject.Parse(lt.Description)["contestantid"] == SBD) ? (string)JObject.Parse(lt.Description)["test_id"] : null,
                    STTCAU = ((string)JObject.Parse(lt.Description)["contestantid"] == SBD) ? (string)JObject.Parse(lt.Description)["subquestionid"] : null,
                    DAPANCU = ((string)JObject.Parse(lt.Description)["contestantid"] == SBD) ? (string)JObject.Parse(lt.Description)["old_aws"] : null,
                    DAPANMOI = ((string)JObject.Parse(lt.Description)["contestantid"] == SBD) ? (string)JObject.Parse(lt.Description)["new_aws"] : null,
                    THOIGIAN = ((string)JObject.Parse(lt.Description)["contestantid"] == SBD) ? (string)JObject.Parse(lt.Description)["timeinterrupt"] : null
                }
                ).ToList();

                var logchangaws2 = (
                from lt in logchangeaws.Where(pz => pz.STT != 0).ToList()
                select new
                {
                    STT = lt.STT,
                    STTCAU = STTCAUHOI(lt.STTCAU,lt.MADE),
                    DAPANCU = STTCAUTRALOI(lt.STTCAU, lt.MADE, lt.DAPANCU),
                    DAPANMOI = STTCAUTRALOI(lt.STTCAU, lt.MADE, lt.DAPANMOI),
                    THOIGIAN = UnixTimeStampToDateTime(double.Parse(lt.THOIGIAN, System.Globalization.CultureInfo.InvariantCulture))
                }
                ).ToList();

                dataSet.Tables.Add(EXON.Common.Helper.ToDataTable(logchangaws2));
                ReportDataSource rp = new ReportDataSource("LogChangeAws", dataSet.Tables[0]);
                //
                this.reportViewer1.LocalReport.DataSources.Clear();
                this.reportViewer1.LocalReport.DataSources.Add(rp);


                ReportParameter[] listPara = new ReportParameter[]{
                    new ReportParameter("ContestName",kithi.ContestName.ToUpper()),
                    new ReportParameter("ShiftName",shift.ShiftName),
                    new ReportParameter("LocationName", location.LocationName),
                    new ReportParameter("RoomTestName", roomTest.RoomTestName),
                    new ReportParameter("ShiftDate", EXON.Common.DateTimeHelpers.ConvertUnixToDateTime(shift.ShiftDate).ToString("dd/MM/yyyy")),
                    new ReportParameter("StartTime",EXON.Common.DateTimeHelpers.ConvertUnixToDateTime(shift.StartTime).ToString("HH:mm")),
                    new ReportParameter("EndTime", EXON.Common.DateTimeHelpers.ConvertUnixToDateTime(shift.EndTime).ToString("HH:mm")),
                    new ReportParameter("HoTen", sv.FullName),
                    new ReportParameter("SBD", sv.ContestantCode),
                    new ReportParameter("FullName", staff.FullName.ToUpper())
                };
                this.reportViewer1.LocalReport.SetParameters(listPara);
            }
            catch (Exception ex)
            {
                Log.Instance.WriteErrorLog(Properties.Resources.MSG_LOG_ERROR, string.Format("Expetion : {0}  ", ex.Message));

                MessageBox.Show(ex.Message);
            }
            this.reportViewer1.LocalReport.Refresh();
            this.reportViewer1.RefreshReport();
        }

        private string STTCAUHOI(string chuoi, string made)
        {
            string hienthi;
            int sttcauhoi = 0;
            SUBQUESTION sqs = db.SUBQUESTIONS.Where(p => p.SubQuestionID.ToString() == chuoi).FirstOrDefault();
            TEST_DETAILS txtdt = db.TEST_DETAILS.Where(p => (p.QuestionID.ToString() == sqs.QuestionID.ToString()) && (p.TestID.ToString() == made)).FirstOrDefault();
            sttcauhoi = txtdt.NumberIndex + 1;
            hienthi = sttcauhoi.ToString();

            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
            //rtBox.Font = new Font(Constant.FONT_FAMILY_DEFAULT, 18, FontStyle.Bold);
            //rtBox.Width = 300;
            //rtBox.Height = sq.HeightToDisplay;
            rtBox.Rtf = sqs.SubQuestionContent;
            //MessageBox.Show(rtBox.Text);
            hienthi = "Câu: " + hienthi + " " + rtBox.Text;
            return hienthi;
        }

        private string STTCAUTRALOI(string chuoi, string made, string dapan)
        {
            int stt = 0;
            int stt_da = 0;
            string dap_an = null;
            List<SubQuestion> lstSubQuestion = new List<SubQuestion>();
            List<Answer> q = new List<Answer>();
            SUBQUESTION sqs = db.SUBQUESTIONS.Where(p => p.SubQuestionID.ToString() == chuoi).FirstOrDefault();
            List<TEST_DETAILS> txtdt = db.TEST_DETAILS.Where(p => (p.QuestionID == sqs.QuestionID) && (p.TestID.ToString() == made)).ToList();
            TEST_DETAILS TD = txtdt.Count == 1 ? txtdt[0] : null;
            lstSubQuestion = new JavaScriptSerializer().Deserialize<List<SubQuestion>>(TD.RandomAnswer);
            foreach (var sq in lstSubQuestion.Select((value, index) => new { data = value, index = index }))
            {
                foreach (int index in sq.data.ListAnswerID)
                {
                    stt++;
                    if (index.ToString() == dapan)
                    {
                        stt_da = stt;
                    }
                }
            }
            switch (stt_da)
            {
                case 1:
                    dap_an = "A";
                    break;
                case 2:
                    dap_an = "B";
                    break;
                case 3:
                    dap_an = "C";
                    break;
                case 4:
                    dap_an = "D";
                    break;
            }
            ANSWER aws = db.ANSWERS.Where(p => p.AnswerID.ToString() == dapan).FirstOrDefault();
            System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
            rtBox.Rtf = aws.AnswerContent;
            dapan = "Câu trả lời: " + "\n" + dap_an + "." +  rtBox.Text;
            return dapan;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        //private string get_made(string ma_de)
        //{
        //    made = ma_de;
        //    return made;
        //}
    }
}
