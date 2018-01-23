using System;
using System.Collections.Generic;

namespace HGSSSARAssistant.Core
{
    public class Action : Entity
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime MeetupTime { get; set; }
        public User Leader { get; set; }
        public List<User> InvitedRescuers { get; set; }
        public List<User> AttendedRescuers { get; set; }
        public Location Location { get; set; }
        public ActionType ActionType { get; set; }
    }
}
