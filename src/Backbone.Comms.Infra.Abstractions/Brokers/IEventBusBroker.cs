using Backbone.Comms.Infra.Abstractions.Events;

namespace Backbone.Comms.Infra.Abstractions.Brokers;

/// <summary>
/// Defines the broker functionality for publishing events through the event bus.
/// </summary>
public interface IEventBusBroker
{
    /// <summary>
    /// Publishes an event with in-process event bus.
    /// </summary>
    /// <typeparam name="TEvent">Type of the event to be published.</typeparam>
    /// <param name="eventContext">The event instance to be published.</param>
    /// <param name="eventOptions">The options to customize the execution.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    ValueTask PublishLocalAsync<TEvent>(
        TEvent eventContext,
        EventOptions eventOptions = default,
        CancellationToken cancellationToken = default
    ) where TEvent : EventBase;

    /// <summary>
    /// Publishes an event with out-of-process event-bus.
    /// </summary>
    /// <typeparam name="TEvent">Type of the event to be published.</typeparam>
    /// <param name="eventContext">The event instance to be published.</param>
    /// <param name="eventOptions">The options to customize the execution.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    ValueTask PublishAsync<TEvent>(
        TEvent eventContext,
        EventOptions eventOptions = default,
        CancellationToken cancellationToken = default
    ) where TEvent : EventBase;
}