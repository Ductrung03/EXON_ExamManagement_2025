namespace EXON_EM.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GROUP_SUBJECTS
    {
        [Key]
        public int Group_SubjectID { get; set; }

        public int? GroupID { get; set; }

        public int? SubjectID { get; set; }

        public int? Ordinal { get; set; }

        public virtual GROUP GROUP { get; set; }

        public virtual SUBJECT SUBJECT { get; set; }
    }
}
