using ECommerceBackendSystem.Application.Abstractions.Caching;
using ECommerceBackendSystem.Application.Abstractions.Dtos.Events;
using MassTransit;

namespace ECommerceBackendSystem.OrderProcessingWorker
{
    public class OrderPlacedConsumer(ILogger<OrderPlacedConsumer> logger,
                                     IRedisCacheService redisCacheService) : IConsumer<OrderCreatedEvent>
    {
        private readonly ILogger<OrderPlacedConsumer> _logger = logger;
        private readonly IRedisCacheService _redisCacheService = redisCacheService;

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            try
            {
                var order = context.Message;

                await Task.Delay(2000);

                await _redisCacheService.SetAsync($"order:processed:{order.Id}",
                                                  $"Order {order.Id} processed at {DateTime.UtcNow:O}");

                Console.WriteLine($"Order:Processed {order.UserId} for order {order.Id}");
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Order processing failed for message: {@Message}", context.Message);
                throw;
            }
        }
    }
}
