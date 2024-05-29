using Core.Application.Interfaces.Infrastructure.MongoDB;
using Core.Domain.Entities;
using MongoDB.Driver;

namespace Core.Persistance.Repositories.MongoDb
{
    public abstract class MongoDbGenericRepository<T> : IMongoDbGenericRepository<T> where T : MongoDbBaseEntity
    {
        protected readonly IMongoCollection<T> Collection;

        protected MongoDbGenericRepository(IMongoDatabase database)
        {
            Collection = database.GetCollection<T>($"{typeof(T).Name.ToLower()}");
        }

        protected MongoDbGenericRepository(IMongoDatabase database, string collectionName)
        {
            Collection = database.GetCollection<T>(collectionName);
        }

        public virtual Task<T> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            return Collection.Find<T>(c => c.Id == id).FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public virtual async Task<T> InsertAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
            return entity;
        }

        public virtual async Task<T> UpdateAsync(string id, T entity, CancellationToken cancellationToken = default)
        {
            await Collection.ReplaceOneAsync(e => e.Id == id, entity, cancellationToken: cancellationToken);
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Collection.DeleteOneAsync(e => e.Id == entity.Id, cancellationToken: cancellationToken);
            return true;
        }
    }
}