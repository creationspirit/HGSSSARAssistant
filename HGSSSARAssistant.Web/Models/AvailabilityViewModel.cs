using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HGSSSARAssistant.Web.Models
{
    public class AvailabilityViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }
        public LocationViewModel Location { get; set; }
        [Required]
        [Display(Prompt = "Enter start time")]
        public TimeSpan StartTime { get; set; }
        [Required]
        [Display(Prompt = "Enter end time")]
        public TimeSpan EndTime { get; set; }
    }
}
