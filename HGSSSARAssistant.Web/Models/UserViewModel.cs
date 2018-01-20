using Microsoft.AspNetCore.Mvc;
using System;
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
        [Required]
        [Display(Prompt = "Enter address")]
        public String Address { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Prompt = "Enter email")]
        public String Email { get; set; }
        public String AndroidPushId { get; set; }
        [DataType(DataType.Password)]
        [HiddenInput(DisplayValue = false)]
        public String Password { get; set; }
        [HiddenInput(DisplayValue = false)]
        public String PasswordSalt { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Prompt = "Enter contact number")]
        public String ContactNumber { get; set; }
        public String AdditionalContactNumbers { get; set; }
        public StationViewModel Station { get; set; }
        public CategoryViewModel Category { get; set; }
        public RoleViewModel Role { get; set; }
        public AvailabilityViewModel Availability { get; set; }
        public ExpertiseViewModel[] Expertise { get; set; }
    }
}
