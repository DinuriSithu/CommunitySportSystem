using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunitySportSystem.Models
{
    public class Member_Sport
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Member")]
        public int Member_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Sport")]
        public int Sport_ID { get; set; }

        
        public virtual Member Member { get; set; }

        public virtual Sport Sport { get; set; }
    }
}