using ECommerceBackendSystem.Application.Abstractions.Services.OrderService.Factory;
using ECommerceBackendSystem.Application.Abstractions.Services.OrderService.Strategy;

namespace ECommerceBackendSystem.Application.OrderService.Factory;

public class OrderStrategyFactory(IEnumerable<IOrderStrategy> strategies) : IOrderStrategyFactory
{
    private readonly IEnumerable<IOrderStrategy> _strategies = strategies;

    public IOrderStrategy GetStrategy(string paymentMethod)
    {
        var strategy = _strategies.FirstOrDefault(s =>
            string.Equals(s.PaymentMethod, paymentMethod, StringComparison.OrdinalIgnoreCase));

        if (strategy == null)
            throw new InvalidOperationException($"No strategy found for payment method: {paymentMethod}");

        return strategy;
    }
}
