namespace EXON_EM.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class STRUCTURE_SUBJECT_DETAILS
    {
        [Key]
        public int StructureDetailID { get; set; }

        public int NumberQuestions { get; set; }

        public int Level { get; set; }

        public double Score { get; set; }

        public int Status { get; set; }

        public int StructureID { get; set; }

        public int TopicID { get; set; }

        public virtual STRUCTURES_SUBJECT STRUCTURES_SUBJECT { get; set; }

        public virtual TOPIC TOPIC { get; set; }
    }
}
