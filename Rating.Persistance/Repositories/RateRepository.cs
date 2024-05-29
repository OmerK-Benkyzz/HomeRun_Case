using Core.Persistance.Repositories.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Rating.Application.Interfaces.Repositories;
using Rating.Domain.Entities;
using Rating.Persistance.Context;

namespace Rating.Persistance.Repositories;

public class RateRepository : EfRepositoryBase<Rate, RatingDbContext>, IRateRepository
{
    public RateRepository(RatingDbContext context) : base(context)
    {
    }

    public async Task<double> GetAverageRatingAsync(Guid serviceProviderId, CancellationToken cancellationToken)
    {
        var rate = await Context.Rates
            .Where(r => r.ServiceProviderId == serviceProviderId)
            .AverageAsync(r => r.Score, cancellationToken: cancellationToken);

        return Math.Round(rate, 1);
    }
}