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
using EXON.SubData.Services;
using EXON.SubModel.Models;
namespace EXON.MONITOR.GUI
{
     public partial class frmUpdateQuestion : Form
     {
          EXON.SubModel.Models.MTAQuizDbContext db = new EXON.SubModel.Models.MTAQuizDbContext();
          public frmUpdateQuestion()
          {
               InitializeComponent();
               InitControl();
          }
          public static class Constant
          {
               public static int WIDTH_SCREEN = Screen.PrimaryScreen.Bounds.Size.Width;
               public static int HEIGHT_SCREEN = Screen.PrimaryScreen.Bounds.Size.Height;
               public static int WIDTH_PANEL_INFORMATION = Convert.ToInt32(WIDTH_SCREEN * 0.15);


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
               public static Color COLOR_GRAY = Color.Gray;
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
          public void InitControl()
          {
               cbMonthi.DataSource = db.SUBJECTS.ToList();
               cbMonthi.DisplayMember = "SubjectName";
               cbMonthi.ValueMember = "SubjectID";        
          }

          private void btnSave_Click(object sender, EventArgs e)
          {
               string subjectName;
               subjectName = cbMonthi.Text.ToString();
               SUBJECT sUBJECT = db.SUBJECTS.Where(p => p.SubjectName == subjectName).FirstOrDefault();
               var lstQuestions = (from test in db.TESTS
                                   where test.SubjectID == sUBJECT.SubjectID
                                   from testdt in db.TEST_DETAILS
                                   where testdt.TestID == test.TestID
                                   from ques in db.QUESTIONS
                                   where ques.QuestionID == testdt.QuestionID

                                   select new
                                   {
                                        ques.QuestionID,
                                        ques.Level
                                   }).Distinct().ToList();
               foreach (var item in lstQuestions)
               {
                    QUESTION question = db.QUESTIONS.Where(p => p.QuestionID == item.QuestionID).FirstOrDefault();
                    ComboBox cb = Controls.Find("cb" + item.QuestionID.ToString(), true).FirstOrDefault() as ComboBox;
                    question.Level = Int32.Parse(cb.Text);
                    List<SUBQUESTION> lst = db.SUBQUESTIONS.Where(p => p.QuestionID == item.QuestionID).ToList();

                    foreach (var item1 in lst)
                    {
                         TextBox tb = Controls.Find("tb" + item.QuestionID.ToString(), true).FirstOrDefault() as TextBox;
                         item1.Score = Int32.Parse(tb.Text); 
                    }
                    db.SaveChanges();
               }
               MessageBox.Show("Cập nhật thành công", "Thành Công");
               InitControl();
               
          }

          private void cbMonthi_SelectedValueChanged(object sender, EventArgs e)
          {
               try
               {
                    flpnListOfQuestions.Controls.Clear();
                    string subjectName;
                    if (cbMonthi.Text != null)
                    {
                         subjectName = cbMonthi.Text.ToString();
                         SUBJECT sUBJECT = db.SUBJECTS.Where(p => p.SubjectName == subjectName).FirstOrDefault();
                         int XHeader = 0;
                         int YHeader = 0;
                         FlowLayoutPanel flpnMultiQuestion = new FlowLayoutPanel();
                         flpnMultiQuestion.FlowDirection = FlowDirection.LeftToRight;
                         flpnMultiQuestion.Width = Constant.WIDTH_SCREEN - Constant.WIDTH_PANEL_INFORMATION;
                         flpnMultiQuestion.Name = "flpnMultiQuestion";
                         flpnMultiQuestion.BackColor = Constant.COLOR_GRAY;
                         flpnMultiQuestion.AutoScroll = true;
                         var lstQuestions = (from test in db.TESTS
                                             where test.SubjectID == sUBJECT.SubjectID
                                             from testdt in db.TEST_DETAILS
                                             where testdt.TestID == test.TestID
                                             from ques in db.QUESTIONS
                                             where ques.QuestionID == testdt.QuestionID

                                             select new
                                             {
                                                  ques.QuestionID,
                                                  ques.Level
                                             }).Distinct().ToList();
                         foreach (var item in lstQuestions)
                         {
                              Control.ucQuestion ucQuestion = new Control.ucQuestion(item.QuestionID, Constant.WIDTH_SCREEN - Constant.WIDTH_PANEL_INFORMATION);
                              ucQuestion.Width = flpnMultiQuestion.Width;
                              ucQuestion.Location = new Point(0, YHeader);
                              ComboBox comboBox = new ComboBox();
                              comboBox.Name = "cb" + item.QuestionID.ToString();
                              comboBox.Items.Insert(0, "1");
                              comboBox.Items.Insert(1, "2");
                              comboBox.Items.Insert(2, "3");
                              comboBox.Items.Insert(3, "4");
                              comboBox.Location = new Point(1000, 18);
                              comboBox.Text = item.Level.ToString();
                              ucQuestion.Controls.Add(comboBox);
                              TextBox textBox = new TextBox();
                              textBox.Name = "tb" + item.QuestionID.ToString();
                              textBox.Text = db.SUBQUESTIONS.Where(p => p.QuestionID == item.QuestionID).First().Score.ToString();
                              textBox.Location = new Point(1000, 42);
                              ucQuestion.Controls.Add(textBox);


                              ucQuestion.Controls.Add(textBox);

                              flpnMultiQuestion.Height += ucQuestion.Height + 5;

                              ucQuestion.Update();
                              flpnMultiQuestion.Controls.Add(ucQuestion);
                              List<SUBQUESTION> lst = db.SUBQUESTIONS.Where(p => p.QuestionID == item.QuestionID).ToList();

                              foreach (var item1 in lst)
                              {
                                   Control.ucSubquestion ucSubquestion = new Control.ucSubquestion(item1.SubQuestionID, Constant.WIDTH_SCREEN - Constant.WIDTH_PANEL_INFORMATION);
                                   ucSubquestion.Location = new Point(15, YHeader);
                                   ucSubquestion.Width = ucQuestion.Width - 15;
                                   ucSubquestion.Update();
                                   flpnMultiQuestion.Controls.Add(ucSubquestion);
                                   YHeader += ucSubquestion.Height + 5;
                                   TextBox textBox2 = new TextBox();
                                   textBox2.Name = "score" + item.QuestionID.ToString();
                                   textBox2.Location = new Point(960, 24);
                                   textBox2.Text = item1.Score.ToString();
                                   textBox2.ReadOnly = true;
                                   ucSubquestion.Controls.Add(textBox2);
                              }
                              YHeader += ucQuestion.Bottom + 5;
                         }



                         flpnListOfQuestions.Controls.Add(flpnMultiQuestion);
                    }


               }
               catch (Exception ex)
               {

               }
          }

          private void cbMonthi_TextChanged(object sender, EventArgs e)
          {
               
          }

          private void button1_Click(object sender, EventArgs e)
          {
               frmQuickUpdate frm = new frmQuickUpdate();
               frm.ShowDialog();
          }
     }
}
