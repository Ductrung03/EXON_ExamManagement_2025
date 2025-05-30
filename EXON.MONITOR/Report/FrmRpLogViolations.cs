using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EXON.SubData.Services;
using EXON.SubModel.Models;
using Microsoft.Reporting.WinForms;

namespace EXON.MONITOR.Report
{
    public partial class FrmRpLogViolations : Form
    {
        private IDivisionShiftService _DivisionShiftService;
        private BindingSource _source;
        private int _DivisionShiftID;
        public FrmRpLogViolations(int divisionShiftID, BindingSource source)
        {
            InitializeComponent();
            _DivisionShiftID = divisionShiftID;
            _source = source;
        }

        private void FrmRpLogViolations_Load(object sender, EventArgs e)
        {
            DIVISION_SHIFTS ds = new DIVISION_SHIFTS();
            _DivisionShiftService = new DivisionShiftService();
            ds = _DivisionShiftService.GetById(_DivisionShiftID);
            string date = Common.DatetimeConvert.GetDateTimeServer().ToString(@"\n\g\à\y dd \t\h\á\n\g MM \n\ă\m yyyy");
            ReportParameter[] listPara = new ReportParameter[]{

                  new ReportParameter("RoomTestName",ds.ROOMTEST.RoomTestName.ToLower()),
                  new ReportParameter("ContestName",ds.ROOMTEST.LOCATION.CONTEST.ContestName.ToUpper()),
                  new ReportParameter("LocationName",ds.ROOMTEST.LOCATION.LocationName.ToUpper()),

                  new ReportParameter("StartTime",Common.DatetimeConvert.ConvertUnixToDateTime(ds.SHIFT.StartTime).ToString("HH: mm:ss")),
                  new ReportParameter("EndTime",Common.DatetimeConvert.ConvertUnixToDateTime(ds.SHIFT.EndTime).ToString("HH: mm:ss")),
                  new ReportParameter("NgayThi",Common.DatetimeConvert.ConvertUnixToDateTime(ds.SHIFT.ShiftDate).ToString("dd/MM/yyyy")),
                  new ReportParameter("Date",date),
            };
            ReportDataSource PrintViolations = new ReportDataSource("PrintViolations", _source);
            this.rpVInLog.LocalReport.DataSources.Clear();
            this.rpVInLog.LocalReport.DataSources.Add(PrintViolations);
            this.rpVInLog.LocalReport.SetParameters(listPara);
            this.rpVInLog.LocalReport.Refresh();
            this.rpVInLog.RefreshReport();
        }
    }
}
