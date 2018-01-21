using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HGSSSARAssistant.Core.Models
{
    public class LocationViewModel
    {
        public long Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }
}
