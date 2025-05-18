using ECommerceBackendSystem.Domain.Shared.Base;

namespace ECommerceBackendSystem.Application.Abstractions.Services.Base;

public interface IGenericQueryService<T> where T : EntityBase, new()
{
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
}
