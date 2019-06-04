using System.Collections.Generic;
using System.Linq;
using Altkom.ZF.IServices;
using Altkom.ZF.Models;

namespace Altkom.ZF.DbServices
{
    public class DbCustomersService : ICustomersService
    {
        private readonly MyContext context;

        public DbCustomersService(MyContext context)
        {
            this.context = context;
        }
        public void Add(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
        }
        public IEnumerable<Customer> Get()
        {
            return context.Customers.ToList();
        }
    }

}