using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Interfaces.Repositories;
using Notification.Persistance.Context;
using Notification.Persistance.Repositories;

namespace Notification.Persistance.Registration;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection UsePersistenceServiceCollection(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .RegisterPostgresql(configuration).RegisterServices();
        return services;
    }

    public static IServiceCollection RegisterPostgresql(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<NotificationDbContext>(
            options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("NotificationConnectionString"),
                    m =>
                    {
                        m.MigrationsHistoryTable("Homerun_Migrations_Notifications");
                        m.EnableRetryOnFailure();
                    }
                );
            });
        return services;
    }

    private static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<INotificationRepository, NotificationRepository>();
        return services;
    }
}