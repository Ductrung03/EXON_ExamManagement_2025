namespace EXON_EM.Data.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PARTS_DETAILS
    {
        [Key]
        public int PartDetailID { get; set; }

        public int? PartID { get; set; }

        public int? TopicID { get; set; }

        public int? OrderOfTopic { get; set; }

        public int? Status { get; set; }

        public virtual PART PART { get; set; }

        public virtual TOPIC TOPIC { get; set; }
    }
}
