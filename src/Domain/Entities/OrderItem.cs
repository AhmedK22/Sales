using System;
namespace Domain.Entities
{
    public class OrderItem
    {
        private OrderItem() { }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        public OrderItem(string productName, decimal unitPrice, int quantity)
        {
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }
    }
}
