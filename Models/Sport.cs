using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CommunitySportSystem.Models
{
    public class Sport
    {
        [Key]
        public int Sport_ID { get; set; }

        [Required]
        public string SportName { get; set; }

        
        public virtual ICollection<Member_Sport> MemberSports { get; set; }
    }
}