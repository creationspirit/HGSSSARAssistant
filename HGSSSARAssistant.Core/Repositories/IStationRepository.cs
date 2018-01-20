using System;

namespace HGSSSARAssistant.Core.Repositories
{
    public interface IStationRepository : IRepository<Station>
    {
        Station GetStationByName(String name);
    }
}
