using Core.Application.Interfaces.Services;
using Core.Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Services;

public class ClientInfoService : IClientInfoService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClientInfoService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClientInfoDto GetClientInfo()
    {
        var context = _httpContextAccessor.HttpContext;
        var ip = context.Request.Headers.TryGetValue("X-Forwarded-For", out var header);

        var ipAddress =
            !ip ? context.Connection.RemoteIpAddress?.MapToIPv4().ToString() : header.ToString().Split(",")[0];
        var userAgent = context.Request.Headers["User-Agent"].FirstOrDefault();

        return new ClientInfoDto
        {
            IpAddress = ipAddress,
            UserAgent = userAgent
        };
    }
}