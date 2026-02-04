using Application.Discounts;
using Application.Factories;
using Application.Interfaces;
using Application.Orders.Commands;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Orders.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IRepository<Order> _orderRepo;
        private readonly IUnitOfWork _uow;
        private readonly OrderFactory _factory;
        private readonly DiscountStrategyFactory _discountFactory;

        public CreateOrderHandler(IRepository<Order> orderRepo, IUnitOfWork uow, OrderFactory factory, DiscountStrategyFactory discountFactory)
        {
            _orderRepo = orderRepo;
            _uow = uow;
            _factory = factory;
            _discountFactory = discountFactory;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var items = request.Items.ConvertAll(i => new OrderItem(i.ProductName, i.UnitPrice, i.Quantity));
            var order = _factory.Create(request.CustomerId, items);

            var strategy = _discountFactory.Create(request.DiscountStrategy);
            var discount = strategy.Calculate(order);
            order.ApplyDiscount(discount);

            await _orderRepo.AddAsync(order);
            await _uow.SaveChangesAsync();
            return order.Id;
        }
    }
}
