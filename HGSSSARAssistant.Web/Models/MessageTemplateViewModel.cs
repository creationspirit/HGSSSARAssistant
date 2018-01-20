using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HGSSSARAssistant.Web.Models
{
    public class MessageTemplateViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }
        [Required]
        [Display(Prompt = "Enter message")]
        public String Message { get; set; }
    }
}
