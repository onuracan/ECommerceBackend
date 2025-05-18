using ECommerceBackendSystem.Application.Abstractions.Services.OrderService.Strategy;

namespace ECommerceBackendSystem.Application.Abstractions.Services.OrderService.Factory;

public interface IOrderStrategyFactory
{
    IOrderStrategy GetStrategy(string paymentMethod);
}
