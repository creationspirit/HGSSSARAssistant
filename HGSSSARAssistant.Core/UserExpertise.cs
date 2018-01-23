using System;
using System.Collections.Generic;
using System.Text;

namespace HGSSSARAssistant.Core
{
    public class UserExpertise
    {
        public long UserId { get; set; }
        public User User { get; set; }

        public long ExpertiseId { get; set; }
        public Expertise Expertise { get; set; }
    }
}
