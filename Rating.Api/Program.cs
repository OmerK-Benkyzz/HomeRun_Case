using Core.Application.Configurations;
using Core.Application.Configurations.Registration;
using Core.Application.Extensions;
using Core.Persistance.Configuration;
using Rating.Api.Middlewares;
using Rating.Persistance.Context;
using Rating.Persistance.Registration;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

Configure(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.UsePersistenceServiceCollection(builder.Configuration);
    services.AddDependencies(configuration);
    services.UsePersistence(builder.Configuration);
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddEndpointsApiExplorer();


    services.UseRabbitMq(configuration);

    services.AddHttpContextAccessor();
}

void Configure(WebApplication application)
{
    application.UseMiddleware<ValidationExceptionHandlerMiddleware>();

    application.MigrateDatabase<RatingDbContext>();

    application.UseHttpsRedirection();

    application.UseAuthorization();

    application.MapControllers();

    if (application.Environment.IsDevelopment())
    {
        application.UseSwagger();
        application.UseSwaggerUI();
    }
}
