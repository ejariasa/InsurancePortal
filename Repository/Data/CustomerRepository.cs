using Microsoft.EntityFrameworkCore;
using Model.EntityModels;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Data
{
    public class CustomerRepository : IGenericRepository<CustomerModel>
    {
        protected readonly CpeDbContext _context;

        public CustomerRepository(CpeDbContext context)
        {
            _context = context;
        }
        public void Add(CustomerModel entity)
        {
            MapperHelper<Customer, CustomerModel> utils = new MapperHelper<Customer, CustomerModel>();

            var dbEntity = _context.Set<Customer>().Add(utils.setValues((Customer)Activator.CreateInstance(typeof(Customer)), entity));
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var dbEntity = _context.Set<Customer>().Where(x => x.Id == id).FirstOrDefault();
            _context.Set<Customer>().Remove(dbEntity);
            _context.SaveChanges();
        }

        public List<CustomerModel> GetAll()
        {
            var list = _context.Set<Customer>().ToList();
            var requestList = new List<CustomerModel>();
            MapperHelper<CustomerModel, Customer> utils = new MapperHelper<CustomerModel, Customer>();

            list.ForEach(x => requestList.Add(utils.setValues((CustomerModel)Activator.CreateInstance(typeof(CustomerModel)), x)));

            return requestList;
        }

        public void Update(CustomerModel entity)
        {
            MapperHelper<Customer, CustomerModel> utils = new MapperHelper<Customer, CustomerModel>();
            var dbEntity = _context.Set<Customer>().Where(x => x.Id == entity.Id).FirstOrDefault();
            utils.setValues(dbEntity, entity);
            _context.Entry(dbEntity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
