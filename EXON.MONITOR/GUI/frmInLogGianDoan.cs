using EXON.SubData.Services;
using EXON.SubModel.Models;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using EXON.MONITOR.Report;
using Newtonsoft.Json.Linq;

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
                    : "In lịch sử bù giờ";
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
               dGVGianDoan.Columns[3].HeaderText = "Thời điểm mất kết nối";
               dGVGianDoan.Columns[4].HeaderText = "Ghi chú";
               dGVGianDoan.Columns[5].HeaderText = "Nguồn phát hiện";

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
                List<VIOLATION> violations = _violationService
                    .GetByDivisionShiftAndLevel(divisionShiftID, Common.Constanst.LEVEL_ADDTIME)
                    .OrderByDescending(x => x.ViolationID)
                    .ToList();

                for (int i = 0; i < violations.Count; i++)
                {
                    AddTimeHistoryRecord record;
                    if (!TryCreateAddTimeHistoryRecord(violations[i], out record))
                    {
                        continue;
                    }

                    dataTable.Rows.Add(
                        dataTable.Rows.Count + 1,
                        record.ContestantName,
                        record.ContestantCode,
                        record.AddedMinutesText,
                        record.Note,
                        "Bù giờ");
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

          private static bool TryCreateAddTimeHistoryRecord(VIOLATION violation, out AddTimeHistoryRecord record)
          {
              record = null;
              if (violation == null || string.IsNullOrWhiteSpace(violation.Description))
              {
                  return false;
              }

              try
              {
                  JObject data = JObject.Parse(violation.Description);
                  string lastResponseTime = ReadString(data, "LastResponseTime");
                  string note = ReadString(data, "Note");
                  if (!string.IsNullOrWhiteSpace(lastResponseTime))
                  {
                      note = string.IsNullOrWhiteSpace(note)
                          ? string.Format("Lần phản hồi cuối: {0}", lastResponseTime)
                          : string.Format("{0} (Lần phản hồi cuối: {1})", note, lastResponseTime);
                  }

                  record = new AddTimeHistoryRecord
                  {
                      ContestantName = ReadString(data, "nameContestant"),
                      ContestantCode = ReadString(data, "code"),
                      AddedMinutesText = string.Format("{0} phút", ReadInt(data, "Time")),
                      Note = note
                  };

                  return !string.IsNullOrWhiteSpace(record.ContestantCode) || !string.IsNullOrWhiteSpace(record.ContestantName);
              }
              catch
              {
                  return false;
              }
          }

          private static string ReadString(JObject data, params string[] keys)
          {
              foreach (string key in keys)
              {
                  JToken token = data[key];
                  if (token != null && token.Type != JTokenType.Null)
                  {
                      return token.ToString();
                  }
              }

              return string.Empty;
          }

          private static int ReadInt(JObject data, params string[] keys)
          {
              foreach (string key in keys)
              {
                  JToken token = data[key];
                  if (token == null || token.Type == JTokenType.Null)
                  {
                      continue;
                  }

                  int value;
                  if (int.TryParse(token.ToString(), out value))
                  {
                      return value;
                  }
              }

              return 0;
          }

          private class AddTimeHistoryRecord
          {
              public string ContestantName { get; set; }
              public string ContestantCode { get; set; }
              public string AddedMinutesText { get; set; }
              public string Note { get; set; }
          }

          private void btnBaoCao_Click(object sender, EventArgs e)
          {
               FrmRpLogViolations frm = new FrmRpLogViolations(divisionShiftID, source);
               frm.ShowDialog();
          }
     }
}
