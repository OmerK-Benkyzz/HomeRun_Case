using Core.Application.Configurations;
using Core.Application.Configurations.Registration;
using Core.Application.Extensions;
using Core.Persistance.Configuration;
using Notification.Application.Consumers;
using Notification.Persistance.Context;
using Notification.Persistance.Registration;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

Configure(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.UsePersistenceServiceCollection(builder.Configuration);
    services.UsePersistence(builder.Configuration);
    services.AddControllers();

    services.AddEndpointsApiExplorer();

    services.AddDependencies(configuration);

    var consumerTypeNames = new List<string?>
    {
        typeof(RateSubmitConsumer).AssemblyQualifiedName
    };
    services.UseRabbitMq(configuration, consumerTypeNames);

    services.AddHttpContextAccessor();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddEndpointsApiExplorer();
}

void Configure(WebApplication application)
{
    application.UseHttpsRedirection();

    application.MigrateDatabase<NotificationDbContext>();

    application.UseAuthorization();

    application.MapControllers();

    if (application.Environment.IsDevelopment())
    {
        application.UseSwagger();
        application.UseSwaggerUI();
    }
}
