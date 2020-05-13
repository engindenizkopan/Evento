namespace MyProject.Models
{
  
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class userANDevent
    {

        [StringLength(50)]
        public string userName { get; set; }

        [StringLength(50)]
        public string eventName { get; set; }

        
    }
}
