using System;
using System.Collections.Generic;
using System.Text;

namespace HGSSSARAssistant.Core.Repositories
{
    public interface IActionRepository : IRepository<Action>
    {
        Action GetActionByName(String name);
        Action GetActionByType(ActionType actionType);
    }
}
