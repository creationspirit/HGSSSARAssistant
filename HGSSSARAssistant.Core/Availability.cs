using System;
using System.Collections.Generic;

namespace HGSSSARAssistant.Core
{
    public class Availability : Entity
    {
        public Location Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<UserAvailability> UserAvailability { get; set; }
    }
}
