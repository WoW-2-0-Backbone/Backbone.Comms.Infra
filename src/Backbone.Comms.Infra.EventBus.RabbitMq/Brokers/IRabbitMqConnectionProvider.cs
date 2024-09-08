using RabbitMQ.Client;

namespace Backbone.Comms.Infra.EventBus.RabbitMq.Brokers;

/// <summary>
/// Defines rabbit mq connection provider functionality.
/// </summary>
public interface IRabbitMqConnectionProvider
{
    /// <summary>
    /// Creates a RabbitMQ channel for communication.
    /// </summary>
    /// <returns>Created channel</returns>
    ValueTask<IChannel> CreateChannelAsync();
}