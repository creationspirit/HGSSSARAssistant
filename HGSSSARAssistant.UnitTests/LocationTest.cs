using System;
using HGSSSARAssistant.Core;
using Xunit;

namespace HGSSSARAssistant.UnitTests
{
    public class LocationTest
    {
        private Location _location;

        public LocationTest()
        {
            this._location = new Location();
        }
        [Fact]
        public void ShouldCreateLocation()
        {
            Assert.NotNull(this._location);
        }

        [Fact]
        public void ShouldSetLatitude()
        {
            decimal expected = 16.589m;
            this._location.Latitude = expected;

            Assert.Equal(expected, this._location.Latitude);
        }

        [Fact]
        public void ShouldSetLongitude()
        {
            decimal expected = 68.009m;
            this._location.Longitude = expected;

            Assert.Equal(expected, this._location.Longitude);
        }
    }
}
