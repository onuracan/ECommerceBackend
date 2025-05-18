using ECommerceBackendSystem.Domain.Shared.Base;
using System.Linq.Expressions;

namespace ECommerceBackendSystem.Domain.Repositories;

public interface IGenericRepository<T> where T : EntityBase, new()
{
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    IQueryable<T> GetQueryable(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}
