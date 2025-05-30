using EXON.SubModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using static EXON.MONITOR.GUI.frmXemBaiLam;

namespace EXON.MONITOR.GUI
{
    public partial class frmChangeHeightToDisplay : Form
    {
        int rs, rr;int WIDTH_SCREEN = Screen.PrimaryScreen.Bounds.Size.Width;
        public frmChangeHeightToDisplay()
        {
            InitializeComponent();
            btnSave.Enabled = false;
            
            panel1.Width = WIDTH_SCREEN - groupBox1.Width -20;
        }
        private List<frmXemBaiLam.Answer> GetListAnswerByListAnswerID(List<int> lstAnswerID, SqlConnection sql)
        {

            List<frmXemBaiLam.Answer> lstAnswer = new List<frmXemBaiLam.Answer>();
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
                        SUBQUESTION sub_ques = DB.SUBQUESTIONS.Where(s => s.SubQuestionID == A.SubQuestionID).FirstOrDefault();
                        //if (A.SUBQUESTION.Score.HasValue)
                        if (sub_ques.Score.HasValue)
                            Score = sub_ques.Score.Value;
                        lstAnswer.Add(new frmXemBaiLam.Answer(A.AnswerID, A.AnswerContent, height, A.IsCorrect, A.SubQuestionID, Score));
                    }
                }
            }
            return lstAnswer;
        }
        public List<SubQuestion> GetListSubQuestionByQuestionID(int questionID, int testID, SqlConnection sql)
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
        public void GetListQuestionByTestID(int TestID, out List<List<frmXemBaiLam.Questions>> rLLstQuest, out bool IsContinute, out int numberQuestionsOfTest, frmXemBaiLam.ErrorController EC, SqlConnection sql)
        {
            IsContinute = false;
            numberQuestionsOfTest = 0;
            using (SubModel.Models.MTAQuizDbContext DB = new SubModel.Models.MTAQuizDbContext())
            {
                try
                {
                    int count = 0;
                    List<SqlParameter> para = new List<SqlParameter>();
                    para.Add(new SqlParameter("@TestID", TestID));
                    List<TEST_DETAILS> lstTD = DB.TEST_DETAILS.Where(s => s.TestID == TestID).ToList();
                    //List<TEST_DETAILS> lstTD = DB.TEST_DETAILS.Where(x => x.TestID == CI.TestID).ToList();

                    



                    lstTD.OrderBy(x => x.NumberIndex);
                    //Debug.WriteLine("lstTD.Count {0}", lstTD.Count);
                    List<frmXemBaiLam.Questions> lstQuestions;
                    List<List<frmXemBaiLam.Questions>> lstLQuestion = new List<List<frmXemBaiLam.Questions>>();


                    if (lstTD.Count > 0)
                    {
                        List<SubQuestion> lstSubQuestiton = new List<SubQuestion>();
                        foreach (TEST_DETAILS td in lstTD)
                        {
                            lstQuestions = new List<frmXemBaiLam.Questions>();
                            lstSubQuestiton = GetListSubQuestionByQuestionID(td.QuestionID, td.TestID, sql);

                            para = new List<SqlParameter>();
                            para.Add(new SqlParameter("@QuestionID", td.QuestionID));
                            //QUESTION ques = ExcuteObject<QUESTION>("SELECT * FROM QUESTIONS WHERE QuestionID = @QuestionID", para, sql).ToList()[0];
                            QUESTION ques = DB.QUESTIONS.Where(s => s.QuestionID == td.QuestionID).FirstOrDefault();

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

                                    frmXemBaiLam.Questions q = new frmXemBaiLam.Questions();
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
                                        lstQuestions.Insert(0, new frmXemBaiLam.Questions(ques.Audio, SQ.QuestionID, td.TestDetailID, td.Status, q.Type));
                                        lstQuestions.Insert(1, new frmXemBaiLam.Questions(ques.QuestionContent, td.Status, HeightToDisplayForQ, td.TestDetailID));
                                        firstIndex = 1;
                                    }
                                    //else if (lstQuestions.Count == SQ.QUESTION.NumberSubQuestion && SQ.QUESTION.NumberSubQuestion > 1 && firstIndex == 0)
                                    else if (lstQuestions.Count == ques.NumberSubQuestion && ques.NumberSubQuestion > 1 && firstIndex == 0)
                                    {
                                        //lstQuestions.Insert(0, new Questions(SQ.QUESTION.QuestionContent, td.Status, HeightToDisplayForQ, td.TestDetailID));
                                        lstQuestions.Insert(0, new frmXemBaiLam.Questions(ques.QuestionContent, td.Status, HeightToDisplayForQ, td.TestDetailID));
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

                }
            }
        }
        private static SqlConnection Sql;
        

        private ContestantInformation CILogged;
        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Width = WIDTH_SCREEN - groupBox1.Width - 80;

            panel1.Controls.Clear();
            panel1.Width = WIDTH_SCREEN - groupBox1.Width - 80;

            int TestID = int.Parse(textBox1.Text);
            int NO = int.Parse(textBox2.Text);
            var rLLstQuest = new List<List<frmXemBaiLam.Questions>>();
            bool iC = false;
            int nbt = 0;
            var EC = new frmXemBaiLam.ErrorController();
            var sql = new SqlConnection();
            try
            {
                GetListQuestionByTestID(TestID, out rLLstQuest, out iC, out nbt, EC, sql);
                lblTestCode.Text = TestID.ToString();
                var r = (from sublist in rLLstQuest
                         from item in sublist
                         where item.NO == NO select item).FirstOrDefault();
                lblSQuestion.Text = r.NO.ToString();
                lblCurrentHeight.Text = r.HighToDisplayForSubQuestion.ToString();
                rs = r.SubQuestionID;
                rr = r.QuestionID;
                btnSave.Enabled = true;
                SubData.MTAQuizDbContext DB = new SubData.MTAQuizDbContext();
                EXONSYSTEM.ucQuestionsRTF ucRTF = new EXONSYSTEM.ucQuestionsRTF(Sql, CILogged);
                ucRTF.Location = new Point(0, 0);
                List<SqlParameter> para = new List<SqlParameter>();
                para.Add(new SqlParameter("@SubQuestionID", rs));
                //SUBQUESTION SQ = ExcuteObject<SUBQUESTION>("SELECT * FROM SUBQUESTIONS WHERE SubQuestionID = @SubQuestionID", para, sql).ToList()[0];
                SUBQUESTION SQ = DB.SUBQUESTIONS.SingleOrDefault(x => x.SubQuestionID == rs);

                para = new List<SqlParameter>();
                para.Add(new SqlParameter("@QuestionID", SQ.QuestionID));
                //ques = ExcuteObject<QUESTION>("SELECT * FROM QUESTIONS WHERE QuestionID = @QuestionID", para, sql).ToList()[0];
                //QUESTION ques = DB.QUESTIONS.Where(s => s.QuestionID == td.QuestionID).FirstOrDefault();

                Questions q = new Questions();
                //q.NO = td.NumberIndex + sq.index + 1;
                // Todo


                q.QuestionID = SQ.QuestionID;
                q.SubQuestionID = SQ.SubQuestionID;

                q.TitleOfQuestion = SQ.SubQuestionContent;
                q.AnswerChecked = 2000;
                //q.IsSingleChoice = SQ.QUESTION.IsSingleChoice;
                //q.IsQuestionContent = SQ.QUESTION.IsQuestionContent;

                q.Score = SQ.Score != null ? (float)SQ.Score : default(float);
                q.ListAnswer = r.ListAnswer;
                int HeightToDisplayForSub = 0;
                if (SQ.HeightToDisplay.HasValue)
                {
                    HeightToDisplayForSub = SQ.HeightToDisplay.Value;
                }
                q.HighToDisplayForSubQuestion = HeightToDisplayForSub;
       
                ucRTF.Width = panel1.Width;
                SendQuestion sq = new SendQuestion(ucRTF.HandleQuestion);
                sq(q , 0);
                ucRTF.Tag = rs;
                ucRTF.BorderStyle = BorderStyle.FixedSingle;
                #region xử lý lại khi click câu hỏi

                ucRTF.toolTip1.SetToolTip(ucRTF.rtbAnswerD, "Bạn có thể nhấp đúp chuột để thay đổi chiều cao câu trả lời");
                ucRTF.toolTip1.SetToolTip(ucRTF.rtbAnswerA, "Bạn có thể nhấp đúp chuột để thay đổi chiều cao câu trả lời");
                ucRTF.toolTip1.SetToolTip(ucRTF.rtbAnswerB, "Bạn có thể nhấp đúp chuột để thay đổi chiều cao câu trả lời");
                ucRTF.toolTip1.SetToolTip(ucRTF.rtbAnswerC, "Bạn có thể nhấp đúp chuột để thay đổi chiều cao câu trả lời");
                ucRTF.rtbAnswerA.DoubleClick += new EventHandler(ucRTF.mrbAnswer_double);
                ucRTF.rtbAnswerB.DoubleClick += new EventHandler(ucRTF.mrbAnswer_double);
                ucRTF.rtbAnswerC.DoubleClick += new EventHandler(ucRTF.mrbAnswer_double);
                ucRTF.rtbAnswerD.DoubleClick += new EventHandler(ucRTF.mrbAnswer_double);
                #endregion
                //ucRTF.SendWorking += UcRTF_SendWorking;
                panel1.Controls.Add(ucRTF);
                //panel1.AutoSize = true;
                
                //panel1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                panel1.Height += ucRTF.Height + 5;
            }
            catch { }
            
        }

        private void frmChangeHeightToDisplay_Load(object sender, EventArgs e)
        {
            panel1.AutoScroll = false;
            panel1.HorizontalScroll.Enabled = false;
            panel1.HorizontalScroll.Visible = false;
            panel1.HorizontalScroll.Maximum = 0;
            panel1.AutoScroll = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SubData.MTAQuizDbContext DB = new SubData.MTAQuizDbContext())
            {
                SUBQUESTION b = DB.SUBQUESTIONS.FirstOrDefault(i => i.SubQuestionID == rs);
                b.HeightToDisplay = int.Parse(textBox3.Text);
                DB.SaveChanges();
                button1_Click(sender, e);
                MessageBox.Show("OK");
            }
        }
    }
}
