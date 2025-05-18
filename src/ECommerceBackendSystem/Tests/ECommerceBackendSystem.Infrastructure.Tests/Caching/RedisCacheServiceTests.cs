using ECommerceBackendSystem.Infrastructure.Caching;
using Moq;
using StackExchange.Redis;

namespace ECommerceBackendSystem.Infrastructure.Tests.Caching;

public class RedisCacheServiceTests
{
    [Fact]
    public async Task SetAsync_ShouldStoreValue()
    {
        // Arrange
        var dbMock = new Mock<IDatabase>();
        dbMock.Setup(x => x.StringSetAsync(It.IsAny<RedisKey>(), It.IsAny<RedisValue>(), It.IsAny<TimeSpan?>(), null, null, null, null))
              .ReturnsAsync(true);

        var multiplexerMock = new Mock<IConnectionMultiplexer>();
        multiplexerMock.Setup(x => x.GetDatabase(It.IsAny<int>(), null)).Returns(dbMock.Object);

        var configMock = new Mock<IConfiguration>();
        configMock.Setup(x => x["ConnectionStrings:Redis"]).Returns("localhost:6379");

        var service = new RedisCacheService(multiplexerMock.Object);

        // Act
        var result = await service.SetAsync("key", "value", TimeSpan.FromMinutes(1));

        // Assert
        Assert.True(result);
    }
}
