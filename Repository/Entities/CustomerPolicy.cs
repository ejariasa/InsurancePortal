using System;
using System.Collections.Generic;

namespace Repository.Entities
{
    public partial class CustomerPolicy
    {
        public int Id { get; set; }
        public int PolicyId { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Policy Policy { get; set; }
    }
}
