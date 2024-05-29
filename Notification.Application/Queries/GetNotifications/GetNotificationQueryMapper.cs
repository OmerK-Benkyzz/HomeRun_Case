using AutoMapper;
using Core.Application.Extensions;
using Core.Application.Helper;
using Core.Application.Interfaces.Services;
using Notification.Domain.Dtos;

namespace Notification.Application.Queries.GetNotifications;

public class GetNotificationQueryMapper : Profile
{
    public GetNotificationQueryMapper()
    {
        CreateMap<Domain.Entities.Notification, NotificationDto>().ReverseMap();
        CreateMap<IPaginate<Domain.Entities.Notification>, IPaginate<NotificationDto>>()
            .ConvertUsing(new PaginateConverter<Domain.Entities.Notification, NotificationDto>());
    }
    
    
}