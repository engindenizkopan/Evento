namespace MyProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class userEvents
    {
        
        [StringLength(80)]
        public string eventName { get; set; }

        [StringLength(80)]
        public string eventDate { get; set; }

        [StringLength(20)]
        public string eventTime { get; set; }

        [StringLength(50)]
        public string eventLocation { get; set; }

        [StringLength(50)]
        public string eventCategory { get; set; }

        [StringLength(50)]
        public string eventDetails { get; set; }

        
        public int eventQuota { get; set; }
        public int currentQuota { get; set; }
    }
}
