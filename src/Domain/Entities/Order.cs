
using System;
using System.Collections.Generic;
namespace Domain.Entities
{
    public class Order
    {
        private Order() { }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid CustomerId { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public List<OrderItem> Items { get; private set; } = new();
        public decimal Discount { get; private set; }
        public decimal Total => Math.Max(0, SubTotal - Discount);
        public decimal SubTotal => Items == null ? 0 : Items.Sum(i => i.UnitPrice * i.Quantity);

        public Order(Guid customerId, IEnumerable<OrderItem> items)
        {
            CustomerId = customerId;
            Items = new List<OrderItem>(items);
        }

        public void ApplyDiscount(decimal amount) => Discount = amount;
        public void AddItem(OrderItem item) => Items.Add(item);
    }
}

