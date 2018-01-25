using System;
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
        public void SetLatitude()
        {
            
        }
    }
}
