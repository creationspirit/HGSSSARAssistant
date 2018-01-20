using HGSSSARAssistant.Core;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HGSSSARAssistant.DAL
{
    public class LocationRepository : Repository<Location>, ILocationRepository, IDisposable
    {
        private ApplicationContext _context;

        private DbSet<Location> _locationEntity;

        public LocationRepository(ApplicationContext context) : base(context)
        {
            this._context = context;
            this._locationEntity = context.Set<Location>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Location GetLocationByName(string name)
        {
            return _locationEntity.SingleOrDefault(l => l.Name.Equals(name));
        }
    }
}
