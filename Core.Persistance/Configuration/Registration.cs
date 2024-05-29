using Core.Application.Interfaces.Infrastructure.MongoDB;
using Core.Domain.Common;
using Core.Persistance.Repositories.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Core.Persistance.Configuration
{
    public static class Registration
    {
        public static IServiceCollection UsePersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .RegisterServices()
                .AddMongoDb(configuration);
            return services;
        }

        private static IServiceCollection AddMongoDb(this IServiceCollection services,
            IConfiguration configuration,
            string sectionName = "MongoDbSettings")
        {
            var settings = configuration.GetSection(sectionName).Get<DatabaseSettings>();

            services.AddScoped<IMongoDatabase>(serviceProvider =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<IMongoDatabase>>();

                if (settings == null)
                {
                    logger.LogError("MongoDB database settings are null!");
                    throw new ArgumentNullException(nameof(settings), "MongoDB database settings are not provided.");
                }

                var client = new MongoClient(settings.ConnectionString);
                var database = client.GetDatabase(settings.DatabaseName);

                try
                {
                    var collection = database.GetCollection<BsonDocument>("connection_collection");
                    collection.Find(new BsonDocument()).FirstOrDefault();
                    logger.LogInformation("MongoDB connected successfully and test query executed.");
                }
                catch (Exception ex)
                {
                    logger.LogError($"MongoDB connection failed: {ex.Message}");
                    throw new InvalidOperationException("MongoDB connection failed.", ex);
                }

                return database;
            });

            return services;
        }


        private static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ITransactionLogMongoRepository, TransactionLogMongoMongoRepository>();
            return services;
        }
    }
}