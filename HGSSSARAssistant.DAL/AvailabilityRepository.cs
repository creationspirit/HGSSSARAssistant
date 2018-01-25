using HGSSSARAssistant.Core;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HGSSSARAssistant.DAL
{
    public class AvailabilityRepository : Repository<Availability>, IAvailabilityRepository, IDisposable
    {
        private ApplicationContext _context;

        private DbSet<Availability> _availabilityEntity;

        public AvailabilityRepository(ApplicationContext context) : base(context)
        {
            this._context = context;
            this._availabilityEntity = context.Set<Availability>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
