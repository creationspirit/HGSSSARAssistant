using FluentAssertions;
using HGSSSARAssistant.Core;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.IntegrationTests.Fixture;
using HGSSSARAssistant.Web.Api;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HGSSSARAssistant.IntegrationTests.Tests
{
    [Collection("ContextCollection")]
    public class UsersControllerTests
    {
        private readonly TestContext _context;

        public UsersControllerTests(TestContext context)
        {
            this._context = context;
        }

        [Fact]
        public async Task UsersReturnsOkResponse()
        {
            var response = await _context.Client.GetAsync("/api/users");

            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public Task UsersReturnsAViewResultWithAListOfUsers()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetAll()).Returns(GetUsers());
            //mockRepo.Setup(repo => repo.GetAvailableUsers(new DateTime())).Returns(GetUsers());
            var controller = new UsersController(mockRepo.Object);

            // Act
            var result = controller.GetUsers();

            // Assert
            var viewResult = Assert.IsType<JsonResult>(result);
            //var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
            //    viewResult.ViewData.Model);
            //Assert.Equal(2, model.Count());
        }

        private List<User> GetUsers()
        {
            var sessions = new List<User>
            {
                new User()
                {
                    Id = 1,
                    FirstName = "Matej",
                    LastName = "Janjic",
                    Address = new Location()
                    {
                        Id = 1,
                        Latitude = 53.4564M,
                        Longitude = 34.2324M,
                        Name = "Kuca",
                        Description = "Tu zivin"
                    },
                    Category = new Category
                    {
                        Id = 1,
                        Name = "Planinar"
                    },
                    Email = "a@b.com",
                    Password = "veoma dobra lozinka",
                    Role = new Role
                    {
                        Id = 1,
                        Name = "Admin"
                    },
                    AdditionalContactNumbers = "14124124",
                    AndroidPushId = "a1911421jasd2rj",
                    ContactNumber = "141412414",
                    Station = new Station
                    {
                        Id = 1,
                        Name = "Stanica ZG",
                        Location = new Location
                        {
                            Id = 2,
                            Name = "Lokacija stanice",
                            Description = "Tu se stanica nalazi",
                            Latitude = 53.131M,
                            Longitude = 12.1312M
                        }
                    },
                    Availiabilities = new List<Availability>
                    {
                        new Availability()
                        {
                            Id = 1,
                            Day = Days.Mon,
                            StartTime = new DateTime(),
                            EndTime = new DateTime(),
                            Location = new Location
                            {
                                Id = 3,
                                Name = "Poso",
                                Description = "Tu radin",
                                Latitude = 34.131M,
                                Longitude = 67.1312M
                            }
                        }
                    }                    
                },
                new User()
                {
                    Id = 2,
                    FirstName = "Luka",
                    LastName = "Lazanja",
                    Address = new Location()
                    {
                        Id = 4,
                        Latitude = 23.4564M,
                        Longitude = 24.2324M,
                        Name = "Kuca",
                        Description = "Tu zivin"
                    },
                    Category = new Category
                    {
                        Id = 2,
                        Name = "Speleolog"
                    },
                    Email = "b@c.com",
                    Password = "veoma dobra lozinka",
                    Role = new Role
                    {
                        Id = 2,
                        Name = "Spasavatelj"
                    },
                    AdditionalContactNumbers = "14124124",
                    AndroidPushId = "a1911421jasd2rj",
                    ContactNumber = "141412414",
                    Station = new Station
                    {
                        Id = 2,
                        Name = "Stanica ZD",
                        Location = new Location
                        {
                            Id = 5,
                            Name = "Lokacija stanice u zd",
                            Description = "Tu se stanica nalazi",
                            Latitude = 23.131M,
                            Longitude = 32.1312M
                        }
                    },
                    Availiabilities = new List<Availability>
                    {
                        new Availability()
                        {
                            Id = 2,
                            Day = Days.Mon,
                            StartTime = new DateTime(),
                            EndTime = new DateTime(),
                            Location = new Location
                            {
                            Id = 6,
                                Name = "PosoZD",
                                Description = "Tu ne radin nista",
                                Latitude = 34.131M,
                                Longitude = 17.1312M
                            }
                        }
                    },

                }
            };
            return sessions;
        }
    }
}
