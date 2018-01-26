using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace HGSSSARAssistant.Core
{
    public class User : Entity
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String AndroidPushId { get; set; }
        public String Password { get; set; }
        public String ContactNumber { get; set; }
        public String AdditionalContactNumbers { get; set; }

		public Location Address { get; set; }
        public Station Station { get; set; }
        public Category Category { get; set; }
        public Role Role { get; set; }

        public List<Availability> Availiabilities { get; set; }
        public List<UserExpertise> UserExpertise { get; set; }


        public bool IsAvailable() {
            return this.GetCurrentAvailabilityPeriod() != null;
        }

        public Location GetCurrentLocation() {
            Availability currentAvailability = this.GetCurrentAvailabilityPeriod();
            if (currentAvailability != null) {
                return currentAvailability.Location;
            } else {
                return Address;
            }
        }

        private Availability GetCurrentAvailabilityPeriod() {
            DateTime now = DateTime.Now;
            foreach (Availability a in this.Availiabilities)
            {
                if (a.StartTime < now && a.EndTime > now && (int)a.Day == (int)now.DayOfWeek)
                {
                    return a;
                }
            }
            return null;
        }
    }
}
