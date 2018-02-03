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


        public bool IsAvailable(TimeSpan timeOfDay, Days day)
        {
            return this.GetCurrentAvailabilityPeriod(timeOfDay, day) != null;
        }

        public bool IsAvailable(DateTime epoch)
        {
            return this.GetCurrentAvailabilityPeriod(epoch.TimeOfDay, (Days) epoch.DayOfWeek) != null;
        }

        public Location GetLocationAtTime(TimeSpan timeOfDay, Days day)
        {
            Availability currentAvailability = this.GetCurrentAvailabilityPeriod(timeOfDay, day);
            if (currentAvailability != null)
            {
                return currentAvailability.Location;
            }

            return Address;
        }


        public Location GetLocationAtTime(DateTime epoch)
        {
            Availability currentAvailability = this.GetCurrentAvailabilityPeriod(epoch.TimeOfDay, (Days) epoch.DayOfWeek);
            if (currentAvailability != null)
            {
                return currentAvailability.Location;
            }

            return Address;
        }

        private Availability GetCurrentAvailabilityPeriod(TimeSpan timeOfDay, Days day) {
            foreach (Availability a in this.Availiabilities)
            {
                if (a.StartTime <= timeOfDay && a.EndTime >= timeOfDay && a.Day == day)
                {
                    return a;
                }
            }
            return null;
        }
    }
}
