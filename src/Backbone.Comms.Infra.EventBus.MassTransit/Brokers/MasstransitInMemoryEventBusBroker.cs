using Backbone.Comms.Infra.Abstractions.Brokers;
using Backbone.Comms.Infra.Abstractions.Events;
using MassTransit;
using MediatR;

namespace Backbone.Comms.Infra.EventBus.MassTransit.Brokers;

/// <summary>
/// Provides event bus functionality using in-memory event bus with MassTransit
/// </summary>
public class MasstransitInMemoryEventBusBroker(IMediator mediator, IBus bus) : IEventBusBroker
{
    /// <summary>
    /// Publishes an event with in-process event bus using MediatR.
    /// </summary>
    /// <typeparam name="TEvent">Type of the event to be published.</typeparam>
    /// <param name="eventContext">The event instance to be published.</param>
    /// <param name="eventOptions">The options to customize the execution.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async ValueTask PublishLocalAsync<TEvent>(
        TEvent eventContext,
        EventOptions eventOptions = default,
        CancellationToken cancellationToken = default
    ) where TEvent : EventBase
    {
        await mediator.Publish(eventContext, cancellationToken);
    }

    /// <summary>
    /// Publishes an event with in-process but separate thread event-bus using MassTransit.
    /// </summary>
    /// <typeparam name="TEvent">Type of the event to be published.</typeparam>
    /// <param name="eventContext">The event instance to be published.</param>
    /// <param name="eventOptions">The options to customize the execution.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async ValueTask PublishAsync<TEvent>(
        TEvent eventContext,
        EventOptions eventOptions = default,
        CancellationToken cancellationToken = default
    ) where TEvent : EventBase
    {
        await bus.Publish(eventContext, cancellationToken);
    }
}