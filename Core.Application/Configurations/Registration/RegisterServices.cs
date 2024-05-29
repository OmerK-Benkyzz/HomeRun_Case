using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.Configurations.Registration;

public static class RegisterServices
{
    public static IServiceCollection UseRabbitMq(this IServiceCollection services,
        IConfiguration configuration,
        List<string?>? consumers = null)
    {
        return services
            .AddMassTransitRabbitMq(configuration, consumers);
    }

    private static IServiceCollection AddMassTransitRabbitMq(this IServiceCollection services,
        IConfiguration configuration,
        List<string?>? consumers = null)
    {
        services.AddMassTransit(x =>
        {
            if (consumers is not null)
            {
                var consumerTypes = consumers.Select(typeName =>
                {
                    var type = Type.GetType(typeName);
                    if (type == null)
                    {
                        throw new ArgumentException($"The type '{typeName}' could not be found.");
                    }

                    return type;
                }).ToList();

                foreach (var consumerType in consumerTypes)
                {
                    x.AddConsumer(consumerType);
                }
            }


            x.AddConsumers(Assembly.GetExecutingAssembly());
            x.UsingRabbitMq((context, cfg) =>
            {
                var hostAndPort = $"rabbitmq://{configuration["EventBus:Hostname"]}:{configuration["EventBus:Port"]}";

                cfg.Host(hostAndPort, h =>
                {
                    h.Username(configuration["EventBus:Username"]);
                    h.Password(configuration["EventBus:Password"]);
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        services.Configure<MassTransitHostOptions>(options => { options.WaitUntilStarted = true; });
        return services;
    }
}