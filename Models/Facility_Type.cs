using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CommunitySportSystem.Models
{
    public class Facility_Type
    {
        [Key]
        public int FacilityTypeID { get; set; }

        [Required]
        public string TypeName { get; set; }

        
        public virtual ICollection<Facility> Facilities { get; set; }
    }
}