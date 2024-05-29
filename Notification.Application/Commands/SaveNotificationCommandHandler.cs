using MediatR;
using Notification.Application.Interfaces.Repositories;
using Notification.Domain.Entities;

namespace Notification.Application.Commands;

public class SaveNotificationCommandHandler : IRequestHandler<SaveNotificationCommand>
{
    private readonly INotificationRepository _notificationRepository;

    public SaveNotificationCommandHandler( INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }

    public async Task Handle(SaveNotificationCommand request, CancellationToken cancellationToken)
    {
        await _notificationRepository.AddAsync(new Domain.Entities.Notification
        {
            ProviderId = request.ServiceProviderId,
            Message = request.Message,
            IsRead = false
        });
    }
}