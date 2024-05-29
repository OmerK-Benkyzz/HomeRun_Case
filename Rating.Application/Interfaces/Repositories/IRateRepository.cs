using Core.Persistance.Repositories.PostgreSQL;
using Rating.Domain.Entities;

namespace Rating.Application.Interfaces.Repositories;

public interface IRateRepository : IAsyncRepository<Rate>, IRepository<Rate>
{
    Task<double> GetAverageRatingAsync(Guid serviceProviderId, CancellationToken cancellationToken = default);
}