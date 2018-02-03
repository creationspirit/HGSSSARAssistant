using System;
using HGSSSARAssistant.Core;
using Xunit;

namespace HGSSSARAssistant.UnitTests
{
    public class AvailiabilityTest
    {

        private DateTime _timeStamp = new DateTime(2018, 1, 15, 12, 35, 14);
        private readonly Availability _availability;

        public AvailiabilityTest()
        {
            this._availability = new Availability
            {
                Day = Days.Mon,
                EndTime = _timeStamp.AddHours(2),
                StartTime = _timeStamp.AddHours(-2),
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
        public void ShouldNormalizeEndTimeCorrectlyForSameWeek()
        {
            Assert.Equal(this._timeStamp.AddHours(2).Year, this._availability.EndTime.Year);
            Assert.Equal(this._timeStamp.AddHours(2).Month, this._availability.EndTime.Month);
            Assert.Equal(this._timeStamp.AddHours(2).Hour, this._availability.EndTime.Hour);
            Assert.Equal(this._timeStamp.AddHours(2).Minute, this._availability.EndTime.Minute);
            Assert.Equal((int)this._availability.Day, (int)this._availability.EndTime.DayOfWeek);
        }

        [Fact]
        public void ShouldNormalizeStartTimeCorrectlyForSameWeek()
        {
            Assert.Equal(this._timeStamp.AddHours(-2).Year, this._availability.StartTime.Year);
            Assert.Equal(this._timeStamp.AddHours(-2).Month, this._availability.StartTime.Month);
            Assert.Equal(this._timeStamp.AddHours(-2).Hour, this._availability.StartTime.Hour);
            Assert.Equal(this._timeStamp.AddHours(-2).Minute, this._availability.StartTime.Minute);
            Assert.Equal((int)this._availability.Day, (int)this._availability.StartTime.DayOfWeek);
        }

        [Fact]
        public void ShouldNormalizeEndTimeCorrectlyForDifferentWeek()
        {
            this._availability.EndTime = this._timeStamp.AddHours(2).AddDays(-20);

            Assert.Equal(this._timeStamp.AddHours(2).Year, this._availability.EndTime.Year);
            Assert.Equal(this._timeStamp.AddHours(2).Month, this._availability.EndTime.Month);
            Assert.Equal(this._timeStamp.AddHours(2).Hour, this._availability.EndTime.Hour);
            Assert.Equal(this._timeStamp.AddHours(2).Minute, this._availability.EndTime.Minute);
            Assert.Equal((int)this._availability.Day, (int)this._availability.EndTime.DayOfWeek);
        }

        [Fact]
        public void ShouldNormalizeStartTimeCorrectlyForDifferentWeek()
        {
            this._availability.StartTime = this._timeStamp.AddHours(-2).AddDays(-20);

            Assert.Equal(this._timeStamp.AddHours(-2).Year, this._availability.StartTime.Year);
            Assert.Equal(this._timeStamp.AddHours(-2).Month, this._availability.StartTime.Month);
            Assert.Equal(this._timeStamp.AddHours(-2).Hour, this._availability.StartTime.Hour);
            Assert.Equal(this._timeStamp.AddHours(-2).Minute, this._availability.StartTime.Minute);
            Assert.Equal((int)this._availability.Day, (int)this._availability.StartTime.DayOfWeek);
        }

        [Fact]
        public void ShouldNormalizeStartTimeCorrectlyForDifferentDay()
        {
            Days day = Days.Fri;
            this._availability.Day = day;

            Assert.Equal(this._timeStamp.AddHours(-2).Year, this._availability.StartTime.Year);
            Assert.Equal(this._timeStamp.AddHours(-2).Month, this._availability.StartTime.Month);
            Assert.Equal(this._timeStamp.AddHours(-2).Hour, this._availability.StartTime.Hour);
            Assert.Equal(this._timeStamp.AddHours(-2).Minute, this._availability.StartTime.Minute);
            Assert.Equal((int) day, (int)this._availability.StartTime.DayOfWeek);
        }
    }
}
