using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
namespace Application.Factories
{
    public class OrderFactory
    {
        public Order Create(Guid customerId, IEnumerable<OrderItem> items)
        {
            if (items == null || !items.Any()) throw new ArgumentException("Order must have at least one item");
            var order = new Order(customerId, items);
            return order;
        }
    }
}