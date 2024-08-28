using MassTransit;
using MediatR;

namespace Backbone.Comms.Infra.Abstractions.Events;

/// <summary>
/// Represents a handler for a specific type of event.
/// </summary>
/// <typeparam name="TEvent">The type of event being handled.</typeparam>
public interface IEventHandler<in TEvent> : IEventHandler,
    INotificationHandler<TEvent>,
    IConsumer<TEvent>
    where TEvent : class, INotification;

/// <summary>
/// Defines interface for event handlers.
/// </summary>
public interface IEventHandler;