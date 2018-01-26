using System;
using System.Collections.Generic;

namespace HGSSSARAssistant.Core
{
    public class Availability : Entity
    {
        public Location Location { get; set; }
        public DateTime StartTime {
            get {
                DateTime refDateTime = DateTime.Now;
                int dayDiff = (int) this.Day - (int) refDateTime.DayOfWeek;
                refDateTime = this.StartTime.AddDays((double) dayDiff);

                return new DateTime(
                    refDateTime.Year,
                    refDateTime.Month,
                    refDateTime.Day,
                    this.StartTime.Hour,
                    this.StartTime.Minute,
                    this.StartTime.Second,
                    this.StartTime.Millisecond,
                    this.StartTime.Kind);
            }
            set {
                this.StartTime = value;
            }
        }
        public DateTime EndTime {
            get
            {
                DateTime refDateTime = DateTime.Now;
                int dayDiff = (int)this.Day - (int)refDateTime.DayOfWeek;
                refDateTime = this.EndTime.AddDays((double)dayDiff);

                return new DateTime(
                    refDateTime.Year,
                    refDateTime.Month,
                    refDateTime.Day,
                    this.EndTime.Hour,
                    this.EndTime.Minute,
                    this.EndTime.Second,
                    this.EndTime.Millisecond,
                    this.EndTime.Kind);
            }
            set
            {
                this.EndTime = value;
            }
        }
        public Days Day { get; set; }
    }

    public enum Days
    {
        Mon = 1,
        Tue = 2,
        Wed = 3,
        Thu = 4,
        Fri = 5,
        Sat = 6,
        Sun = 7
    }
}
