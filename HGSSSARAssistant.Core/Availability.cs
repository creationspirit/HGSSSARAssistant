using System;
using System.Collections.Generic;

// The TimeSpan structure can also be used to represent the time of day,
// but only if the time is unrelated to a particular date.
// https://msdn.microsoft.com/en-us/library/system.timespan(v=vs.110).aspx

namespace HGSSSARAssistant.Core
{
    public class Availability : Entity
    {
        public Location Location { get; set; }

        private TimeSpan startTime;
        public TimeSpan StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                if (value >= endTime) {
                    throw new InvalidOperationException("Availability period cannot begin before it has ended.");
                }
                startTime = value;
            }
        }

        private TimeSpan endTime;
        public TimeSpan EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                if (value <= startTime)
                {
                    throw new InvalidOperationException("Availability period cannot end before it has begun.");
                }
                endTime = value;
            }
        }

        public Days Day { get; set; }
    }
}