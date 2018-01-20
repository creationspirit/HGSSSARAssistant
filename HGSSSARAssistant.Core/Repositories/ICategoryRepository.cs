using System;
using System.Collections.Generic;
using System.Text;

namespace HGSSSARAssistant.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Category GetCategoryByName(String name);
    }
}
