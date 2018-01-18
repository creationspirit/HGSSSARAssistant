using HGSSSARAssistant.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace HGSSSARAssistant.Web.Models
{
    public class UserViewModel : Entity
    {
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
        [Required]
        [DataType(DataType.Password)]
        [Display(Prompt = "Enter password")]
        public String Password { get; set; }
        [Required]
        [Display(Prompt = "Enter password salt")]
        public String PasswordSalt { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Prompt = "Enter contact number")]
        public String ContactNumber { get; set; }
        public String AdditionalContactNumbers { get; set; }
        public Station Station { get; set; }
        public Category Category { get; set; }
        public Role Role { get; set; }
        public Availability Availability { get; set; }
        public Expertise[] Expertise { get; set; }
    }
}
