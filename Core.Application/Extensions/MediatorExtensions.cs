using System.Net;
using Core.Domain.Exceptions;
using Core.Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Core.Application.Extensions;

public static class MediatorExtensions
{
    private static readonly Dictionary<Type, (HttpStatusCode StatusCode, string ErrorMessage)> ExceptionMappings =
        new Dictionary<Type, (HttpStatusCode, string)>
        {
            { typeof(NotFoundException), (HttpStatusCode.NotFound, "Resource not found.") },
            { typeof(ValidationException), (HttpStatusCode.BadRequest, "Validation failed.") },
            { typeof(UnauthorizedAccessException), (HttpStatusCode.Unauthorized, "Unauthorized access.") },
        };

    public static async Task<IActionResult> HandleWithExceptionsAsync<T>(this Task<ApiResponse<T>> responseTask,
        ControllerBase controller)
    {
        try
        {
            var response = await responseTask;
            return controller.StatusCode(response.StatusCode, response);
        }
        catch (Exception ex) when (ExceptionMappings.ContainsKey(ex.GetType()))
        {
            var mapping = ExceptionMappings[ex.GetType()];
            var errorResponse = new ApiResponse<T>
            {
                Response = default,
                Error = mapping.ErrorMessage,
                StatusCode = (int)mapping.StatusCode
            };
            return controller.StatusCode((int)mapping.StatusCode, errorResponse);
        }
        catch (Exception ex)
        {
            var errorResponse = new ApiResponse<T>
            {
                Response = default,
                Error = "An unexpected error occurred.",
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
            return controller.StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
        }
    }
}