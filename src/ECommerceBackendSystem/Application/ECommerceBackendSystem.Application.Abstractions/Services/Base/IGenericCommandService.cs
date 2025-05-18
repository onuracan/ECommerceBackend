using ECommerceBackendSystem.Domain.Shared.Base;

namespace ECommerceBackendSystem.Application.Abstractions.Services.Base;

public interface IGenericCommandService<T> where T : EntityBase, new()
{
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
