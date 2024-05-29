using Core.Application.Interfaces.Infrastructure.MongoDB;
using Core.Application.Interfaces.Services;
using Core.Domain.Entities;

namespace Core.Application.Services;

public class LogService : ILogService
{
    private readonly ITransactionLogMongoRepository _logMongoRepository;

    public LogService(ITransactionLogMongoRepository logMongoRepository)
    {
        _logMongoRepository = logMongoRepository;
    }

    public async Task AddLogAsync(TransactionLog log, CancellationToken cancellationToken = default)
    {
        await _logMongoRepository.InsertAsync(log, cancellationToken);
    }
}