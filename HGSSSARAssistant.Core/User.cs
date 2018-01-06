using System;
namespace HGSSSARAssistant.Core
{
    public class User : Entity
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public String ContactNumber { get; set; }
        public String[] AdditionalContactNumbers { get; set; }
        public bool IsAvailable { get; set; }

        public Station Station { get; set; }
        public Category Category { get; set; }
        public Role Role { get; set; }
        public Availability Availability { get; set; }
        public Expertise[] Expertise { get; set; }
    }
}
