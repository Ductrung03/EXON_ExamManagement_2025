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
using System.Data.Sql;
using TXTextControl.Windows.Forms;
using System.Web.Script.Serialization;

namespace EXON.MONITOR.Report
{
    public partial class FrmRpLogChangeAws : Form
    {
        private DIVISION_SHIFTS dv = new DIVISION_SHIFTS();
        private CONTESTANTS_SHIFTS cs = new CONTESTANTS_SHIFTS();
        private CONTESTANTS_TESTS ct = new CONTESTANTS_TESTS();
        private MTAQuizDbContext db;
        DataTable dt = new DataTable();
        string SBD;
        string made;
        TXTextControl.TextControl rtbTitleOfQuestion = new TXTextControl.TextControl();

        public FrmRpLogChangeAws()
        {
            InitializeComponent();
            MTAQuizDbContext db = new MTAQuizDbContext();
        }

        public FrmRpLogChangeAws(DIVISION_SHIFTS tg)
        {
            InitializeComponent();
            dv = tg;
            MTAQuizDbContext db = new MTAQuizDbContext();

            //load du lieu ra comboboxkithi
            List<CONTEST> kithi = db.CONTESTS.Where(p => p.ContestName != null).ToList();
            comboboxkithi.DataSource = kithi;
            comboboxkithi.DisplayMember = "ContestName";
            comboboxkithi.ValueMember = "ContestID";
            //su kien chon du lieu
            comboboxkithi.SelectedIndex = -1;
            this.comboboxkithi.SelectedIndexChanged += comboboxkithi_SelectedIndexChanged;
            comboboxcathi.SelectedIndex = -1;
            this.comboboxcathi.SelectedIndexChanged += comboboxcathi_SelectedIndexChanged;
            comboboxthisinh.SelectedIndex = -1;
            this.comboboxthisinh.SelectedIndexChanged += comboboxthisinh_SelectedIndexChanged;
            //them cac truong du lieu datagridview
            dt.Columns.Add("STT");
            dt.Columns.Add("MADE");
            dt.Columns.Add("STT CAU");
            dt.Columns.Add("DAPANCU");
            dt.Columns.Add("DAPANMOI");
            dt.Columns.Add("THOIGIAN");
        }

        public FrmRpLogChangeAws(CONTESTANTS_SHIFTS CS, CONTESTANTS_TESTS CT)
        {
            InitializeComponent();
            cs = CS;
            ct = CT;
            MTAQuizDbContext db = new MTAQuizDbContext();

            dt.Columns.Add("STT");
            dt.Columns.Add("MADE");
            dt.Columns.Add("STT CAU");
            dt.Columns.Add("DAPANCU");
            dt.Columns.Add("DAPANMOI");
            dt.Columns.Add("THOIGIAN");

            //
            comboboxcathi.Visible = false;
            comboboxkithi.Visible = false;
            comboboxthisinh.Visible = false;
            TIMKIEMLOG.Enabled = false;
            label1.Visible = false;
            label2.Visible = false;
            label4.Visible = false;
            //

            SBD = cs.ContestantID.ToString();

            dt.Clear();
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
                MADE = get_made(lt.MADE),
                STTCAU = STTCAUHOI(lt.STTCAU, lt.MADE),
                DAPANCU = STTCAUTRALOI(lt.STTCAU, lt.MADE, lt.DAPANCU),
                DAPANMOI = STTCAUTRALOI(lt.STTCAU, lt.MADE, lt.DAPANMOI),
                THOIGIAN = UnixTimeStampToDateTime(double.Parse(lt.THOIGIAN, System.Globalization.CultureInfo.InvariantCulture))
            }
            ).ToList();
            //

