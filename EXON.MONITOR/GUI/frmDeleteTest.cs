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
using System.Web.Script.Serialization;
using EXON.Common;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using EXON.Common;
using EXON.GradedEssay;

namespace EXON.MONITOR.GUI
{
    public partial class frmDeleteTest : Form
    {
        MTAQuizDbContext db = new MTAQuizDbContext();
        private int _ContestID;
        private int _LocationID;
        private int _QuestionID;

        public frmDeleteTest()
        {
            InitializeComponent();
        }
        public frmDeleteTest(int contestID, int locationID)
        {
            InitializeComponent();
            this._ContestID = contestID;
            this._LocationID = locationID;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.ControlBox = false;
            this.WindowState = FormWindowState.Maximized;
            this.BringToFront();
        }

        private QUESTION SearchQuestion(int testID, int orderOfQuestion)
        {
            List<TEST_DETAILS> lstTD = db.TEST_DETAILS.Where(p => p.TestID == testID).OrderBy(p => p.TestDetailID).ToList();
            QUESTION result = new QUESTION();
            try
            {
                result = lstTD[orderOfQuestion-1].QUESTION;
            }
            catch
            {
                result = null;
            }
            return result;
        }

        private void SearchRelateTest(int questionID)
        {
            try
            {
                List<int> lstShift = db.SHIFTS.Where(p => p.ContestID == _ContestID).Select(p => p.ShiftID).ToList();
                List<int> lstRoomttest = db.ROOMTESTS.Where(p => p.LocationID == _LocationID).Select(p => p.RoomTestID).ToList();
                List<int> lstDivShift = db.DIVISION_SHIFTS.Where(p => lstShift.Contains(p.ShiftID) && lstRoomttest.Contains(p.RoomTestID) && p.Status <= 1).Select(p => p.DivisionShiftID).ToList();
                List<int> lstBagOfTest = db.BAGOFTESTS.Where(p => lstDivShift.Contains(p.DivisionShiftID)).Select(p => p.BagOfTestID).ToList();
                List<int> lstTestID = db.TEST_DETAILS.Where(p => p.QuestionID == questionID).Select(p => p.TestID).Distinct().ToList();
                List<TEST> lstTest = db.TESTS.Where(p => lstBagOfTest.Contains(p.BagOfTestID) && lstTestID.Contains(p.TestID)).ToList();
                int count = 0;
                dgvDeleteTestInfo.DataSource = (from item in lstTest
                                                select new
                                                {
                                                    STT = ++count,
                                                    TestID = item.TestID,
                                                    NumOfQuestion = item.TEST_DETAILS.Count(),
                                                    SubjectName = item.SUBJECT.SubjectName,
                                                    DivisionShiftName = item.BAGOFTEST.DIVISION_SHIFTS.SHIFT.ShiftName
                                                }).ToList();
                lblNumOfTest.Text = "Tìm thấy " + count.ToString() + " đề có câu hỏi " + _QuestionID.ToString();
                lblNumOfTest.Visible = true;
                if (questionID == -1)
                    lblNumOfTest.Visible = false;
            }
            catch
            {
                lblNumOfTest.Visible = false;
            }
        }

        private void HandleSearch()
        {
            try
            {
                QUESTION ques = SearchQuestion(Convert.ToInt32(txtTestID.Text), Convert.ToInt32(txtOrderOfQuestion.Text));
                if (ques == null)
                {
                    MessageBox.Show("Không tìm thấy câu hỏi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _QuestionID = -1;
                    SearchRelateTest(_QuestionID);
                    lblQuestionID.Visible = false;
                    return;
                }
                else
                {
                    _QuestionID = ques.QuestionID;
                    lblQuestionID.Visible = true;
                    lblQuestionID.Text = "Mã câu hỏi: " + _QuestionID.ToString();
                    SearchRelateTest(ques.QuestionID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mã đề và số thứ tự câu hỏi không hợp lệ", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            if (btnSelectAll.Text == "Chọn tất cả")
            {
                for (int i = 0; i < dgvDeleteTestInfo.Rows.Count; i++)
                    dgvDeleteTestInfo.Rows[i].Selected = true;
                btnSelectAll.Text = "Bỏ chọn tất cả";
            }
            else if (btnSelectAll.Text == "Bỏ chọn tất cả")
            {
                for (int i = 0; i < dgvDeleteTestInfo.Rows.Count; i++)
                    dgvDeleteTestInfo.Rows[i].Selected = false;
                btnSelectAll.Text = "Chọn tất cả";
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            HandleSearch();
        }

        private void btnDeleteTest_Click(object sender, EventArgs e)
        {
            using (System.Data.Entity.DbContextTransaction tran = db.Database.BeginTransaction())
            {
                try
                {
                    int count = 0;
                    for (int i = 0; i < dgvDeleteTestInfo.Rows.Count; i++)
                    {
                        if (dgvDeleteTestInfo.Rows[i].Selected == true)
                        {
                            int testid = Convert.ToInt32(dgvDeleteTestInfo.Rows[i].Cells["colTestID"].Value);
                            TEST test = db.TESTS.Where(p => p.TestID == testid).FirstOrDefault();
                            List<TEST_DETAILS> lstTestDetail = db.TEST_DETAILS.Where(p => p.TestID == test.TestID).ToList();
                            db.TEST_DETAILS.RemoveRange(lstTestDetail);
                            db.TESTS.Remove(test);
                            count++;
                        }
                    }
                    db.SaveChanges();
                    tran.Commit();
                    MessageBox.Show("Đã xóa " + count.ToString() + " đề thi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show("Có lỗi trong quá trình xóa đề thi\n0 có đề nào được xóa", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            SearchRelateTest(_QuestionID);
        }

        private void txtTestID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                HandleSearch();
            }
        }

        private void txtOrderOfQuestion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                HandleSearch();
            }
        }
    }
}
