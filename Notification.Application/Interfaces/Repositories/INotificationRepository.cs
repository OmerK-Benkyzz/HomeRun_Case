using Core.Persistance.Repositories.PostgreSQL;
using Notification.Domain.Entities;

namespace Notification.Application.Interfaces.Repositories;

public interface INotificationRepository : IAsyncRepository<Domain.Entities.Notification>, IRepository<Domain.Entities.Notification>
{
}