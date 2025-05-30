using EXON.MONITOR.Common;
using EXON.SubData.Services;
using EXON.SubModel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EXON.MONITOR.GUI
{
    public partial class frmChangeTimeBegin : Form
    {
        public static int ConvertDateTimeToUnix(DateTime dt)
        {
            
            return Convert.ToInt32((dt.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds);
        }
        int divisionShiftID;
        int shiftID;
        int timeForDS;
        ShiftService _ShiftService = new ShiftService();
        ExaminationcouncilStaffService _ExaminationcouncilStaffService = new ExaminationcouncilStaffService();
        DivisionShiftService _DivisionShiftService = new DivisionShiftService();
        DIVISION_SHIFTS dvs;
        SHIFT shf;



        public frmChangeTimeBegin(int _divisionShiftID, int _shiftID)
        {
            InitializeComponent();
            divisionShiftID = _divisionShiftID;
            shiftID = _shiftID;

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void frmChangeTimeBegin_Load(object sender, EventArgs e)
        {
            


            dvs = _DivisionShiftService.GetById(divisionShiftID);
            shf = _ShiftService.GetById(shiftID);

            int logTime = AppSession.LogTime;
            int TimeNow = (int)EXON.SubModel.GetDateTimeServer.GetDateTime().TimeOfDay.TotalSeconds;
            timeForDS = (dvs.EndTime ?? default(int)) - (dvs.StartTime ?? default(int));
            label1.Text = "Kỳ thi " + shf.ShiftName;
            //shift = _ShiftService.GetShiftNow(TimeNow, logTime, DIF_TIME, supervisorID);
            label8.Text = "Ngày: "+ Common.DatetimeConvert.ConvertUnixToDateTime(dvs.StartDate ?? default(int)).ToString("dd/MM/yyyy");
            label4.Text = DatetimeConvert.ConvertUnixToDateTime(dvs.StartTime ?? default(int)).ToString("HH:mm:ss"); ;
            label5.Text = DatetimeConvert.ConvertUnixToDateTime(dvs.EndTime ?? default(int)).ToString("HH:mm:ss"); ;
            numericUpDown1.Value = decimal.Parse( DatetimeConvert.ConvertUnixToDateTime(dvs.StartTime ?? default(int)).ToString("HH")) ;
            numericUpDown2.Value = decimal.Parse( DatetimeConvert.ConvertUnixToDateTime(dvs.StartTime ?? default(int)).ToString("mm")) ;
        }
        private void calculateTime()
        {
            int endTime = (int)numericUpDown1.Value * 60 * 60 + (int)numericUpDown2.Value * 60 + timeForDS;
            label11.Text = DatetimeConvert.ConvertUnixToDateTime(endTime).ToString("HH:mm:ss");
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                calculateTime();
            }
            catch { }
            
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                calculateTime();
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int startTime = (int)numericUpDown1.Value * 60 * 60 + (int)numericUpDown2.Value * 60;
                int endTime = startTime + timeForDS;
                DateTime newdate = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, 0, 0, 0, 0);
                int date = ConvertDateTimeToUnix(newdate);

                dvs.StartTime = startTime;
                dvs.EndTime = endTime;
                dvs.StartDate = date;
                shf.StartTime = startTime;
                shf.EndTime = endTime;
                shf.ShiftDate = date;
                _DivisionShiftService.Save();
                _ShiftService.Save();
                MessageBox.Show("Thay đổi thành công thời gian ca thi");
                this.Dispose();
            }
            catch { }
            
        }
    }
}
