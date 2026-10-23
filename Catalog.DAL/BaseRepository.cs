using Catalog.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogApp.Domain
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly AppDbContext _db;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _db = context;
            _dbSet = context.Set<T>();
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T? GetOneById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _db.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            T? entity = _dbSet.Find(id);

            if (entity != null)
            {
                _dbSet.Remove(entity);
                _db.SaveChanges();
            }
        }
    }
}
