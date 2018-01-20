using System;
using System.Collections.Generic;
using System.Text;

namespace HGSSSARAssistant.Core.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role GetRoleByName(String name);
    }
}
