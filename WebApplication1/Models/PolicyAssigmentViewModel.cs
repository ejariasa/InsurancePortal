using Model.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsurancePortal.Models
{
    public class PolicyAssigmentViewModel
    {
        public PolicyAssigmentViewModel()
        {
            Assigments = new List<CustomerPolicyViewModel>();
        }
        public string Name { get; set; }
        public List<CustomerPolicyViewModel> Assigments { get; set; }

    }
}
