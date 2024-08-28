namespace Backbone.Comms.Infra.Abstractions.Events;

/// <summary>
/// Represents an implementation of the IEvent interface, defining the properties for an event.
/// </summary>
public abstract record EventBase: IEvent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public DateTimeOffset CreatedTime { get; set; } = DateTimeOffset.UtcNow;
    
    public bool Redelivered { get; set; }
}