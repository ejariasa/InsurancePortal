using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.EntityModels;
using Repository.Entities;
using Repository.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace InsuranceTest
{
    [TestClass]
    public class PolicyUnitTest1
    {
        protected readonly CpeDbContext context;
        protected readonly PolicyRepository PolicyRepository;
        public PolicyUnitTest1()
        {
            context = GetDbHelper.GetDbContext();
            PolicyRepository = new PolicyRepository(context);
        }

        [TestMethod]
        public void Get()
        {           
            var list = PolicyRepository.GetAll();
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void Add()        {
           
            PolicyModel policy = new PolicyModel()
            {
                Name="testName",
                Description="test description",
                Price=500,
                CoveragePecentage=20,
                CoveragePeriod=10,
                RiskType="Low",
                CoverageType="Fire",
                StartDate=DateTime.Now
            };
            PolicyRepository.Add(policy);
            var policyCreated = PolicyRepository.GetAll().FirstOrDefault(x => x.Name == "testName");
            Assert.IsNotNull(policyCreated);
        }

       
        [TestMethod]
        public void Update()
        {
            var policyCreated = PolicyRepository.GetAll().FirstOrDefault(x => x.Id == 1);

            policyCreated.Name = "Test2";
            PolicyRepository.Update(policyCreated);
            var policyUpdated = PolicyRepository.GetAll().FirstOrDefault(x => x.Name == "Test2");
            Assert.AreEqual(policyCreated.Id,policyUpdated.Id);

        }

        [TestMethod]
        public void Delete()
        {
            var policyCreated = PolicyRepository.GetAll().FirstOrDefault(x => x.Name == "testName");
            PolicyRepository.Delete(policyCreated.Id);
            var policyDeleted= PolicyRepository.GetAll().FirstOrDefault(x => x.Id == policyCreated.Id);
            Assert.IsNull(policyDeleted);

        }

      

    }
}
