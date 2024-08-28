namespace Backbone.Comms.Infra.Abstractions.Interfaces;

/// <summary>
/// Defines cloning behavior for an object.
/// </summary>
/// <typeparam name="TClone">The type of object to clone.</typeparam>
public interface ICloneable<out TClone>
{
    /// <summary>
    /// Creates a clone of the object.
    /// </summary>
    TClone Clone();
}