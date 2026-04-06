using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EXON.SubModel.Models;
using EXON.SubData.Services;
using EXON.MONITOR.Common;
using EXON.Common;
using Newtonsoft.Json;

namespace EXON.MONITOR.Control
{
    public partial class ucComputer : UserControl
    {
        //public bool CheckedConfirm;
        public bool CheckedConfirmtoload = false;
        public bool ChangedStatus = false;
        CONTESTANT _contestant = new CONTESTANT();
        CONTESTANTS_SHIFTS _contestantshift = new CONTESTANTS_SHIFTS();
        ROOMTEST _roomtest = new ROOMTEST();
        ROOMDIAGRAM _roomdiagram = new ROOMDIAGRAM();
        public int contestantid;
        public int contestanshifttid;
        private IDivisionShiftService _DivisionShiftService;
        private IContestantShiftService _ContestantShiftService;
        private IRoomDiagramService _RoomDiagramService;
        private IContestantService _ContestantService;
        private IRoomTestService _RoomTestService;
        int _divisionshiftid;
        int _roomdiagramid =0;
        int _roomtestid;
        int _contesttansid;
        public int status;
        public string ComputerName;
        private int _previousStatus=1;
        private bool _disconnectActive = false;
        public string current_com_name;


        public ucComputer(ROOMDIAGRAM roomdia, int divisionShiftID)
        {
            InitializeComponent();
            _divisionshiftid = divisionShiftID;
            _roomdiagramid = roomdia.RoomDiagramID;                 
            _ContestantShiftService = new ContestantShiftService();
            _RoomDiagramService = new RoomDiagramService();
            string fullnameCom = roomdia.ComputerName;
            ComputerName = roomdia.ComputerName;

            if (roomdia.Status == 4002)
            {
                ptbImage.Image = EXON.MONITOR.Properties.Resources.monitor_hong;
                lbComputername.Text = fullnameCom;
                lbComputername.ForeColor = Color.Red;

            }
            else if (roomdia.Status == 4003)
            {
                ptbImage.Image = EXON.MONITOR.Properties.Resources.monitor_dubi;
                lbComputername.Text = fullnameCom;
                lbComputername.ForeColor = Color.Yellow;
            }
            else
            {
                lbComputername.Text = fullnameCom;
            }

            // this.po.Y = _y;
        }
        public ucComputer(CONTESTANTS_SHIFTS cONTESTANTS_SHIFTS , int divisionShiftID)
        {
               InitializeComponent();
               _divisionshiftid = divisionShiftID;
               _contestantshift = cONTESTANTS_SHIFTS;
               _contesttansid = cONTESTANTS_SHIFTS.ContestantID;
               if(cONTESTANTS_SHIFTS.RoomDiagramID != null) { _roomdiagramid = int.Parse(cONTESTANTS_SHIFTS.RoomDiagramID.ToString()); }
               _RoomDiagramService = new RoomDiagramService();
               _ContestantShiftService = new ContestantShiftService();
               _ContestantService = new ContestantService();
               if (_roomdiagramid != 0)
               {
                    _roomdiagram = new ROOMDIAGRAM();
                    _roomdiagram = _RoomDiagramService.GetById(_roomdiagramid);
                     string fullnameCom = _roomdiagram.ComputerName;
                     ComputerName = _roomdiagram.ComputerName;

                    if (_roomdiagram.Status == 4002)
                    {
                         ptbImage.Image = EXON.MONITOR.Properties.Resources.monitor_hong;
                         lbComputername.Text = fullnameCom;
                         current_com_name = lbComputername.Text;
                         lbComputername.ForeColor = Color.Red;

                    }
                    else if (_roomdiagram.Status == 4003)
                    {
                         ptbImage.Image = EXON.MONITOR.Properties.Resources.monitor_dubi;
                         lbComputername.Text = fullnameCom;
                         current_com_name = lbComputername.Text;
                         lbComputername.ForeColor = Color.Yellow;
                    }
                    else
                    {
                         ptbImage.Image = EXON.MONITOR.Properties.Resources.monitor;
                         lbComputername.Text = fullnameCom;
                         current_com_name = lbComputername.Text;
                    }
               }
               else
               current_com_name = null;
        }

