using Domain.Entities;

namespace Application.Discounts
{
    public class FixedAmountDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal _amount;
        public FixedAmountDiscountStrategy(decimal amount) => _amount = amount;

        public decimal Calculate(Order order) => _amount <= 0 ? 0m : Math.Min(_amount, order.SubTotal);
    }
}
