using System.Net;
using Core.Application.Interfaces.Services;
using Core.Domain.Messages;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Application.Behaviors;

public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IClientInfoService _clientInfo;
    private readonly ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBus _publishEndpoint;

    public ExceptionHandlingBehavior(ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger,
        IHttpContextAccessor httpContextAccessor,
        IBus publishEndpoint,
        IClientInfoService clientInfo)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
        _publishEndpoint = publishEndpoint;
        _clientInfo = clientInfo;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var clientInfo = _clientInfo.GetClientInfo();
        string responseTypeName = typeof(TResponse).Name;
        if (typeof(TResponse).IsGenericType)
        {
            var genericTypeArgument = typeof(TResponse).GetGenericArguments()[0];
            responseTypeName = $"{typeof(TResponse).Name.Split('`')[0]}<{genericTypeArgument.Name}>";
        }

        try
        {
            var response = await next();
            var responseJObject = JObject.FromObject(response);
            var errorValue = responseJObject["StatusCode"];

            var logMessage = new LogMessage
            {
                RequestName = typeof(TRequest).Name,
                ResponseName = responseTypeName,
                Request = JsonConvert.SerializeObject(request),
                Response = JsonConvert.SerializeObject(response),
                DeviceInfo = clientInfo.UserAgent,
                IpAddress = clientInfo.IpAddress,
                StatusCode = errorValue?.Value<int>() ?? 0
            };

            await _publishEndpoint.Publish(logMessage, cancellationToken);
            return response;
        }
        catch (Exception ex)
        {
            int statusCode;

            if (ex is ValidationException validationException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                statusCode = _httpContextAccessor.HttpContext?.Response.StatusCode ?? 0;
            }

            var logMessage = new LogMessage
            {
                RequestName = typeof(TRequest).Name,
                ResponseName = responseTypeName,
                Request = JsonConvert.SerializeObject(request),
                Response = ex.Message,
                DeviceInfo = clientInfo.UserAgent,
                IpAddress = clientInfo.IpAddress,
                StatusCode = statusCode
            };

            await _publishEndpoint.Publish(logMessage, cancellationToken);
            throw;
        }
    }
}