using System;
namespace HGSSSARAssistant.Core.Repositories
{
    public interface IUserRepository
    {
        User GetUserById(long id);

        User[] GetUsersByName(String name);
        User[] GetUsersByExpertise(Expertise expertise);
        User[] GetUsersByRole(Role role);
        User[] GetUsersByCategory(Category category);
        User[] GetUsersByStation(Station station);

        User[] GetAvailableUsers(DateTime time);
        User[] GetAvailableUsers(Availability availability);


        void AddUser(User user);
        void DeleteUser(User user);
        void UpdateUser(User user);
    }
}
