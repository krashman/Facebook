using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Facebook.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FacebookDatabaseContext databaseContext;
        private readonly DbSet<T> entities;
        private string errorMessage = string.Empty;

        public Repository(FacebookDatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
            entities = databaseContext.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T Get(Guid id)
        {
            throw new NotImplementedException();
            //return entities.SingleOrDefault(s => s.Id == id);
        }

        public void Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entities.Add(entity);
            databaseContext.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            databaseContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entities.Remove(entity);
            databaseContext.SaveChanges();
        }

        public void Remove(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entities.Remove(entity);
        }

        public void SaveChanges()
        {
            databaseContext.SaveChanges();
        }
    }
}