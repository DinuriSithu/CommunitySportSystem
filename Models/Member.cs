using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CommunitySportSystem.Models
{
    public class Member
    {
        [Key]
        public int Member_ID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public DateTime RegistrationDate { get; set; }

        
        public virtual ICollection<Member_Sport> MemberSports { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Inquiry> Inquiries { get; set; }
    }
}