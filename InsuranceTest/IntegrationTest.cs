using ApiSevices.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.EntityModels;
using Repository.Data;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.AspNetCore.mvc

namespace InsuranceTest
{
    [TestClass]
    public class IntegrationTest
    {
        protected readonly CpeDbContext context;
        protected readonly CustomerRepository CustomerRepository;
        protected readonly CustomerPolicyRepository CustomerPolicyRepository;
        protected readonly PolicyRepository PolicyRepository;

        public IntegrationTest()
        {
            context = GetDbHelper.GetDbContext();
            PolicyRepository = new PolicyRepository(context);
            CustomerRepository = new CustomerRepository(context);
            CustomerPolicyRepository = new CustomerPolicyRepository(context);

        }

        [TestMethod]
        public void GetCustomersPolicy()
        {
            CustomerPolicyController customerPolicyController = new CustomerPolicyController(CustomerRepository, CustomerPolicyRepository, PolicyRepository);
            var list = customerPolicyController.GetCustomersPolicy(1);
            Assert.IsNotNull(list);
        }


    }
}
