using Core.Domain.Entities;

namespace Core.Application.Interfaces.Infrastructure.MongoDB;

public interface ITransactionLogMongoRepository: IMongoDbGenericRepository<TransactionLog>
{
    
}