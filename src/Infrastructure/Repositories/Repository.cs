using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _ctx;
        public Repository(AppDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(T entity) { _ctx.Add(entity); await Task.CompletedTask; }
        public async Task DeleteAsync(T entity) { _ctx.Remove(entity); await Task.CompletedTask; }
        public async Task<T?> GetByIdAsync(Guid id) => await _ctx.FindAsync<T>(id);
        public async Task UpdateAsync(T entity) { _ctx.Update(entity); await Task.CompletedTask; }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T>? spec = null)
        {
            IQueryable<T> query = _ctx.Set<T>();
            if (spec?.Criteria != null) query = query.Where(spec.Criteria);
            if (spec?.OrderBy != null) query = spec.OrderBy(query);
            if (spec?.Skip != null) query = query.Skip(spec.Skip.Value);
            if (spec?.Take != null) query = query.Take(spec.Take.Value);
            return await query.ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T>? spec = null)
        {
            IQueryable<T> query = _ctx.Set<T>();
            if (spec?.Criteria != null) query = query.Where(spec.Criteria);
            return await query.CountAsync();
        }
    }
}