using System;
using System.Collections.Generic;

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


        public bool IsAvailable(DateTime epoch) {
            return this.GetCurrentAvailabilityPeriod(epoch) != null;
        }

        public Location GetLocationAtTime(DateTime epoch) {
            Availability currentAvailability = this.GetCurrentAvailabilityPeriod(epoch);
            if (currentAvailability != null) {
                return currentAvailability.Location;
            } else {
                return Address;
            }
        }

        private Availability GetCurrentAvailabilityPeriod(DateTime epoch) {
            foreach (Availability a in this.Availiabilities)
            {
                if (a.StartTime < epoch.TimeOfDay && a.EndTime > epoch.TimeOfDay && (int)a.Day == (int)epoch.DayOfWeek)
                {
                    return a;
                }
            }
            return null;
        }
    }
}
