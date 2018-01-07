
using HGSSSARAssistant.BLL.BusinessEntities;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.DAL;
using HGSSSARAssistant.DAL.EF;
using System;
using System.Collections.Generic;

namespace HGSSSARAssistant.BLL
{
    public class UserBLL
    {
        private UserRepository _repository;

        public UserBLL(UserContext context)
        {
            // TODO
            _repository = new UserRepository(context);
        }

        public void AddUser(User user)
        {
            // TODO
            _repository.AddUser(user);
        }

        public void DeleteUser(User user)
        {
            // TODO
            _repository.DeleteUser(user);
        }

        public void DeleteUser(long id)
        {
            _repository.DeleteUser(id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _repository.GetAllUsers();
        }

        public IEnumerable<User> GetAvailableUsers(DateTime time)
        {
            return _repository.GetAvailableUsers(time);
        }

        public IEnumerable<User> GetAvailableUsers(Availability availability)
        {
            return _repository.GetAvailableUsers(availability);
        }

        public User GetUserById(long id)
        {
            return _repository.GetUserById(id);
        }

        public IEnumerable<User> GetUsersByCategory(Category category)
        {
            return _repository.GetUsersByCategory(category);
        }

        public IEnumerable<User> GetUsersByExpertise(Expertise expertise)
        {
            return _repository.GetUsersByExpertise(expertise);
        }

        public IEnumerable<User> GetUsersByName(string name)
        {
            return _repository.GetUsersByName(name);
        }

        public IEnumerable<User> GetUsersByRole(Role role)
        {
            return _repository.GetUsersByRole(role);
        }

        public IEnumerable<User> GetUsersByStation(Station station)
        {
            return _repository.GetUsersByStation(station);          
        }

        public User UpdateUser(User user)
        {
            return _repository.UpdateUser(user);
        }

        public bool UserExists(long id)
        {
            return _repository.UserExists(id);
        }
    }
}
