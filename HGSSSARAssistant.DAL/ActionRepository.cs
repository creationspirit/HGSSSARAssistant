using HGSSSARAssistant.Core;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;

namespace HGSSSARAssistant.DAL
{
    public class ActionRepository : Repository<Core.Action>, IActionRepository, IDisposable
    {
        private ApplicationContext _context;

        private IIncludableQueryable<Core.Action, ActionType> _actionEntity;

        public ActionRepository(ApplicationContext context) : base(context)
        {
            this._context = context;
            this._actionEntity = context.Actions
                .Include(a => a.Leader)
                .Include(a => a.Location)
                .Include(a => a.InvitedRescuers)
                .Include(a => a.AttendedRescuers)
                .Include(a => a.ActionType);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Core.Action GetActionByName(string name)
        {
            return _actionEntity.SingleOrDefault(a => a.Name.Equals(name));
        }
    }
}
