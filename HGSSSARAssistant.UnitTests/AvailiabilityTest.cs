using System;
using HGSSSARAssistant.Core;
using Xunit;

namespace HGSSSARAssistant.UnitTests
{
    public class AvailiabilityTest
    {
        private readonly Availability _availability;

        public AvailiabilityTest()
        {
            this._availability = new Availability
            {
                Day = Days.Mon,
                StartTime = new TimeSpan(10, 0, 0),
                EndTime = new TimeSpan(12, 0, 0),
                Location = new Location
                {
                    Description = "Location_1_Description",
                    Name = "Location_1_Name",
                    Id = 1,
                    Latitude = 16.3425m,
                    Longitude = 45.432m
                }
            };
        }

        [Fact]
        public void ShouldCreateAvailability()
        {
            Assert.NotNull(this._availability);
        }


        [Fact]
        public void ShouldAllowSettingEndOfDay()
        {
            TimeSpan time = new TimeSpan(23, 59, 59);
            this._availability.EndTime = time;
            Assert.Equal(time, this._availability.EndTime);
        }


        [Fact]
        public void ShouldAllowSettingStartOfDay()
        {
            TimeSpan time = new TimeSpan(0, 0, 0);
            this._availability.StartTime = time;
            Assert.Equal(time, this._availability.StartTime);
        }


        [Fact]
        public void ShouldAllowTimeSpanWithinADay()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                this._availability.StartTime = new TimeSpan(1, 0, 0, 0);
            });

            Assert.Throws<InvalidOperationException>(() =>
            {
                this._availability.StartTime = new TimeSpan(23, 59, 60);
            });


            Assert.Throws<InvalidOperationException>(() =>
            {
                this._availability.EndTime = new TimeSpan(1, 0, 0, 0);
            });

            Assert.Throws<InvalidOperationException>(() =>
            {
                this._availability.EndTime = new TimeSpan(23, 59, 60);
            });
        }
    }
}
