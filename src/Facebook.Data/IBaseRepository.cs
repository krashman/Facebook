using System.Collections.Generic;

namespace Facebook.Data
{
    public interface IBaseRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Add(T item);
        T Find(string key);
        T Remove(string key);
        void Update(T item);
    }
}