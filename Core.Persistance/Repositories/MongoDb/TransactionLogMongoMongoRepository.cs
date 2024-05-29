using Core.Application.Interfaces.Infrastructure.MongoDB;
using Core.Domain.Entities;
using MongoDB.Driver;

namespace Core.Persistance.Repositories.MongoDb;

public class TransactionLogMongoMongoRepository : MongoDbGenericRepository<TransactionLog>,
    ITransactionLogMongoRepository
{
    public TransactionLogMongoMongoRepository(IMongoDatabase database) : base(database)
    {
    }
}