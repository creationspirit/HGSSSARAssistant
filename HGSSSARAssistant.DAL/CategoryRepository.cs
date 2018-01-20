using HGSSSARAssistant.Core;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HGSSSARAssistant.DAL
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository, IDisposable
    {
        private ApplicationContext _context;

        private DbSet<Category> _categoryEntity;

        public CategoryRepository(ApplicationContext context) : base(context)
        {
            this._context = context;
            this._categoryEntity = context.Set<Category>();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Category GetCategoryByName(string name)
        {
            return _categoryEntity.SingleOrDefault(c => c.Name.Equals(name));
        }
    }
}
