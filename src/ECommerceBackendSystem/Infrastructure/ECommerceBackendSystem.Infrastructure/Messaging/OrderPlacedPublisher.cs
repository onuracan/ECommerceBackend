using ECommerceBackendSystem.Application.Abstractions.Dtos.Events;
using ECommerceBackendSystem.Application.Abstractions.Publisher;
using ECommerceBackendSystem.Infrastructure.Messaging.Options;
using MassTransit;
using Microsoft.Extensions.Options;

namespace ECommerceBackendSystem.Infrastructure.Messaging;

public class OrderPlacedPublisher(ISendEndpointProvider sendEndpointProvider,
                                  IOptions<RabbitMqOptions> options) : IOrderPlacedPublisher
{
    private readonly ISendEndpointProvider _sendEndpointProvider = sendEndpointProvider;
    private readonly RabbitMqOptions _rabbitMqOptions = options.Value;

    public async Task SendAsync(OrderCreatedEvent message)
    {
        var endpoint = await this._sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{_rabbitMqOptions.QueueName}")).ConfigureAwait(false);

        await endpoint.Send(message).ConfigureAwait(false);
    }
}
