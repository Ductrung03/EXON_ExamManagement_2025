using EXON.SubData.Infrastructures;
using EXON.SubData.Repositories;
using EXON.SubModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXON.Common;
using Newtonsoft.Json.Linq;
namespace EXON.SubData.Services
{
    public interface IViolationService
    {
        VIOLATION Add(VIOLATION _VIOLATION);

        void Update(VIOLATION _VIOLATION);

        VIOLATION Delete(int id);

        IEnumerable<VIOLATION> GetAll();
        IEnumerable<VIOLATION> GetByConstestshiftID(string contestantCodeID);
        IEnumerable<VIOLATION> GetByDivisionShiftAndLevel(int divisionShiftID, int level);
        IEnumerable<DisconnectHistoryRecord> GetDisconnectHistoryRecords(int divisionShiftID, int? contestantShiftID = null);
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

    public class ViolationService : IViolationService
    {
        private const string DisconnectEventName = "SYS_EVT::CONTESTANT_DISCONNECTED";
        private const int LegacyInterruptLevel = 8002;
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
                    VIOLATION c;
                    UserLoginComputerDifferent b;
                    c = all[i];
                    b = UserHelper.FromJSONToObject3(all[i].Description);
                    if (b.ContestantCode == contestantCodeID)
                    {
                        a.Add(c.ViolationID);
                    }
                }
                catch { }
                
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
            if (violation == null || string.IsNullOrWhiteSpace(violation.Description))
            {
                return false;
            }

            try
            {
                JObject data = JObject.Parse(violation.Description);
                bool isNewDisconnectEvent = string.Equals(violation.ViolationName, DisconnectEventName, StringComparison.OrdinalIgnoreCase);
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
                    ServerTimeText = ReadString(data, "ServerTime", "ServerTimeText", "Time"),
                    ServerUnixTime = ReadInt(data, "ServerUnix", "ServerUnixTime"),
                    LastResponseTimeText = ReadString(data, "LastResponseTime", "LastResponseTimeText"),
                    LastResponseUnixTime = ReadInt(data, "LastResponseUnix", "LastResponseUnixTime", "ContestantRealPauseTime"),
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
