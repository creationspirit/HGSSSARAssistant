using System;
using System.Collections.Generic;

namespace HGSSSARAssistant.Core
{
    public class Availability : Entity
    {
        public Location Location { get; set; }
        private DateTime startTime;
        public DateTime StartTime
        {
            get
            {
                DateTime refDateTime = DateTime.Now;
                int dayDiff = (int)this.Day - (int)refDateTime.DayOfWeek;
                refDateTime = this.startTime.AddDays((double)dayDiff);

                return new DateTime(
                    refDateTime.Year,
                    refDateTime.Month,
                    refDateTime.Day,
                    startTime.Hour,
                    startTime.Minute,
                    startTime.Second,
                    startTime.Millisecond,
                    startTime.Kind);
            }
            set
            {
                startTime = value;
            }
        }

        private DateTime endTime;
        public DateTime EndTime {
            get
            {
                DateTime refDateTime = DateTime.Now;
                int dayDiff = (int)this.Day - (int)refDateTime.DayOfWeek;
                refDateTime = this.endTime.AddDays((double)dayDiff);

                return new DateTime(
                    refDateTime.Year,
                    refDateTime.Month,
                    refDateTime.Day,
                    endTime.Hour,
                    endTime.Minute,
                    endTime.Second,
                    endTime.Millisecond,
                    endTime.Kind);
            }
            set
            {
                endTime = value;
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
