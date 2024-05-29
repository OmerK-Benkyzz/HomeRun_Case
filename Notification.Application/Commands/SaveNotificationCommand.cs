using System;
using System.Text.Json.Serialization;
using MediatR;

namespace Notification.Application.Commands;

public class SaveNotificationCommand : IRequest
{
    public Guid ServiceProviderId { get; set; }
    public string Message { get; set; }
}