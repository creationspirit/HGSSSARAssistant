using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HGSSSARAssistant.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Available = false;
        }

        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Adress { get; set; }
        public String Contact { get; set; }
        public String AlternateContact { get; set; }
        public bool Available { get; set; }

        public Station Station { get; set; }
        public Category Category { get; set; }
    }
}
