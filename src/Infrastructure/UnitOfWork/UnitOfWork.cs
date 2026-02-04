using System.Threading.Tasks;
using Application.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _ctx;
        public UnitOfWork(AppDbContext ctx) => _ctx = ctx;
        public async Task<int> SaveChangesAsync() => await _ctx.SaveChangesAsync();
    }
}