using HGSSSARAssistant.Core;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HGSSSARAssistant.DAL
{
    public class StationRepository : Repository<Station>, IStationRepository, IDisposable
    {
        private ApplicationContext _context;

        private DbSet<Station> _stationEntity;

        public StationRepository(ApplicationContext context) : base(context)
        {
            this._context = context;
            this._stationEntity = context.Set<Station>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Station GetStationByName(string name)
        {
            return _stationEntity.SingleOrDefault(s => s.Name.Equals(name));
        }
    }
}
