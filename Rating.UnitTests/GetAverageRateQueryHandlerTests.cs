using Moq;
using Rating.Application.Interfaces.Repositories;
using Rating.Application.Queries.GetAvarageRating;
using Xunit;

namespace Rating.UnitTests;

public class GetAverageRateQueryHandlerTests
{
    private readonly Mock<IRateRepository> _rateRepositoryMock;
    private readonly Mock<IServiceProviderEntityRepository> _serviceProviderEntityRepositoryMock;
    private readonly GetAverageRateQueryHandler _handler;

    public GetAverageRateQueryHandlerTests()
    {
        _rateRepositoryMock = new Mock<IRateRepository>();
        _serviceProviderEntityRepositoryMock = new Mock<IServiceProviderEntityRepository>();
        _handler = new GetAverageRateQueryHandler(_rateRepositoryMock.Object, _serviceProviderEntityRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAverageRating()
    {
        var serviceProviderId = Guid.NewGuid();

        _serviceProviderEntityRepositoryMock.Setup(x => x.ServiceProviderExistsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _rateRepositoryMock.Setup(x => x.GetAverageRatingAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(4.5);

        var query = new GetAverageRateQuery { ServiceProviderId = serviceProviderId };

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Response);
        Assert.Equal(4.5, result.Response.AvarageRate);
    }
}