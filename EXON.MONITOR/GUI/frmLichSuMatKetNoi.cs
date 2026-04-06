using EXON.Common;
using EXON.SubData.Services;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EXON.MONITOR.GUI
{
     public partial class frmLichSuMatKetNoi : MetroForm
     {
          private readonly int _divisionShiftID;
          private readonly int? _contestantShiftID;
          private readonly IViolationService _violationService;
          private List<DisconnectHistoryViewRow> _historyRows;

          public frmLichSuMatKetNoi(int divisionShiftID)
               : this(divisionShiftID, null)
          {
          }

          public frmLichSuMatKetNoi(int divisionShiftID, int? contestantShiftID)
          {
               InitializeComponent();
               _divisionShiftID = divisionShiftID;
               _contestantShiftID = contestantShiftID;
               _violationService = new ViolationService();
               _historyRows = new List<DisconnectHistoryViewRow>();
               Text = contestantShiftID.HasValue ? "Lịch sử mất kết nối thí sinh" : "Lịch sử mất kết nối";
               LoadHistory();
          }

          private void LoadHistory()
          {
               List<DisconnectHistoryRecord> records = _violationService
                    .GetDisconnectHistoryRecords(_divisionShiftID, _contestantShiftID)
                    .ToList();

               _historyRows = records
                    .Select((x, index) => new DisconnectHistoryViewRow
                    {
                         STT = index + 1,
                         ContestantShiftID = x.ContestantShiftID,
                         ContestantCode = x.ContestantCode,
                         ContestantName = x.ContestantName,
                         ComputerName = x.ComputerName,
                         RoomTestName = x.RoomTestName,
                         DetectSource = x.DetectSource,
                         ServerTimeText = x.ServerTimeText,
                         LastResponseTimeText = x.LastResponseTimeText,
                         Note = x.Note,
                         ServerUnixTime = x.ServerUnixTime
                    })
                    .ToList();

               dataGridView1.AutoGenerateColumns = false;
               dataGridView1.DataSource = _historyRows;
               dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
               btnInBaoCao.Enabled = _historyRows.Count > 0;
               btnXuatFile.Enabled = _historyRows.Count > 0;
          }

          private void btnInBaoCao_Click(object sender, EventArgs e)
          {
               frmInLogGianDoan frm = new frmInLogGianDoan(_divisionShiftID, _contestantShiftID);
               frm.ShowDialog();
          }

          private void btnXuatFile_Click(object sender, EventArgs e)
          {
               if (_historyRows.Count == 0)
               {
                    MessageBox.Show("Không có dữ liệu lịch sử mất kết nối để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
               }

               SaveFileDialog saveFileDialog = new SaveFileDialog();
               saveFileDialog.Filter = "CSV (*.csv)|*.csv";
               saveFileDialog.FileName = string.Format("LichSuMatKetNoi_{0}.csv", DateTime.Now.ToString("yyyyMMdd_HHmmss"));
               if (saveFileDialog.ShowDialog() != DialogResult.OK)
               {
                    return;
               }

               using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, new UTF8Encoding(true)))
               {
                    writer.WriteLine("STT;Số báo danh;Thí sinh;Máy thi;Phòng thi;Nguồn phát hiện;Thời gian máy chủ;Lần phản hồi cuối;Ghi chú");
                    foreach (DisconnectHistoryViewRow row in _historyRows)
                    {
                         writer.WriteLine(string.Join(";", new[]
                         {
                              EscapeCsv(row.STT.ToString()),
                              EscapeCsv(row.ContestantCode),
                              EscapeCsv(row.ContestantName),
                              EscapeCsv(row.ComputerName),
                              EscapeCsv(row.RoomTestName),
                              EscapeCsv(row.DetectSource),
                              EscapeCsv(row.ServerTimeText),
                              EscapeCsv(row.LastResponseTimeText),
                              EscapeCsv(row.Note)
                         }));
                    }
               }

               MessageBox.Show("Xuất lịch sử mất kết nối thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
          }

          private static string EscapeCsv(string value)
          {
               string safeValue = value ?? string.Empty;
               return string.Format("\"{0}\"", safeValue.Replace("\"", "\"\""));
          }

          private class DisconnectHistoryViewRow
          {
               public int STT { get; set; }
               public int ContestantShiftID { get; set; }
               public string ContestantCode { get; set; }
               public string ContestantName { get; set; }
               public string ComputerName { get; set; }
               public string RoomTestName { get; set; }
               public string DetectSource { get; set; }
               public string ServerTimeText { get; set; }
               public string LastResponseTimeText { get; set; }
               public string Note { get; set; }
               public int ServerUnixTime { get; set; }
          }
     }
}
