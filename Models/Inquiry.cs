using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunitySportSystem.Models
{
    public class Inquiry
    {
        [Key]
        public int Inquiry_ID { get; set; }

        [ForeignKey("Member")]
        public int? Member_ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public DateTime Inq_Date { get; set; }

        public string Inq_Status { get; set; }

     
        public virtual Member Member { get; set; }
    }
}