using Core.Domain.Models;
using MediatR;
using Rating.Domain.Dtos;

namespace Rating.Application.Queries.GetAvarageRating;

public class GetAverageRateQuery : IRequest<ApiResponse<AvarageRateDto>>
{
    public Guid ServiceProviderId { get; set; }
}