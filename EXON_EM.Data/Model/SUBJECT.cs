namespace EXON_EM.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SUBJECTS")]
    public partial class SUBJECT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SUBJECT()
        {
            CONTESTANTS_SUBJECTS = new HashSet<CONTESTANTS_SUBJECTS>();
            GROUP_SUBJECTS = new HashSet<GROUP_SUBJECTS>();
            MODULES = new HashSet<MODULE>();
            ORIGINAL_TESTS = new HashSet<ORIGINAL_TESTS>();
            PARTS_SUBJECT = new HashSet<PARTS_SUBJECT>();
            REGISTERS_SUBJECTS = new HashSet<REGISTERS_SUBJECTS>();
            SCHEDULES = new HashSet<SCHEDULE>();
            STRUCTURES_SUBJECT = new HashSet<STRUCTURES_SUBJECT>();
            TESTS = new HashSet<TEST>();
            TOPICS = new HashSet<TOPIC>();
        }

        public int SubjectID { get; set; }

        [Required]
        [StringLength(30)]
        public string SubjectCode { get; set; }

        [Required]
        public string SubjectName { get; set; }

        public int Status { get; set; }

        public int DepartmentID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CONTESTANTS_SUBJECTS> CONTESTANTS_SUBJECTS { get; set; }

        public virtual DEPARTMENT DEPARTMENT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GROUP_SUBJECTS> GROUP_SUBJECTS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MODULE> MODULES { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORIGINAL_TESTS> ORIGINAL_TESTS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PARTS_SUBJECT> PARTS_SUBJECT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REGISTERS_SUBJECTS> REGISTERS_SUBJECTS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SCHEDULE> SCHEDULES { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<STRUCTURES_SUBJECT> STRUCTURES_SUBJECT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TEST> TESTS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TOPIC> TOPICS { get; set; }
    }
}
