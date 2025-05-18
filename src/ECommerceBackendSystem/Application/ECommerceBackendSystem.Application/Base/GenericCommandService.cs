using ECommerceBackendSystem.Application.Abstractions.Services.Base;
using ECommerceBackendSystem.Domain.Repositories;
using ECommerceBackendSystem.Domain.Shared.Base;

namespace ECommerceBackendSystem.Application.Base;

public class GenericCommandService<T>(IGenericRepository<T> repository) : IGenericCommandService<T> where T : EntityBase, new()
{
    private readonly IGenericRepository<T> _repository = repository;

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await this._repository.AddAsync(entity, cancellationToken).ConfigureAwait(false);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await this._repository.UpdateAsync(entity, cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await this._repository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

        await this._repository.DeleteAsync(entity);
    }
}
