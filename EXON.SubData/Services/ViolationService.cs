using EXON.SubData.Infrastructures;
using EXON.SubData.Repositories;
using EXON.SubModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXON.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace EXON.SubData.Services
{
    public interface IViolationService
    {
        VIOLATION Add(VIOLATION _VIOLATION);
        VIOLATION AddMonitorViolation(MonitorViolationEventData eventData, int level, int status);

        void Update(VIOLATION _VIOLATION);

        VIOLATION Delete(int id);

        IEnumerable<VIOLATION> GetAll();
        IEnumerable<VIOLATION> GetByConstestshiftID(string contestantCodeID);
        IEnumerable<VIOLATION> GetByDivisionShiftAndLevel(int divisionShiftID, int level);
        IEnumerable<AddTimeHistoryRecord> GetAddTimeHistoryRecords(int divisionShiftID, int? contestantShiftID = null);
        IEnumerable<CHANGECOMPUTER_LOG> GetChangeComputerHistoryRecords(int divisionShiftID, int? contestantShiftID = null);
        IEnumerable<DisconnectHistoryRecord> GetDisconnectHistoryRecords(int divisionShiftID, int? contestantShiftID = null);
        IEnumerable<LoginHistoryRecord> GetLoginHistoryRecords(int divisionShiftID, string contestantCode, int? contestantShiftID = null);
        VIOLATION GetById(int id);
        int GetNextViolationId();

        void Save();
    }

    public class DisconnectHistoryRecord
    {
        public int ViolationID { get; set; }
        public string EventType { get; set; }
        public int ContestantShiftID { get; set; }
        public int ContestantID { get; set; }
        public string ContestantCode { get; set; }
        public string ContestantName { get; set; }
        public int DivisionShiftID { get; set; }
        public string ComputerName { get; set; }
        public int RoomDiagramID { get; set; }
        public int RoomTestID { get; set; }
        public string RoomTestName { get; set; }
        public string DetectSource { get; set; }
        public int Status { get; set; }
        public string ServerTimeText { get; set; }
        public int ServerUnixTime { get; set; }
        public string LastResponseTimeText { get; set; }
        public int LastResponseUnixTime { get; set; }
        public string Note { get; set; }
        public int Level { get; set; }
    }

    public class AddTimeHistoryRecord
    {
        public int ViolationID { get; set; }
        public string EventType { get; set; }
        public int ContestantShiftID { get; set; }
        public int ContestantID { get; set; }
        public string ContestantCode { get; set; }
        public string ContestantName { get; set; }
        public int DivisionShiftID { get; set; }
        public int AddedMinutes { get; set; }
        public string AddedMinutesText { get; set; }
        public int PauseUnixTime { get; set; }
        public int ServerUnixTime { get; set; }
        public string ServerTimeText { get; set; }
        public string LastResponseTimeText { get; set; }
        public string Note { get; set; }
        public string HistoryType { get; set; }
    }

    public class LoginHistoryRecord
    {
        public int ViolationID { get; set; }
        public string EventType { get; set; }
        public int ContestantShiftID { get; set; }
        public string ContestantCode { get; set; }
        public string ContestantName { get; set; }
        public string ComputerName { get; set; }
        public string ContestShift { get; set; }
        public string ContestSubject { get; set; }
        public string RoomTest { get; set; }
        public int DivisionShiftID { get; set; }
        public int ServerUnixTime { get; set; }
        public DateTime EventTime { get; set; }
    }

    public class MonitorViolationEventData
    {
        public string SchemaVersion { get; set; }
        public string EventType { get; set; }
        public int ContestID { get; set; }
        public string ContestName { get; set; }
        public int ShiftID { get; set; }
        public string ShiftName { get; set; }
        public int DivisionShiftId { get; set; }
        public int ContestantShiftId { get; set; }
        public int ContestantId { get; set; }
        public string ContestantCode { get; set; }
        public string ContestantName { get; set; }
        public int RoomTestId { get; set; }
        public string RoomTestName { get; set; }
        public int RoomDiagramId { get; set; }
        public string ComputerName { get; set; }
        public string SubjectName { get; set; }
        public int ServerUnixTime { get; set; }
        public string ServerTimeText { get; set; }
        public int LastResponseUnixTime { get; set; }
        public string LastResponseTimeText { get; set; }
        public int PauseUnixTime { get; set; }
        public int AddedMinutes { get; set; }
        public string DetectSource { get; set; }
        public string OldComputerName { get; set; }
        public string NewComputerName { get; set; }
        public string Note { get; set; }
    }

    public class ViolationService : IViolationService
    {
        private const int LegacyInterruptLevel = 8002;
        private const int LegacyAddTimeLevel = 8005;
        private ViolationRepository _ViolationRepository;
        private IUnitOfWork _unitOfWork;
        private IDbFactory dbFactory;

        public ViolationService()
        {
            dbFactory = new DbFactory();
            this._ViolationRepository = new ViolationRepository(dbFactory);
            this._unitOfWork = new UnitOfWork(dbFactory);
        }

        public VIOLATION Add(VIOLATION _VIOLATION)
        {
            return _ViolationRepository.Add(_VIOLATION);
        }

        public VIOLATION AddMonitorViolation(MonitorViolationEventData eventData, int level, int status)
        {
            MonitorViolationEventData normalizedEventData = NormalizeMonitorViolationEventData(eventData);
            VIOLATION violation = new VIOLATION
            {
                ViolationID = GetNextViolationId(),
                ViolationName = normalizedEventData.EventType,
                Level = level,
                Status = status,
                Description = JObject.FromObject(normalizedEventData).ToString(Formatting.None)
            };

            return Add(violation);
        }

        public VIOLATION Delete(int id)
        {
            return _ViolationRepository.Delete(id);
        }

        public IEnumerable<VIOLATION> GetAll()
        {
            return _ViolationRepository.GetAll();
        }

        public VIOLATION GetById(int id)
        {
            return _ViolationRepository.GetSingleById(id);
        }

        public IEnumerable<VIOLATION> GetByDivisionShiftAndLevel(int divisionShiftID, int level)
        {
            string violationName = divisionShiftID.ToString();
            return _ViolationRepository.GetMulti(x => x.ViolationName == violationName && x.Level == level);
        }

        public IEnumerable<AddTimeHistoryRecord> GetAddTimeHistoryRecords(int divisionShiftID, int? contestantShiftID = null)
        {
            List<AddTimeHistoryRecord> result = new List<AddTimeHistoryRecord>();
            List<VIOLATION> violations = _ViolationRepository.GetAll().OrderByDescending(x => x.ViolationID).ToList();

            foreach (VIOLATION violation in violations)
            {
                AddTimeHistoryRecord record;
                if (!TryCreateAddTimeHistoryRecord(violation, divisionShiftID, out record))
                {
                    continue;
                }

                if (contestantShiftID.HasValue && record.ContestantShiftID > 0 && record.ContestantShiftID != contestantShiftID.Value)
                {
                    continue;
                }

                result.Add(record);
            }

            return result.OrderByDescending(x => x.ServerUnixTime).ThenByDescending(x => x.ViolationID);
        }

        public IEnumerable<CHANGECOMPUTER_LOG> GetChangeComputerHistoryRecords(int divisionShiftID, int? contestantShiftID = null)
        {
            List<CHANGECOMPUTER_LOG> result = new List<CHANGECOMPUTER_LOG>();
            List<VIOLATION> violations = _ViolationRepository.GetAll().OrderByDescending(x => x.ViolationID).ToList();

            foreach (VIOLATION violation in violations)
            {
                CHANGECOMPUTER_LOG record;
                if (!TryCreateChangeComputerHistoryRecord(violation, divisionShiftID, out record))
                {
                    continue;
                }

                if (contestantShiftID.HasValue && record.ContestantID > 0 && record.ContestantID != contestantShiftID.Value)
                {
                    continue;
                }

                result.Add(record);
            }

            return result;
        }

        public IEnumerable<DisconnectHistoryRecord> GetDisconnectHistoryRecords(int divisionShiftID, int? contestantShiftID = null)
        {
            List<DisconnectHistoryRecord> result = new List<DisconnectHistoryRecord>();
            List<VIOLATION> violations = _ViolationRepository.GetAll().OrderByDescending(x => x.ViolationID).ToList();

            foreach (VIOLATION violation in violations)
            {
                DisconnectHistoryRecord record;
                if (!TryCreateDisconnectHistoryRecord(violation, divisionShiftID, out record))
                {
                    continue;
                }

                if (contestantShiftID.HasValue && record.ContestantShiftID != contestantShiftID.Value)
                {
                    continue;
                }

                result.Add(record);
            }

            return result.OrderByDescending(x => x.ServerUnixTime).ThenByDescending(x => x.ViolationID);
        }

        public IEnumerable<LoginHistoryRecord> GetLoginHistoryRecords(int divisionShiftID, string contestantCode, int? contestantShiftID = null)
        {
            List<LoginHistoryRecord> result = new List<LoginHistoryRecord>();
            List<VIOLATION> violations = _ViolationRepository.GetAll().OrderByDescending(x => x.ViolationID).ToList();

            foreach (VIOLATION violation in violations)
            {
                LoginHistoryRecord record;
                if (!TryCreateLoginHistoryRecord(violation, divisionShiftID, contestantCode, out record))
                {
                    continue;
                }

                if (contestantShiftID.HasValue && record.ContestantShiftID > 0 && record.ContestantShiftID != contestantShiftID.Value)
                {
                    continue;
                }

                result.Add(record);
            }

            return result.OrderByDescending(x => x.ServerUnixTime).ThenByDescending(x => x.ViolationID);
        }

        public int GetNextViolationId()
        {
            VIOLATION lastViolation = _ViolationRepository.GetAll().OrderByDescending(x => x.ViolationID).FirstOrDefault();
            return lastViolation == null ? 1 : lastViolation.ViolationID + 1;
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(VIOLATION _VIOLATION)
        {
            _ViolationRepository.Update(_VIOLATION);
        }
        
          public IEnumerable<VIOLATION> GetByConstestshiftID(string contestantCodeID)
          {
            List<int> a = new List<int>();
            List<VIOLATION> d = new List<VIOLATION>();
            var all = _ViolationRepository.GetAll().ToList();
            for (int i = 0; i < all.Count; i++)
            {
                try
                {
                    string contestantCode = ReadContestantCodeSafe(all[i]);
                    if (string.Equals(contestantCode, contestantCodeID, StringComparison.OrdinalIgnoreCase))
                    {
                        a.Add(all[i].ViolationID);
                    }
                }
                catch
                {
                }
                
            }
            foreach (var i in a)
            {
                _ViolationRepository.GetSingleById(i);
                d.Add(_ViolationRepository.GetSingleById(i));
            }
             return d;
          }

        private bool TryCreateDisconnectHistoryRecord(VIOLATION violation, int divisionShiftID, out DisconnectHistoryRecord record)
        {
            record = null;
            JObject data;
            if (!TryParseDescription(violation, out data))
            {
                return false;
            }

            try
            {
                bool isNewDisconnectEvent = IsMonitorEvent(violation, data, Constant.VIOLATION_EVENT_DISCONNECT);
                bool isLegacyDisconnectEvent = violation.Level == LegacyInterruptLevel
                    && string.Equals(violation.ViolationName, divisionShiftID.ToString(), StringComparison.OrdinalIgnoreCase);

                if (!isNewDisconnectEvent && !isLegacyDisconnectEvent)
                {
                    return false;
                }

                int payloadDivisionShiftId = ReadInt(data, "DivisionShiftId", "DivisionShiftID");
                if (payloadDivisionShiftId <= 0)
                {
                    payloadDivisionShiftId = isLegacyDisconnectEvent ? divisionShiftID : 0;
                }

                if (payloadDivisionShiftId != divisionShiftID)
                {
                    return false;
                }

                record = new DisconnectHistoryRecord
                {
                    ViolationID = violation.ViolationID,
                    EventType = ReadString(data, "EventType") ?? violation.ViolationName ?? string.Empty,
                    ContestantShiftID = ReadInt(data, "ContestantShiftId", "ContestantShiftID"),
                    ContestantID = ReadInt(data, "ContestantId", "ContestantID"),
                    ContestantCode = ReadString(data, "ContestantCode", "code"),
                    ContestantName = ReadString(data, "ContestantName", "nameContestant"),
                    DivisionShiftID = payloadDivisionShiftId,
                    ComputerName = ReadString(data, "ComputerName"),
                    RoomDiagramID = ReadInt(data, "RoomDiagramId", "RoomDiagramID"),
                    RoomTestID = ReadInt(data, "RoomTestId", "RoomTestID"),
                    RoomTestName = ReadString(data, "RoomTestName"),
                    DetectSource = ReadString(data, "DetectSource", "DetectionSource"),
                    Status = ReadInt(data, "Status"),
                    ServerTimeText = ReadString(data, "ServerTimeText", "ServerTime", "Time"),
                    ServerUnixTime = ReadInt(data, "ServerUnixTime", "ServerUnix"),
                    LastResponseTimeText = ReadString(data, "LastResponseTimeText", "LastResponseTime"),
                    LastResponseUnixTime = ReadInt(data, "LastResponseUnixTime", "LastResponseUnix", "ContestantRealPauseTime"),
                    Note = ReadString(data, "Note"),
                    Level = violation.Level
                };

                if (string.IsNullOrWhiteSpace(record.DetectSource) && isLegacyDisconnectEvent)
                {
                    record.DetectSource = violation.Status == Constant.STATUS_DOING_BUT_INTERRUPT ? "STATUS_3004" : string.Empty;
                }

                if (string.IsNullOrWhiteSpace(record.ServerTimeText) && record.ServerUnixTime > 0)
                {
                    record.ServerTimeText = ConvertUnixToDateTime(record.ServerUnixTime).ToString("dd-MM-yyyy HH:mm:ss");
                }

                if (string.IsNullOrWhiteSpace(record.LastResponseTimeText) && record.LastResponseUnixTime > 0)
                {
                    record.LastResponseTimeText = ConvertUnixToDateTime(record.LastResponseUnixTime).ToString("HH:mm:ss");
                }

                if (record.ServerUnixTime <= 0)
                {
                    record.ServerUnixTime = violation.ViolationID;
                }

                return record.ContestantShiftID > 0 || !string.IsNullOrWhiteSpace(record.ContestantCode);
            }
            catch
            {
                return false;
            }
        }

        private bool TryCreateAddTimeHistoryRecord(VIOLATION violation, int divisionShiftID, out AddTimeHistoryRecord record)
        {
            record = null;
            JObject data;
            if (!TryParseDescription(violation, out data))
            {
                return false;
            }

            bool isNewAddTimeEvent = IsMonitorEvent(violation, data, Constant.VIOLATION_EVENT_ADDTIME);
            bool isNewInterruptEvent = IsMonitorEvent(violation, data, Constant.VIOLATION_EVENT_INTERRUPT);
            bool isLegacyAddTimeEvent = violation.Level == LegacyAddTimeLevel
                && string.Equals(violation.ViolationName, divisionShiftID.ToString(), StringComparison.OrdinalIgnoreCase);

            if (!isNewAddTimeEvent && !isNewInterruptEvent && !isLegacyAddTimeEvent)
            {
                return false;
            }

            int payloadDivisionShiftId = ReadInt(data, "DivisionShiftId", "DivisionShiftID");
            if (payloadDivisionShiftId <= 0)
            {
                payloadDivisionShiftId = isLegacyAddTimeEvent ? divisionShiftID : 0;
            }

            if (payloadDivisionShiftId != divisionShiftID)
            {
                return false;
            }

            int addedMinutes = ReadInt(data, "AddedMinutes", "Time");
            string lastResponseTime = ReadString(data, "LastResponseTimeText", "LastResponseTime");
            string note = ReadString(data, "Note");
            if (!string.IsNullOrWhiteSpace(lastResponseTime))
            {
                note = string.IsNullOrWhiteSpace(note)
                    ? string.Format("Lần phản hồi cuối: {0}", lastResponseTime)
                    : string.Format("{0} (Lần phản hồi cuối: {1})", note, lastResponseTime);
            }

            record = new AddTimeHistoryRecord
            {
                ViolationID = violation.ViolationID,
                EventType = ReadString(data, "EventType"),
                ContestantShiftID = ReadInt(data, "ContestantShiftId", "ContestantShiftID"),
                ContestantID = ReadInt(data, "ContestantId", "ContestantID"),
                ContestantCode = ReadString(data, "ContestantCode", "code"),
                ContestantName = ReadString(data, "ContestantName", "nameContestant"),
                DivisionShiftID = payloadDivisionShiftId,
                AddedMinutes = addedMinutes,
                AddedMinutesText = string.Format("{0} phút", addedMinutes),
                PauseUnixTime = ReadInt(data, "PauseUnixTime", "ContestantRealPauseTime"),
                ServerUnixTime = ReadInt(data, "ServerUnixTime", "ServerUnix"),
                ServerTimeText = ReadString(data, "ServerTimeText", "ServerTime"),
                LastResponseTimeText = lastResponseTime,
                Note = note,
                HistoryType = isNewInterruptEvent ? "Gián đoạn" : "Bù giờ"
            };

            if (record.ServerUnixTime <= 0)
            {
                record.ServerUnixTime = violation.ViolationID;
            }

            return !string.IsNullOrWhiteSpace(record.ContestantCode) || !string.IsNullOrWhiteSpace(record.ContestantName);
        }

        private bool TryCreateChangeComputerHistoryRecord(VIOLATION violation, int divisionShiftID, out CHANGECOMPUTER_LOG record)
        {
            record = null;
            JObject data;
            if (!TryParseDescription(violation, out data))
            {
                return false;
            }

            bool isNewChangeComputerEvent = IsMonitorEvent(violation, data, Constant.VIOLATION_EVENT_CHANGE_COMPUTER);
            bool isLegacyChangeComputerEvent = string.Equals(violation.ViolationName, divisionShiftID.ToString(), StringComparison.OrdinalIgnoreCase)
                && (!string.IsNullOrWhiteSpace(ReadString(data, "oldComputer")) || !string.IsNullOrWhiteSpace(ReadString(data, "newComputer")));

            if (!isNewChangeComputerEvent && !isLegacyChangeComputerEvent)
            {
                return false;
            }

            int payloadDivisionShiftId = ReadInt(data, "DivisionShiftId", "DivisionShiftID");
            if (payloadDivisionShiftId <= 0)
            {
                payloadDivisionShiftId = isLegacyChangeComputerEvent ? divisionShiftID : 0;
            }

            if (payloadDivisionShiftId != divisionShiftID)
            {
                return false;
            }

            record = new CHANGECOMPUTER_LOG
            {
                ContestID = ReadInt(data, "ContestID", "ContestId"),
                ShiftID = ReadInt(data, "ShiftID", "ShiftId"),
                RoomContest = ReadInt(data, "RoomContest", "RoomTestId", "RoomTestID"),
                ProjectName = ReadString(data, "SubjectName", "ProjectName", "ContestSubject"),
                ContestantID = ReadInt(data, "ContestantShiftId", "ContestantShiftID", "ContestantID", "ContestantId"),
                ContestantCode = ReadString(data, "ContestantCode", "code"),
                ContestantName = ReadString(data, "ContestantName", "nameContestant"),
                oldComputer = ReadString(data, "OldComputerName", "oldComputer"),
                newComputer = ReadString(data, "NewComputerName", "newComputer", "ComputerName"),
                time_changecmp = ReadString(data, "ServerTimeText", "ServerTime", "time_changecmp")
            };

            return !string.IsNullOrWhiteSpace(record.ContestantCode)
                || !string.IsNullOrWhiteSpace(record.oldComputer)
                || !string.IsNullOrWhiteSpace(record.newComputer);
        }

        private bool TryCreateLoginHistoryRecord(VIOLATION violation, int divisionShiftID, string contestantCode, out LoginHistoryRecord record)
        {
            record = null;
            JObject data;
            if (!TryParseDescription(violation, out data))
            {
                return false;
            }

            string payloadContestantCode = ReadString(data, "ContestantCode", "code");
            if (!string.Equals(payloadContestantCode, contestantCode, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            bool isNewLoginEvent = IsMonitorEvent(violation, data, Constant.VIOLATION_EVENT_LOGIN);
            bool isLegacyLoginEvent = violation.Level == Constant.LEVEL_LOGIN
                || !string.IsNullOrWhiteSpace(ReadString(data, "ContestShift", "ShiftName", "nameShift"))
                || !string.IsNullOrWhiteSpace(ReadString(data, "ContestSubject", "SubjectName", "ProjectName"))
                || !string.IsNullOrWhiteSpace(ReadString(data, "RoomTest", "RoomTestName"));

            if (!isNewLoginEvent && !isLegacyLoginEvent)
            {
                return false;
            }

            int payloadDivisionShiftId = ReadInt(data, "DivisionShiftId", "DivisionShiftID");
            if (payloadDivisionShiftId > 0 && payloadDivisionShiftId != divisionShiftID)
            {
                return false;
            }

            int serverUnixTime = ReadInt(data, "ServerUnixTime", "ServerUnix");
            if (serverUnixTime <= 0)
            {
                serverUnixTime = violation.Level;
            }

            record = new LoginHistoryRecord
            {
                ViolationID = violation.ViolationID,
                EventType = ReadString(data, "EventType"),
                ContestantShiftID = ReadInt(data, "ContestantShiftId", "ContestantShiftID"),
                ContestantCode = payloadContestantCode,
                ContestantName = ReadString(data, "ContestantName", "nameContestant"),
                ComputerName = ReadString(data, "ComputerName", "TenMay"),
                ContestShift = ReadString(data, "ShiftName", "ContestShift", "nameShift"),
                ContestSubject = ReadString(data, "SubjectName", "ContestSubject", "ProjectName"),
                RoomTest = ReadString(data, "RoomTestName", "RoomTest"),
                DivisionShiftID = payloadDivisionShiftId,
                ServerUnixTime = serverUnixTime,
                EventTime = serverUnixTime > 0 ? ConvertUnixToDateTime(serverUnixTime) : DateTime.MinValue
            };

            return true;
        }

        private static MonitorViolationEventData NormalizeMonitorViolationEventData(MonitorViolationEventData eventData)
        {
            MonitorViolationEventData normalizedEventData = eventData ?? new MonitorViolationEventData();
            normalizedEventData.SchemaVersion = string.IsNullOrWhiteSpace(normalizedEventData.SchemaVersion)
                ? Constant.VIOLATION_SCHEMA_MONITOR_EVENT_V1
                : normalizedEventData.SchemaVersion;

            if (normalizedEventData.ServerUnixTime > 0 && string.IsNullOrWhiteSpace(normalizedEventData.ServerTimeText))
            {
                normalizedEventData.ServerTimeText = ConvertUnixToDateTime(normalizedEventData.ServerUnixTime).ToString("dd-MM-yyyy HH:mm:ss");
            }

            if (normalizedEventData.LastResponseUnixTime > 0 && string.IsNullOrWhiteSpace(normalizedEventData.LastResponseTimeText))
            {
                normalizedEventData.LastResponseTimeText = ConvertUnixToDateTime(normalizedEventData.LastResponseUnixTime).ToString("HH:mm:ss");
            }

            return normalizedEventData;
        }

        private static bool TryParseDescription(VIOLATION violation, out JObject data)
        {
            data = null;
            if (violation == null || string.IsNullOrWhiteSpace(violation.Description))
            {
                return false;
            }

            try
            {
                data = JObject.Parse(violation.Description);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsMonitorEvent(VIOLATION violation, JObject data, string eventType)
        {
            string payloadEventType = ReadString(data, "EventType");
            return string.Equals(payloadEventType, eventType, StringComparison.OrdinalIgnoreCase)
                || string.Equals(violation.ViolationName, eventType, StringComparison.OrdinalIgnoreCase);
        }

        private static string ReadContestantCodeSafe(VIOLATION violation)
        {
            JObject data;
            if (!TryParseDescription(violation, out data))
            {
                return string.Empty;
            }

            return ReadString(data, "ContestantCode", "code");
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

        private static string ReadString(JObject data, params string[] keys)
        {
            foreach (string key in keys)
            {
                JToken token = data[key];
                if (token == null || token.Type == JTokenType.Null)
                {
                    continue;
                }

                string value = token.ToString();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }

            return string.Empty;
        }

        private static DateTime ConvertUnixToDateTime(int unixTime)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime).ToLocalTime();
        }
    }
    }
