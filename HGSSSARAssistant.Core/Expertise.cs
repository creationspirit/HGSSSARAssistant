using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HGSSSARAssistant.Core
{
    public class Expertise : Entity
    {
        public String Name { get; set; }

        public ICollection<UserExpertise> UserExpertise { get; set; }
    }
}
