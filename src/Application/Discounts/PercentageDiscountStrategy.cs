using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Discounts
{
    public class PercentageDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal _percent;
        public PercentageDiscountStrategy(decimal percent = 0.1m) => _percent = percent;
        public decimal Calculate(Order order) => Math.Round(order.SubTotal * _percent, 2);
    }

}
