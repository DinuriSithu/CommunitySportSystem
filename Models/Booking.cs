using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunitySportSystem.Models
{
    public class Booking
    {
        [Key]
        public int Booking_ID { get; set; }

        [ForeignKey("Member")]
        public int Member_ID { get; set; }

        [ForeignKey("Facility")]
        public int Facility_ID { get; set; }

        public DateTime BookingDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string BookingStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual Member Member { get; set; }

        public virtual Facility Facility { get; set; }
    }
}