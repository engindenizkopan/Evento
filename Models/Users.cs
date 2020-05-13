namespace MyProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [Key]
        public int userID { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string surname { get; set; }

        public int? age { get; set; }

        public string interests { get; set; }

        [StringLength(10)]
        public string city { get; set; }

        [StringLength(50)]
        public string userName { get; set; }

        [StringLength(50)]
        public string mail { get; set; }

        [StringLength(50)]
        public string password { get; set; }
    }
}
