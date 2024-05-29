using System.Linq.Expressions;
using AutoMapper;
using Core.Application.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using Notification.Application.Interfaces.Repositories;
using Notification.Application.Queries.GetNotifications;
using Notification.Domain.Dtos;
using Notification.Persistance.Context;
using Xunit;

namespace Notification.UnitTests;

public class NotificationIntegrationTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _databaseFixture;

    public NotificationIntegrationTests(DatabaseFixture databaseFixture)
    {
        _databaseFixture = databaseFixture;
    }

    [Fact]
    public async Task TestDatabaseConnection()
    {
        using (var context = new NotificationDbContext(_databaseFixture.DbContextOptions))
        {
            Assert.True(await context.Database.CanConnectAsync());
        }
    }

    [Fact]
    public async Task GetNotificationsQueryHandler_ShouldReturnNotifications()
    {
        var providerId = Guid.NewGuid();
        var notificationId = Guid.NewGuid();

        using (var context = new NotificationDbContext(_databaseFixture.DbContextOptions))
        {
            try
            {
                context.Notifications.Add(new Notification.Domain.Entities.Notification
                {
                    Id = notificationId,
                    ProviderId = providerId,
                    IsRead = false,
                    Message = "Test message" // Provide a value for the Message field
                });
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }

        var notificationRepositoryMock = new Mock<INotificationRepository>();
        var mapperMock = new Mock<IMapper>();

        var handler = new GetNotificationsQueryHandler(notificationRepositoryMock.Object, mapperMock.Object);

        var notifications = CreatePaginate(new List<Notification.Domain.Entities.Notification>
        {
            new Notification.Domain.Entities.Notification
            {
                Id = notificationId,
                ProviderId = providerId,
                IsRead = false,
                Message = "Test message"
            }
        });

        notificationRepositoryMock.Setup(x => x.GetListAsync(
            It.IsAny<Expression<Func<Notification.Domain.Entities.Notification, bool>>>(),
            It.IsAny<Func<IQueryable<Notification.Domain.Entities.Notification>,
                IOrderedQueryable<Notification.Domain.Entities.Notification>>>(),
            It.IsAny<Func<IQueryable<Notification.Domain.Entities.Notification>,
                IIncludableQueryable<Notification.Domain.Entities.Notification, object>>>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(notifications);

        var notificationDtos = CreatePaginate(new List<NotificationDto>
        {
            new NotificationDto { ProviderId = notificationId }
        });

        mapperMock.Setup(m =>
                m.Map<IPaginate<NotificationDto>>(It.IsAny<IPaginate<Notification.Domain.Entities.Notification>>()))
            .Returns(notificationDtos);

        var query = new GetNotificationsQuery { ServiceProviderId = providerId, PageIndex = 1, PageSize = 10 };

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.Equal(200, result.StatusCode);
        Assert.NotNull(result.Response);
        Assert.Single(result.Response.Items);
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