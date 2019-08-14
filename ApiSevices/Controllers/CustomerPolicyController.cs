using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.EntityModels;
using Repository.Data;

namespace ApiSevices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPolicyController : ControllerBase
    {
        protected readonly CustomerRepository CustomerRepository;
        protected readonly CustomerPolicyRepository CustomerPolicyRepository;
        protected readonly PolicyRepository PolicyRepository;
        public CustomerPolicyController(CustomerRepository customerRepository, CustomerPolicyRepository customerPolicyRepository, PolicyRepository policyRepository)
        {
            this.CustomerRepository = customerRepository;
            this.CustomerPolicyRepository = customerPolicyRepository;
            this.PolicyRepository = policyRepository;
        }
        // GET: api/Customer
        [HttpGet]
        [Route("GetCustomersPolicy")]
        public List<CustomerPolicyViewModel> GetCustomersPolicy(int idPolicy)
        {
            var policy = this.PolicyRepository.GetAll().FirstOrDefault(x => x.Id == idPolicy);
            if (policy != null)
            {
                List<CustomerPolicyViewModel> customerPolicies = new List<CustomerPolicyViewModel>();
                List<CustomerModel> customers = this.CustomerRepository.GetAll();
                customers.ForEach(x =>
                {
                    CustomerPolicyViewModel CustomerPolicyViewModel = new CustomerPolicyViewModel();
                    CustomerPolicyViewModel.Customer = x;
                    CustomerPolicyViewModel.Policy = policy;
                    var assigment = this.CustomerPolicyRepository.GetAll().FirstOrDefault(y => y.PolicyId == policy.Id && y.CustomerId == x.Id);
                    if (assigment != null)
                    {
                        CustomerPolicyViewModel.Id = assigment.Id;
                        CustomerPolicyViewModel.IsSelected = true;
                    }
                    customerPolicies.Add(CustomerPolicyViewModel);
                });
                return customerPolicies;
            }
            return null;
        }



        // POST: api/Customer
        [HttpPost]
        [Route("AssignPolicy")]
        public int AssignPolicy([FromBody] List<CustomerPolicyViewModel> value)
        {
            if (value != null)
            {
                var insertItems = value.Where(x => x.Id == 0 && x.IsSelected).ToList();
                insertItems.ForEach(x =>
                {
                    CustomerPolicyModel customerPolicy = new CustomerPolicyModel()
                    {
                        CustomerId = x.Customer.Id,
                        PolicyId = x.Policy.Id
                    };
                    this.CustomerPolicyRepository.Add(customerPolicy);
                });

                var deleteItems = value.Where(y => y.Id != 0 && !y.IsSelected).ToList();
                deleteItems.ForEach(x=>
                {
                    this.CustomerPolicyRepository.Delete(x.Id);
                });
            }
            return 0;
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
