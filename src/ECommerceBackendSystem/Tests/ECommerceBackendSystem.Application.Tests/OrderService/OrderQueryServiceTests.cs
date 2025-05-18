using ECommerceBackendSystem.Application.Abstractions.Caching;
using ECommerceBackendSystem.Application.Abstractions.Dtos.Orders;
using ECommerceBackendSystem.Application.Abstractions.Dtos.ServiceResponse;
using ECommerceBackendSystem.Application.Abstractions.Services.Base;
using ECommerceBackendSystem.Application.OrderService;
using ECommerceBackendSystem.Domain.Entities.Orders;
using ECommerceBackendSystem.Domain.Repositories;
using ECommerceBackendSystem.Tests;
using Moq;
using System.Linq.Expressions;
using System.Net;

namespace ECommerceBackendSystem.Application.Tests.OrderService;

public class OrderQueryServiceTests: TestBase
{
    private readonly Mock<IGenericRepository<Order>> _repositoryMock = new();
    private readonly Mock<IServiceResponseHelper> _serviceResponseHelperMock = new();
    private readonly Mock<IRedisCacheService> _redisCacheMock = new();

    [Fact]
    public async Task GetOrdersByUserIdAsync_ReturnsFromCache_IfExists()
    {
        var userId = AGuid();

        var orderDtos = new List<OrderDto> { CreateAnOrderDto(id: userId) };
        
        _redisCacheMock.Setup(x => x.KeyExists(userId.ToString())).Returns(true);
        _redisCacheMock.Setup(x => x.GetAsync<IEnumerable<OrderDto>>(userId.ToString())).ReturnsAsync(orderDtos);

        _serviceResponseHelperMock.Setup(x => x.Success(It.IsAny<IEnumerable<OrderDto>>(), It.IsAny<string>()))
            .Returns(new ServiceResponse<IEnumerable<OrderDto>> { IsSuccessful = true, Data = orderDtos,StatusCode = HttpStatusCode.OK });

        var service = new OrderQueryService(_repositoryMock.Object, _serviceResponseHelperMock.Object, _redisCacheMock.Object);

        var result = await service.GetOrdersByUserIdAsync(userId);

        Assert.True(result.IsSuccessful);
        Assert.Equal(orderDtos.FirstOrDefault(), result.Data.FirstOrDefault());
        _repositoryMock.Verify(x => x.GetQueryable(It.IsAny<Expression<Func<Order, bool>>>()), Times.Never);
    }

    [Fact]
    public async Task GetOrdersByUserIdAsync_ReturnsFromDb_IfNotInCache()
    {
        var userId = AGuid();

        var orderDtos = new List<OrderDto>();

        _redisCacheMock.Setup(x => x.KeyExists(userId.ToString())).Returns(true);
        _redisCacheMock.Setup(x => x.GetAsync<IEnumerable<OrderDto>>(userId.ToString())).ReturnsAsync(orderDtos);

        _serviceResponseHelperMock.Setup(x => x.Success(It.IsAny<IEnumerable<OrderDto>>(), It.IsAny<string>()))
            .Returns(new ServiceResponse<IEnumerable<OrderDto>> { IsSuccessful = false, Data = orderDtos, StatusCode = HttpStatusCode.NoContent });

        var service = new OrderQueryService(_repositoryMock.Object, _serviceResponseHelperMock.Object, _redisCacheMock.Object);

        var result = await service.GetOrdersByUserIdAsync(userId);

        Assert.False(result.IsSuccessful);
        Assert.NotNull(result.Data);
        _repositoryMock.Verify(x => x.GetQueryable(It.IsAny<Expression<Func<Order, bool>>>()), Times.Never);
    }
}
