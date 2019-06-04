using System;

namespace Altkom.ZF.Models
{
    public class Order : BaseEntity
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public Customer Customer { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; }
    }

    public enum OrderStatus
    {
        Draft,
        InProgress,
        Delivered,
        Canceled
    }
}