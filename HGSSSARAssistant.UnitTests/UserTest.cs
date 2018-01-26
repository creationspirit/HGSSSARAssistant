using System;
using System.Collections.Generic;
using HGSSSARAssistant.Core;
using Xunit;

namespace HGSSSARAssistant.UnitTests
{
    public class UserTest
    {
        private Role _adminRole = new Role
        {
            Id = 1,
            Name = "Admin"
        };

        private User _user;

        public UserTest()
        {
            this._user = new User();
        }
        [Fact]
        public void CreateUser()
        {
            User user = new User();
            Assert.NotNull(user);
        }

        [Fact]
        public void SetAvailabilities()
        {
            List<Availability> availabilities = new List<Availability>();
            availabilities.Add(new Availability
            {
				Id = 1,
                Day = Days.Mon,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                Location = new Location {
                    Id = 1,
                    Description = "Desc",
                    Name = "Name",
                    Longitude = 1.1m,
                    Latitude = 1.1m
                }
            });

            this._user.Availiabilities = availabilities;

            Assert.Equal(availabilities, this._user.Availiabilities);
        }
    }
}
