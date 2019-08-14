using System;
using System.Collections.Generic;

namespace Repository.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerPolicy = new HashSet<CustomerPolicy>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string IdentifyNumber { get; set; }

        public virtual ICollection<CustomerPolicy> CustomerPolicy { get; set; }
    }
}
