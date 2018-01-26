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
using System.Net.Http;
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
        public async Task GetUsersReturnsOkResponse()
        {            
            var response = await _context.Client.GetAsync("/api/users");

            response.EnsureSuccessStatusCode();

            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public void PostUserWithInvalidModelReturnsBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var controller = new UsersController(mockRepo.Object);
            controller.ModelState.AddModelError("Email", "Required");

            // Act
            var result = controller.PostUser(new User());

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void PostUserWithValidModelReturnsCreated()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            var controller = new UsersController(mockRepo.Object);
            var user = new User()
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
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now.AddDays(4),
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
            };

            // Act
            var result = controller.PostUser(user);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.IsType<User>(createdResult.Value);
            Assert.Equal(user, createdResult.Value);
        }

        [Fact]
        public void PutUserWithValidModelReturnsNoContent()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();            
            var controller = new UsersController(mockRepo.Object);
            var user = new User()
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
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now.AddDays(4),
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
            };
            mockRepo.Object.Insert(user);
            user = new User()
            {
                Id = 1,
                FirstName = "Luka",
                LastName = "Lazanja",
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
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now.AddDays(4),
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
            };

            // Act
            var result = controller.PutUser(1, user);

            // Assert
            var createdResult = Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteNonExistingUserReturnsNotFound()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.Exists(1)).Returns(false);
            var controller = new UsersController(mockRepo.Object);

            // Act
            var result = controller.DeleteUser(1);

            // Assert
            var createdResult = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteExistingUserReturnsOk()
        {
            // Arrange
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.Exists(1)).Returns(true);
            var controller = new UsersController(mockRepo.Object);
            var user = new User()
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
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now.AddDays(4),
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
            };
            mockRepo.Object.Insert(user);

            // Act
            var result = controller.DeleteUser(user.Id);

            // Assert
            var createdResult = Assert.IsType<OkResult>(result);
        }
    }
}
