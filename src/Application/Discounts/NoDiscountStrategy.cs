using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Discounts
{
    public class NoDiscountStrategy : IDiscountStrategy
    {
        public decimal Calculate(Order order) => 0m;
    }
}
