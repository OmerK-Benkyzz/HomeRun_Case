using Microsoft.EntityFrameworkCore;
using Notification.Domain.Entities;

namespace Notification.Persistance.Context;

public class NotificationDbContext : DbContext
{
    public DbSet<Domain.Entities.Notification> Notifications { get; set; }

    public NotificationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public override int SaveChanges()
    {
        OnBeforeSaving();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new CancellationToken())
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        OnBeforeSaving();

        return base.SaveChangesAsync(cancellationToken);
    }

    private void OnBeforeSaving()
    {
        var addedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added).ToList();
        foreach (var e in addedEntries)
        {
            if (e.Metadata.FindProperty("CreatedAt") != null)
            {
                e.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
            }
        }
    }
}