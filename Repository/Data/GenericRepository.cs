using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
      
        public IEnumerable <T> GetAll()
        {
            IEnumerable<T> query = _context.Set<T>();
            return query;
        }

       
    }
}
