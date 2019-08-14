using Microsoft.EntityFrameworkCore;
using Model.EntityModels;
using Repository.Data;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Data
{
    public class PolicyRepository : IGenericRepository<PolicyModel>
    {
        protected readonly CpeDbContext _context;

        public PolicyRepository(CpeDbContext context)
        {
            _context = context;
        }

        public void Add(PolicyModel entity)
        {
            MapperHelper<Policy, PolicyModel> utils = new MapperHelper<Policy, PolicyModel>();

            var dbEntity = _context.Set<Policy>().Add(utils.setValues((Policy)Activator.CreateInstance(typeof(Policy)), entity));
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
             var dbEntity = _context.Set<Policy>().Where(x=>x.Id==id).FirstOrDefault();
            _context.Set<Policy>().Remove(dbEntity);
            _context.SaveChanges();
        }

        public List<PolicyModel> GetAll()
        {
            var list = _context.Set<Policy>().ToList();
            var requestList = new List<PolicyModel>();
            MapperHelper<PolicyModel, Policy> utils = new MapperHelper<PolicyModel, Policy>();

            list.ForEach(x => requestList.Add(utils.setValues((PolicyModel)Activator.CreateInstance(typeof(PolicyModel)), x)));

            return requestList;
        }

        public void Update(PolicyModel entity)
        {
            MapperHelper<Policy, PolicyModel> utils = new MapperHelper<Policy, PolicyModel>();
            var dbEntity = _context.Set<Policy>().Where(x => x.Id == entity.Id).FirstOrDefault();
            utils.setValues(dbEntity, entity);
            _context.Entry(dbEntity).State = EntityState.Modified;
            _context.SaveChanges();

        }
    }
}
