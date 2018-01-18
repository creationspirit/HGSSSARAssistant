using System;
using System.Collections.Generic;
using System.Text;

namespace HGSSSARAssistant.Core.Repositories
{
    public interface IRepository <T> where T : Entity
    {
        IEnumerable<T> GetAll();
        T GetById(long Id);
        T Insert(T entity);
        void Delete(long Id);
        T Update(T entity);
        void Save();
        bool Exists(long id);
    }
}
