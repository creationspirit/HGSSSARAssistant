using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using HGSSSARAssistant.Core;
using Microsoft.AspNetCore.Mvc;

namespace HGSSSARAssistant.BLL.BusinessEntities
{
    [ModelMetadataType(typeof(UserMetadata))]
    public class User : HGSSSARAssistant.Core.User
    {
    }

    internal class UserMetadata {
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        public String Address { get; set; }
        [Required]
        public String Email { get; set; }
        public String AndroidPushId { get; set; }
        [Required]
        public String Password { get; set; }
        [Required]
        public String PasswordSalt { get; set; }
        [Required]
        public String ContactNumber { get; set; }
        public String AdditionalContactNumbers { get; set; }
        public Station Station { get; set; }
        public Category Category { get; set; }
        public Role Role { get; set; }
        public Availability Availability { get; set; }
        public Expertise[] Expertise { get; set; }
    }
}
