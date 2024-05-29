using System.Text;
using Core.Application.Interfaces.Services;
using Core.Domain.Entities;
using Core.Domain.Messages;
using MassTransit;
using MassTransit.Logging;
using Microsoft.Extensions.Logging;

namespace Core.Application.Consumers;

public class LogConsumer : IConsumer<LogMessage>
{
    private readonly ILogService _logService;
    private readonly ILogger<LogConsumer> _logger;

    public LogConsumer(ILogService logService, ILogger<LogConsumer> logger)
    {
        _logService = logService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<LogMessage> context)
    {
        var logMessage = context.Message;

        await _logService.AddLogAsync(new TransactionLog
        {
            RequestName = context.Message.RequestName,
            ResponseName = context.Message.ResponseName,
            CreateTime = DateTime.Now.ToUniversalTime(),
            IpAddress = logMessage.IpAddress,
            DeviceInfo = logMessage.DeviceInfo,
            Request = logMessage.Request,
            Response = logMessage.Response,
            StatusCode = logMessage.StatusCode
        });
    }
}