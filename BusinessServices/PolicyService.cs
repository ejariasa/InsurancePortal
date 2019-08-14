using Model.EntityModels;
using Repository.Data;
using Repository.Entities;
using System.Collections.Generic;

namespace BusinessServices
{
    public class PolicyService
    {
        protected readonly PolicyRepository PolicyRepository;

        public PolicyService(PolicyRepository policyRepository)
        {
            this.PolicyRepository = policyRepository;
        }

        public List<PolicyModel> GetAll()
        {
            return this.PolicyRepository.GetAll();
        }

        public void Add(PolicyModel policy)
        {
            this.PolicyRepository.Add(policy);
        }

        public void Delete(PolicyModel policy)
        {
            this.PolicyRepository.Delete(policy);
        }

        public void Update(PolicyModel policy)
        {
            this.PolicyRepository.Update(policy);
        }
    }
}
