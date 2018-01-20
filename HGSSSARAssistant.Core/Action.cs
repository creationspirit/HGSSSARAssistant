using System;
namespace HGSSSARAssistant.Core
{
    public class Action : Entity
    {
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime MeetupTime { get; set; }
        public User Leader { get; set; }
        public User[] InvitedRescuers { get; set; }
        public User[] AttendedRescuers { get; set; }
        public Location Location { get; set; }
        public ActionType ActionType { get; set; }
    }
}
