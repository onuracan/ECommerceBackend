using ECommerceBackendSystem.API.Models.Orders.Request;
using ECommerceBackendSystem.Application.Abstractions.Dtos.Orders;
using ECommerceBackendSystem.Domain.Entities.Orders;

namespace ECommerceBackendSystem.Tests;

public partial class TestBase
{
    public Order CreateAnOrder(Guid? productId = null, int quantity = 1, string paymentMethod = "CreditCard", Guid? userId = null, int isActive = default, Guid id = default)
    {
        return new Order
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id,
            ProductId = productId ?? Guid.NewGuid(),
            Quantity = quantity,
            PaymentMethod = paymentMethod,
            UserId = userId ?? Guid.NewGuid(),
            IsActive = isActive
        };
    }

    public OrderDto CreateAnOrderDto(Guid? productId = null, int quantity = 1, string paymentMethod = "CreditCard", Guid? userId = null, int isActive = default, Guid id = default)
    {
        return new OrderDto
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id,
            ProductId = productId ?? Guid.NewGuid(),
            Quantity = quantity,
            PaymentMethod = paymentMethod,
            UserId = userId ?? Guid.NewGuid(),
            IsActive = isActive
        };
    }

    public CreateOrderDto CreateACreateOrderDto(Guid? productId = null, int quantity = 1, string paymentMethod = "CreditCard", Guid? userId = null, int isActive = default, Guid id = default)
    {
        return new CreateOrderDto
        {
            ProductId = productId ?? Guid.NewGuid(),
            Quantity = quantity,
            PaymentMethod = paymentMethod,
            UserId = userId ?? Guid.NewGuid(),
        };
    }

    public GetOrdersRequest CreateAGetOrderRequest(string userId = null)
    {
        return new GetOrdersRequest()
        {
            UserId = userId == default ? Guid.NewGuid().ToString() : userId
        };
    }

    public CreateOrderRequest CreateACreateOrderRequest(Guid userId = default, Guid productId = default, int quantity = default, string paymentMethod = null)
    {
        return new CreateOrderRequest()
        {
            UserId = userId == default ? Guid.NewGuid().ToString() : userId.ToString(),
            ProductId = productId == default ? Guid.NewGuid().ToString() : productId.ToString(),
            Quantity = quantity,
            PaymentMethod = paymentMethod == default ? "CreditCard" : paymentMethod
        };
    }
}