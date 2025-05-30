namespace EXON_EM.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PARTS_SUBJECT_DETAILS
    {
        [Key]
        public int PartSubjectDetailID { get; set; }

        public int? PartSubjectID { get; set; }

        public int? TopicID { get; set; }

        public int? OrderOfTopic { get; set; }

        public int? Status { get; set; }

        public virtual PARTS_SUBJECT PARTS_SUBJECT { get; set; }

        public virtual TOPIC TOPIC { get; set; }
    }
}
