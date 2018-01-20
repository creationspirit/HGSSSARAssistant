using HGSSSARAssistant.Core;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HGSSSARAssistant.DAL
{
    public class RoleRepository : Repository<Role>, IRoleRepository, IDisposable
    {
        private ApplicationContext _context;

        private DbSet<Role> _roleEntity;

        public RoleRepository(ApplicationContext context) : base(context)
        {
            this._context = context;
            this._roleEntity = context.Set<Role>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Role GetRoleByName(string name)
        {
            return _roleEntity.SingleOrDefault(r => r.Name.Equals(name));
        }
    }
}
