using AutoMapper;
using ECommerceBackendSystem.Application.Abstractions.Caching;
using ECommerceBackendSystem.Application.Abstractions.Dtos.Events;
using ECommerceBackendSystem.Application.Abstractions.Publisher;
using ECommerceBackendSystem.Application.Abstractions.Services.OrderService;
using ECommerceBackendSystem.Domain.Entities.Orders;

namespace ECommerceBackendSystem.Application.OrderService;

public class OrderProviderService(IOrderPlacedPublisher orderPlacedPublisher,
                                  IRedisCacheService redisCacheService,
                                  IMapper mapper) : IOrderProviderService
{
    private readonly IOrderPlacedPublisher _orderPlacedPublisher = orderPlacedPublisher;
    private readonly IRedisCacheService _redisCaheService = redisCacheService;
    private readonly IMapper _mapper = mapper;

    public async Task ExecuteRabbitMqAndRedisServicesAsync(Order order)
    {
        var orderCreatedEvent = this._mapper.Map<OrderCreatedEvent>(order);

        await this._orderPlacedPublisher.SendAsync(orderCreatedEvent).ConfigureAwait(false);

        await this._redisCaheService.RemoveAsync(order.UserId.ToString()).ConfigureAwait(false);
    }
}
