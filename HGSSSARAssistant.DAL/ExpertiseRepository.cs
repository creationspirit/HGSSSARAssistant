using HGSSSARAssistant.Core;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HGSSSARAssistant.DAL
{
    public class ExpertiseRepository : Repository<Expertise>, IExpertiseRepository, IDisposable
    {
        private ApplicationContext _context;

        private DbSet<Expertise> _expertiseEntity;

        public ExpertiseRepository(ApplicationContext context) : base(context)
        {
            this._context = context;
            this._expertiseEntity = context.Set<Expertise>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Expertise GetExpertiseByName(string name)
        {
            return _expertiseEntity.SingleOrDefault(e => e.Name.Equals(name));
        }
    }
}
