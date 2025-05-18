using ECommerceBackendSystem.Application.Abstractions.Dtos.Events;

namespace ECommerceBackendSystem.Application.Abstractions.Publisher;

public interface IOrderPlacedPublisher
{
    Task SendAsync(OrderCreatedEvent message);
}
