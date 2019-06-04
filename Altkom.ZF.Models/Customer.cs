using System;

namespace Altkom.ZF.Models
{
    public class Address 
    {
        public string City { get; set; }

        public string Country { get; set; }

        public string Street { get; set; }
    }

    public class Customer : BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public Address ShippingAddress { get; set; }

        public bool IsDeleted { get; set; }
    }
}