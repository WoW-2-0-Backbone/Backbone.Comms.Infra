using System.Text;
using Backbone.Comms.Infra.Abstractions.Brokers;
using Backbone.Comms.Infra.Abstractions.Events;
using Backbone.General.Serialization.Json.Abstractions.Brokers;
using Backbone.General.Serialization.Json.Abstractions.Constants;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Backbone.Comms.Infra.EventBus.RabbitMq.Brokers;

/// <summary>
/// Provides event bus functionality using RabbitMQ
/// </summary>
public class RabbitMqEventBusBroker(
    ILogger<RabbitMqEventBusBroker> logger,
    IRabbitMqConnectionProvider rabbitMqConnectionProvider,
    IJsonSerializer jsonSerializer,
    IMediatorBroker mediatorBroker
) : IEventBusBroker
{
    public async ValueTask PublishLocalAsync<TEvent>(
        TEvent eventContext,
        EventOptions eventOptions = default,
        CancellationToken cancellationToken = default
    ) where TEvent : EventBase
    {
        await mediatorBroker.SendAsync(eventContext, eventOptions, cancellationToken);
    }

    async ValueTask IEventBusBroker.PublishAsync<TEvent>(
        TEvent eventContext,
        EventOptions eventOptions,
        CancellationToken cancellationToken)
    {
        var properties = new BasicProperties
        {
            Persistent = eventOptions.IsPersistent,
            ReplyTo = eventOptions.ReplyTo
        };

        if (string.IsNullOrWhiteSpace(eventOptions.Exchange))
            logger.LogDebug("Event exchange is not provided. Event will be pushed to the default exchange");
        else
            properties.CorrelationId = eventOptions.CorrelationId;

        if (string.IsNullOrWhiteSpace(eventOptions.RoutingKey))
            logger.LogDebug("Event routing key is not provided. Event will be pushed with empty routing key");

        if (!string.IsNullOrWhiteSpace(eventOptions.ReplyTo))
            properties.ReplyTo = eventOptions.ReplyTo;

        var channel = await rabbitMqConnectionProvider.CreateChannelAsync();

        var body = Encoding.UTF8.GetBytes(await jsonSerializer.SerializeAsync(eventContext,
            JsonSerializationConstants.GeneralSerializationWithTypeHandlingSettings));
        
        await channel.BasicPublishAsync(eventOptions.Exchange, eventOptions.RoutingKey, properties, body, cancellationToken: cancellationToken);
    }
}