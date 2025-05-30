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
using EXON.SubModel.Models;
using EXON.MONITOR.Common;
using EXON.Common;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MetroFramework.Forms;
using EXON.MONITOR.Report;

using System.Web.Script.Serialization;
using EXON.SubData.Services;


namespace EXON.MONITOR.GUI
{
     public partial class frmInLogGianDoan : MetroForm
     {
          private VIOLATION dv = new VIOLATION();
          private MTAQuizDbContext db;
          public BindingSource source;
          private IDivisionShiftService _DivisionShiftService;
          public int divisionShiftID;
          public frmInLogGianDoan(VIOLATION ds, int _dvsID)
          {
               InitializeComponent();
               DIVISION_SHIFTS db = new DIVISION_SHIFTS();
               _DivisionShiftService = new DivisionShiftService();
               db = _DivisionShiftService.GetById(_dvsID);
               divisionShiftID = _dvsID;
               txtKyThi.Text = db.ROOMTEST.LOCATION.CONTEST.ContestName.ToUpper();
               txtCaThi.Text = db.SHIFT.ShiftName.ToUpper();
            btnBaoCao.Enabled = false;
          }



          public VIOLATION Ds { get; set; }
          public frmInLogGianDoan()
          {
               InitializeComponent();
          }

          private void frmInLogGianDoan_Load(object sender, EventArgs e)
          {

               db = new MTAQuizDbContext();
               DataTable dt = new DataTable();
               dt.Columns.Add("STT");
               dt.Columns.Add("THISINH");
               dt.Columns.Add("SBD");
               dt.Columns.Add("TIMEGD");
               dt.Columns.Add("NOTE");
               dt.Columns.Add("LV");

               int i = 1;
               //List<VIOLATION> listviolation = db.VIOLATIONS.Where(x => x.Level == 8005).ToList();
               var dvID = divisionShiftID.ToString();
               List<VIOLATION> listviolation = db.VIOLATIONS.Where(x => x.ViolationName == dvID).ToList();
               var logviolation = (
               from lt in listviolation.ToList()
               select new
               {
                    STT = i++,
                    THISINH = (string)JObject.Parse(lt.Description)["nameContestant"],
                    SBD = (string)JObject.Parse(lt.Description)["code"],
                    //TIMEGD = (string)JObject.Parse(lt.Description)["ContestantRealPauseTime"], 
                    TIMEGD = DatetimeConvert.ConvertUnixToDateTime((int)JObject.Parse(lt.Description)["ContestantRealPauseTime"]),
                    NOTE = (string)JObject.Parse(lt.Description)["Note"],
                    TIME = (string)JObject.Parse(lt.Description)["Time"],
                    LV = lt.Level.ToString()
               }
               ).ToList();


               dGVGianDoan.DataSource = logviolation;
               dGVGianDoan.Columns[0].HeaderText = "STT";
               dGVGianDoan.Columns[1].HeaderText = "Thí sinh";
               dGVGianDoan.Columns[2].HeaderText = "Số báo danh";
               dGVGianDoan.Columns[3].HeaderText = "Thời gian";
               dGVGianDoan.Columns[4].HeaderText = "Lý do";
               dGVGianDoan.Columns[5].HeaderText = "Thời gian bù";
               dGVGianDoan.Columns[6].HeaderText = "Loại gián đoạn";

               source = new BindingSource(logviolation, null);
               this.dGVGianDoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
          }

          private void btnBaoCao_Click(object sender, EventArgs e)
          {


               FrmRpLogViolations frm = new FrmRpLogViolations(divisionShiftID, source);
               frm.ShowDialog();
          }
     }
}
