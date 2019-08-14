using System;
using System.Collections.Generic;

namespace Repository.Entities
{
    public partial class Policy
    {
        public Policy()
        {
            CustomerPolicy = new HashSet<CustomerPolicy>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CoverageType { get; set; }
        public decimal? CoveragePecentage { get; set; }
        public DateTime StartDate { get; set; }
        public int? CoveragePeriod { get; set; }
        public decimal? Price { get; set; }
        public string RiskType { get; set; }

        public virtual ICollection<CustomerPolicy> CustomerPolicy { get; set; }
    }
}
