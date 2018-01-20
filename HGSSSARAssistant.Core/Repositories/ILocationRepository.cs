using System;
using System.Collections.Generic;
using System.Text;

namespace HGSSSARAssistant.Core.Repositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        Location GetLocationByName(String name);
    }
}
