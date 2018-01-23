using System;
using System.Collections.Generic;
using System.Text;

namespace HGSSSARAssistant.Core
{
    public class UserAvailability
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public long AvailabilityId { get; set; }
        public Availability Availability { get; set; }
    }
}
