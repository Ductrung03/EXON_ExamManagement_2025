namespace EXON_EM.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PARTS")]
    public partial class PART
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PART()
        {
            PARTS_DETAILS = new HashSet<PARTS_DETAILS>();
        }

        public int PartID { get; set; }

        public int? CreatedDate { get; set; }

        public int? Status { get; set; }

        public int? ScheduleID { get; set; }

        public int? CreateStaffID { get; set; }

        public string Name { get; set; }

        public int? OrderOfQuestion { get; set; }

        public int? OrderInTest { get; set; }

        public int? Shuffle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PARTS_DETAILS> PARTS_DETAILS { get; set; }

        public virtual SCHEDULE SCHEDULE { get; set; }

        public virtual STAFF STAFF { get; set; }
    }
}
