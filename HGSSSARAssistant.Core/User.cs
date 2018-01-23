using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace HGSSSARAssistant.Core
{
    public class User : Entity
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
        public String AndroidPushId { get; set; }
        public String Password { get; set; }
        public String ContactNumber { get; set; }
        public String AdditionalContactNumbers { get; set; }
        public Station Station { get; set; }
        public Category Category { get; set; }
        public Role Role { get; set; }
        public List<UserAvailability> UserAvailability { get; set; }
        public List<UserExpertise> UserExpertise { get; set; }
    }
}
