using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CommunitySportSystem.ViewModels
{
    public class ReviewViewModel
    {
        public int FacilityID { get; set; }

        public string FacilityName { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Display(Name = "Comment")]
        public string Comment { get; set; }
    }
}