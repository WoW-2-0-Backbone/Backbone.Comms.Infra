namespace Backbone.Comms.Infra.Abstractions.Events;

/// <summary>
/// Represents event options
/// </summary>
public readonly struct EventOptions()
{
    /// <summary>
    /// Gets event correlation ID
    /// </summary>
    public string? CorrelationId { get; init; }

    /// <summary>
    /// Event exchange name
    /// </summary>
    public string Exchange { get; init; } = string.Empty;

    /// <summary>
    /// Gets an event routing key for the exchange
    /// </summary>
    public string RoutingKey { get; init; } = string.Empty;
    
    /// <summary>
    /// Gets the reply-to address for the event
    /// </summary>
    public string? ReplyTo { get; init; }

    /// <summary>
    /// Gets a value indicating whether the event message should be persistent
    /// </summary>
    public bool IsPersistent { get; init; }
}