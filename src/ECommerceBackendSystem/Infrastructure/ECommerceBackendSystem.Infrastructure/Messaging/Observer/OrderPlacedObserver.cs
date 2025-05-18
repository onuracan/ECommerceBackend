using MassTransit;
using Microsoft.Extensions.Logging;

namespace ECommerceBackendSystem.Infrastructure.Messaging.Observer;

public class OrderPlacedObserver(ILogger<OrderPlacedObserver> logger) : ISendObserver
{
    private readonly ILogger<OrderPlacedObserver> _logger = logger;

    public Task PostSend<T>(SendContext<T> context) where T : class
    {
       this. _logger.LogInformation("PreSend: Sending {MessageType} | CorrelationId: {CorrelationId} | Destination: {DestinationAddress} | Source: {SourceAddress} | ConversationId: {ConversationId}",
             typeof(T).Name,
             context.CorrelationId,
             context.DestinationAddress,
             context.SourceAddress,
             context.ConversationId);
        return Task.CompletedTask;
    }

    public Task PreSend<T>(SendContext<T> context) where T : class
    {
        this._logger.LogInformation("PostSend: Sent {MessageType} | MessageId: {MessageId} | Destination: {DestinationAddress}",
          typeof(T).Name,
          context.MessageId,
          context.DestinationAddress);
        return Task.CompletedTask;
    }

    public Task SendFault<T>(SendContext<T> context, Exception exception) where T : class
    {
        this._logger.LogError(exception, "SendFault: Failed to send {MessageType} | CorrelationId: {CorrelationId} | Destination: {DestinationAddress}",
            typeof(T).Name,
            context.CorrelationId,
            context.DestinationAddress);
        return Task.CompletedTask;
    }
}
