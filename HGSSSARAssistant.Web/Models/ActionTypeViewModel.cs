using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace HGSSSARAssistant.Web.Models
{
    public class ActionTypeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }
        public LocationViewModel Location { get; set; }
        [Required]
        [Display(Prompt = "Enter action type name")]
        public String Name;
    }
}
