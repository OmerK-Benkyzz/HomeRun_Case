using Core.Domain.Dtos;

namespace Core.Application.Interfaces.Services;

public interface IClientInfoService
{
    ClientInfoDto GetClientInfo();
}