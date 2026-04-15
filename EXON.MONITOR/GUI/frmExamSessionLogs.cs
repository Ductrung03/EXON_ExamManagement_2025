using EXON.SubData.Services;
using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EXON.MONITOR.GUI
{
    public partial class frmExamSessionLogs : MetroForm
    {
        private readonly int _contestantShiftId;
        private readonly string _contestantCode;
        private readonly IExamSessionLogService _examSessionLogService;

        public frmExamSessionLogs(int contestantShiftId, string contestantCode)
        {
            InitializeComponent();
            _contestantShiftId = contestantShiftId;
            _contestantCode = contestantCode;
            _examSessionLogService = new ExamSessionLogService();
        }

        private void frmExamSessionLogs_Load(object sender, EventArgs e)
        {
            LoadLogs();
        }

        private void LoadLogs()
        {
            try
            {
                lblHeader.Text = string.Format("ContestantShiftID: {0} | SBD: {1}", _contestantShiftId, string.IsNullOrEmpty(_contestantCode) ? "(không rõ)" : _contestantCode);

                ExamSessionLogViewData data = _examSessionLogService.GetLatestByContestantShiftId(_contestantShiftId);
                if (data == null || data.Audit == null)
                {
                    string emptyText = "Không tìm thấy logs đã upload trong DB cho thí sinh/ca thi này.";
                    rtbOverview.Text = emptyText;
                    rtbConnection.Text = emptyText;
                    rtbActions.Text = emptyText;
                    rtbSubmit.Text = emptyText;
                    rtbRuntime.Text = emptyText;
                    rtbOthers.Text = emptyText;
                    return;
                }

                rtbOverview.Text = BuildOverviewText(data);
                rtbConnection.Text = GetFileContent(data, "_connection.log");
                rtbActions.Text = GetFileContent(data, "_actions.log");
                rtbSubmit.Text = GetSubmitText(data);
                rtbRuntime.Text = GetRuntimeText(data);
                rtbOthers.Text = BuildOtherFilesText(data);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, "Có lỗi khi tải logs: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EXON.Common.Log.Instance.WriteErrorLog(EXON.MONITOR.Properties.Resources.MSG_LOG_ERROR, string.Format("Load exam session logs failed: {0}", ex));
            }
        }

        private string BuildOverviewText(ExamSessionLogViewData data)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("THÔNG TIN UPLOAD LOGS");
            sb.AppendLine(new string('=', 60));
            sb.AppendLine(string.Format("ContestantShiftID : {0}", data.Audit.ContestantShiftId));
            sb.AppendLine(string.Format("ContestantID      : {0}", data.Audit.ContestantId));
            sb.AppendLine(string.Format("DivisionShiftID   : {0}", data.Audit.DivisionShiftId));
            sb.AppendLine(string.Format("AnswerSheetID     : {0}", data.Audit.AnswerSheetId));
            sb.AppendLine(string.Format("ComputerName      : {0}", data.Audit.ComputerName));
            sb.AppendLine(string.Format("SubmitTimeUnixMs  : {0}", data.Audit.SubmitTimeUnixMs));
            sb.AppendLine(string.Format("SubmitTimeText    : {0}", data.Audit.SubmitTimeText));
            sb.AppendLine(string.Format("TimeWorkedMs      : {0}", data.Audit.TimeWorkedMs));
            sb.AppendLine(string.Format("TimeWorkedText    : {0}", data.Audit.TimeWorkedText));
            sb.AppendLine(string.Format("UploadSource      : {0}", data.Audit.UploadSource));
            sb.AppendLine(string.Format("Audit UpdatedAt   : {0}", data.Audit.UpdatedAt));
            sb.AppendLine();
            if (data.Segment != null)
            {
                sb.AppendLine("SEGMENT GẦN NHẤT");
                sb.AppendLine(new string('-', 60));
                sb.AppendLine(string.Format("SessionSegmentId  : {0}", data.Segment.SessionSegmentId));
                sb.AppendLine(string.Format("SegmentGuid       : {0}", data.Segment.SegmentGuid));
                sb.AppendLine(string.Format("FolderName        : {0}", data.Segment.SessionFolderName));
                sb.AppendLine(string.Format("UploadStatus      : {0}", data.Segment.UploadStatus));
                sb.AppendLine(string.Format("UploadedAt        : {0}", data.Segment.UploadedAt));
                sb.AppendLine(string.Format("RetryCount        : {0}", data.Segment.UploadRetryCount));
                sb.AppendLine(string.Format("LastMessage       : {0}", data.Segment.LastUploadMessage));
                sb.AppendLine();
            }

            sb.AppendLine("DANH SÁCH FILE");
            sb.AppendLine(new string('-', 60));
            foreach (ExamSessionRawFileDto file in data.Files)
            {
                sb.AppendLine(string.Format("- {0} ({1} bytes)", file.FileName, file.FileSize));
            }

            return sb.ToString();
        }

        private string GetFileContent(ExamSessionLogViewData data, string fileNameSuffix)
        {
            ExamSessionRawFileDto file = data.Files.FirstOrDefault(x => !string.IsNullOrEmpty(x.FileName) && x.FileName.EndsWith(fileNameSuffix, StringComparison.OrdinalIgnoreCase));
            return file != null && !string.IsNullOrEmpty(file.FileContent)
                ? file.FileContent
                : "Không có dữ liệu.";
        }

        private string GetSubmitText(ExamSessionLogViewData data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ExamSessionRawFileDto file in data.Files.Where(x => !string.IsNullOrEmpty(x.FileName) && x.FileName.EndsWith(".submitlog", StringComparison.OrdinalIgnoreCase)).OrderBy(x => x.FileName))
            {
                sb.AppendLine("FILE: " + file.FileName);
                sb.AppendLine(new string('-', 60));
                sb.AppendLine(file.FileContent);
                sb.AppendLine();
            }

            if (sb.Length == 0)
            {
                return "Không có dữ liệu.";
            }

            return sb.ToString();
        }

        private string GetRuntimeText(ExamSessionLogViewData data)
        {
            StringBuilder sb = new StringBuilder();
            AppendNamedFile(sb, data, "runtime.state");
            AppendNamedFile(sb, data, "final.result");
            AppendNamedFile(sb, data, "session.info");
            return sb.Length == 0 ? "Không có dữ liệu." : sb.ToString();
        }

        private string BuildOtherFilesText(ExamSessionLogViewData data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ExamSessionRawFileDto file in data.Files.Where(x => IsOtherFile(x.FileName)).OrderBy(x => x.FileName))
            {
                sb.AppendLine("FILE: " + file.FileName);
                sb.AppendLine(new string('-', 60));
                sb.AppendLine(file.FileContent);
                sb.AppendLine();
            }

            return sb.Length == 0 ? "Không có dữ liệu khác." : sb.ToString();
        }

        private void AppendNamedFile(StringBuilder sb, ExamSessionLogViewData data, string fileName)
        {
            ExamSessionRawFileDto file = data.Files.FirstOrDefault(x => string.Equals(x.FileName, fileName, StringComparison.OrdinalIgnoreCase));
            if (file == null)
            {
                return;
            }

            sb.AppendLine("FILE: " + file.FileName);
            sb.AppendLine(new string('-', 60));
            sb.AppendLine(file.FileContent);
            sb.AppendLine();
        }

        private bool IsOtherFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            string lower = fileName.ToLowerInvariant();
            return !lower.EndsWith("_connection.log")
                && !lower.EndsWith("_actions.log")
                && !lower.EndsWith(".submitlog")
                && lower != "runtime.state"
                && lower != "final.result"
                && lower != "session.info";
        }
    }
}
