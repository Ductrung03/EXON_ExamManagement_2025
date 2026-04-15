using EXON.SubModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EXON.SubData.Services
{
    public interface IExamSessionLogService
    {
        ExamSessionLogViewData GetLatestByContestantShiftId(int contestantShiftId);
    }

    public class ExamSessionLogAuditDto
    {
        public long SessionAuditId { get; set; }
        public long ContestantShiftId { get; set; }
        public long? ContestantId { get; set; }
        public long? DivisionShiftId { get; set; }
        public long? AnswerSheetId { get; set; }
        public string ComputerName { get; set; }
        public long? SubmitTimeUnixMs { get; set; }
        public string SubmitTimeText { get; set; }
        public long? TimeWorkedMs { get; set; }
        public string TimeWorkedText { get; set; }
        public string UploadSource { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class ExamSessionLogSegmentDto
    {
        public long SessionSegmentId { get; set; }
        public long SessionAuditId { get; set; }
        public string SegmentGuid { get; set; }
        public long ContestantShiftId { get; set; }
        public string ComputerName { get; set; }
        public string SessionFolderName { get; set; }
        public string UploadStatus { get; set; }
        public DateTime? UploadedAt { get; set; }
        public int UploadRetryCount { get; set; }
        public string LastUploadMessage { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class ExamSessionRawFileDto
    {
        public long RawFileId { get; set; }
        public long SessionSegmentId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileContent { get; set; }
        public long FileSize { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class ExamSessionLogViewData
    {
        public ExamSessionLogAuditDto Audit { get; set; }
        public ExamSessionLogSegmentDto Segment { get; set; }
        public List<ExamSessionRawFileDto> Files { get; set; }

        public ExamSessionLogViewData()
        {
            Files = new List<ExamSessionRawFileDto>();
        }
    }

    public class ExamSessionLogService : IExamSessionLogService
    {
        public ExamSessionLogViewData GetLatestByContestantShiftId(int contestantShiftId)
        {
            MTAQuizDbContext db = new MTAQuizDbContext();
            ExamSessionLogViewData result = new ExamSessionLogViewData();

            string auditSql = @"
SELECT TOP 1
    SessionAuditId,
    ContestantShiftId,
    ContestantId,
    DivisionShiftId,
    AnswerSheetId,
    ComputerName,
    SubmitTimeUnixMs,
    SubmitTimeText,
    TimeWorkedMs,
    TimeWorkedText,
    UploadSource,
    CreatedAt,
    UpdatedAt
FROM EXAM_SESSION_AUDIT
WHERE ContestantShiftId = @p0
ORDER BY UpdatedAt DESC, CreatedAt DESC";

            result.Audit = db.Database.SqlQuery<ExamSessionLogAuditDto>(auditSql, contestantShiftId).FirstOrDefault();
            if (result.Audit == null)
            {
                return result;
            }

            string segmentSql = @"
SELECT TOP 1
    SessionSegmentId,
    SessionAuditId,
    SegmentGuid,
    ContestantShiftId,
    ComputerName,
    SessionFolderName,
    UploadStatus,
    UploadedAt,
    UploadRetryCount,
    LastUploadMessage,
    CreatedAt
FROM EXAM_SESSION_SEGMENT
WHERE SessionAuditId = @p0
ORDER BY UploadedAt DESC, CreatedAt DESC, SessionSegmentId DESC";

            result.Segment = db.Database.SqlQuery<ExamSessionLogSegmentDto>(segmentSql, result.Audit.SessionAuditId).FirstOrDefault();
            if (result.Segment == null)
            {
                return result;
            }

            string filesSql = @"
SELECT
    RawFileId,
    SessionSegmentId,
    FileName,
    FileType,
    FileContent,
    FileSize,
    CreatedAt
FROM EXAM_SESSION_RAW_FILE
WHERE SessionSegmentId = @p0
ORDER BY FileName, RawFileId";

            result.Files = db.Database.SqlQuery<ExamSessionRawFileDto>(filesSql, result.Segment.SessionSegmentId).ToList();
            return result;
        }
    }
}
