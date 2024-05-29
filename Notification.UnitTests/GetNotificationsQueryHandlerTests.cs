using System.Linq.Expressions;
using Moq;
using AutoMapper;
using Core.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore.Query;
using Notification.Application.Interfaces.Repositories;
using Notification.Application.Queries.GetNotifications;
using Notification.Domain.Dtos;
using Xunit;

namespace Notification.UnitTests;

public class GetNotificationsQueryHandlerTests
{
    private readonly Mock<INotificationRepository> _notificationRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetNotificationsQueryHandler _handler;

    public GetNotificationsQueryHandlerTests()
    {
        _notificationRepositoryMock = new Mock<INotificationRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetNotificationsQueryHandler(_notificationRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotifications()
    {
        // Arrange
        var providerId = Guid.NewGuid();
        var notificationId = Guid.NewGuid();

        var notifications = CreatePaginate(new List<Domain.Entities.Notification>
        {
            new Domain.Entities.Notification
            {
                Id = notificationId,
                ProviderId = providerId,
                IsRead = false
            }
        });

        _notificationRepositoryMock.Setup(x => x.GetListAsync(
            It.IsAny<Expression<Func<Domain.Entities.Notification, bool>>>(),
            It.IsAny<Func<IQueryable<Domain.Entities.Notification>, IOrderedQueryable<Domain.Entities.Notification>>>(),
            It.IsAny<Func<IQueryable<Domain.Entities.Notification>, IIncludableQueryable<Domain.Entities.Notification, object>>>(),
            It.IsAny<int>(), // index
            It.IsAny<int>(), // size
            It.IsAny<bool>(), // enableTracking
            It.IsAny<CancellationToken>() // cancellationToken
        )).ReturnsAsync(notifications);

        var notificationDtos = CreatePaginate(new List<NotificationDto>
        {
            new NotificationDto { ProviderId = providerId }
        });

        _mapperMock.Setup(m => m.Map<IPaginate<NotificationDto>>(It.IsAny<IPaginate<Domain.Entities.Notification>>()))
            .Returns(notificationDtos);

        var query = new GetNotificationsQuery { ServiceProviderId = providerId, PageIndex = 1, PageSize = 10 };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Response);
        Assert.Single(result.Response.Items);
        _notificationRepositoryMock.Verify(x => x.BulkUpdateAsync(
            It.IsAny<Expression<Func<Domain.Entities.Notification, bool>>>(),
            It.IsAny<Action<Domain.Entities.Notification>>(),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }

    private static IPaginate<T> CreatePaginate<T>(IEnumerable<T> items)
    {
        var paginateMock = new Mock<IPaginate<T>>();
        paginateMock.Setup(p => p.Items).Returns(items.ToList());
        paginateMock.Setup(p => p.Index).Returns(1);
        paginateMock.Setup(p => p.Size).Returns(10);
        paginateMock.Setup(p => p.Count).Returns(items.Count());
        return paginateMock.Object;
    }
}