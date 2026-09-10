using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunitySportSystem.Models
{
    public class Review
    {
        [Key]
        public int Review_ID { get; set; }

        [ForeignKey("Member")]
        public int Member_ID { get; set; }

        [ForeignKey("Facility")]
        public int Facility_ID { get; set; }

        public int Rating { get; set; }

        public string Comment { get; set; }

        public DateTime ReviewDate { get; set; }

        
        public virtual Member Member { get; set; }

        public virtual Facility Facility { get; set; }
    }
}