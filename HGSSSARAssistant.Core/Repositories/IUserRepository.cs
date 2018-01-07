using System;
using System.Collections.Generic;

namespace HGSSSARAssistant.Core.Repositories
{
    public interface IUserRepository
    {
        User GetUserById(long id);   
        IEnumerable<User> GetUsersByName(String name);
        IEnumerable<User> GetUsersByExpertise(Expertise expertise);
        IEnumerable<User> GetUsersByRole(Role role);
        IEnumerable<User> GetUsersByCategory(Category category);
        IEnumerable<User> GetUsersByStation(Station station);
        IEnumerable<User> GetAvailableUsers(DateTime time);
        IEnumerable<User> GetAvailableUsers(Availability availability);
        void AddUser(User user);
        void DeleteUser(User user);
        User UpdateUser(User user);
    }
}
