using ECommerceBackendSystem.Application.Abstractions.Dtos.Orders;
using ECommerceBackendSystem.Application.Abstractions.Dtos.ServiceResponse;

namespace ECommerceBackendSystem.Application.Abstractions.Services.OrderService.Strategy;

public interface IOrderStrategy
{
    string PaymentMethod { get; }
    Task<ServiceResponse> CreateOrderAsync(CreateOrderDto createOrder);
}
