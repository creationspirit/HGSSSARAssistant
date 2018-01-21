using System;
using System.ComponentModel.DataAnnotations;

namespace HGSSSARAssistant.Core.Models
{
    public class UserModel
    {
        public long Id { get; set;  }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
        public String AndroidPushId { get; set; }
        public String Password { get; set; }
        public String PasswordSalt { get; set; }
        public String ContactNumber { get; set; }
        public String AdditionalContactNumbers { get; set; }
        public StationViewModel Station { get; set; }
        public CategoryViewModel Category { get; set; }
        public RoleViewModel Role { get; set; }
        public AvailabilityViewModel Availability { get; set; }
        public ExpertiseViewModel[] Expertise { get; set; }
    }
}
