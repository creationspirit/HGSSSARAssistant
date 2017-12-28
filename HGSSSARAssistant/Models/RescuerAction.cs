using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HGSSSARAssistant.Models
{
    public class RescuerAction
    {
        public long ID { get; set; }
        public ApplicationUser Rescuer { get; set; }
        public Action Action { get; set; }

    }
}
