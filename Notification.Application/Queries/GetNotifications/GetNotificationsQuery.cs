using System;
using System.Text.Json.Serialization;
using Core.Application.Interfaces.Services;
using Core.Domain.Models;
using MediatR;
using Notification.Domain.Dtos;

namespace Notification.Application.Queries.GetNotifications;

public class GetNotificationsQuery : IRequest<ApiResponse<IPaginate<NotificationDto>>>
{
    public Guid ServiceProviderId { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}