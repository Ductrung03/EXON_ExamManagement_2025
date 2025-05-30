using EXON.SubData;
using EXON.SubModel.Models;
using EXONSYSTEM;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Drawing.Html;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace EXON.MONITOR.GUI
{
    
    public partial class frmXemBaiLam : Form
    {
        private FlowLayoutPanel flpnListOfQuestions2 = new FlowLayoutPanel();
        ErrorController EC = new ErrorController();
        public delegate void SendWorking(bool isProgress);
        public delegate void SendQuestion(Questions q, int AnswerSheetID);
        SendWorking s;
        private static SqlConnection Sql;
        public frmXemBaiLam(int _cshID, string Contestant_code)
        {
            SqlConnection sql = new SqlConnection();
            string connectString = EXON.MONITOR.Common.AppConfig.GetConnectString("MTA_QUIZ_1");
            sql.ConnectionString = connectString;
            sql.Open();
            Sql = sql;
            CONTESTANT CS = null;
            ErrorController rEC = new ErrorController();
            SubModel.Models.MTAQuizDbContext DB = new SubModel.Models.MTAQuizDbContext();
            CS = DB.CONTESTANTS.Where(s => s.ContestantCode == Contestant_code).FirstOrDefault();
            ContestantInformation CI = new ContestantInformation();
            CONTESTANTS_SHIFTS CSH = new CONTESTANTS_SHIFTS();
            CSH = DB.CONTESTANTS_SHIFTS.Where(s => s.ContestantShiftID == _cshID).FirstOrDefault();
            CI.Fullname = CS.FullName;
            CI.ContestantID = CS.ContestantID;
            CI.ContestantCode = CS.ContestantCode;
            CI.DOB = CS.DOB.Value;
            CI.SEX = CS.Sex.Value;
            CI.ContestantShiftID = CSH.ContestantShiftID;
            CI.Ethnic = CS.Ethnic;
            CI.HighSchool = CS.HighSchool;
            CI.IdentityCardName = CS.IdentityCardNumber;
            CI.CurrentAddress = CS.CurrentAddress;
            CI.TrainingSystem = CS.TrainingSystem;
            CI.StudentCode = CS.StudentCode;
            CI.SubjectName = CSH.SCHEDULE.SUBJECT.SubjectName;
            CI.TimeOfTest = CSH.SCHEDULE.TimeOfTest;
            /// 5pkiem tra bai
            CI.TimeToSubmit = CSH.SCHEDULE.TimeToSubmit;
            
            CI.DivisionShiftID = CSH.DivisionShiftID;
            CI.ScheduleID = CSH.ScheduleID;
            CI.Unit = CS.Unit;
            CI.Status = CSH.Status;
            CONTESTANTS_TESTS CT = DB.CONTESTANTS_TESTS.SingleOrDefault(y => y.ContestantShiftID == CSH.ContestantShiftID);
            if (CT != null)
            {
                CI.ContestantTestID = CT.ContestantTestID;
                CI.TestID = CT.TestID;
            }
            CILogged = CI;
            bool IsContinute;
            int NumberQuestionsOfTest = 0;
            GetListQuestionByContestantInformation(CI, out lstLQuestion, out lstPartOfTest, out IsContinute, out NumberQuestionsOfTest,  rEC, sql);
            InitializeComponent();
            
        }
        public static class Common
        {
            public static int STATUS_OK = 1;
            public static int STATUS_ERROR = -1;

            //TRANG THAI THI SINH DA HET CA THI
            public static int STATUS_COMPLETE = 2;

            // Trạng thái ca thi divisionShift chia de

            public static int STATUS_DIVISION_GENERATE = 2;
            // Trạng thái ca thi divisionShift phát đề 

            public static int STATUS_DIVISION_TEST = 6;
            // Trạng thái ca thi bắt đầu làm bài
            public static int STATUS_START_TEST = 7;
            // Trạng thái ca thi hoàn thành
            public static int STATUS_DIVISION_COMPLETE = 8;
            // Trạng thái ca thi bị tạm ngừng
            public static int STATUS_DIVISION_PAUSE = 9;
            // Trạng thái thí sinh đã đăng nhập ( là thí sinh mới)
            public static int STATUS_LOGGED = 3000;

            // Trạng thái thí sinh đã đăng nhập (trước đó đã thi nhưng bị gián đoạn)
            public static int STATUS_LOGGED_DO_NOT_FINISH = 3001;

            // Trạng thái thí sinh sẵn sàng để thi ( đã load xong câu hỏi)
            public static int STATUS_READY = 3002;

            // Trạng thái thí sinh đang làm bài thi
            public static int STATUS_DOING = 3003;

            // Trạng thái đang làm thì bị gián đoạn (bị crash chương trình)
            public static int STATUS_DOING_BUT_INTERRUPT = 3004;

            // Trạng thái đã thi xong
            public static int STATUS_FINISHED = 3005;

            // Trạng thái thí sinh bị cảnh cáo
            public static int STATUS_REPORT_ERROR = 3006;

            // Trạng thái thí sinh đăng nhập sai số báo danh
            public static int STATUS_LOGIN_FAIL = 3007;

            // Trạng thái thông báo bị cảnh cáo
            public static int STATUS_WARNING = 3008;

            // Trạng thái thông báo bắt đầu làm bài thi
            public static int STATUS_STARTED = 3009;
            // trạng thái tạm dừng
            public static int STATUS_PAUSE = 5000;
            // trạng thái  cấm thi
            public static int STATUS_BAN = 5001;
            // trạng thái  cấm thi
            public static int STATUS_SIGNED = 5002;
            // Trạng thái lỗi từ SQL
            public static int STATUS_UNKOWN_EXCEPTION = 1001;
            public static string STR_STATUS_UNKOWN_EXCEPTION = "Unknown exception. [{0}]";

            // Trạng thái khởi tạo
            public static int STATUS_INITIALIZE = 4001;

            // Trạng thái đã bị thay đổi
            public static int STATUS_CHANGED = 4002;

            // Đăng nhập sai máy thi
            public static int STATUS_WRONG_COMPUTTER = 3012;

            public static int STATUS_DUPLICATE_NAMECOMPUTER = 3013;
            public static int STATUS_EXIST_CONTESTANT = 3015;

            // Đăng nhập hết thời gian
            public static int STATUS_lOGIN_OUTTIME = 3014;

            public static int LOGIN_WITH_CONTESTANT_CODE = 5000;
            public static int LOGIN_WITH_STUDENT_CODE = 5001;
            public static int LOGIN_WITH_IDENTITY_CARD_NAME = 5002;

            // Các Level trong [VIOLATIONS]
            public static int LEVEL_LOGIN = 8001; // Đăng nhập
            public static int LEVEL_INTERRUPT = 8002; // bù giờ, gián đoạn
            public static int LEVEL_CHANGECMP = 8003; // đổi máy khi thi
            public static int LEVEL_CHANGEAWS = 8004; // đổi câu trả lời

            public static string GetStringStatus(int status)
            {
                switch (status)
                {
                    // STATUS_LOGGED_DO_NOT_FINISH 
                    case 3001:
                        return "STATUS_LOGGED_DO_NOT_FINISH";
                    // STATUS_READY = 3002;
                    case 3002:
                        return "STATUS_READY";
                    // STATUS_DOING
                    case 3003:
                        return "STATUS_DOING";
                    // STATUS_DOING_BUT_INTERRUPT
                    case 3004:
                        return "STATUS_DOING_BUT_INTERRUPT";
                    // STATUS_FINISHED
                    case 3005:
                        return "STATUS_FINISHED";
                    // STATUS_REPORT_ERROR
                    case 3006:
                        return "STATUS_REPORT_ERROR";
                    // STATUS_LOGIN_FAIL
                    case 3007:
                        return "STATUS_LOGIN_FAIL";
                    // STATUS_WARNING
                    case 3008:
                        return "STATUS_WARNING";
                    // STATUS_STARTED
                    case 3009:
                        return "STATUS_STARTED";
                    // STATUS_READY_TO_GET_TEST
                    case 3010:
                        return "STATUS_READY_TO_GET_TEST";
                    // STATUS_GET_TEST
                    case 3011:
                        return "STATUS_GET_TEST";
                    // STATUS_WRONG_COMPUTTER
                    case 3012:
                        return "STATUS_WRONG_COMPUTTER";
                    // UNKOWN_EXCEPTION
                    default:
                        return "UNKOWN_EXCEPTION";
                }
            }

            public static int ConvertDateTimeToUnix(DateTime dt)
            {
                return Convert.ToInt32((dt.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds);
            }
            public static DateTime ConvertUnixToDateTime(int unix)
            {
                DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return dt.AddSeconds(unix);
            }
            
        }
        public class ContestantInformation
        {
            public string ContestantCode { get; set; }
            public int ContestantID { get; set; }
            public int ContestantTestID { get; set; }
            public int ContestantShiftID { get; set; }
            public int TimeOfTest { get; set; }
            public int TestID { get; set; }
            public int DivisionShiftID { get; set; }
            public string Fullname { get; set; }
            public int DOB { get; set; }
            public int SEX { get; set; }
            public string Ethnic { get; set; }
            public int ScheduleID { get; set; }
            public string HighSchool { get; set; }
            public string IdentityCardName { get; set; }
            public string Unit { get; set; }
            public string CurrentAddress { get; set; }
            public bool IsNewStarted { get; set; }
            public int TimeStarted { get; set; }
            public int TimeRemained { get; set; }
            public int ThoiGianBu { get; set; }
            public bool IsDisconnected { get; set; }
            public int Status { get; set; }
            public int AnswerSheetID { get; set; }
            public string TrainingSystem { get; set; }
            public string StudentCode { get; set; }
            public int Warning { get; set; }
            public string SubjectName { get; set; }
            public int TimeToSubmit { get; set; }
            public int RoomDiagramID { get; set; }
            public bool ReadOnly { get; set; }
            //public List<int> {}
            public ContestantInformation()
            {
                this.IsDisconnected = false;
                this.IsNewStarted = false;
            }
        }
        public class Answer
        {
            public int AnswerID { get; set; }
            public string AnswerContent { get; set; }
            public int HighToDisplay { get; set; }
            public bool IsCorrect { get; set; }
            public int SubQuestionID { get; set; }
            public Nullable<double> Score { get; set; }

            public Answer() { }
            public Answer(int answerID, string answerContent, int highToDisplay, bool isCorrect, int subQuestionID, double score)
            {
                AnswerID = answerID;
                AnswerContent = answerContent;
                HighToDisplay = highToDisplay;
                IsCorrect = isCorrect;
                SubQuestionID = subQuestionID;
                Score = score;
            }
        }
        
        private List<Answer> GetListAnswerByListAnswerID(List<int> lstAnswerID, SqlConnection sql)
        {
            
            List<Answer> lstAnswer = new List<Answer>();
            using (SubData.MTAQuizDbContext DB = new SubData.MTAQuizDbContext())
            {
                foreach (int index in lstAnswerID)
                {
                    List<SqlParameter> para = new List<SqlParameter>();
                    para.Add(new SqlParameter("@AnswerID", index));
                    //List<ANSWER> Atemp = ExcuteObject<ANSWER>("SELECT * FROM ANSWERS WHERE AnswerID = @AnswerID", para, sql).ToList();
                    //ANSWER A = Atemp.Count == 1 ? Atemp[0] : null;
                    ANSWER A = DB.ANSWERS.Where(x => x.AnswerID == index).SingleOrDefault();

                    if (A != null)
                    {
                        int height = 0;
                        if (A.HeightToDisplay.HasValue)
                            height = A.HeightToDisplay.Value;
                        double Score = 0;
                        para = new List<SqlParameter>();
                        para.Add(new SqlParameter("@SubQuestionID", A.SubQuestionID));
                        //SUBQUESTION sub_ques = ExcuteObject<SUBQUESTION>("SELECT * FROM SUBQUESTIONS WHERE SubQuestionID = @SubQuestionID", para, sql).ToList()[0];
                        SUBQUESTION sub_ques = DB.SUBQUESTIONS.Where(s=>s.SubQuestionID==A.SubQuestionID).FirstOrDefault();
                        //if (A.SUBQUESTION.Score.HasValue)
                        if (sub_ques.Score.HasValue)
                            Score = sub_ques.Score.Value;
                        lstAnswer.Add(new Answer(A.AnswerID, A.AnswerContent, height, A.IsCorrect, A.SubQuestionID, Score));
                    }
                }
            }
            return lstAnswer;
        }
        public class Questions
        {
            public int NO { get; set; }
            public int QuestionID { get; set; }
            public int SubQuestionID { get; set; }
            public string TitleOfQuestion { get; set; }

            public List<Answer> ListAnswer { get; set; }
            public int Type { get; set; }

            public int NumberQuestion { get; set; }
            public byte[] Audio { get; set; }

            //public string AnswerA { get; set; }
            //public int AnswerAID { get; set; }
            //public string AnswerB { get; set; }
            //public int AnswerBID { get; set; }
            //public string AnswerC { get; set; }
            //public int AnswerCID { get; set; }
            //public string AnswerD { get; set; }
            //public int AnswerDID { get; set; }
            public int FormatQuestion { get; set; }
            public int AnswerChecked { get; set; }
            public int TestID { get; set; }
            public int TestDetailID { get; set; }
            public bool IsSingleChoice { get; set; }
            public string AnswerSheetContent { get; set; }
            public bool IsQuestionContent { get; set; }
            public int HighToDisplayForQuestion { get; set; }
            public int HighToDisplayForSubQuestion { get; set; }

            public float Score { get; set; }
            public Questions()
            {
                this.ListAnswer = new List<Answer>();
            }
            // caau hoi  lớn
            public Questions(string title, int formatQuestion, int HighToDisplay, int testDetailID)
            {

                this.ListAnswer = new List<Answer>();
                this.TitleOfQuestion = title;
                this.FormatQuestion = formatQuestion;
                this.AnswerChecked = -1;
                this.HighToDisplayForQuestion = HighToDisplay;
                this.TestDetailID = testDetailID;
            }
            public Questions(byte[] _Audio, int questionID, int testDetailID, int formatQuestion, int type)
            {
                this.Type = type;
                this.TestDetailID = testDetailID;
                this.QuestionID = questionID;
                this.ListAnswer = new List<Answer>();
                this.Audio = _Audio;
                this.FormatQuestion = formatQuestion;
                this.AnswerChecked = -1;
            }

        }
        public class PartOfTest
        {

            public string PartContent { get; set; }
            public int Index { get; set; }
            public PartOfTest() { }
            public PartOfTest(string _PartContent, int _index)
            {
                PartContent = _PartContent;
                Index = _index;

            }
        }
        public class ErrorController
        {
            public int ErrorCode { get; set; }
            public string Message { get; set; }
            public ErrorController()
            {
                this.ErrorCode = 0;
                this.Message = string.Empty;
            }
            public ErrorController(int errorCode, string message)
            {
                this.ErrorCode = errorCode;
                this.Message = message;
            }
        }
        public static DataTable Select(string query_string, List<SqlParameter> parameters, SqlConnection sql)
        {
            DataTable dataTable = new DataTable();
            using (SqlCommand command = new SqlCommand(query_string, sql))
            {
                command.Parameters.AddRange(parameters.ToArray());
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }
        public static IEnumerable<T> ExcuteObject<T>(string query_string, List<SqlParameter> parameters, SqlConnection sql)
        {
            List<T> items = new List<T>();
            var dataTable = Select(query_string, parameters, sql); //this will use the DataTable Select function
            foreach (var row in dataTable.Rows)
            {
                T item = (T)Activator.CreateInstance(typeof(T), row);
                items.Add(item);
            }
            return items;
        }
        public class AnswersheetDetail
        {
            public int AnswerSheetDetailID { get; set; }
            public int ChoosenAnswer { get; set; }
            public Nullable<int> LastTime { get; set; }
            public int Status { get; set; }

            public string AnswerContent { get; set; }
            public int AnswerSheetID { get; set; }
            public int SubQuestionID { get; set; }
            public Nullable<double> Score { get; set; }
            public AnswersheetDetail() { }
        }
        public void GetHastableAnswersheetDetailByAnswerSheetID(ContestantInformation CI, out Hashtable hstbAnswersheetDetailOut, out ErrorController EC, SqlConnection sql)
        {
            using (SubData.MTAQuizDbContext DB = new SubData.MTAQuizDbContext())
            {
                try
                {
                    List<SqlParameter> para = new List<SqlParameter>();
                    para.Add(new SqlParameter("@ContestantTestID", CI.ContestantTestID));
                    //List<ANSWERSHEET> ANStemp = ExcuteObject<ANSWERSHEET>("SELECT * FROM ANSWERSHEETS WHERE ContestantTestID = @ContestantTestID", para, sql).ToList();

                    ANSWERSHEET ANS = DB.ANSWERSHEETS.SingleOrDefault(x => x.ContestantTestID == CI.ContestantTestID);
                    //ANSWERSHEET ANS = ANStemp.Count == 1 ? ANStemp[0] : null;
                    if (ANS != null)
                    {
                        Hashtable hstbAnswersheetDetail = new Hashtable();
                        List<AnswersheetDetail> lstAnswersheetDetail = new List<AnswersheetDetail>();
                        CI.AnswerSheetID = ANS.AnswerSheetID;
                        para = new List<SqlParameter>();
                        para.Add(new SqlParameter("@AnswerSheetID", CI.AnswerSheetID));
                        //List<ANSWERSHEET_DETAILS> lstDBAnswersheetDetail = ExcuteObject<ANSWERSHEET_DETAILS>("SELECT * FROM ANSWERSHEET_DETAILS WHERE AnswerSheetID = @AnswerSheetID", para, sql).ToList();
                        List<ANSWERSHEET_DETAILS> lstDBAnswersheetDetail = DB.ANSWERSHEET_DETAILS.Where(x => x.AnswerSheetID == CI.AnswerSheetID).ToList();
                        foreach (ANSWERSHEET_DETAILS AD in lstDBAnswersheetDetail)
                        {
                            if (AD.AnswerContent == null)
                            {
                                hstbAnswersheetDetail.Add(AD.SubQuestionID, AD.ChoosenAnswer);
                            }
                            else
                            {
                                hstbAnswersheetDetail.Add(AD.SubQuestionID, AD.AnswerContent);
                            }
                        }
                        hstbAnswersheetDetailOut = hstbAnswersheetDetail;
                        EC = new ErrorController(1, "Nhận danh sách câu trả lời thành công");
                    }
                    else
                    {
                        EC = new ErrorController(1, "Không thể nhận ANSWERSHEET");
                        hstbAnswersheetDetailOut = null;
                    }
                }
                catch (Exception ex)
                {
                    hstbAnswersheetDetailOut = null;
                    EC = new ErrorController(1, string.Format(Common.STR_STATUS_UNKOWN_EXCEPTION, ex.Message));
                }
            }
        }
        private List<SubQuestion> GetListSubQuestionByQuestionID(int questionID, int testID, SqlConnection sql)
        {
            List<SubQuestion> lstSubQuestiton = new List<SubQuestion>();
            using (SubData.MTAQuizDbContext DB = new SubData.MTAQuizDbContext())
            {
                List<SqlParameter> para = new List<SqlParameter>();
                para.Add(new SqlParameter("@QuestionID", questionID));
                para.Add(new SqlParameter("@TestID", testID));
                //List<TEST_DETAILS> TDtemp = ExcuteObject<TEST_DETAILS>("SELECT * FROM TEST_DETAILS WHERE QuestionID = @QuestionID and TestID = @TestID", para, sql).ToList();
                //TEST_DETAILS TD = TDtemp.Count == 1 ? TDtemp[0] : null;
                TEST_DETAILS TD = DB.TEST_DETAILS.SingleOrDefault(x => x.QuestionID == questionID && x.TestID == testID);

                lstSubQuestiton = new JavaScriptSerializer().Deserialize<List<SubQuestion>>(TD.RandomAnswer);
            }
            return lstSubQuestiton;
        }
        public enum QuizTypeEnum { Regular, Audio, Fill, FillAudio, Essay, Speaking, Match, ReWritting, ListenMatching }
        private List<List<Questions>> lstLQuestion;
        public void GetListQuestionByContestantInformation(ContestantInformation CI, out List<List<Questions>> rLLstQuest, out List<PartOfTest> rlstPartOfTest, out bool IsContinute, out int numberQuestionsOfTest, ErrorController EC, SqlConnection sql)
        {
            IsContinute = false;
            numberQuestionsOfTest = 0;
            using (SubModel.Models.MTAQuizDbContext DB = new SubModel.Models.MTAQuizDbContext())
            {
                try
                {
                    int count = 0;
                    List<SqlParameter> para = new List<SqlParameter>();
                    para.Add(new SqlParameter("@TestID", CI.TestID));
                    List<TEST_DETAILS> lstTD = DB.TEST_DETAILS.Where(s => s.TestID == CI.TestID).ToList();
                    //List<TEST_DETAILS> lstTD = DB.TEST_DETAILS.Where(x => x.TestID == CI.TestID).ToList();

                    List<PartOfTest> lstPOFT = new List<PartOfTest>();

                    para = new List<SqlParameter>();
                    para.Add(new SqlParameter("@ScheduleID", CI.ScheduleID));
                    List<PART> lstpt = ExcuteObject<PART>("SELECT * FROM PARTS WHERE ScheduleID = @ScheduleID", para, sql).ToList();
                    //List<PART> lstpt = new List<PART>();
                    //lstpt = DB.PARTS.Where(x => x.ScheduleID == CI.ScheduleID).ToList();
                    //List<PART> lstpt = DB.PARTS.Where(x => x.ScheduleID == CI.ScheduleID).ToList();
                    if (lstpt.Count > 0)
                    {
                        foreach (PART pt in lstpt)
                        {
                            lstPOFT.Add(new PartOfTest(pt.Name, pt.OrderOfQuestion.Value));
                        }
                        rlstPartOfTest = lstPOFT;
                    }
                    else
                    {
                        rlstPartOfTest = null;
                    }

                    lstTD.OrderBy(x => x.NumberIndex);
                    //Debug.WriteLine("lstTD.Count {0}", lstTD.Count);
                    List<Questions> lstQuestions;
                    List<List<Questions>> lstLQuestion = new List<List<Questions>>();
                    Hashtable hstbAnswersheetDetail = null;

                    {
                        hstbAnswersheetDetail = new Hashtable();
                        GetHastableAnswersheetDetailByAnswerSheetID(CI, out hstbAnswersheetDetail, out EC, sql);
                        IsContinute = true;
                    }

                    if (lstTD.Count > 0)
                    {
                        List<SubQuestion> lstSubQuestiton = new List<SubQuestion>();
                        foreach (TEST_DETAILS td in lstTD)
                        {
                            lstQuestions = new List<Questions>();
                            lstSubQuestiton = GetListSubQuestionByQuestionID(td.QuestionID, td.TestID, sql);

                            para = new List<SqlParameter>();
                            para.Add(new SqlParameter("@QuestionID", td.QuestionID));
                            //QUESTION ques = ExcuteObject<QUESTION>("SELECT * FROM QUESTIONS WHERE QuestionID = @QuestionID", para, sql).ToList()[0];
                            QUESTION ques = DB.QUESTIONS.Where(s=>s.QuestionID==td.QuestionID).FirstOrDefault();
                            
                            //int TypeQ = td.QUESTION.Type ?? default(int);
                            int TypeQ = ques.Type ?? default(int);
                            if (TypeQ != 5)
                            {
                                int firstIndex = 0;
                                foreach (var sq in lstSubQuestiton.Select((value, index) => new { data = value, index = index }))
                                {
                                    para = new List<SqlParameter>();
                                    para.Add(new SqlParameter("@SubQuestionID", sq.data.SubQuestionID));
                                    //SUBQUESTION SQ = ExcuteObject<SUBQUESTION>("SELECT * FROM SUBQUESTIONS WHERE SubQuestionID = @SubQuestionID", para, sql).ToList()[0];
                                    SUBQUESTION SQ = DB.SUBQUESTIONS.SingleOrDefault(x => x.SubQuestionID == sq.data.SubQuestionID);

                                    para = new List<SqlParameter>();
                                    para.Add(new SqlParameter("@QuestionID", SQ.QuestionID));
                                    //ques = ExcuteObject<QUESTION>("SELECT * FROM QUESTIONS WHERE QuestionID = @QuestionID", para, sql).ToList()[0];
                                    ques = DB.QUESTIONS.Where(s => s.QuestionID == td.QuestionID).FirstOrDefault();

                                    Questions q = new Questions();
                                    q.NO = td.NumberIndex + sq.index + 1;
                                    // Todo

                                    q.FormatQuestion = td.Status;
                                    q.TestDetailID = td.TestDetailID;
                                    q.QuestionID = SQ.QuestionID;
                                    q.SubQuestionID = SQ.SubQuestionID;
                                    q.TestID = td.TestID;
                                    q.TitleOfQuestion = SQ.SubQuestionContent;
                                    q.AnswerChecked = 2000;
                                    //q.IsSingleChoice = SQ.QUESTION.IsSingleChoice;
                                    //q.IsQuestionContent = SQ.QUESTION.IsQuestionContent;
                                    q.IsSingleChoice = ques.IsSingleChoice;
                                    q.IsQuestionContent = ques.IsQuestionContent;
                                    q.Score = SQ.Score != null ? (float)SQ.Score : default(float);
                                    q.ListAnswer = GetListAnswerByListAnswerID(sq.data.ListAnswerID, sql);
                                    int HeightToDisplayForSub = 0;
                                    if (SQ.HeightToDisplay.HasValue)
                                    {
                                        HeightToDisplayForSub = SQ.HeightToDisplay.Value;
                                    }
                                    q.HighToDisplayForSubQuestion = HeightToDisplayForSub;
                                    if (hstbAnswersheetDetail != null )
                                    {
                                        //if (hstbAnswersheetDetail.ContainsKey(q.SubQuestionID) && hstbAnswersheetDetail[q.SubQuestionID].GetType() == typeof(int) && SQ.QUESTION.Type != (int)EXON.Common.QuizTypeEnum.Match)
                                        if (hstbAnswersheetDetail.ContainsKey(q.SubQuestionID) && hstbAnswersheetDetail[q.SubQuestionID].GetType() == typeof(int) && ques.Type != (int)QuizTypeEnum.Match)
                                        {
                                            q.AnswerChecked = 2000 + sq.data.ListAnswerID.IndexOf(Convert.ToInt32(hstbAnswersheetDetail[q.SubQuestionID])) + 1;
                                        }
                                        // câu hỏi nối
                                        //else if (hstbAnswersheetDetail.ContainsKey(q.SubQuestionID) && hstbAnswersheetDetail[q.SubQuestionID].GetType() == typeof(int) && SQ.QUESTION.Type == (int)EXON.Common.QuizTypeEnum.Match)
                                        else if (hstbAnswersheetDetail.ContainsKey(q.SubQuestionID) && hstbAnswersheetDetail[q.SubQuestionID].GetType() == typeof(int) && ques.Type == (int)QuizTypeEnum.Match)
                                        {
                                            ANSWER ansMatch = new ANSWER();

                                            int AnswerID = Convert.ToInt32(hstbAnswersheetDetail[q.SubQuestionID]);
                                            para = new List<SqlParameter>();
                                            para.Add(new SqlParameter("@AnswerID", AnswerID));
                                            List<ANSWER> ansMatchtemp = ExcuteObject<ANSWER>("SELECT * FROM ANSWERS WHERE AnswerID = @AnswerID", para, sql).ToList();
                                            ansMatch = ansMatchtemp.Count == 1 ? ansMatchtemp[0] : null;
                                            //ansMatch = DB.ANSWERS.Where(x => x.AnswerID == AnswerID).SingleOrDefault();
                                            q.AnswerSheetContent = ansMatch.AnswerContent;

                                        }
                                        else if (hstbAnswersheetDetail.ContainsKey(q.SubQuestionID) && hstbAnswersheetDetail[q.SubQuestionID].GetType() == typeof(string))
                                        {
                                            q.AnswerSheetContent = hstbAnswersheetDetail[q.SubQuestionID].ToString();
                                        }

                                    }

                                    //q.Type = SQ.QUESTION.Type ?? default(int);
                                    q.Type = ques.Type ?? default(int);

                                    //q.NumberQuestion = SQ.QUESTION.NumberSubQuestion;
                                    q.NumberQuestion = ques.NumberSubQuestion;
                                    count++;
                                    lstQuestions.Add(q);
                                    // chieeuf cao cau hoi
                                    int HeightToDisplayForQ = 0;
                                    //if (SQ.QUESTION.HeightToDisplay.HasValue)
                                    if (ques.HeightToDisplay.HasValue)
                                    {
                                        //HeightToDisplayForQ = SQ.QUESTION.HeightToDisplay.Value;
                                        HeightToDisplayForQ = ques.HeightToDisplay.Value;
                                    }
                                    //if (SQ.QUESTION.Audio != null && firstIndex == 0)
                                    if (ques.Audio != null && firstIndex == 0)
                                    {

                                        //lstQuestions.Insert(0, new Questions(SQ.QUESTION.Audio, SQ.QuestionID, td.TestDetailID, td.Status, q.Type));
                                        //lstQuestions.Insert(1, new Questions(SQ.QUESTION.QuestionContent, td.Status, HeightToDisplayForQ, td.TestDetailID));
                                        lstQuestions.Insert(0, new Questions(ques.Audio, SQ.QuestionID, td.TestDetailID, td.Status, q.Type));
                                        lstQuestions.Insert(1, new Questions(ques.QuestionContent, td.Status, HeightToDisplayForQ, td.TestDetailID));
                                        firstIndex = 1;
                                    }
                                    //else if (lstQuestions.Count == SQ.QUESTION.NumberSubQuestion && SQ.QUESTION.NumberSubQuestion > 1 && firstIndex == 0)
                                    else if (lstQuestions.Count == ques.NumberSubQuestion && ques.NumberSubQuestion > 1 && firstIndex == 0)
                                    {
                                        //lstQuestions.Insert(0, new Questions(SQ.QUESTION.QuestionContent, td.Status, HeightToDisplayForQ, td.TestDetailID));
                                        lstQuestions.Insert(0, new Questions(ques.QuestionContent, td.Status, HeightToDisplayForQ, td.TestDetailID));
                                        firstIndex = 1;
                                    }
                                    //Thread.Sleep(100);
                                }
                                lstLQuestion.Add(lstQuestions);
                            }
                        }
                        numberQuestionsOfTest = count;
                        //Debug.WriteLine("lstLQuestion: " + lstLQuestion.Count);

                        rLLstQuest = lstLQuestion;
                    }
                    else
                    {
                        rLLstQuest = null;
                    }
                }
                catch (Exception ex)
                {
                    rLLstQuest = null;
                    rlstPartOfTest = null;
                }
            }
        }
        public struct ObjControl
        {
            public List<Questions> Question;
            public int Width;
            public string Top;
            public int AnswerSheetID;

            public ObjControl(List<Questions> question, int answerSheetID, int width)
            {
                this.Question = question;
                this.Width = width;
                this.Top = string.Empty;
                this.AnswerSheetID = answerSheetID;
            }
            public ObjControl(string top, int width)
            {
                this.Question = null;
                this.Width = width;
                this.Top = top;
                this.AnswerSheetID = 0;
            }
        }
        public static class Constant
        {
            public static int WIDTH_SCREEN = Screen.PrimaryScreen.Bounds.Size.Width;
            public static int HEIGHT_SCREEN = Screen.PrimaryScreen.Bounds.Size.Height;
            public static int WIDTH_PANEL_INFORMATION = Convert.ToInt32(WIDTH_SCREEN * 0.3);


            public static string pathTempHelp = Application.StartupPath + "\\temp";

            public static int ASSIGN_OBJECT = 1000;
            public static int CONTROL_QUESTION = 1001;
            public static int LAYOUT_QUESTION = 1002;
            public static int CONTROL_BUTTON = 1003;
            public static int LAYOUT_BUTTON = 1004;
            public static int RADIO_PERFORMCLICK = 1005;

            public static int STATUS_OK = 1;
            public static int STATUS_NORMAL = -1;
            //TRANG THAI THI SINH DA HET CA THI
            public static int STATUS_COMPLETE = 2;
            public static int ANS_UNCHECK = 2000;
            public static int ANS_CHECKED_A = 2001;
            public static int ANS_CHECKED_B = 2002;
            public static int ANS_CHECKED_C = 2003;
            public static int ANS_CHECKED_D = 2004;

            // Trạng thái ca thi divisionShift phát đề 
            public static int STATUS_DIVISION_TEST = 6;
            // Trạng thái ca thi bắt đầu làm bài
            public static int STATUS_START_TEST = 7;
            // Trạng thái ca thi hoàn thành
            public static int STATUS_DIVISION_COMPLETE = 8;
            // Trạng thái thí sinh đã đăng nhập ( là thí sinh mới)
            public static int STATUS_LOGGED = 3000;

            // Trạng thái thí sinh đã đăng nhập (trước đó đã thi nhưng bị gián đoạn)
            public static int STATUS_LOGGED_DO_NOT_FINISH = 3001;
            // Trạng thái thí sinh sẵn sàng để thi ( đã load xong câu hỏi)
            public static int STATUS_READY = 3002;

            // Trạng thái thí sinh đang làm bài thi
            public static int STATUS_DOING = 3003;
            // Trạng thái đang làm thì bị gián đoạn (bị crash chương trình)
            public static int STATUS_DOING_BUT_INTERRUPT = 3004;
            // Trạng thái đã thi xong
            public static int STATUS_FINISHED = 3005;

            // Trạng thái thí sinh bị cảnh cáo
            public static int STATUS_REPORT_ERROR = 3006;

            // Trạng thái thí sinh đăng nhập sai số báo danh
            public static int STATUS_LOGIN_FAIL = 3007;

            // Trạng thái thông báo bị cảnh cáo
            public static int STATUS_WARNING = 3008;

            // Trạng thái thông báo bắt đầu làm bài thi
            public static int STATUS_STARTED = 3009;

            // Trạng thái sẵn sàng nhận đề thi và danh sách câu hỏi
            public static int STATUS_READY_TO_GET_TEST = 3010;

            // Trạng thái nhận đề thi và danh sách câu hỏi
            public static int STATUS_GET_TEST = 3011;

            // Đăng nhập sai máy thi
            public static int STATUS_WRONG_COMPUTTER = 3012;
            public static int STATUS_DUPLICATE_NAMECOMPUTER = 3013;

            public static int STATUS_EXIST_CONTESTANT = 3015;
            // trạng thái tạm dừng
            public static int STATUS_PAUSE = 5000;
            // trạng thái  cấm thi
            public static int STATUS_BAN = 5001;
            // trạng thái đã ký
            public static int STATUS_SIGNED = 5002;
            public static int STATUS_lOGIN_OUTTIME = 3014;

            // Trạng thái thí sinh bị đình chỉ thi
            public static int STATUS_REJECT = 3013;

            // Trạng thái khởi tạo
            public static int STATUS_INITIALIZE = 4001;

            // Trạng thái đã bị thay đổi
            public static int STATUS_CHANGED = 4002;

            // Trạng thái lỗi từ SQL
            public static int STATUS_UNKOWN_EXCEPTION = 1001;
            public static string STR_STATUS_UNKOWN_EXCEPTION = "Unknown exception. [{0}]";

            public static int FORMAT_QUESTION_HTML = 0;
            public static int FORMAT_QUESTION_RTF = 1;
            public static string STRING_QUESTION_HTML = "ucQuestionsHTML";
            public static string STRING_QUESTION_RTF = "ucQuestionsRTF";

            public static string STRING_QUESTION_LISTENNING = "ucListenning";

            public static string STRING_FLOWLAYOUTPANEL = "FlowLayoutPanel";

            public static string STRING_TEXTCONTROL = "TextControl";

            public static string FONT_FAMILY_DEFAULT = "Times New Roman";
            public static int FONT_SIZE_DEFAULT = 14;

            public static Color COLOR_TRANSPARENT = Color.Transparent;
            public static Color COLOR_WHITE = Color.White;
            public static Color COLOR_RED = Color.Red;
            public static Color COLOR_BLACK = Color.Black;
            public static Color BACKCOLOR_PANEL_WELCOME = Color.FromArgb(33, 150, 243);
            public static Color BACKCOLOR_PANEL_INFORMATION = Color.FromArgb(225, 245, 254);
            public static Color BACKCOLOR_PANEL_WRAPPER_CONTENT = Color.FromArgb(30, 136, 229);
            public static Color BACKCOLOR_PANEL_QUESTION = Color.FromArgb(129, 212, 250);

            public static Color BACKCOLOR_BUTTON_CONTROLLER = Color.FromArgb(41, 182, 246);
            public static Color BACKCOLOR_BUTTON_QUESTION = Color.FromArgb(255, 234, 0);

            public static Color FORCECOLOR_BUTTON_CONFIRM = Color.FromArgb(33, 150, 243);
            public static Color FORCECOLOR_BUTTON_SUBMIT = Color.FromArgb(250, 250, 250);
            public static Color FORCECOLOR_BUTTON_REPORTERROR = Color.FromArgb(229, 57, 53);
            public static Color FORCECOLOR_BUTTON_QUESTION = Color.FromArgb(97, 97, 97);
            public static Color FORECECOLOR_LABEL_OK = Color.FromArgb(0, 230, 118);

            public static Color FORCECOLOR_LABEL_CONTEST_NAME = Color.Black;
            public static Color FORCECOLOR_LABEL_TIMER = Color.FromArgb(30, 136, 229);
            public static Color FORCECOLOR_LABEL_HEADER_CONTENT = Color.FromArgb(233, 30, 99);

            public static Size SIZE_BUTTON_DEFAULT = new Size(80, 30);
            public static Size SIZE_BUTTON_QUESTION = new Size(85, 40);

            public static string FORMAT_DATE_DEFAULT = "dd-MM-yyyy";
            public static string FORMAT_TIME_DEFAULT = "H:mm";

            public static int LOGIN_WITH_CONTESTANT_CODE = 5000;
            public static int LOGIN_WITH_STUDENT_CODE = 5001;
            public static int LOGIN_WITH_IDENTITY_CARD_NAME = 5002;

            public static int TYPE_NOTIFICATION_INFO = 6000;
            public static int TYPE_NOTIFICATION_YESNO = 6001;
            public static int TYPE_NOTIFICATION_WARNING = 6002;
            public static int TYPE_NOTIFICATION_RESULT = 6003;
            public static int TYPE_NOTIFICATION_WARNINGOPENWORD = 6004;
            // Khi thí sinh mới đăng nhập vào và đợi lệnh lấy đề thi
            public static int WAITING_BY_ADMIN_TO_LOAD_TEST = 7000;

            // Khi thí sinh mới đăng nhập vào và đợi lệnh lấy đề thi
            public static int LOAD_TEST_WITH_CONTESTANT_INTERRUPT = 7001;

            //trạng thái ca thi bắt đầu làm bài
            public static int WAITING_BY_LOAD_TEST = 7002;
            public static DateTime DATETIME_ORIGINAL_DATE = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            public static DateTime DATETIME_START_DATE = DateTime.Now;

            // Mật khẩu mã hóa
            public static string ENCRYPT_PASS_HASH = "76c0c8fdb904774249e83128262ffcb4"; //EXON2020

            // Cấu hình file config
            public static string SECTION_COMMON = "COMMON";

            public static string SECTION_DATABASE = "DATABASE";

            public static string SECTION_SUPERVISOR = "SUPERVISOR";

            public static int BUFFER_SIZE_DEFAULT = 255;
            public enum QuizTypeEnum { Regular, Audio, Fill, FillAudio, Essay, Speaking, Match, ReWritting, ListenMatching }

            // Các Level trong [VIOLATIONS]
            public static int LEVEL_LOGIN = 8001; // Đăng nhập
            public static int LEVEL_INTERRUPT = 8002; // bù giờ, gián đoạn
            public static int LEVEL_CHANGECMP = 8003; // đổi máy khi thi
            public static int LEVEL_CHANGEAWS = 8004; // đổi câu trả lời
        }
        public void HandleRichTextBoxStyle(TXTextControl.TextControl rtb)
        {
            //rtb.ReadOnly = true;
            rtb.EditMode = TXTextControl.EditMode.ReadOnly;
            rtb.BackColor = Constant.COLOR_WHITE;
            rtb.BorderStyle = TXTextControl.BorderStyle.None;
            //rtb.ShortcutsEnabled = false;
            rtb.Cursor = Cursors.Hand;

        }
        private static List<System.Windows.Forms.Control> lstControlQuestions = new List<System.Windows.Forms.Control>();
        private void HandleUCQuestionPerformClick()
        {
            Application.DoEvents();
            for (int i = 0; i < lstControlQuestions.Count; i++)
            {
                if (true)
                {
                    foreach (System.Windows.Forms.Control c in flpnListOfQuestions.Controls)
                    {
                        if (c.GetType().ToString().EndsWith(Constant.STRING_FLOWLAYOUTPANEL))
                        {
                            foreach (var c1 in c.Controls)
                            {
                                if (c1.GetType().ToString().EndsWith(Constant.STRING_QUESTION_HTML))
                                {
                                    (c1 as ucQuestionsHTML).HandleClickMrbtnAnswer();
                                }
                                else if (c1.GetType().ToString().EndsWith(Constant.STRING_QUESTION_RTF))
                                {
                                    (c1 as ucQuestionsRTF).HandleClickRadioAnswer();
                                    if ((c1 as ucQuestionsRTF).q.AnswerSheetContent != null)
                                    {
                                        (c1 as ucQuestionsRTF).mbtnControl.BackColor = Constant.BACKCOLOR_BUTTON_QUESTION;
                                        (c1 as ucQuestionsRTF).mbtnControl.ForeColor = Constant.FORCECOLOR_BUTTON_QUESTION;
                                        (c1 as ucQuestionsRTF).mbtnControl.Update();
                                    }

                                }
                            }
                        }
                        else
                        {
                            if (c.GetType().ToString().EndsWith(Constant.STRING_QUESTION_HTML))
                            {
                                (c as ucQuestionsHTML).HandleClickMrbtnAnswer();
                            }
                            else if (c.GetType().ToString().EndsWith(Constant.STRING_QUESTION_RTF))
                            {
                                (c as ucQuestionsRTF).HandleClickRadioAnswer();
                            }
                        }
                    }
                    flpnListOfQuestions2.Visible = true;
                    foreach (System.Windows.Forms.Control c in flpnListOfQuestions2.Controls)
                    {
                        if (c.GetType().ToString().EndsWith(Constant.STRING_FLOWLAYOUTPANEL))
                        {
                            foreach (System.Windows.Forms.Control c1 in c.Controls)
                            {
                                if (c1.GetType().ToString().EndsWith(Constant.STRING_QUESTION_HTML))
                                {
                                    (c1 as ucQuestionsHTML).HandleClickMrbtnAnswer();
                                }
                                else if (c1.GetType().ToString().EndsWith(Constant.STRING_QUESTION_RTF))
                                {
                                    (c1 as ucQuestionsRTF).HandleClickRadioAnswer();
                                    if ((c1 as ucQuestionsRTF).q.AnswerSheetContent != null)
                                    {
                                        (c1 as ucQuestionsRTF).mbtnControl.BackColor = Constant.BACKCOLOR_BUTTON_QUESTION;
                                        (c1 as ucQuestionsRTF).mbtnControl.ForeColor = Constant.FORCECOLOR_BUTTON_QUESTION;
                                        (c1 as ucQuestionsRTF).mbtnControl.Update();
                                    }

                                }
                            }
                        }
                        else
                        {
                            if (c.GetType().ToString().EndsWith(Constant.STRING_QUESTION_HTML))
                            {
                                (c as ucQuestionsHTML).HandleClickMrbtnAnswer();
                            }
                            else if (c.GetType().ToString().EndsWith(Constant.STRING_QUESTION_RTF))
                            {
                                (c as ucQuestionsRTF).HandleClickRadioAnswer();
                            }
                        }
                    }
                    flpnListOfQuestions2.Visible = false;
                }
            }
            
        }

        private ContestantInformation CILogged;
        private static List<ObjControl> lstObjControl;
        private List<PartOfTest> lstPartOfTest;
        private int NumberOfPage = 50; // số câu trên trang
        private int NumberFlpnOfPage = 0; // Tổng số flow panel trên 1 trang.
        private void GenerateLayoutQuestions()
        {
            Application.DoEvents();

            int Number = 1;
            int Xheader = 0;
            //    flpnListOfQuestions.SuspendLayout();
            flpnListOfQuestions2 = new FlowLayoutPanel();
            flpnListOfQuestions2.Dock = DockStyle.Right;
            flpnListOfQuestions2.BackColor = Constant.BACKCOLOR_PANEL_INFORMATION;
            flpnListOfQuestions2.Width = Constant.WIDTH_SCREEN - Constant.WIDTH_PANEL_INFORMATION;
            flpnListOfQuestions2.Controls.Clear();
            flpnListOfQuestions2.FlowDirection = FlowDirection.LeftToRight;
            flpnListOfQuestions2.AutoScroll = true;
            // flpnListOfQuestions2.AutoSize = true;
            this.Controls.Add(flpnListOfQuestions2);

            foreach (System.Windows.Forms.Control e in lstControlQuestions)
            {
                if (e.GetType().ToString().EndsWith(Constant.STRING_FLOWLAYOUTPANEL))
                {

                    if (Number <= NumberOfPage)
                    {
                        e.Visible = true;
                        flpnListOfQuestions.Controls.Add(e);
                        NumberFlpnOfPage++;
                    }
                    else
                    {
                        e.Visible = true;
                        //e.Location = new Point(0, Xheader);
                        flpnListOfQuestions2.Controls.Add(e);
                        Xheader = e.Height + 10;

                        //e.Visible = false;
                    }
                    foreach (System.Windows.Forms.Control c in e.Controls)
                    {


                        if (c.GetType().ToString().EndsWith(Constant.STRING_QUESTION_HTML) || c.GetType().ToString().EndsWith(Constant.STRING_QUESTION_RTF))
                        {
                            
                            Number++;
                        }
                        else
                        {
                            //   Xheader = c.Top;
                        }

                    }

                }
                else
                {
                }
            }

            // Do TextControl phải load lên form trước, sau đó mới load dữ liệu lên được
            // Khác với RichTextBox, ta có thể gán dữ liệu vào trước khi load lên
            // Đoạn code này là để load dữ liệu lên TextControl sau khi đã load TextControl lên form.
            foreach (System.Windows.Forms.Control flowcontrol in lstControlQuestions)
            {
                foreach (System.Windows.Forms.Control e in flowcontrol.Controls)
                {
                    if (e.GetType().ToString().EndsWith(Constant.STRING_TEXTCONTROL))
                    {
                        TXTextControl.TextControl title_of_question = e as TXTextControl.TextControl;
                        //title_of_question.Height = 1000;

                        title_of_question.ViewMode = TXTextControl.ViewMode.SimpleControl;
                        title_of_question.AutoControlSize.AutoExpand = TXTextControl.AutoSizeDirection.Both;
                        title_of_question.MaximumSize = new Size(title_of_question.Width, 500);

                        title_of_question.Load(title_of_question.Text, TXTextControl.StringStreamType.RichTextFormat);
                        title_of_question.AutoControlSize.AutoExpand = TXTextControl.AutoSizeDirection.None;
                        if (title_of_question.Height > 300)
                        {
                            title_of_question.ViewMode = TXTextControl.ViewMode.FloatingText;
                            title_of_question.ScrollBars = ScrollBars.Vertical;
                        }

                    }
                }
            }



            flpnListOfQuestions2.Visible = false;

            flpnListOfQuestions.ResumeLayout();

            


        }
        private void GenerateControlQuestions(List<ObjControl> lstObjControl)
        {
            var lstbtnQuestions = new List<string>();

            int next = 0;
            int nextd = 0;
            int XHeader = 0;
            int YHeader = 0;
            Application.DoEvents();
            foreach (ObjControl obj in lstObjControl)
            {

                if (obj.Question.Count > 1)
                {

                    FlowLayoutPanel flpnMultiQuestion = new FlowLayoutPanel();
                    flpnMultiQuestion.FlowDirection = FlowDirection.LeftToRight;
                    flpnMultiQuestion.Width = Constant.WIDTH_SCREEN - Constant.WIDTH_PANEL_INFORMATION;
                    flpnMultiQuestion.Name = "flpnMultiQuestion";
                    flpnMultiQuestion.BackColor = Constant.COLOR_WHITE;
                    flpnMultiQuestion.Width = obj.Width;

                    flpnMultiQuestion.AutoSize = true;

                    Questions qHeader = obj.Question[0];
                    obj.Question.RemoveAt(0);
                    if (lstPartOfTest != null)
                    {
                        foreach (PartOfTest pt in lstPartOfTest)
                        {
                            if (pt.Index == next + 1)
                            {
                                TRichTextBox.AdvanRichTextBox rtbTitlePart = new TRichTextBox.AdvanRichTextBox();
                                //rtbTitlePart = new TXTextControl.TextControl();
                                rtbTitlePart.Font = new Font(Constant.FONT_FAMILY_DEFAULT, 18, FontStyle.Bold);
                                rtbTitlePart.Width = flpnMultiQuestion.Width - 30;
                                rtbTitlePart.Rtf = pt.PartContent;
                                rtbTitlePart.Margin = new Padding(15, 5, 0, 20);
                                rtbTitlePart.Location = new Point(0, YHeader);
                                // rtbTitlePart.ReadOnly = true;

                                //Vì khi di chuyển chuột quan các tiêu đề các phần bị lỗi, chưa fix được
                                //Nguyễn Hữu Hải
                                //rtbTitlePart.MouseHover += RtbTitleOfQuestion_MouseHover;
                                flpnMultiQuestion.Controls.Add(rtbTitlePart);
                                //rtbTitlePart.Rtf = pt.PartContent;
                                //rtbTitlePart.Load(pt.PartContent, TXTextControl.StringStreamType.RichTextFormat);
                                //rtbTitlePart.Text = pt.PartContent;
                                flpnMultiQuestion.Height += rtbTitlePart.Height + 5;
                                YHeader = rtbTitlePart.Bottom + 5;

                                break;
                            }

                        }
                        //foreach (PartOfTest pt in lstPartOfTest)
                        //{
                        //    rtbTitlePart.Load(pt.PartContent, TXTextControl.StringStreamType.RichTextFormat);
                        //}
                    }

                    if (qHeader.Audio == null)
                    {
                        TRichTextBox.AdvanRichTextBox temp = new TRichTextBox.AdvanRichTextBox();
                        //temp.ContentsResized += RtbTitleOfQuestion_ContentsResized;
                        temp.Width = flpnMultiQuestion.Width - 30;
                        temp.Rtf = qHeader.TitleOfQuestion;
                        TXTextControl.TextControl rtbTitleOfQuestion = new TXTextControl.TextControl();
                        // rtbTitleOfQuestion.ContentsResized += RtbTitleOfQuestion_ContentsResized;
                        rtbTitleOfQuestion.Width = flpnMultiQuestion.Width - 30;
                        HandleRichTextBoxStyle(rtbTitleOfQuestion);

                        if (qHeader.HighToDisplayForQuestion > 0)
                        {
                            rtbTitleOfQuestion.Height = qHeader.HighToDisplayForQuestion;
                        }
                        //rtbTitleOfQuestion.Height = temp.Height;

                        rtbTitleOfQuestion.ScrollBars = ScrollBars.Vertical;
                        rtbTitleOfQuestion.Margin = new Padding(15, 5, 0, 5);
                        //  rtbTitleOfQuestion.ReadOnly = true;
                       
                        
                        rtbTitleOfQuestion.Location = new Point(0, YHeader);
                        rtbTitleOfQuestion.ViewMode = TXTextControl.ViewMode.SimpleControl;
                        rtbTitleOfQuestion.AutoControlSize.AutoExpand = TXTextControl.AutoSizeDirection.Both;
                        rtbTitleOfQuestion.MaximumSize = new Size(rtbTitleOfQuestion.Width, 500);
                        //TXTextControl.AutoSize auto_size = new TXTextControl.AutoSize();
                        //auto_size.AutoExpand = TXTextControl.AutoSizeDirection.Both;
                        //rtbTitleOfQuestion.AutoControlSize = auto_size;

                        flpnMultiQuestion.Controls.Add(rtbTitleOfQuestion);
                        flpnMultiQuestion.Height += rtbTitleOfQuestion.Height + 5;
                        XHeader = rtbTitleOfQuestion.Bottom + 5;
                        //rtbTitleOfQuestion.Load(qHeader.TitleOfQuestion, TXTextControl.StringStreamType.RichTextFormat);
                        rtbTitleOfQuestion.Text = qHeader.TitleOfQuestion;
                    }
                        
                    
                    foreach (Questions q in obj.Question)
                    {

                        if (q.IsQuestionContent && q.IsSingleChoice)
                        {
                            ucQuestionsRTF ucRTF = new ucQuestionsRTF(Sql, CILogged);
                            ucRTF.Location = new Point(0, XHeader);
                            ucRTF.Width = obj.Width;
                            q.NO = (next + 1);
                            next++;
                            nextd = q.NO;
                            SendQuestion sq = new SendQuestion(ucRTF.HandleQuestion);
                            sq(q, obj.AnswerSheetID);
                            ucRTF.Tag = q.SubQuestionID;
                            //ucRTF.SendWorking += UcRTF_SendWorking;
                            flpnMultiQuestion.Controls.Add(ucRTF);
                            flpnMultiQuestion.Height += ucRTF.Height + 5;
                        }
                        else if (!q.IsQuestionContent && q.IsSingleChoice)
                        {
                            ucQuestionsRTF ucRTF = new ucQuestionsRTF(Sql, CILogged);
                            ucRTF.Location = new Point(0, XHeader);


                            ucRTF.Width = obj.Width;
                            q.NO = (next + 1);
                            next++;
                            nextd = q.NO;
                            SendQuestion sq = new SendQuestion(ucRTF.HandleQuestion);
                            sq(q, obj.AnswerSheetID);
                            ucRTF.Tag = q.SubQuestionID;
                            //ucRTF.SendWorking += UcRTF_SendWorking;
                            flpnMultiQuestion.Controls.Add(ucRTF);
                            flpnMultiQuestion.Height += ucRTF.Height + 5;
                        }
                        else if (q.IsQuestionContent && !q.IsSingleChoice)
                        {
                            ucQuestionsRTF ucRTF = new ucQuestionsRTF(Sql, CILogged);
                            ucRTF.Location = new Point(0, XHeader);
                            ucRTF.Width = obj.Width;
                            q.NO = (next + 1);
                            next++;
                            nextd = q.NO;
                            SendQuestion sq = new SendQuestion(ucRTF.HandleQuestion);
                            sq(q, obj.AnswerSheetID);
                            ucRTF.Tag = q.SubQuestionID;
                            //ucRTF.SendWorking += UcRTF_SendWorking;
                            flpnMultiQuestion.Controls.Add(ucRTF);
                            flpnMultiQuestion.Height += ucRTF.Height + 5;
                        }
                        else
                        {
                            // TODO
                        }
                    }


                    // }
                    lstControlQuestions.Add(flpnMultiQuestion);
                }
                else
                {
                    FlowLayoutPanel flpnMultiQuestion = new FlowLayoutPanel();
                    flpnMultiQuestion.FlowDirection = FlowDirection.LeftToRight;

                    flpnMultiQuestion.BackColor = Constant.COLOR_WHITE;
                    flpnMultiQuestion.Width = obj.Width;

                    flpnMultiQuestion.AutoSize = true;
                    if (lstPartOfTest != null)
                    {
                        foreach (PartOfTest pt in lstPartOfTest)
                        {
                            if (pt.Index == next + 1)
                            {
                                TRichTextBox.AdvanRichTextBox rtbTitlePart = new TRichTextBox.AdvanRichTextBox();
                                rtbTitlePart.Font = new Font(Constant.FONT_FAMILY_DEFAULT, 18, FontStyle.Bold);
                                rtbTitlePart.Width = flpnMultiQuestion.Width - 30;
                                rtbTitlePart.Rtf = pt.PartContent;
                                rtbTitlePart.Margin = new Padding(15, 5, 0, 20);
                                rtbTitlePart.Location = new Point(0, XHeader);
                                // rtbTitlePart.ReadOnly = true;

                                //Vì khi di chuyển chuột quan các tiêu đề các phần bị lỗi, chưa fix được
                                //Nguyễn Hữu Hải
                                //rtbTitlePart.MouseHover += RtbTitleOfQuestion_MouseHover;
                                flpnMultiQuestion.Controls.Add(rtbTitlePart);
                                flpnMultiQuestion.Height += rtbTitlePart.Height + 5;
                                XHeader = rtbTitlePart.Bottom + 5;
                                break;
                            }

                        }
                    }
                    foreach (Questions q in obj.Question)
                    {

                        if (q.IsQuestionContent && q.IsSingleChoice)
                        {

                            if (nextd != 0)
                            {
                                q.NO = nextd + 1;
                                nextd = 0;
                            }
                            else
                            {
                                q.NO = next + 1;
                            }
                            EXONSYSTEM.ucQuestionsRTF ucRTF = new ucQuestionsRTF(Sql, CILogged);
                            ucRTF.Location = new Point(0, XHeader);


                            ucRTF.Width = obj.Width;
                            //if (count != 0)
                            //{
                            //    q.NO +=(count-1);
                            //}
                            next = q.NO;
                            SendQuestion sq = new SendQuestion(ucRTF.HandleQuestion);
                            sq(q, obj.AnswerSheetID);
                            ucRTF.Tag = q.SubQuestionID;
                            //ucRTF.SendWorking += UcRTF_SendWorking;
                            flpnMultiQuestion.Controls.Add(ucRTF);
                            flpnMultiQuestion.Height += ucRTF.Height + 5;
                        }
                        else
                        {
                            if (nextd != 0)
                            {
                                q.NO = nextd + 1;
                                nextd = 0;
                            }
                            else
                            {
                                q.NO = next + 1;
                            }
                            ucQuestionsRTF ucRTF = new ucQuestionsRTF(Sql, CILogged);
                            ucRTF.Location = new Point(0, XHeader);


                            ucRTF.Width = obj.Width;
                            //if (count != 0)
                            //{
                            //    q.NO +=(count-1);
                            //}
                            next = q.NO;
                            SendQuestion sq = new SendQuestion(ucRTF.HandleQuestion);
                            sq(q, obj.AnswerSheetID);
                            ucRTF.Tag = q.SubQuestionID;
                            //ucRTF.SendWorking += UcRTF_SendWorking;
                            flpnMultiQuestion.Controls.Add(ucRTF);
                            flpnMultiQuestion.Height += ucRTF.Height + 5;
                        }

                        lstControlQuestions.Add(flpnMultiQuestion);
                    }
                }


            }
            
        }
        public void SetCanChangeMetroPanelColor(MetroPanel mpn)
        {
            mpn.UseCustomBackColor = true;
            mpn.UseCustomForeColor = true;
        }
        private void HandleGenerateLabelCodeTest(string CodeTest)
        {
            MetroPanel mpnController = new MetroPanel();
            SetCanChangeMetroPanelColor(mpnController);
            mpnController.Name = "mpnController";
            mpnController.AutoSize = true;
            mpnController.AutoScroll = false;
            pnInformation.Controls.Add(mpnController);
            mpnController = (MetroPanel)pnInformation.Controls["mpnController"];
            //mã đề

            Label mlblCode = new Label();
            mlblCode.Name = "mlblCode";
            mlblCode.ForeColor = Constant.COLOR_RED;
            mlblCode.AutoSize = true;
            mlblCode.Font = new Font(Constant.FONT_FAMILY_DEFAULT, 17, FontStyle.Bold);
            mlblCode.Text = "Mã đề: " + CodeTest;
            mlblCode.BackColor = Constant.COLOR_TRANSPARENT;

            mpnController.Controls.Add(mlblCode);

            mlblCode.Location = new Point(20, Convert.ToInt32((mpnController.Height - mlblCode.Height) / 2));
            mpnController.Width = mlblCode.Right + 100;
            mpnController.Location = new Point(Convert.ToInt32((Constant.WIDTH_PANEL_INFORMATION - mpnController.Width) / 2), lbTimer.Bottom + 10);
        }
        private void MbtnPrevious_Click(object sender, EventArgs e)
        {

            this.flpnListOfQuestions.Visible = true;
            this.flpnListOfQuestions2.Visible = false;
        }


        private void MbtnNext_Click(object sender, EventArgs e)
        {



            this.flpnListOfQuestions.Visible = false;

            this.flpnListOfQuestions2.Visible = true;


        }
        private void HandleStyleButtonReadTimePause()
        {

            Panel pnlBottom = new Panel();
            pnlBottom.Height = 100;
            pnlBottom.Width = Constant.WIDTH_PANEL_INFORMATION;
            pnlBottom.Location = new Point(0, 0);
            //pnlBottom.BackColor = Color.FromArgb(0, 0, 0);

            //MetroComboBox cbPage = new MetroComboBox();
            //cbPage.Items.Add(10);
            //cbPage.Items.Add(20);
            //cbPage.Items.Add(40);
            ////cbPage.SelectedValueChanged += new EventHandler(this.CbPage_SelectedValueChanged);
            //cbPage.Location = new Point(0, 0);
            //pnlBottom.Controls.Add(cbPage);

            MetroButton mbtnPrevious = new MetroButton();
            mbtnPrevious.Text = "Trang trước";
            mbtnPrevious.Click += new EventHandler(this.MbtnPrevious_Click);
            mbtnPrevious.Location = new Point(100, 0);
            mbtnPrevious.Size = Constant.SIZE_BUTTON_DEFAULT;
            mbtnPrevious.BackColor =  Color.FromArgb(255, 0, 0);
            pnlBottom.Controls.Add(mbtnPrevious);
            
            MetroButton mbtnNext = new MetroButton();
            mbtnNext.Text = "Trang Tiếp";
            mbtnNext.Click += new EventHandler(this.MbtnNext_Click);
            mbtnNext.Location = new Point(mbtnPrevious.Right + 10, 0);
            mbtnNext.Size = Constant.SIZE_BUTTON_DEFAULT;
            mbtnNext.BringToFront();
            mbtnNext.BackColor = Color.FromArgb(255, 0, 0);
            pnlBottom.Controls.Add(mbtnNext);
            pnInformation.Size = new System.Drawing.Size(300, 250);
            pnInformation.Controls.Add(pnlBottom);


            
        }
        private void HandleStylePanelController()
        {
            Label lbTimer = (Label)pnInformation.Controls["lbTimer"];

            MetroPanel mpnController = new MetroPanel();
            SetCanChangeMetroPanelColor(mpnController);
            mpnController.Name = "mpnController";
            mpnController.AutoSize = true;
            mpnController.AutoScroll = false;
            mpnController.BringToFront();
            MetroButton mbtnReportError = new MetroButton();
            
           
            mbtnReportError.Name = "mbtnReportError";
            mbtnReportError.ForeColor = Constant.FORCECOLOR_BUTTON_REPORTERROR;
            mbtnReportError.BackColor = Constant.BACKCOLOR_BUTTON_CONTROLLER;
            mbtnReportError.Size = Constant.SIZE_BUTTON_DEFAULT;
            mpnController.Controls.Add(mbtnReportError);
            mpnController.Height = mbtnReportError.Height + 20;
            mpnController.Width = mbtnReportError.Width;
            mbtnReportError.Location = new Point(0, Convert.ToInt32((mpnController.Height - mbtnReportError.Height) / 2));
            mbtnReportError.BringToFront();
            //THONGBAO LOI
            mbtnReportError.Visible = false;
            mpnController.Location = new Point(Convert.ToInt32((Constant.WIDTH_PANEL_INFORMATION - mpnController.Width) / 2), lbTimer.Bottom);
            pnInformation.Controls.Add(mpnController);
            pnInformation.BringToFront();


            MetroPanel mpnInformationWrapper = new MetroPanel();
            mpnInformationWrapper.Name = "mpnInformationWrapper";
            SetCanChangeMetroPanelColor(mpnInformationWrapper);
            mpnInformationWrapper.Location = new Point(0, mpnController.Bounds.Bottom + 10);
            mpnInformationWrapper.BackColor = Constant.BACKCOLOR_PANEL_INFORMATION;
            pnInformation.Controls.Add(mpnInformationWrapper);
            mpnInformationWrapper.BringToFront();
            //   Controllers.Instance.SetCanChangeMetroPanelColor(mpnInformationWrapper1);
            mpnInformationWrapper1.Location = new Point(0, mpnController.Bounds.Bottom + 10);
            mpnInformationWrapper1.BackColor = Constant.BACKCOLOR_PANEL_INFORMATION;
            mpnInformationWrapper1.BringToFront();
            
            flpnListOfButtonQuestions.Location = new Point(0, mpnController.Bounds.Bottom + 215);
            flpnListOfButtonQuestions.Height = Constant.HEIGHT_SCREEN - flpnListOfButtonQuestions.Top - 120;
            mpnInformationWrapper.Size = new Size(Constant.WIDTH_PANEL_INFORMATION, flpnListOfButtonQuestions.Bottom + 20);
            flpnListOfButtonQuestions.BackColor = Color.FromArgb(255, 0, 0);
            flpnListOfButtonQuestions.BringToFront();
            HandleStyleButtonReadTimePause();
            //HandleStyleWarningCount();
        }
        private void HandlePanelListOfQuestions()
        {
            
            
            flpnListOfQuestions.Dock = DockStyle.Right;
            flpnListOfQuestions.BackColor = Constant.BACKCOLOR_PANEL_INFORMATION;
            flpnListOfQuestions.Width = Constant.WIDTH_SCREEN - Constant.WIDTH_PANEL_INFORMATION;
            flpnListOfQuestions.Controls.Clear();
            
        }
        private void GernerateObjControl()
        {
            Application.DoEvents();
            
            this.Cursor = Cursors.WaitCursor;
            flpnListOfButtonQuestions.Width = Constant.WIDTH_PANEL_INFORMATION;
            // Debug.WriteLine(IsContinute);

            // Khởi tạo tiến trình render giao diện trang làm bài
            var lstObjControl = new List<ObjControl>();

            foreach (List<Questions> lstQ in lstLQuestion)
            {
                lstObjControl.Add(new ObjControl(lstQ, CILogged.AnswerSheetID, Constant.WIDTH_SCREEN - Constant.WIDTH_PANEL_INFORMATION - 50));
            }
            HandlePanelListOfQuestions();
            GenerateControlQuestions(lstObjControl);
            GenerateLayoutQuestions();
            HandleStylePanelController();
            HandleUCQuestionPerformClick();
            HandleGenerateLabelCodeTest(CILogged.TestID.ToString());
            ErrorController rEC = new ErrorController();
            //Log.Instance.WriteLog(Properties.Resources.MSG_LOG_INFO, "MAIN | RENDER_LAYOUT", Properties.Resources.MSG_MESS_0007);
            //if (currentStatusContestant == Constant.STATUS_LOGGED)
            //{
            //    Label lbContent = (Label)((MetroPanel)GetPanelLoading().Controls["mpnLoadingWrapper"]).Controls["lbContent"];
            //    lbContent.Text = Properties.Resources.MSG_GUI_0034;
            //}
            // Change status to STATUS_READY
            //if (currentStatusContestant == Constant.STATUS_LOGGED_DO_NOT_FINISH && statusDivisionShift == UserHelper.Common.STATUS_STARTTEST)
            //{
            //    IsLoadingTest = Constant.LOAD_TEST_WITH_CONTESTANT_INTERRUPT;
            //}

            this.Cursor = Cursors.Arrow;
        }

        private void frmXemBaiLam_Load(object sender, EventArgs e)
        {
            lstControlQuestions = new List<System.Windows.Forms.Control>();
            GernerateObjControl();

        }
    }
}
