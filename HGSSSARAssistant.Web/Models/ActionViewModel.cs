using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HGSSSARAssistant.Web.Models
{
    public class ActionViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set; }
        [Required]
        [Display(Prompt = "Enter action name")]
        public String Name { get; set; }
        [Required]
        [Display(Prompt = "Enter action description")]
        public String Description { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Prompt = "Enter meetup time")]
        public DateTime MeetupTime { get; set; }
        public UserViewModel Leader { get; set; }
        public UserViewModel[] InvitedRescuers { get; set; }
        public UserViewModel[] AttendedRescuers { get; set; }
        public LocationViewModel Location { get; set; }
        public ActionTypeViewModel ActionType { get; set; }
    }
}
