using HGSSSARAssistant.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HGSSSARAssistant.Web.Models
{
    public class UserAvailabilityViewModel
    {
        public long UserId { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String Name {
            get {
                return (FirstName + " " + LastName);
            }
        }

        public List<Availability> Availability { get; set; }
    }
}
