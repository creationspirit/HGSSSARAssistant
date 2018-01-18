
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

        public IEnumerable<User> GetAvailableUsers(DateTime time)
        {
            return _repository.GetAvailableUsers(time);
        }

        public IEnumerable<User> GetAvailableUsers(Availability availability)
        {
            return _repository.GetAvailableUsers(availability);
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
            return (IEnumerable<User>) _repository.GetUsersByName(name);
        }

        public IEnumerable<User> GetUsersByRole(Role role)
        {
            return _repository.GetUsersByRole(role);
        }

        public IEnumerable<User> GetUsersByStation(Station station)
        {
            return _repository.GetUsersByStation(station);
        }
    }
}
