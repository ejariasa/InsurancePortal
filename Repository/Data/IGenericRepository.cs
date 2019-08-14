using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.Data
{

    public interface IGenericRepository<T> where T : class
    {

        List<T> GetAll();       
        void Add(T entity);
        void Delete(int id);
        void Update(T entity);
       
        

    }

}
