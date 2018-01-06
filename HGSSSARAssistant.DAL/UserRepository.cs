using System;
using Microsoft.EntityFrameworkCore;
using HGSSSARAssistant.Core;
using HGSSSARAssistant.Core.Repositories;

namespace HGSSSARAssistant.DAL
{
    public class UserRepository : DbContext, IUserRepository
    {
        public UserRepository(DbContextOptions<UserRepository> options)
            : base(options)
        { }

        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public User[] GetAvailableUsers(DateTime time)
        {
            throw new NotImplementedException();
        }

        public User[] GetAvailableUsers(Availability availability)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(long id)
        {
            throw new NotImplementedException();
        }

        public User[] GetUsersByCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public User[] GetUsersByExpertise(Expertise expertise)
        {
            throw new NotImplementedException();
        }

        public User[] GetUsersByName(string name)
        {
            throw new NotImplementedException();
        }

        public User[] GetUsersByRole(Role role)
        {
            throw new NotImplementedException();
        }

        public User[] GetUsersByStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
