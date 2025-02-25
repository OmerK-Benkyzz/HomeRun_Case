using System.Linq.Expressions;
using Core.Application.Interfaces.Infrastructure.PostgreSQL;
using Core.Application.Interfaces.Services;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Persistance.Repositories.PostgreSQL;

public interface IAsyncRepository<T> : IQuery<T> where T : BaseEntity
{
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);

    Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int index = 0, int size = 10, bool enableTracking = true,
        CancellationToken cancellationToken = default);

    Task<IPaginate<T>> GetListByDynamicAsync(Dynamic.Dynamic dynamic,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int index = 0, int size = 10, bool enableTracking = true,
        CancellationToken cancellationToken = default);

    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
    
    Task BulkUpdateAsync(Expression<Func<T, bool>> predicate, Action<T> updateAction, CancellationToken cancellationToken = default);
}