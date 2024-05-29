using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notification.Persistance.Context;
using Testcontainers.PostgreSql;
using Xunit;

namespace Notification.UnitTests;

public class DatabaseFixture : IAsyncLifetime
{
    public PostgreSqlContainer PostgreSqlContainer { get; private set; }
    public DbContextOptions<NotificationDbContext> DbContextOptions { get; private set; }

    public async Task InitializeAsync()
    {
        PostgreSqlContainer = new PostgreSqlBuilder()
            .WithDatabase("TestDb")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .Build();
        await PostgreSqlContainer.StartAsync();

        DbContextOptions = new DbContextOptionsBuilder<NotificationDbContext>()
            .UseNpgsql(PostgreSqlContainer.GetConnectionString())
            .Options;

        using (var context = new NotificationDbContext(DbContextOptions))
        {
            context.Database.EnsureCreated();
        }
    }

    public async Task DisposeAsync()
    {
        await PostgreSqlContainer.StopAsync();
    }
}