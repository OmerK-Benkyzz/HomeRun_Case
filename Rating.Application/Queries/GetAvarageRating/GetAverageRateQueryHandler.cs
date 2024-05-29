using System.Net;
using Core.Domain.Exceptions;
using Core.Domain.Models;
using MediatR;
using Rating.Application.Interfaces.Repositories;
using Rating.Domain.Dtos;

namespace Rating.Application.Queries.GetAvarageRating;

public class GetAverageRateQueryHandler : IRequestHandler<GetAverageRateQuery, ApiResponse<AvarageRateDto>>
{
    private readonly IRateRepository _rateRepository;
    private readonly IServiceProviderEntityRepository _serviceProviderEntityRepository;

    public GetAverageRateQueryHandler(IRateRepository rateRepository,
        IServiceProviderEntityRepository serviceProviderEntityRepository)
    {
        _rateRepository = rateRepository;
        _serviceProviderEntityRepository = serviceProviderEntityRepository;
    }

    public async Task<ApiResponse<AvarageRateDto>> Handle(GetAverageRateQuery request,
        CancellationToken cancellationToken)
    {
        var serviceProviderExists =
            await _serviceProviderEntityRepository.ServiceProviderExistsAsync(request.ServiceProviderId,
                cancellationToken);

        if (!serviceProviderExists)
        {
            throw new NotFoundException("Service provider not found.");
        }

        var averageRating = await _rateRepository.GetAverageRatingAsync(request.ServiceProviderId, cancellationToken);

        return new ApiResponse<AvarageRateDto>
        {
            Response = new AvarageRateDto
            {
                AvarageRate = averageRating
            },
            StatusCode = (int)HttpStatusCode.OK
        };
    }
}