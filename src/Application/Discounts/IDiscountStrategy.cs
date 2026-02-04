using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Discounts
{
    public interface IDiscountStrategy
    {
        decimal Calculate(Order order);
    }
}
