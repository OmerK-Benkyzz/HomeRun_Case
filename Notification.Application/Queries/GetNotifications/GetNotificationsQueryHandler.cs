using AutoMapper;
using Core.Application.Interfaces.Services;
using Core.Domain.Models;
using MediatR;
using Notification.Application.Interfaces.Repositories;
using Notification.Domain.Dtos;

namespace Notification.Application.Queries.GetNotifications;

public class
    GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, ApiResponse<IPaginate<NotificationDto>>>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IMapper _mapper;

    public GetNotificationsQueryHandler(INotificationRepository notificationRepository, IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<IPaginate<NotificationDto>>> Handle(GetNotificationsQuery request,
        CancellationToken cancellationToken)
    {
        var notifications = await _notificationRepository.GetListAsync(
            predicate: n => n.ProviderId == request.ServiceProviderId,
            index: request.PageIndex,
            size: request.PageSize,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        var notificationDtos = _mapper.Map<IPaginate<NotificationDto>>(notifications);

        var unreadNotificationsIds = notifications.Items.Where(n => !n.IsRead).Select(n => n.Id).ToList();
        if (unreadNotificationsIds.Any())
        {
            await _notificationRepository.BulkUpdateAsync(
                n => unreadNotificationsIds.Contains(n.Id),
                n => n.IsRead = true,
                cancellationToken
            );
        }


        return new ApiResponse<IPaginate<NotificationDto>>
        {
            Response = notificationDtos,
            Error = null,
            StatusCode = 200
        };
    }
}