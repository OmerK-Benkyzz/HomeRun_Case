using Core.Domain.Models;
using FluentValidation;
using Newtonsoft.Json;

namespace Notification.Api.Middlewares;

public class ValidationExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, ValidationException exception)
    {
        var errorMessage = string.Join(", ", exception.Errors.Select(x => x.ErrorMessage));

        var response = new ApiResponse<object>
        {
            Error = errorMessage,
            StatusCode = StatusCodes.Status405MethodNotAllowed
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
        return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}