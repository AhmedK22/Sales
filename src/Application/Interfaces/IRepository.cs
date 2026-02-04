using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Specifications;
namespace Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T>? spec = null);
        Task<int> CountAsync(ISpecification<T>? spec = null);
    }
}