using System;
using System.Collections.Generic;
using System.Text;

namespace HGSSSARAssistant.Core.Repositories
{
    public interface IActionTypeRepository : IRepository<ActionType>
    {
        ActionType GetActionTypeByName(String name);
    }
}
