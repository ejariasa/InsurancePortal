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
    public class PolicyController : ControllerBase
    {
        protected readonly PolicyRepository PolicyRepository;
        public PolicyController(PolicyRepository policyRepository)
        {
            this.PolicyRepository = policyRepository;
        }

        // GET: api/Policy
        [HttpGet]
        public List<PolicyModel> GetAll()
        {
            return this.PolicyRepository.GetAll();
        }

        // GET: api/Policy/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Policy
        [HttpPost]
        [Route("AddPolicy")]
        public int Add([FromBody] PolicyModel policy)
        {
            this.PolicyRepository.Add(policy);
            return 0;
        }

        [HttpPost]
        [Route("UpdatePolicy")]
        public int Update([FromBody] PolicyModel policy)
        {
            this.PolicyRepository.Update(policy);
            return 0;
        }

        // DELETE: api/ApiWithActions/5
        [Route("DeletePolicy")]
        public int Delete(int id)
        {
            this.PolicyRepository.Delete(id);
            return id;
        }
    }
}
