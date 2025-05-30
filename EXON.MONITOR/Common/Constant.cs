using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Environment;

namespace EXON.MONITOR.Common
{
    public class Constanst
    {
        // Các Level trong [VIOLATIONS]
        public static int LEVEL_LOGIN = 8001; // Đăng nhập
        public static int LEVEL_INTERRUPT = 8002; // gián đoạn
        public static int LEVEL_CHANGECMP = 8003; // đổi máy khi thi
        public static int LEVEL_CHANGEAWS = 8004; // đổi câu trả lời
        public static int LEVEL_ADDTIME = 8005; // bù giờ
    }
}
