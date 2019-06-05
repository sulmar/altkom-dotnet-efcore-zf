using System;
using Altkom.ZF.IServices;
using Altkom.ZF.Models;
using System.Collections.Generic;

namespace Altkom.ZF.FakeServices
{
    public class FakeCustomersService : ICustomersService
    {
        private readonly ICollection<Customer> customers;

        public FakeCustomersService() : this(new CustomerFaker())
        {

        }

        public FakeCustomersService(CustomerFaker customerFaker)
        {
            customers = customerFaker.Generate(1000);
        }


        public void Add(Customer customer)
        {
            customers.Add(customer);
        }

        public IEnumerable<Customer> Get()
        {
            return customers;
        }
      
    }
}
