using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HGSSSARAssistant.Core
{
    public class Expertise : Entity
    {
        public String Name { get; set; }
        public List<UserExpertise> UserExpertise { get; set; }
    }
}
