using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rating.Application.Interfaces.Repositories;
using Rating.Persistance.Context;
using Rating.Persistance.Repositories;

namespace Rating.Persistance.Registration;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection UsePersistenceServiceCollection(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .RegisterPostgresql(configuration).RegisterServices();
        return services;
    }

    private static IServiceCollection RegisterPostgresql(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<RatingDbContext>(
            options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("RatingConnectionString"),
                    m =>
                    {
                        m.MigrationsHistoryTable("Homerun_Migration_Rating");
                        m.EnableRetryOnFailure();
                    }
                );
            });
        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IRateRepository, RateRepository>();
        services.AddScoped<IServiceProviderEntityRepository, ServiceProviderEntityRepository>();
        return services;
    }
}