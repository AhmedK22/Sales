using Application.Orders.Commands;

namespace Application.Discounts
{
    public class DiscountStrategyFactory
    {
        public IDiscountStrategy Create(string? name)
        {
            var key = (name ?? "None").Trim().ToLowerInvariant();
            return key switch
            {
                "none" => new NoDiscountStrategy(),
                "percent10" => new PercentageDiscountStrategy(0.10m),
                "percent20" => new PercentageDiscountStrategy(0.20m),
                "fixed5" => new FixedAmountDiscountStrategy(5m),
                "fixed10" => new FixedAmountDiscountStrategy(10m),
                _ => new NoDiscountStrategy()
            };
        }
    }
}
