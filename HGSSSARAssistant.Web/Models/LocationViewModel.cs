using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HGSSSARAssistant.Web.Models
{
    public class LocationViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }
        [Required]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Display(Prompt = "Enter latitude value")]
        public decimal Latitude { get; set; }
        [Required]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        [Display(Prompt = "Enter longitude value")]
        public decimal Longitude { get; set; }
        [Required]
        [Display(Prompt = "Enter location name")]
        public String Name { get; set; }
        [Required]
        [Display(Prompt = "Enter location description")]
        public String Description { get; set; }
    }
}
