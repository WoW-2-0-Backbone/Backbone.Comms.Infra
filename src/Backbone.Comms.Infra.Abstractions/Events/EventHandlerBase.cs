using MassTransit;
using MediatR;

namespace Backbone.Comms.Infra.Abstractions.Events;

/// <summary>
/// Represents base for event handlers
/// </summary>
/// <typeparam name="TEvent"></typeparam>
public abstract class EventHandlerBase<TEvent> : IEventHandler<TEvent> where TEvent : class, INotification
{
    public async Task Handle(TEvent notification, CancellationToken cancellationToken) => await HandleAsync(notification, cancellationToken);

    public async Task Consume(ConsumeContext<TEvent> context) => await HandleAsync(context.Message, context.CancellationToken);

    /// <summary>
    /// Internal handle method that can be called by any event bus
    /// </summary>
    /// <param name="eventContext">Event to handle</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    protected abstract ValueTask HandleAsync(TEvent eventContext, CancellationToken cancellationToken);
}