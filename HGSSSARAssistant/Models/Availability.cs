using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HGSSSARAssistant.Models
{
    public class Availability
    {
        public long ID { get; set; }

        public ApplicationUser Rescuer { get; set; }
        
        public Location Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public String DaysInWeek { get; set; }
    }
}
