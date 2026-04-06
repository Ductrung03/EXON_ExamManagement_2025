using EXON.SubData.Services;
using EXON.SubModel.Models;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using EXON.MONITOR.Report;

namespace EXON.MONITOR.GUI
{
     public partial class frmInLogGianDoan : MetroForm
     {
          private readonly bool _isDisconnectHistory;
          private readonly IViolationService _violationService;
          private readonly IDivisionShiftService _divisionShiftService;
          private readonly int? _contestantShiftID;
          public BindingSource source;
          public int divisionShiftID;

          public frmInLogGianDoan(VIOLATION ds, int dvsID)
               : this(dvsID, null, false)
          {
          }

          public frmInLogGianDoan(int dvsID)
               : this(dvsID, null, false)
          {
          }

          public frmInLogGianDoan(int dvsID, int? contestantShiftID)
               : this(dvsID, contestantShiftID, true)
          {
          }

          public frmInLogGianDoan(int dvsID, int? contestantShiftID, bool isDisconnectHistory)
          {
               InitializeComponent();
                divisionShiftID = dvsID;
                _contestantShiftID = contestantShiftID;
                _isDisconnectHistory = isDisconnectHistory;
                _violationService = new ViolationService();
                _divisionShiftService = new DivisionShiftService();
                source = new BindingSource();
                btnBaoCao.Enabled = false;
                 Text = _isDisconnectHistory
                    ? contestantShiftID.HasValue ? "In lịch sử mất kết nối thí sinh" : "In lịch sử mất kết nối"
                    : "In lịch sử gián đoạn, bù giờ";
                LoadDivisionShiftInfo();
          }

          public VIOLATION Ds { get; set; }

          public frmInLogGianDoan()
          {
               InitializeComponent();
               _violationService = new ViolationService();
               _divisionShiftService = new DivisionShiftService();
               source = new BindingSource();
          }

          private void LoadDivisionShiftInfo()
          {
               DIVISION_SHIFTS divisionShift = _divisionShiftService.GetById(divisionShiftID);
               if (divisionShift == null)
               {
                    return;
               }

               txtKyThi.Text = divisionShift.ROOMTEST.LOCATION.CONTEST.ContestName.ToUpper();
               txtCaThi.Text = divisionShift.SHIFT.ShiftName.ToUpper();
          }

          private void frmInLogGianDoan_Load(object sender, EventArgs e)
          {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("STT", typeof(int));
                dataTable.Columns.Add("THISINH", typeof(string));
               dataTable.Columns.Add("SBD", typeof(string));
               dataTable.Columns.Add("TIMEGD", typeof(string));
                dataTable.Columns.Add("NOTE", typeof(string));
                dataTable.Columns.Add("LV", typeof(string));

                if (_isDisconnectHistory)
                {
                    LoadDisconnectHistory(dataTable);
                }
                else
                {
                    LoadAddTimeHistory(dataTable);
                }

               dGVGianDoan.DataSource = dataTable;
               dGVGianDoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
               dGVGianDoan.Columns[0].HeaderText = "STT";
               dGVGianDoan.Columns[1].HeaderText = "Thí sinh";
               dGVGianDoan.Columns[2].HeaderText = "Số báo danh";
                dGVGianDoan.Columns[3].HeaderText = _isDisconnectHistory ? "Thời điểm mất kết nối" : "Thời gian";
                dGVGianDoan.Columns[4].HeaderText = "Ghi chú";
                dGVGianDoan.Columns[5].HeaderText = _isDisconnectHistory ? "Nguồn phát hiện" : "Loại lịch sử";

                source = new BindingSource(dataTable, null);
                btnBaoCao.Enabled = dataTable.Rows.Count > 0;
          }

          private void LoadDisconnectHistory(DataTable dataTable)
          {
                List<DisconnectHistoryRecord> records = _violationService
                     .GetDisconnectHistoryRecords(divisionShiftID, _contestantShiftID)
                     .ToList();

                for (int i = 0; i < records.Count; i++)
                {
                     DisconnectHistoryRecord record = records[i];
                     dataTable.Rows.Add(
                          i + 1,
                          record.ContestantName,
                          record.ContestantCode,
                          record.ServerTimeText,
                          BuildReportNote(record),
                          record.DetectSource);
                }
          }

          private void LoadAddTimeHistory(DataTable dataTable)
          {
                List<AddTimeHistoryRecord> violations = _violationService
                    .GetAddTimeHistoryRecords(divisionShiftID, _contestantShiftID)
                    .OrderByDescending(x => x.ViolationID)
                    .ToList();

                 for (int i = 0; i < violations.Count; i++)
                 {
                    AddTimeHistoryRecord record = violations[i];

                    dataTable.Rows.Add(
                        dataTable.Rows.Count + 1,
                        record.ContestantName,
                        record.ContestantCode,
                        record.AddedMinutesText,
                        record.Note,
                        record.HistoryType);
                }
          }

          private static string BuildReportNote(DisconnectHistoryRecord record)
          {
               if (string.IsNullOrWhiteSpace(record.LastResponseTimeText) || record.LastResponseTimeText == "Chưa có dữ liệu")
               {
                    return record.Note;
               }

                return string.Format("{0} (Lần phản hồi cuối: {1})", record.Note, record.LastResponseTimeText);
          }

          private void btnBaoCao_Click(object sender, EventArgs e)
          {
               FrmRpLogViolations frm = new FrmRpLogViolations(divisionShiftID, source);
               frm.ShowDialog();
          }
     }
}
