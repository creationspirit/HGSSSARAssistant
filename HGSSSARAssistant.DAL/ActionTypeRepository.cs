using HGSSSARAssistant.Core;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HGSSSARAssistant.DAL
{
    public class ActionTypeRepository : Repository<ActionType>, IActionTypeRepository, IDisposable
    {
        private ApplicationContext _context;

        private DbSet<ActionType> _actionTypeEntity;

        public ActionTypeRepository(ApplicationContext context) : base(context)
        {
            this._context = context;
            this._actionTypeEntity = context.Set<ActionType>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public ActionType GetActionTypeByName(string name)
        {
            return _actionTypeEntity.SingleOrDefault(at => at.Name.Equals(name));
        }
    }
}
