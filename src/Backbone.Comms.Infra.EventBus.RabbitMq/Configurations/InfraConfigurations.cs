using Backbone.Comms.Infra.Abstractions.Brokers;
using Backbone.Comms.Infra.EventBus.RabbitMq.Brokers;
using Microsoft.Extensions.DependencyInjection;

namespace Backbone.Comms.Infra.EventBus.RabbitMq.Configurations;

/// <summary>
/// Provides extension methods to configure the event bus.
/// </summary>
public static class InfraConfigurations
{
    /// <summary>
    /// Configures in-memory event bus using MassTransit. 
    /// </summary>
    /// <param name="services">The service collection to add MediatR to.</param>
    public static void AddEventBusWithRabbitMq(IServiceCollection services)
    {
        services.AddSingleton<IEventBusBroker, RabbitMqEventBusBroker>();
    }
}