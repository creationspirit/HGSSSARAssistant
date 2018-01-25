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
        public void CreateLocation()
        {
            Location location = new Location();
            Assert.NotNull(location);
        }

        [Fact]
        public void SetLatitude()
        {
            decimal expected = 16.589m;
            this._location.Latitude = expected;

            Assert.Equal(expected, this._location.Latitude);
        }

        [Fact]
        public void SetLongitude()
        {
            decimal expected = 68.009m;
            this._location.Longitude = expected;

            Assert.Equal(expected, this._location.Longitude);
        }
    }
}
