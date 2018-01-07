using HGSSSARAssistant.Core;
using HGSSSARAssistant.Core.Repositories;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HGSSSARAssistant.DAL.EF;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;

namespace HGSSSARAssistant.DAL
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private UserContext _context;

        private DbSet<HGSSSARAssistant.Core.User> _userEntity;

        public UserRepository(UserContext context)
        {
            this._context = context;
            this._userEntity = context.Set<HGSSSARAssistant.Core.User>();
        }

        public void AddUser(User user)
        {
            _userEntity.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _userEntity.Remove(user);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IEnumerable<User> GetAvailableUsers(DateTime time)
        {
            return _userEntity.AsEnumerable();
        }

        public IEnumerable<User> GetAvailableUsers(Availability availability)
        {
            return _userEntity.AsEnumerable();
        }

        public User GetUserById(long id)
        {
            return _userEntity.SingleOrDefaultAsync(user => user.Id == id).Result;
        }

        public IEnumerable<User> GetUsersByCategory(Category category)
        {
            return (from user in _userEntity where user.Category.Id == category.Id select user);
        }

        public IEnumerable<User> GetUsersByExpertise(Expertise expertise)
        {
            return (from user in _userEntity where user.Expertise.Contains(expertise) select user);
        }

        public IEnumerable<User> GetUsersByName(string name)
        {
            return (from user in _userEntity where user.FirstName.Equals(name) || user.LastName.Equals(name) select user);
        }

        public IEnumerable<User> GetUsersByRole(Role role)
        {
            return (from user in _userEntity where user.Role.Id == role.Id select user);
        }

        public IEnumerable<User> GetUsersByStation(Station station)
        {
            return (from user in _userEntity where user.Station.Id == station.Id select user);
        }

        public User UpdateUser(User user)
        {
            User updatedUser = _userEntity.Update(user).Entity;
            _context.SaveChanges();

            return updatedUser;
        }
    }
}
