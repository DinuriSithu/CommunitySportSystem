using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunitySportSystem.Models
{
    public class Facility
    {
        [Key]
        public int Facility_ID { get; set; }

        [ForeignKey("Facility_Type")]
        public int FacilityTypeID { get; set; }

        [Required]
        public string FacilityName { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public int Capacity { get; set; }

        public decimal HourlyRate { get; set; }

        public string Status { get; set; }


        public virtual Facility_Type Facility_Type { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}