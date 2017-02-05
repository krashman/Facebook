using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Facebook.Data
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private static readonly ConcurrentDictionary<string, T> _items =
            new ConcurrentDictionary<string, T>();
        

        public IEnumerable<T> GetAll()
        {
            return _items.Values;
        }

        public virtual void Add(T item)
        {
            throw new NotImplementedException();
        }

        public virtual T Find(string key)
        {
            throw new NotImplementedException();
        }

        public virtual T Remove(string key)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T item)
        {
            throw new NotImplementedException();

        }
    }
}
