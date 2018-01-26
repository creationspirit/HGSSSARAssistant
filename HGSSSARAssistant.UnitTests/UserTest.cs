using System;
using System.Collections.Generic;
using HGSSSARAssistant.Core;
using Xunit;

namespace HGSSSARAssistant.UnitTests
{
    public class UserTest
    {
        private Location _testLocation1 = new Location
        {
            Id = 1,
            Latitude = 17.3214m,
            Longitude = 48.5432m,
            Name = "Location_1_Name",
            Description = "Location_1_Decription"
        };

        private Location _testLocation2 = new Location
        {
            Id = 2,
            Latitude = 21.3214m,
            Longitude = 13.5432m,
            Name = "Location_2_Name",
            Description = "Location_2_Decription"
        };

        private User _user;

        public UserTest()
        {
            this._user = new User {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                AndroidPushId = "439t43k903",
                Email = "test@test.io",
                ContactNumber = "+385 91 111 111",
                AdditionalContactNumbers = "+385 92 222 222; +385 93 333 333",
                Password = "******",
                Role = new Role
                    {
                        Id = 1,
                        Name = "Admin"
                    },
                Address = this._testLocation1,
                Station = new Station {
                    Id = 1,
                    Name = "Station 1",
                    Location = this._testLocation2
                },
                Category = new Category {
                    Id = 1,
                    Name = "Rescuer"
                },
                Availiabilities = new List<Availability> {
                    new Availability {
                        StartTime = DateTime.Now.AddHours(-1),
                        EndTime = DateTime.Now.AddHours(1),
                        Location = this._testLocation2,
                        Day = (Days) DateTime.Now.DayOfWeek,
                        Id = 1
                    }
                }
            };
        }
        [Fact]
        public void ShouldCreateUser()
        {
            Assert.NotNull(this._user);
        }

        [Fact]
        public void ShouldSetAvailabilities()
        {
            List<Availability> availabilities = new List<Availability>();
            availabilities.Add(new Availability
            {
				Id = 1,
                Day = (Days) DateTime.Now.DayOfWeek,
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


        [Fact]
        public void ShouldBeAvailable()
        {
            Assert.True(this._user.IsAvailable());
        }

        [Fact]
        public void ShouldBeUnavailable()
        {
            this._user.Availiabilities[0].EndTime = DateTime.Now.AddHours(-5);
            Assert.False(this._user.IsAvailable());
        }

        [Fact]
        public void ShouldBeAtHomeIfUnavailable()
        {
            this._user.Availiabilities[0].EndTime = DateTime.Now.AddHours(-5);
            Assert.Equal(this._user.Address, this._user.GetCurrentLocation());
        }

        [Fact]
        public void ShouldBeAtLocationIfAvailable()
        {
            Assert.Equal(this._user.Availiabilities[0].Location, this._user.GetCurrentLocation());
        }
    }
}