        CONTESTANTS_SHIFTS GetContestantShiftByComName(int divisionshiftID, int comid)
        {
            CONTESTANTS_SHIFTS result = new CONTESTANTS_SHIFTS();
            MTAQuizDbContext Db = new MTAQuizDbContext();
            try
            {

                result = (from obj in Db.CONTESTANTS_SHIFTS
                          where obj.DivisionShiftID == divisionshiftID && obj.RoomDiagramID == comid
                          select obj).FirstOrDefault();
                return result;

            }
            catch(Exception ex) {
                Log.Instance.WriteErrorLog(Properties.Resources.MSG_LOG_ERROR, string.Format("Expetion : {0}  ", ex.Message));

                return new CONTESTANTS_SHIFTS();
            }
        }
        CONTESTANTS_SHIFTS GetContestantShiftByConName(int divisionshiftID, int conid)
        {
               CONTESTANTS_SHIFTS result = new CONTESTANTS_SHIFTS();
               MTAQuizDbContext Db = new MTAQuizDbContext();
               try
               {

                    result = (from obj in Db.CONTESTANTS_SHIFTS
                              where obj.DivisionShiftID == divisionshiftID && obj.ContestantID == conid
                              select obj).FirstOrDefault();
                    return result;

               }
               catch (Exception ex)
               {
                    Log.Instance.WriteErrorLog(Properties.Resources.MSG_LOG_ERROR, string.Format("Expetion : {0}  ", ex.Message));

                    return new CONTESTANTS_SHIFTS();
               }
        }
        CONTESTANT GetInfoContestant(int contestantID)
        {
            CONTESTANT result = new CONTESTANT();
            MTAQuizDbContext Db = new MTAQuizDbContext();

            try
            {
                result = Db.CONTESTANTS.Where(x => x.ContestantID == contestantID).FirstOrDefault();
                return result;
            }
            catch
            {
                return new CONTESTANT();
            }

        }
          
