using ECommerceBackendSystem.Application.Abstractions.Services.Base;
using ECommerceBackendSystem.Domain.Repositories;
using ECommerceBackendSystem.Domain.Shared.Base;

namespace ECommerceBackendSystem.Application.Base;

public class GenericQueryService<T>(IGenericRepository<T> repository) : IGenericQueryService<T> where T : EntityBase, new()
{
    private readonly IGenericRepository<T> _repository = repository;

    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await this._repository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await this._repository.GetAllAsync(cancellationToken).ConfigureAwait(false);
}
