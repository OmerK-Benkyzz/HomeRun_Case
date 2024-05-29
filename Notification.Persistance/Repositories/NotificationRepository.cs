using Core.Persistance.Repositories.PostgreSQL;
using Notification.Application.Interfaces.Repositories;
using Notification.Domain.Entities;
using Notification.Persistance.Context;

namespace Notification.Persistance.Repositories;

public class NotificationRepository: EfRepositoryBase<Domain.Entities.Notification, NotificationDbContext>, INotificationRepository
{
    public NotificationRepository(NotificationDbContext context) : base(context)
    {
    }
        
}