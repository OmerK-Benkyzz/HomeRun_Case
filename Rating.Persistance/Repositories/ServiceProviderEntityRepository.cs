using Core.Persistance.Repositories.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Rating.Application.Interfaces.Repositories;
using Rating.Domain.Entities;
using Rating.Persistance.Context;

namespace Rating.Persistance.Repositories;

public class ServiceProviderEntityRepository : EfRepositoryBase<ServiceProviderEntity, RatingDbContext>,
    IServiceProviderEntityRepository
{
    public ServiceProviderEntityRepository(RatingDbContext context) : base(context)
    {
    }

    public async Task<bool> ServiceProviderExistsAsync(Guid serviceProviderId, CancellationToken cancellationToken)
    {
        return await Context.ServiceProviders.AnyAsync(sp => sp.Id == serviceProviderId, cancellationToken);
    }
}