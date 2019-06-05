using System;
using System.Collections.Generic;

namespace Altkom.ZF.Models
{

    public class OrderHeader
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
      
    }
}