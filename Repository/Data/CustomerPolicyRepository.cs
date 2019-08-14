using Microsoft.EntityFrameworkCore;
using Model.EntityModels;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Data
{
    public class CustomerPolicyRepository : IGenericRepository<CustomerPolicyModel>
    {
        protected readonly CpeDbContext _context;
        public CustomerPolicyRepository(CpeDbContext context)
        {
            _context = context;
        }

        public void Add(CustomerPolicyModel entity)
        {
            MapperHelper<CustomerPolicy, CustomerPolicyModel> utils = new MapperHelper<CustomerPolicy, CustomerPolicyModel>();

            var dbEntity = _context.Set<CustomerPolicy>().Add(utils.setValues((CustomerPolicy)Activator.CreateInstance(typeof(CustomerPolicy)), entity));
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var dbEntity = _context.Set<CustomerPolicy>().Where(x => x.Id == id).FirstOrDefault();
            _context.Set<CustomerPolicy>().Remove(dbEntity);
            _context.SaveChanges();
        }

        public List<CustomerPolicyModel> GetAll()
        {
            var list = _context.Set<CustomerPolicy>().ToList();
            var requestList = new List<CustomerPolicyModel>();
            MapperHelper<CustomerPolicyModel, CustomerPolicy> utils = new MapperHelper<CustomerPolicyModel, CustomerPolicy>();

            list.ForEach(x => requestList.Add(utils.setValues((CustomerPolicyModel)Activator.CreateInstance(typeof(CustomerPolicyModel)), x)));

            return requestList;
        }

        public void Update(CustomerPolicyModel entity)
        {
            throw new NotImplementedException(); MapperHelper<CustomerPolicy, CustomerPolicyModel> utils = new MapperHelper<CustomerPolicy, CustomerPolicyModel>();
            var dbEntity = _context.Set<CustomerPolicy>().Where(x => x.Id == entity.Id).FirstOrDefault();
            utils.setValues(dbEntity, entity);
            _context.Entry(dbEntity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
    
    
    
}
