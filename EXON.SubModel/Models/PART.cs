namespace EXON.SubModel.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PARTS")]
    public partial class PART
    {
        public int PartID { get; set; }

        public int? CreatedDate { get; set; }

        public int? Status { get; set; }

        public int? ScheduleID { get; set; }

        public int? CreateStaffID { get; set; }

        public string Name { get; set; }

        public int? OrderOfQuestion { get; set; }

        public int? OrderInTest { get; set; }

        public int? Shuffle { get; set; }

        public virtual SCHEDULE SCHEDULE { get; set; }

        public virtual STAFF STAFF { get; set; }
    }
}
