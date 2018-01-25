using System;
using System.Collections.Generic;

namespace HGSSSARAssistant.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        IEnumerable<User> GetUsersByName(String name);
        IEnumerable<User> GetUsersByExpertise(Expertise expertise);
        IEnumerable<User> GetUsersByRole(Role role);
        IEnumerable<User> GetUsersByCategory(Category category);
        IEnumerable<User> GetUsersByStation(Station station);
        IEnumerable<User> GetAvailableUsers(DateTime time);
        IEnumerable<User> GetAvailableUsers(Availability availability);
        User GetUserByEmail(String email);
        IEnumerable<Availability> GetAvailabilitiesByUser(long id);
    }
}