        ROOMDIAGRAM GetInfoComputer(int roomtestID)
        {
               ROOMDIAGRAM result = new ROOMDIAGRAM();
               MTAQuizDbContext Db = new MTAQuizDbContext();

               try
               {
                    result = Db.ROOMDIAGRAMS.Where(x => x.RoomTestID == roomtestID).FirstOrDefault();
                    return result;
               }
               catch
               {
                    return new ROOMDIAGRAM();
               }

        }
        delegate void SetTextCallback(string text, Color color);
        private void SetText(string text, Color color)
        {

            if (this.lbStatus.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text, color });
            }
            else
            {
                lbStatus.Text = text;
                lbStatus.BackColor = color;
            }
        }
        public void LoadInfoContestant()
        {
               
               _contestantshift = new CONTESTANTS_SHIFTS();               
               _contestantshift = GetContestantShiftByConName(_divisionshiftid,_contesttansid);
               if (_contestantshift != null)
               {
                    status = _contestantshift.Status;
                    contestantid = _contestantshift.ContestantID;
                    contestanshifttid = _contestantshift.ContestantShiftID;
                    _contestant = new CONTESTANT();
                _contestant = GetInfoContestant(_contestantshift.ContestantID);

                lbContestantCode.Text = _contestant.ContestantCode;
                lbContestantName.Text = _contestant.FullName;
                    #region status
                    string statusStr = "";
                    bool isDisconnected = false;
                    string detectSource = string.Empty;
                    if (_contestantshift.Status == Constant.STATUS_DOING_BUT_INTERRUPT)
                    {
                         isDisconnected = true;
                         detectSource = "STATUS_3004";
                    }
                    else if (_contestantshift.Status == Constant.STATUS_DOING && _contestantshift.TimeCheck.HasValue)
                    {
                         DateTime serverTime = DatetimeConvert.GetDateTimeServer();
                         int nowUnix = DatetimeConvert.ConvertDateTimeToUnix(serverTime);
                         int delta = nowUnix - _contestantshift.TimeCheck.Value;
                         if (delta > 10)
                         {
                              isDisconnected = true;
                              detectSource = "TIMECHECK_TIMEOUT";
                         }
                    }

                    //if (_contestantshift.IsCheckFingerprint == 1 || _contestantshift.IsCheckFingerprint == 2 || _contestantshift.Status != 4001 )
                    if (_contestantshift.Status != 4001)
                    {
                         //ptbImage.Image = EXON.MONITOR.Properties.Resources.monitor;
                         //   cBCheckFP.Checked = true;
                         this.BackColor = Color.White;
                    }
                    else
                    {
                         //  cBCheckFP.Checked = false;
                         this.BackColor = Color.Gray;
                         ptbImage.Image = EXON.MONITOR.Properties.Resources.monitor;
                     }
                     Color color = new Color();
                     if (isDisconnected)
                     {
                         statusStr = "Mất kết nối";
                         color = Color.Fuchsia;
                         if (!_disconnectActive && _previousStatus > 1)
                         {
                              EXON.Common.NotificationBox.Show(String.Format("Thí sinh tại máy {0} mất kết nối", GetDisconnectComputerName()), EXON.Common.NotificationBox.AlertType.error);
                              SaveDisconnectViolation(detectSource);
                         }

                         _disconnectActive = true;
                         _previousStatus = Constant.STATUS_DOING_BUT_INTERRUPT;
                     }
                     else
                     {
                         _disconnectActive = false;
                         _previousStatus = _contestantshift.Status;
                         switch (_contestantshift.Status)
                         {
                              case 3000:
                                   statusStr = "Đăng nhập";
                                   color = Color.SpringGreen;
                                   if (!ChangedStatus)
                                   {
                                        ChangedStatus = true;
                                        CheckedConfirmtoload = true;
                                   }

                                   break;
                              case 3001:
                                   statusStr = "Đăng nhập lại ";
                                   color = Color.GreenYellow;
                                   break;
                              case 3002:
                                   statusStr = "Sẵn sàng thi";
                                   color = Color.DeepSkyBlue;
                                   break;
                              case 3003:
                                   statusStr = "Đang thi";
                                   color = Color.DodgerBlue;
                                   break;
                              case 3005:
                                   statusStr = "Hoàn thành thi";
                                   color = Color.Turquoise;
                                   break;

                              case 3009:
                                   statusStr = "Bắt đầu thi";
                                   break;
                              case 3010:
                                   statusStr = "Sẵn sàng nhận đề";
                                   color = Color.SpringGreen;
                                   if (!ChangedStatus)
                                   {
                                        ChangedStatus = true;
                                        CheckedConfirmtoload = true;
                                   }
                                   break;

                              case 3011:
                                   statusStr = "Phát đề";
                                   break;
                              case 4001:
                                   statusStr = "Chưa đăng nhập";
                                   color = Color.Yellow;
                                   break;
                              case 5000:
                                   statusStr = "Tạm ngừng";
                                   color = Color.Yellow;
                                   break;
                         }
                     }
                     SetText(statusStr, color);

                    //this.BackColor = color;
                    #endregion
               }
               else
               {
                    lbContestantName.Text = "Không có thí sinh";
                    this.BackColor = Color.Gray;
                    ptbImage.Image = EXON.MONITOR.Properties.Resources.monitor_khongcothisinh;
                    lbContestantCode.Text = "SBD";
                lbStatus.Text = "Trạng thái";
                lbStatus.BackColor = Color.Gray;
                    _disconnectActive = false;
                }

         }

         private string GetDisconnectComputerName()
         {
                if (!string.IsNullOrWhiteSpace(ComputerName))
                {
                     return ComputerName;
                }

                if (_roomdiagram != null && !string.IsNullOrWhiteSpace(_roomdiagram.ComputerName))
                {
                     return _roomdiagram.ComputerName;
                }

                return lbComputername.Text;
         }

         private void SaveDisconnectViolation(string detectSource)
         {
                if (_contestantshift == null || _contestantshift.ContestantShiftID <= 0)
                {
                     return;
                }

                try
                {
                     ViolationService violationService = new ViolationService();
                     DateTime serverTime = DatetimeConvert.GetDateTimeServer();
                     int unixTime = DatetimeConvert.ConvertDateTimeToUnix(serverTime);
                     string computerName = GetDisconnectComputerName();
                     string lastResponseTime = GetTimeCheckDisplayText(_contestantshift.TimeCheck);

                     var payload = new
                     {
                          EventType = "SYS_EVT::CONTESTANT_DISCONNECTED",
                          ContestantShiftId = _contestantshift.ContestantShiftID,
                          ContestantId = _contestantshift.ContestantID,
                          ContestantCode = _contestant != null ? _contestant.ContestantCode : string.Empty,
                          ContestantName = _contestant != null ? _contestant.FullName : string.Empty,
                          DivisionShiftId = _divisionshiftid,
                          ComputerName = computerName,
                          RoomDiagramId = _roomdiagram != null ? _roomdiagram.RoomDiagramID : 0,
                          RoomTestId = _roomdiagram != null ? _roomdiagram.RoomTestID : 0,
                          Status = _contestantshift.Status,
                          DetectSource = detectSource,
                          ServerTime = serverTime.ToString("dd-MM-yyyy HH:mm:ss"),
                          ServerUnix = unixTime,
                          LastResponseTime = lastResponseTime,
                          LastResponseUnix = _contestantshift.TimeCheck ?? 0,
                          Note = "Tự động phát hiện thí sinh mất kết nối"
                     };

                     VIOLATION violation = new VIOLATION();
                     violation.ViolationID = violationService.GetNextViolationId();
                     violation.ViolationName = "SYS_EVT::CONTESTANT_DISCONNECTED";
                     violation.Level = 0;
                     violation.Status = _contestantshift.Status;
                     violation.Description = JsonConvert.SerializeObject(payload);

                     violationService.Add(violation);
                     violationService.Save();
                }
                catch
                {
                }
         }

         private string GetTimeCheckDisplayText(int? timeCheck)
         {
                if (!timeCheck.HasValue || timeCheck.Value <= 0)
                {
                     return "Chưa có dữ liệu";
                }

                return DatetimeConvert.ConvertUnixToDateTime(timeCheck.Value).ToString("HH:mm:ss");
         }

       
        public void LoadInfoContestantByContestantID(int _divisionShiftId, int _ContestantId)
        {
            _contestantshift = new CONTESTANTS_SHIFTS();
            _contestantshift = _ContestantShiftService.GetByContestantID(_divisionshiftid, _ContestantId);

            if (_contestantshift != null)
            {
                contestantid = _contestantshift.ContestantID;
                contestanshifttid = _contestantshift.ContestantShiftID;
                _contestant = new CONTESTANT();
                _contestant = GetInfoContestant(_contestantshift.ContestantID);
                lbContestantCode.Text = _contestant.ContestantCode;
                lbContestantName.Text = _contestant.FullName;

                #region status;
                if (_contestantshift.IsCheckFingerprint == 1 || _contestantshift.IsCheckFingerprint == 2)
                {
                    ptbImage.Image = Properties.Resources.monitor;

                    this.BackColor = Color.White;
                }
                else
                {

                    this.BackColor = Color.Gray;
                    ptbImage.Image = Properties.Resources.monitor_khongcothisinh;
                }

                #endregion
            }
            else
            {
                lbContestantName.Text = "Không có thí sinh";
                this.BackColor = Color.Gray;
            }

        }
                    
        private void ucComputer_Load(object sender, EventArgs e)
        {
               LoadInfoContestant();
               
        }
         
        public event EventHandler RightClick;

        public event EventHandler ImageClick;
        protected void ptbImage_Click(object sender, EventArgs e)
        {
            // bubble the event up to the parent
            if (this.ImageClick != null)
                this.ImageClick(this, e);
        }





        private void ptbImage_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.RightClick != null)
                    this.RightClick(this, e);
            }
        }

         
     }
}
