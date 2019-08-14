using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityModels
{
    public class PolicyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CoverageType { get; set; }
        public DateTime StartDate { get; set; }
        public int? CoveragePeriod { get; set; }
        public decimal? CoveragePecentage { get; set; }
        public decimal? Price { get; set; }
        public string RiskType { get; set; }
    }
}
