using HGSSSARAssistant.Core;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HGSSSARAssistant.DAL
{
    public class ActionRepository : Repository<Core.Action>, IActionRepository, IDisposable
    {
        private ApplicationContext _context;

        private DbSet<Core.Action> _actionEntity;

        public ActionRepository(ApplicationContext context) : base(context)
        {
            this._context = context;
            this._actionEntity = context.Set<Core.Action>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Core.Action GetActionByName(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
