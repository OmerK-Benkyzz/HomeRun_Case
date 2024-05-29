using System.Net;
using Core.Domain.Exceptions;
using Core.Domain.Messages;
using Core.Domain.Models;
using MassTransit;
using MediatR;
using Rating.Application.Interfaces.Repositories;
using Rating.Domain.Dtos;
using Rating.Domain.Entities;

namespace Rating.Application.Commands.RatingSubmit
{
    public class RatingSubmitCommandHandler : IRequestHandler<RatingSubmitCommand, ApiResponse<RatingResponseDto>>
    {
        private readonly IRateRepository _rateRepository;
        private readonly IServiceProviderEntityRepository _serviceProviderEntityRepository;
        private readonly IBus _publishEndpoint;

        public RatingSubmitCommandHandler(IBus publishEndpoint,
            IRateRepository rateRepository,
            IServiceProviderEntityRepository serviceProviderEntityRepository)
        {
            _publishEndpoint = publishEndpoint;
            _rateRepository = rateRepository;
            _serviceProviderEntityRepository = serviceProviderEntityRepository;
        }

        public async Task<ApiResponse<RatingResponseDto>> Handle(RatingSubmitCommand request,
            CancellationToken cancellationToken)
        {
            var serviceProvider =
                await _serviceProviderEntityRepository.GetAsync(a => a.Id == request.ServiceProviderId);

            if (serviceProvider is null)
            {
                throw new NotFoundException("Invalid Service Provider Id");
            }

            var entity = await _rateRepository.AddAsync(new Rate
            {
                ServiceProviderId = serviceProvider.Id,
                CustomerId = request.CustomerId,
                Score = request.Score,
                Comment = request.Comment,
                ServiceProviderEntity = serviceProvider
            });

            await _publishEndpoint.Publish(new RateSubmit
            {
                ServiceProviderId = request.ServiceProviderId,
                CustomerId = request.CustomerId,
                Rating = request.Score,
                Comment = request.Comment
            }, cancellationToken);
            return new ApiResponse<RatingResponseDto>
            {
                Response = new RatingResponseDto
                {
                    Id = entity.Id
                },
                StatusCode = 201
            };
        }
    }
}