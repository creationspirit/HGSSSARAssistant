using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HGSSSARAssistant.Models
{
    public class Station
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public Location Location { get; set; }
    }
}
