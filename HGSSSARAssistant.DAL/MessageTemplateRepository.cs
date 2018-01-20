using HGSSSARAssistant.Core;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HGSSSARAssistant.DAL
{
    public class MessageTemplateRepository : Repository<MessageTemplate>, IMessageTemplateRepository, IDisposable
    {
        private ApplicationContext _context;

        private DbSet<MessageTemplate> _messageTemplateEntity;

        public MessageTemplateRepository(ApplicationContext context) : base(context)
        {
            this._context = context;
            this._messageTemplateEntity = context.Set<MessageTemplate>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
