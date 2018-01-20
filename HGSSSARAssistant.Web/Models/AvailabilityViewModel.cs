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
        [DataType(DataType.DateTime)]
        [Display(Prompt = "Enter start time")]
        public DateTime StartTime { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Prompt = "Enter end time")]
        public DateTime EndTime { get; set; }
    }
}
