using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXON.SubModel.Models
{
    public class CHANGECOMPUTER_LOG
    {
        public int ContestID { get; set; }
        public int ShiftID { get; set; }
        public int RoomContest { get; set; }
        public string ProjectName { get; set; }
        public int ContestantID { get; set; }
        public string ContestantCode { get; set; }
        public string ContestantName { get; set; }
        public string oldComputer { get; set; }
        public string newComputer { get; set; }
        public string time_changecmp { get; set; }

    }
}
