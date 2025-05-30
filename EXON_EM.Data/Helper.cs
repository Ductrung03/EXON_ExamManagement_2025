using EXON.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXON_EM.Data
{
    public static class Helper
    {
        public static int ConvertDateTimeToUnix(DateTime dt)
        {
            return Convert.ToInt32((dt.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds);
        }

        public static DateTime ConvertUnixToDateTime(int unix)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return dt.AddSeconds(unix);
        }

        public static string TrangThaiKiThi(int status)
        {
            if (status == (int)ContestStatus.New) return "Mới tạo và chưa phê duyệt";
            if (status == (int)ContestStatus.Accepted) return "Đã phê duyệt và chưa đăng ký";
            if (status == (int)ContestStatus.Cancel) return "Đã hủy";
            if (status == (int)ContestStatus.ContestDone) return "Đã hoàn thành";
            if (status == (int)ContestStatus.RegisterDone) return "Đã đăng ký xong";
            if (status == (int)ContestStatus.GenerateOriginalDone) return "Đã Sinh đề gốc";
            if (status == (int)ContestStatus.ScheduleShiftDone) return "Đã xếp lịch và chưa làm đề";
            if (status == (int)ContestStatus.GenereateTestDone) return "Đã sinh đề và chưa chyển CSDL";
            if (status == (int)ContestStatus.PrepareTestDone) return "Đã chuyển CSDL và đang thi";
            return "";
        }
    }
}
