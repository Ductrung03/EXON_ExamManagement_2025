namespace EXON_EM.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PARTS_SUBJECT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PARTS_SUBJECT()
        {
            PARTS_SUBJECT_DETAILS = new HashSet<PARTS_SUBJECT_DETAILS>();
        }

        [Key]
        public int PartSubjectID { get; set; }

        public int? CreateDate { get; set; }

        public int? Status { get; set; }

        public int? SubjectID { get; set; }

        public int? CreateStaffID { get; set; }

        public string Name { get; set; }

        public int? OrderOfQuestion { get; set; }

        public int? OrderInTest { get; set; }

        public int? Shuffle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PARTS_SUBJECT_DETAILS> PARTS_SUBJECT_DETAILS { get; set; }

        public virtual STAFF STAFF { get; set; }

        public virtual SUBJECT SUBJECT { get; set; }
    }
}
