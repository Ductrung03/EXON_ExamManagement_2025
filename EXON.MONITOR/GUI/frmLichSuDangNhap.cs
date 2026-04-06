using EXON.MONITOR.Common;
using MetroFramework.Forms;
using EXON.SubData.Services;
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
using EXON.Common;

namespace EXON.MONITOR.GUI
{
     public partial class frmLichSuDangNhap : MetroForm
     {
          private IViolationContestantService _violationContestantService;
          private IViolationService _violationService;
          public frmLichSuDangNhap(CONTESTANTS_SHIFTS cs)
          {
               InitializeComponent();
               int STT = 1;
               _violationContestantService = new ViolationContestantService();
               _violationService = new ViolationService();

                var listContestant = (

                    from vtca in _violationService.GetLoginHistoryRecords(cs.DivisionShiftID, cs.CONTESTANT.ContestantCode, cs.ContestantShiftID)
                    select new
                    {
                          //ContestantShiftID = cs.ContestantShiftID,
                         STT = STT++,
                          ContestantCode = cs.CONTESTANT.ContestantCode,
                          Time = vtca.EventTime == DateTime.MinValue ? DatetimeConvert.GetDateTimeServer() : vtca.EventTime,
                          TenMay = string.IsNullOrWhiteSpace(vtca.ComputerName) ? vtca.EventType : vtca.ComputerName,
                          ContestantName = string.IsNullOrWhiteSpace(vtca.ContestantName) ? cs.CONTESTANT.FullName : vtca.ContestantName,
                          ContestShift = vtca.ContestShift,
                        ContestSubject = vtca.ContestSubject,
                        RoomTest = vtca.RoomTest,

                     }).ToList();
                dataGridView1.DataSource = listContestant;
          }

         
     }
}
