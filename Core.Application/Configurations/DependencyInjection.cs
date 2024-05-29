using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Application.Behaviors;
using Core.Application.Interfaces.Services;
using Core.Application.Services;
using FluentValidation;
using MediatR;

namespace Core.Application.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
            .Select(Assembly.LoadFrom)
            .Where(a => a.GetName().Name.EndsWith(".Application"))
            .ToArray();
        services.AddAutoMapper(assemblies);
        services.AddValidatorsFromAssemblies(assemblies);
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(assemblies); });


        services.AddTransient<ILogService, LogService>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient<IClientInfoService, ClientInfoService>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        return services;
    }
}