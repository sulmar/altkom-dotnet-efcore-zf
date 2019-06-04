using System;

namespace Altkom.ZF.Models
{
    public class Customer : BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public bool IsDeleted { get; set; }
    }
}