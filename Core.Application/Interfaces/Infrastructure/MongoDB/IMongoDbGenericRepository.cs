using Core.Domain.Entities;

namespace Core.Application.Interfaces.Infrastructure.MongoDB;

public interface IMongoDbGenericRepository<T> where T : MongoDbBaseEntity
{
    Task<T> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(string id, T entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default);
}