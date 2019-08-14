using Microsoft.AspNetCore.Mvc.Rendering;
using Model.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsurancePortal.Models
{
    public class PolicyViewModel
    {
        public List<SelectListItem> CoverageTypeList { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Earthquake", Text = "Earthquake" },
            new SelectListItem { Value = "Theft", Text = "Theft" },
            new SelectListItem { Value = "Fire", Text = "Fire" },
            new SelectListItem { Value = "Lost", Text = "Lost" },
            new SelectListItem { Value = "Other", Text = "Other" },


        };
        public List<SelectListItem> RiskTypeList { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Low", Text = "Low" },
            new SelectListItem { Value = "Medium", Text = "Medium" },
            new SelectListItem { Value = "MediumHigh", Text = "MediumHigh" },
            new SelectListItem { Value = "High", Text = "High" },
        };
        public PolicyModel Policy { get; set; }

    }
}
