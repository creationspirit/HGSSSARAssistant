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
                return NormalizeAvailibilityPeriod(startTime);
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
                return NormalizeAvailibilityPeriod(endTime);
            }
            set
            {
                endTime = value;
            }
        }
        public Days Day { get; set; }

		private DateTime NormalizeAvailibilityPeriod(DateTime period) {
            DateTime refDateTime = DateTime.Now;
            int dayDiff = (int)this.Day - (int)refDateTime.DayOfWeek;
            refDateTime = refDateTime.AddDays((double)dayDiff);

            return new DateTime(
                refDateTime.Year,
                refDateTime.Month,
                refDateTime.Day,
                period.Hour,
                period.Minute,
                period.Second,
                period.Millisecond,
                period.Kind);
		}
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
