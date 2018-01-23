using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HGSSSARAssistant.Web.Models
{
    public class UserViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public long Id { get; set;  }

        [Required]
        [Display(Prompt = "Enter first name")]
        public String FirstName { get; set; }

        [Required]
        [Display(Prompt = "Enter last name")]

        public String LastName { get; set; }

        public String Name
        {
            get
            {
                return (FirstName + " " + LastName);
            }
        }

        [Required]
        [Display(Prompt = "Enter address")]
        public String Address { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Prompt = "Enter email")]

        public String Email { get; set; }

        public String AndroidPushId { get; set; }

        [DataType(DataType.Password)]

        public String Password { get; set; }

        public String PasswordSalt { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Prompt = "Enter contact number")]

        public String ContactNumber { get; set; }
        [DataType(DataType.PhoneNumber)]

        public String AdditionalContactNumbers { get; set; }

        [Display(Prompt = "Choose station")]
        public long StationId { get; set; }

        [Display(Name = "Station")]
        public String StationName { get; set; }

        [Display(Prompt = "Choose category")]
        public long CategoryId { get; set; }

        [Display(Name = "Category")]
        public String CategoryName { get; set; }

        [Display(Prompt = "Choose role")]
        public long RoleId { get; set; }

        [Display(Name = "Role")]
        public String RoleName { get; set; }

        [Display(Name = "Is Available?")]
        public bool IsAvailable
        {
            get
            {
                if (Availability == null) return false;

                DateTime now = new DateTime();
                foreach(AvailabilityViewModel period in Availability)
                {
                    if (period.StartTime <= now && period.EndTime >= now) return true;
                }
                return false;
            }
        }

        public List<AvailabilityViewModel> Availability { get; set; }
       
        //[Display(Name = "Expertises")]
        //public ExpertiseViewModel[] Expertise { get; set; }
    }
}
