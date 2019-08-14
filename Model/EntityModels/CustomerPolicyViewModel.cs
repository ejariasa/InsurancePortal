using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityModels
{
    public class CustomerPolicyViewModel
    {
        public int Id { get; set; }
        public CustomerModel Customer { get; set; }
        public PolicyModel Policy { get; set; }
        public bool IsSelected { get; set; }
    }
}
