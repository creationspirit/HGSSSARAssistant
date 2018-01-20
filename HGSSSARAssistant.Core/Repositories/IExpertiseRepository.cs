using System;
using System.Collections.Generic;
using System.Text;

namespace HGSSSARAssistant.Core.Repositories
{
    public interface IExpertiseRepository : IRepository<Expertise>
    {
        Expertise GetExpertiseByName(String name);
    }
}
