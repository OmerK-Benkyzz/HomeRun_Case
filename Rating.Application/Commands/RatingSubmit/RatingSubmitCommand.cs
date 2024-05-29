using Core.Domain.Models;
using MediatR;
using Rating.Domain.Dtos;

namespace Rating.Application.Commands.RatingSubmit;

public class RatingSubmitCommand : IRequest<ApiResponse<RatingResponseDto>>
{
    public Guid ServiceProviderId { get; set; }
    public Guid CustomerId { get; set; }
    public int Score { get; set; }
    public string Comment { get; set; }
}