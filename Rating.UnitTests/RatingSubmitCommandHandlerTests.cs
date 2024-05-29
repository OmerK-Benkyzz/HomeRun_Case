using System.Linq.Expressions;
using Core.Domain.Messages;
using MassTransit;
using Moq;
using Rating.Application.Commands.RatingSubmit;
using Rating.Application.Interfaces.Repositories;
using Rating.Domain.Entities;
using Xunit;

namespace Rating.UnitTests;

public class RatingSubmitCommandHandlerTests
{
    private readonly Mock<IRateRepository> _rateRepositoryMock;
    private readonly Mock<IServiceProviderEntityRepository> _serviceProviderEntityRepositoryMock;
    private readonly Mock<IBus> _publishEndpointMock;
    private readonly RatingSubmitCommandHandler _handler;

    public RatingSubmitCommandHandlerTests()
    {
        _rateRepositoryMock = new Mock<IRateRepository>();
        _serviceProviderEntityRepositoryMock = new Mock<IServiceProviderEntityRepository>();
        _publishEndpointMock = new Mock<IBus>();
        _handler = new RatingSubmitCommandHandler(_publishEndpointMock.Object, _rateRepositoryMock.Object, _serviceProviderEntityRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldSubmitRating()
    {
        // Arrange
        var serviceProviderId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var rateId = Guid.NewGuid();

        var serviceProvider = new ServiceProviderEntity { Id = serviceProviderId };
        _serviceProviderEntityRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Expression<Func<ServiceProviderEntity, bool>>>()))
            .ReturnsAsync(serviceProvider);

        var rate = new Rate
        {
            Id = rateId,
            ServiceProviderId = serviceProviderId,
            CustomerId = customerId,
            Score = 5,
            Comment = "Great service!"
        };

        _rateRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Rate>())).ReturnsAsync(rate);

        var command = new RatingSubmitCommand
        {
            ServiceProviderId = serviceProviderId,
            CustomerId = customerId,
            Score = 5,
            Comment = "Great service!"
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(201, result.StatusCode);
        Assert.NotNull(result.Response);
        Assert.Equal(rate.Id, result.Response.Id);
        _publishEndpointMock.Verify(x => x.Publish(It.IsAny<RateSubmit>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}