namespace EXON_EM.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class STRUCTURES_SUBJECT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public STRUCTURES_SUBJECT()
        {
            STRUCTURE_SUBJECT_DETAILS = new HashSet<STRUCTURE_SUBJECT_DETAILS>();
        }

        [Key]
        public int StructureID { get; set; }

        public int CreatedDate { get; set; }

        public int Status { get; set; }

        public int SubjectID { get; set; }

        public int CreatedStaffID { get; set; }

        public virtual STAFF STAFF { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STRUCTURE_SUBJECT_DETAILS> STRUCTURE_SUBJECT_DETAILS { get; set; }

        public virtual SUBJECT SUBJECT { get; set; }
    }
}
