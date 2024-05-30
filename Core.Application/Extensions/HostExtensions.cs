using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Core.Application.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var dbContext = services.GetRequiredService<T>();
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    // Log or handle the error as needed
                    Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
                    throw;
                }
            }

            return host;
        }
    }
}
