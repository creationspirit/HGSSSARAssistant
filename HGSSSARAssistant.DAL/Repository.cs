using HGSSSARAssistant.Core;
using HGSSSARAssistant.Core.Repositories;
using HGSSSARAssistant.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HGSSSARAssistant.DAL
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private ApplicationContext _context;

        private DbSet<T> _entitySet;

        public Repository(ApplicationContext context)
        {
            this._context = context;
            this._entitySet = context.Set<T>();
        }

        public void Delete(long Id)
        {
            T entity = GetById(Id);
            _entitySet.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _entitySet.AsEnumerable();
        }

        public T GetById(long Id)
        {
            return _entitySet.SingleOrDefault(e => e.Id == Id);
        }

        public T Insert(T entity)
        {
            T resultEntity = _entitySet.Add(entity).Entity;

            return resultEntity;
        }
        public T Update(T entity)
        {
            T updatedEntity = _entitySet.Update(entity).Entity;

            return updatedEntity;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool Exists(long id)
        {
            return _entitySet.Any(e => e.Id == id);
        }   
    }
}
