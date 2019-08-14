using System;
using System.Collections.Generic;
using System.Text;

namespace Model.EntityModels
{
   public class CustomerPolicyModel
    {
        public int Id { get; set; }
        public int PolicyId { get; set; }
        public int CustomerId { get; set; }
       
    }
}
