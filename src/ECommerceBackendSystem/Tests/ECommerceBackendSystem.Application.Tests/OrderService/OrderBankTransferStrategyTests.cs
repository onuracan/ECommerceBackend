using AutoMapper;
using ECommerceBackendSystem.Application.Abstractions.Dtos.ServiceResponse;
using ECommerceBackendSystem.Application.Abstractions.Services.Base;
using ECommerceBackendSystem.Application.Abstractions.Services.OrderService;
using ECommerceBackendSystem.Application.Constants;
using ECommerceBackendSystem.Application.OrderService.Strategy;
using ECommerceBackendSystem.Domain.Entities.Orders;
using ECommerceBackendSystem.Domain.Repositories;
using ECommerceBackendSystem.Tests;
using Moq;
using System.Net;

public class OrderBankTransferStrategyTests : TestBase
{
    private readonly Mock<IGenericRepository<Order>> _repositoryMock = new();
    private readonly Mock<IServiceResponseHelper> _serviceResponseHelperMock = new();
    private readonly Mock<IOrderProviderService> _orderProviderServiceMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    [Fact]
    public async Task CreateOrderAsync_Should_AddOrder_And_ExecuteProvider_And_ReturnSuccess()
    {
        var createOrderDto = CreateACreateOrderDto(paymentMethod: PaymentMethodConstants.BANK_TRANSFER);
        var createOrder = CreateAnOrder();

        _mapperMock.Setup(m => m.Map<Order>(createOrderDto)).Returns(createOrder);
        _repositoryMock.Setup(r => r.AddAsync(createOrder, default)).Returns(Task.CompletedTask);
        _orderProviderServiceMock.Setup(s => s.ExecuteRabbitMqAndRedisServicesAsync(createOrder)).Returns(Task.CompletedTask);
        _serviceResponseHelperMock.Setup(s => s.Success(It.IsAny<string>()))
            .Returns((string msg) => new ServiceResponse { IsSuccessful = true, Message = msg, StatusCode = HttpStatusCode.OK });

        var strategy = new OrderBankTransferStrategy(
            _repositoryMock.Object,
            _serviceResponseHelperMock.Object,
            _orderProviderServiceMock.Object,
            _mapperMock.Object
        );

        var result = await strategy.CreateOrderAsync(createOrderDto);

        Assert.True(result.IsSuccessful);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }
}
