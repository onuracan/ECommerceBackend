using AutoMapper;
using ECommerceBackendSystem.API.Controllers;
using ECommerceBackendSystem.Application.Abstractions.Dtos.Orders;
using ECommerceBackendSystem.Application.Abstractions.Dtos.ServiceResponse;
using ECommerceBackendSystem.Application.Abstractions.Services.OrderService;
using ECommerceBackendSystem.Application.Abstractions.Services.OrderService.Factory;
using ECommerceBackendSystem.Application.Abstractions.Services.OrderService.Strategy;
using ECommerceBackendSystem.Tests;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ECommerceBackendSystem.API.Tests.Controllers;

public class OrderControllerTests : TestBase
{
    private readonly Mock<IOrderStrategyFactory> _factoryMock = new();
    private readonly Mock<IOrderQueryService> _orderQueryServiceMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    [Fact]
    public async Task GetOrders_ReturnsOk_WhenSuccessful()
    {
        var userId = AGuid().ToString();
        var orderDtos = new List<OrderDto> { CreateAnOrderDto(userId: Guid.Parse(userId)) };
        var serviceResponse = new ServiceResponse<IEnumerable<OrderDto>> { IsSuccessful = true, Data = orderDtos };

        _orderQueryServiceMock.Setup(x => x.GetOrdersByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(serviceResponse);

        var controller = new OrdersController(_factoryMock.Object, _orderQueryServiceMock.Object, _mapperMock.Object);

        var result = await controller.GetOrders(CreateAGetOrderRequest(userId));

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(orderDtos, ((ServiceResponse<IEnumerable<OrderDto>>)okResult.Value).Data);
    }

    [Fact]
    public async Task GetOrders_ReturnsBadRequest_WhenNotSuccessful()
    {
        var userId = Guid.NewGuid().ToString();
        var serviceResponse = new ServiceResponse<IEnumerable<OrderDto>> { IsSuccessful = false, Message = "Error" };

        _orderQueryServiceMock.Setup(x => x.GetOrdersByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(serviceResponse);

        var controller = new OrdersController(_factoryMock.Object, _orderQueryServiceMock.Object, _mapperMock.Object);

        var result = await controller.GetOrders(CreateAGetOrderRequest(userId));

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task CreateOrder_ReturnsOk_WhenSuccessful()
    {
        var request = CreateACreateOrderRequest();
        var dto = new CreateOrderDto();
        var strategyMock = new Mock<IOrderStrategy>();
        var serviceResponse = new ServiceResponse { IsSuccessful = true };

        _mapperMock.Setup(x => x.Map<CreateOrderDto>(request)).Returns(dto);
        _factoryMock.Setup(x => x.GetStrategy(request.PaymentMethod)).Returns(strategyMock.Object);
        strategyMock.Setup(x => x.CreateOrderAsync(dto)).ReturnsAsync(serviceResponse);

        var controller = new OrdersController(_factoryMock.Object, _orderQueryServiceMock.Object, _mapperMock.Object);

        var result = await controller.CreateOrder(request);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task CreateOrder_ReturnsBadRequest_WhenNotSuccessful()
    {
        var request = CreateACreateOrderRequest();
        var dto = new CreateOrderDto();
        var strategyMock = new Mock<IOrderStrategy>();
        var serviceResponse = new ServiceResponse { IsSuccessful = false };

        _mapperMock.Setup(x => x.Map<CreateOrderDto>(request)).Returns(dto);
        _factoryMock.Setup(x => x.GetStrategy(request.PaymentMethod)).Returns(strategyMock.Object);
        strategyMock.Setup(x => x.CreateOrderAsync(dto)).ReturnsAsync(serviceResponse);

        var controller = new OrdersController(_factoryMock.Object, _orderQueryServiceMock.Object, _mapperMock.Object);

        var result = await controller.CreateOrder(request);

        Assert.IsType<BadRequestObjectResult>(result);
    }
}
