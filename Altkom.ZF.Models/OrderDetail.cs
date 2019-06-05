using System;

namespace Altkom.ZF.Models
{
    public class OrderDetail : BaseEntity
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}