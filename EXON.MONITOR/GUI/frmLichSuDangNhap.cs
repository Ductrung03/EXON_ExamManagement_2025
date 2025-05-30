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

                    from vtca in _violationService.GetByConstestshiftID(cs.CONTESTANT.ContestantCode)
                    select new
                    {
                         //ContestantShiftID = cs.ContestantShiftID,
                         STT = STT++,
                         ContestantCode = cs.CONTESTANT.ContestantCode,
                         Time = DatetimeConvert.ConvertUnixToDateTime(vtca.Level),
                         TenMay = vtca.ViolationName,
                         ContestantName = EXON.Common.UserHelper.FromJSONToObject3(vtca.Description).ContestantName,
                         ContestShift = EXON.Common.UserHelper.FromJSONToObject3(vtca.Description).ContestShift,
                        ContestSubject = EXON.Common.UserHelper.FromJSONToObject3(vtca.Description).ContestSubject,
                        RoomTest = EXON.Common.UserHelper.FromJSONToObject3(vtca.Description).RoomTest,

                    }).ToList();
               dataGridView1.DataSource = listContestant;
          }

         
     }
}