            HienThiLogChangeAws.DataSource = logchangaws2;
            this.HienThiLogChangeAws.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        //Hộp lọc đối tượng để xuất ra reportviewer//
        private void comboboxkithi_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboboxthisinh.DataSource = null;
            comboboxcathi.DataSource = null;
            db = new MTAQuizDbContext();
            if (comboboxkithi.SelectedIndex >= 0)
            {
                List<SHIFT> cathi = db.SHIFTS.Where(p => p.ContestID.ToString() == comboboxkithi.SelectedValue.ToString()).ToList();
                comboboxcathi.DataSource = cathi;
                comboboxcathi.DisplayMember = "ShiftName";
                comboboxcathi.ValueMember = "ShiftID";
                comboboxcathi.SelectedIndex = -1;
            }
        }

        private void comboboxcathi_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboboxthisinh.DataSource = null;
            db = new MTAQuizDbContext();
            if (comboboxcathi.SelectedIndex > 0)
            {
                DIVISION_SHIFTS dvs = db.DIVISION_SHIFTS.Where(p => p.ShiftID.ToString() == comboboxcathi.SelectedValue.ToString()).FirstOrDefault();
                var thisinh = (
                               from lt in db.CONTESTANTS_SHIFTS.Where(pz => pz.DivisionShiftID == dvs.DivisionShiftID && pz.Status != 4001).ToList()
                               from ts in db.CONTESTANTS.Where(pz => pz.Status > 0 && lt.ContestantID == pz.ContestantID).ToList()
                               select new
                               {
                                   TEN = ts.FullName.ToString(),
                                   SBD = ts.ContestantID.ToString()
                               }
                              ).ToList();
                comboboxthisinh.DataSource = thisinh;
                comboboxthisinh.DisplayMember = "TEN";
                comboboxthisinh.ValueMember = "SBD";
                comboboxthisinh.SelectedIndex = -1;
            }
        }

        private void comboboxthisinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboboxthisinh.SelectedIndex >= 0)
            {
                SBD = comboboxthisinh.SelectedValue.ToString();
            }
        }

        //button in ra các thay đổi câu trả lời
        private void TIMKIEMLOG_Click(object sender, EventArgs e)
        {
            dt.Clear();
            if (comboboxcathi.Text != null || comboboxkithi.Text != null || comboboxthisinh.Text != null)
            {
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
                    MADE = get_made(lt.MADE),
                    STTCAU = STTCAUHOI(lt.STTCAU, lt.MADE),
                    DAPANCU = STTCAUTRALOI(lt.STTCAU, lt.MADE, lt.DAPANCU),
                    DAPANMOI = STTCAUTRALOI(lt.STTCAU, lt.MADE, lt.DAPANMOI),
                    THOIGIAN = UnixTimeStampToDateTime(double.Parse(lt.THOIGIAN, System.Globalization.CultureInfo.InvariantCulture))
                }
                ).ToList();

                //
                HienThiLogChangeAws.DataSource = logchangaws2;
                this.HienThiLogChangeAws.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                MessageBox.Show("Không thể tìm kiếm dữ liệu !");
            }
        }

        //click cell tren datagridview
        public void HienThiLogChangeAws_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string namecolumn = HienThiLogChangeAws.Columns[HienThiLogChangeAws.CurrentCell.ColumnIndex].Name.ToString();
            int number = Int32.Parse(HienThiLogChangeAws.CurrentCell.Value.ToString());
            if (namecolumn == "STTCAU")
            {
                TEST_DETAILS tstdt = db.TEST_DETAILS.Where(p => (p.NumberIndex == number - 1) && (p.TestID.ToString() == made)).FirstOrDefault();
                SUBQUESTION sq = db.SUBQUESTIONS.Where(p => p.QuestionID == tstdt.QuestionID).FirstOrDefault();
                System.Windows.Forms.RichTextBox rtBox = new System.Windows.Forms.RichTextBox();
                rtBox.Font = new Font(Constant.FONT_FAMILY_DEFAULT, 18, FontStyle.Bold);
                rtBox.Width = 300;
                //rtBox.Height = sq.HeightToDisplay;
                rtBox.Rtf = sq.SubQuestionContent;
                MessageBox.Show(rtBox.Text);
            }
            else if (namecolumn == "DAPANCU")
            {

            }
            else if (namecolumn == "DAPANMOI")
            {

            }
        }

        //button in ra report viewer
        private void INBIENBANLOG_Click(object sender, EventArgs e)
        {
            if (comboboxthisinh.Text != null || comboboxkithi.Text != null || comboboxcathi.Text != null)
            {
                try
                {
                    Report.frmShowRPLogChangeAws showrplogchangeaws = new frmShowRPLogChangeAws(dv, SBD);
                    showrplogchangeaws.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi" + ex.Message.ToString());
                    Log.Instance.WriteErrorLog(Properties.Resources.MSG_LOG_ERROR, string.Format("Expetion : {0}  ", ex.Message));
                }
            }
            else
            {
                MessageBox.Show("Không thể xuất ra biên bản !");
            }
        }

        private string get_made(string ma_de)
        {
            made = ma_de;
            return made;
        }

        private string STTCAUHOI(string chuoi, string made)
        {
            db = new MTAQuizDbContext();
            int sttcauhoi = 0;
            SUBQUESTION sqs = db.SUBQUESTIONS.Where(p => p.SubQuestionID.ToString() == chuoi).FirstOrDefault();
            TEST_DETAILS txtdt = db.TEST_DETAILS.Where(p => (p.QuestionID.ToString() == sqs.QuestionID.ToString()) && (p.TestID.ToString() == made)).FirstOrDefault();
            sttcauhoi = txtdt.NumberIndex + 1;
            return sttcauhoi.ToString();
        }

        private string STTCAUTRALOI(string chuoi, string made, string dapan)
        {
            db = new MTAQuizDbContext();
            int stt = 0;
            int stt_da = 0;
            string dap_an;
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
                    dapan = "A";
                    break;
                case 2:
                    dapan = "B";
                    break;
                case 3:
                    dapan = "C";
                    break;
                case 4:
                    dapan = "D";
                    break;
            }
            return dapan;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
