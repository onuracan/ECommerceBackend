using ECommerceBackendSystem.Domain.Repositories;
using ECommerceBackendSystem.Domain.Shared.Base;
using ECommerceBackendSystem.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerceBackendSystem.Infrastructure.Persistence.Repositories;

public class GenericRepository<T>(ECommerceDbContext context) : IGenericRepository<T> where T : EntityBase, new()
{
    private readonly ECommerceDbContext _context = context;

    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await this._context.Set<T>().FindAsync(id, cancellationToken).ConfigureAwait(false);

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await this._context.Set<T>().ToListAsync(cancellationToken).ConfigureAwait(false);

    public IQueryable<T> GetQueryable(Expression<Func<T, bool>> predicate)
        => this._context.Set<T>().Where(predicate).AsQueryable();

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await this._context.AddAsync(entity, cancellationToken).ConfigureAwait(false);

        await this._context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        this._context.Update(entity);

        await this._context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        this._context.Remove(entity);

        await this._context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
}
