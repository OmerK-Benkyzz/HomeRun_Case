using Core.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling {typeof(TRequest).Name}");

        try
        {
            var response = await next();
            var innerResponseType = response?.GetType().GetGenericTypeDefinition() == typeof(ApiResponse<>)
                ? response.GetType().GetGenericArguments()[0].Name
                : "UnknownDto";

            _logger.LogInformation(
                $"Handled {typeof(TRequest).Name} on {typeof(TResponse).Name} containing {innerResponseType}");

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error Handling {typeof(TRequest).Name}");
            throw;
        }
    }
}