using ClosedXML.Excel;
using ExcelDataReader;
using EXON.SubModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EXON.MONITOR.GUI
{
    public partial class frmImportExportTestInfor : Form
    {
        int divisionshiftID;
        public frmImportExportTestInfor(int _divisionshiftID)
        {
            this.divisionshiftID = _divisionshiftID;
            InitializeComponent();
        }

        private void frmImportExportTestInfor_Load(object sender, EventArgs e)
        {

        }
        DataTable dt;
        DataTableCollection tbc;
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog opD = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook | *.xls | Excel Workbook | *.xlsx" })
            {
                if (opD.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = opD.FileName;
                    using (var stream = File.Open(opD.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }

                            });
                            tbc = result.Tables;
                            comboBox1.Items.Clear();
                            foreach (DataTable table in tbc)
                            {
                                comboBox1.Items.Add(table.TableName);
                            }
                        }
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt = tbc[comboBox1.SelectedItem.ToString()];
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (MTAQuizDbContext model1 = new MTAQuizDbContext())
            {
                int number = 0;
                int adj = 0;
                if (dt != null)
                {
                    List<CONTESTANTS_TESTS> ctt = new List<CONTESTANTS_TESTS>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CONTESTANTS_TESTS ct = new CONTESTANTS_TESTS();
                        ct.ContestantShiftID = int.Parse(dt.Rows[i][0].ToString());
                        ct.TestID = int.Parse(dt.Rows[i][1].ToString());
                        ct.Status = 4001;


                        try
                        {
                            var sdf = model1.CONTESTANTS_TESTS.Where(j => j.ContestantShiftID == ct.ContestantShiftID).FirstOrDefault();
                            if (sdf == null)
                            {
                                number++;
                                model1.CONTESTANTS_TESTS.Add(ct);
                            }
                            else
                            {

                                var sd = model1.CONTESTANTS_TESTS.Where(j => j.ContestantShiftID == ct.ContestantShiftID).Where(z => z.TestID != ct.TestID).FirstOrDefault();
                                if (sd != null)
                                {
                                    adj++;
                                    sd.TestID = ct.TestID;

                                }
                            }
                        }
                        catch
                        {

                        }


                    }

                }
                try
                {

                model1.SaveChanges();
                MessageBox.Show("Có " + number.ToString() + " bản ghi thêm vào và "+adj.ToString()+" bản ghi chỉnh sửa", "Nhập dữ liệu thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Vui lòng kiểm tra lại file, cơ sở dữ liệu:" + ex.ToString());
                }
            }
        }
        private DataTable getData()
        {
            SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MTA_QUIZ_1"].ConnectionString);
            conn.Open();
            string query = "select CONTESTANTS_TESTS.ContestantShiftID, CONTESTANTS_TESTS.TestID from CONTESTANTS_TESTS, CONTESTANTS_SHIFTS where CONTESTANTS_TESTS.ContestantShiftID = CONTESTANTS_SHIFTS.ContestantShiftID and DivisionShiftID = "+ divisionshiftID;
            SqlCommand cmd = new SqlCommand(query, conn);

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            conn.Close();
            return dt;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt;
            dt = getData();
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            workbook.Worksheets.Add(dt, "Sheet1");
                            workbook.SaveAs(sfd.FileName);
                            MessageBox.Show("Successful");
                        }
                    }
                    catch { }
                }
            }
        }
    }
}
