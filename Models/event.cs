namespace MyProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("event")]
    public partial class _event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int eventID { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(10)]
        public string date { get; set; }

        [StringLength(10)]
        public string time { get; set; }

        [StringLength(20)]
        public string location { get; set; }

        [StringLength(100)]
        public string details { get; set; }

        [StringLength(20)]
        public string category { get; set; }

        [StringLength(20)]
        public string owner { get; set; }
    }
}
