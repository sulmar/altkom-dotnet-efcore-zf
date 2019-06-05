using System;
using Altkom.ZF.Models;
using Bogus;

namespace Altkom.ZF.FakeServices
{
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker()
        {
            Randomizer.Seed = new Random(1234);

            StrictMode(true);
            Ignore(p => p.Id);
            // RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.IsDeleted, f => f.Random.Bool(0.2f));
            RuleFor(p => p.Salary, f => f.Random.Decimal(1000, 5000));
            Ignore(p => p.ShippingAddress);


        }
    }
}