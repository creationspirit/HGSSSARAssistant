using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace HGSSSARAssistant.Web.Models
{
    public class RoleViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }
        [Required]
        [Display(Prompt = "Enter role name")]
        public String Name { get; set; }
    }
}
