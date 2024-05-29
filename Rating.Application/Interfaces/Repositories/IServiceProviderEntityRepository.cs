using Core.Persistance.Repositories.PostgreSQL;
using Rating.Domain.Entities;

namespace Rating.Application.Interfaces.Repositories;

public interface IServiceProviderEntityRepository: IAsyncRepository<ServiceProviderEntity>, IRepository<ServiceProviderEntity>
{
    Task<bool> ServiceProviderExistsAsync(Guid serviceProviderId, CancellationToken cancellationToken);

}