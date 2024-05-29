using Core.Domain.Messages;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Notification.Application.Commands;

namespace Notification.Application.Consumers;

public class RateSubmitConsumer : IConsumer<RateSubmit>
{
    private readonly ILogger<RateSubmitConsumer> _logger;
    private readonly IMediator _mediator;

    public RateSubmitConsumer(ILogger<RateSubmitConsumer> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }


    public async Task Consume(ConsumeContext<RateSubmit> context)
    {
        await _mediator.Send(new SaveNotificationCommand
        {
            ServiceProviderId = context.Message.ServiceProviderId,
            Message = $"You received a rating of {context.Message.Rating} with the comment: {context.Message.Comment}"
        }, context.CancellationToken);
    }
}