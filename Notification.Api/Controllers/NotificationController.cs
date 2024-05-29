using Core.Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.Queries.GetNotifications;

namespace Notification.Api.Controllers;

public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<NotificationController> _logger;

    public NotificationController(IMediator mediator, ILogger<NotificationController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    [HttpGet("GetNotification/{serviceProviderId}")]
    public async Task<IActionResult> GetNotification(Guid serviceProviderId, int pageIndex = 0, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetNotificationsQuery
        {
            ServiceProviderId = serviceProviderId,
            PageIndex = pageIndex,
            PageSize = pageSize
        }, cancellationToken).HandleWithExceptionsAsync(this);
    }
}