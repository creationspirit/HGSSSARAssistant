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
    public class UserRepository : Repository<User>, IUserRepository, IDisposable
    {
        private ApplicationContext _context;

        private DbSet<HGSSSARAssistant.Core.User> _userEntity;

        public UserRepository(ApplicationContext context) : base(context)
        {
            this._context = context;
            this._userEntity = context.Set<HGSSSARAssistant.Core.User>();
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


        public IEnumerable<User> GetUsersByCategory(Category category)
        {
            return _userEntity.Where(user => user.Category.Equals(category));
        }

        public IEnumerable<User> GetUsersByExpertise(Expertise expertise)
        {
            return _userEntity.Where(user => user.Expertise.Contains(expertise));
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
    }
}
