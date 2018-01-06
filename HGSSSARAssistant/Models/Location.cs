using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HGSSSARAssistant.Models
{
    public class Location
    {
        public long ID { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
    }
}
