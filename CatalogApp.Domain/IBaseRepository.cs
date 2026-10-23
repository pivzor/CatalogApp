using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogApp.Domain
{
    public interface IBaseRepository<T> where T : class
    {
        List<T> GetAll();
        T? GetOneById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
