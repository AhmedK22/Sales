using Domain.Entities;
using Application.Interfaces;
using Domain.Specifications;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IRepository<Order>
    {
        public OrderRepository(AppDbContext ctx) : base(ctx) { }
    }
}