using System;
namespace HGSSSARAssistant.Core
{
    public class Location : Entity
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }
}
