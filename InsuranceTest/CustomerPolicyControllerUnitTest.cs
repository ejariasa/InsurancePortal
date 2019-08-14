using ApiSevices.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.EntityModels;
using Repository.Data;
using Repository.Entities;

namespace InsuranceTest
{
    [TestClass]
    public class CustomerPolicyUnitTest1
    {
        
        [TestMethod]
        public void GetCustomersPolicy()
        {
            CpeDbContext context = GetDbHelper.GetDbContext();
            CustomerPolicyRepository CustomerPolicyRepository = new CustomerPolicyRepository(context);
            var list = CustomerPolicyRepository.GetAll();
            Assert.IsNotNull(list);

        }

        [TestMethod]
        public void AddExistingValue()
        {
            CpeDbContext context = GetDbHelper.GetDbContext();
            CustomerPolicyRepository CustomerPolicyRepository = new CustomerPolicyRepository(context);
            CustomerPolicyModel customerPolicy = new CustomerPolicyModel()
            {
                CustomerId=1,
                PolicyId=1
            };
            CustomerPolicyRepository.Add(customerPolicy);
            Assert.Fail();

            

        }
    }
}
