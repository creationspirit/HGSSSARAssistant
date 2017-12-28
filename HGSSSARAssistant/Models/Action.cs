using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HGSSSARAssistant.Models
{
    public class Action
    {
        public long ID { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public DateTime DateTime { get; set; }
        
        public Location Location { get; set; }
        public ActionType ActionType { get; set; }
    }
}
