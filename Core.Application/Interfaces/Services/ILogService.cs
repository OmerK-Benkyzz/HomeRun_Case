using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services;

public interface ILogService
{
    Task AddLogAsync(TransactionLog log, CancellationToken cancellationToken = default);
}