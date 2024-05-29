using Core.Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rating.Application.Commands.RatingSubmit;
using Rating.Application.Queries.GetAvarageRating;

namespace Rating.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatingController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RatingController> _logger;

    public RatingController(IMediator mediator, ILogger<RatingController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("RateServiceProvider")]
    public async Task<IActionResult> RateServiceProvider([FromBody] RatingSubmitCommand request,
        CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(request, cancellationToken).HandleWithExceptionsAsync(this);
    }

    [HttpGet("GetAvarageRating/{serviceProviderId}")]
    public async Task<IActionResult> GetAverageRating(Guid serviceProviderId,
        CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetAverageRateQuery
        {
            ServiceProviderId = serviceProviderId
        }, cancellationToken).HandleWithExceptionsAsync(this);
    }
}