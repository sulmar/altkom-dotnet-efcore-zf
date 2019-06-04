using System;
using Altkom.ZF.Models;
using System.Collections.Generic;

namespace Altkom.ZF.IServices
{
    public interface ICustomersService
    {
        IEnumerable<Customer> Get();
        void Add(Customer customer);
        
    }
    
}
