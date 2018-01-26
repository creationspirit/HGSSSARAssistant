using HGSSSARAssistant.Core;
using HGSSSARAssistant.Core.Repositories;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using HGSSSARAssistant.DAL.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query;

namespace HGSSSARAssistant.DAL
{
    public class UserRepository : Repository<User>, IUserRepository, IDisposable
    {
        private ApplicationContext _context;

        private IIncludableQueryable<User, Station> _userEntity;
        
        public UserRepository(ApplicationContext context) : base(context)
        {
            this._context = context;
            this._userEntity = context.Users
                .Include(u => u.Category)
                .Include(u => u.Role)
                .Include(u => u.Address)
                .Include(u => u.Availiabilities).ThenInclude(a => a.Location)
                .Include(u => u.Station);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public override User GetById(long Id)
        {
            return _userEntity.SingleOrDefault(u => u.Id == Id);
        }

        public override IEnumerable<User> GetAll()
        {
            return _userEntity.AsEnumerable();
        }

        public IEnumerable<User> GetAvailableUsers(DateTime time)
        {
            return _userEntity.AsEnumerable();
        }

        public IEnumerable<User> GetAvailableUsers(Availability availability)
        {
            return _userEntity.AsEnumerable();
        }


        public IEnumerable<User> GetUsersByCategory(Category category)
        {
            return _userEntity.Where(user => user.Category.Equals(category));
        }

        public IEnumerable<User> GetUsersByExpertise(Expertise expertise)
        {
            // TODO: implement
            return _userEntity.AsEnumerable();
        }

        public IEnumerable<User> GetUsersByName(string name)
        {
            return _userEntity.Where(user => user.FirstName.Equals(name) || user.LastName.Equals(name));
        }

        public IEnumerable<User> GetUsersByRole(Role role)
        {
            return _userEntity.Where(user => user.Role.Equals(role));
        }

        public IEnumerable<User> GetUsersByStation(Station station)
        {
            return _userEntity.Where(user => user.Station.Equals(station));            
        }

        public User GetUserByEmail(string email)
        {
            return _userEntity.Single(user => user.Email.Equals(email));
        }

        public IEnumerable<Availability> GetAvailabilitiesByUser(long id)
        {            
                return _userEntity.Single(u => u.Id == id).Availiabilities;
        }
    }
}
