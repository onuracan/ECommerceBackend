using ECommerceBackendSystem.Domain.Entities.Orders;

namespace ECommerceBackendSystem.Application.Abstractions.Services.OrderService;

public interface IOrderProviderService
{
    Task ExecuteRabbitMqAndRedisServicesAsync(Order order);
}
