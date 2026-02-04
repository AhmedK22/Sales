using System;
using System.Collections.Generic;

namespace WebUI.Models
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public decimal SubTotal { get; set; }
    }

    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
