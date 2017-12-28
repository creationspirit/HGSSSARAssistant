using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HGSSSARAssistant.Models
{
    public class RescuerSkill
    {
        public long ID { get; set; }

        public Skill Skill { get; set; }
        public ApplicationUser Rescuer { get; set; }
    }
}
