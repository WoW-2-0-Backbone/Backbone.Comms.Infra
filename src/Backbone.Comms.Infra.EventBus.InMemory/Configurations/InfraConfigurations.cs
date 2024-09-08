using System.Reflection;
using Backbone.Comms.Infra.Abstractions.Brokers;
using Backbone.Comms.Infra.Abstractions.Events;
using Backbone.Comms.Infra.EventBus.InMemory.Brokers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Backbone.Comms.Infra.EventBus.InMemory.Configurations;

/// <summary>
/// Provides extension methods to configure the event bus.
/// </summary>
public static class InfraConfigurations
{
    /// <summary>
    /// Configures in-memory event bus using MassTransit. 
    /// </summary>
    /// <param name="busRegistrationConfigurator">MassTransit bus registration configurator.</param>
    /// <param name="services">The service collection to add event bus to.</param>
    /// <param name="assemblies">An array of assemblies to scan for MassTransit handlers.</param>
    public static void AddInMemoryEventBusWithMassTransit(
        this IBusRegistrationConfigurator busRegistrationConfigurator,
        IServiceCollection services,
        Assembly[] assemblies)
    {
        busRegistrationConfigurator.RegisterAllConsumers(assemblies);
        services.AddSingleton<IEventBusBroker, MasstransitInMemoryEventBusBroker>();
    }

    /// <summary>
    /// Registers all the consumers form the assembly
    /// </summary>
    /// <param name="busConfigurator">MassTransit bus configuration</param>
    /// <param name="assemblies">Collection of assemblies to get consumers from</param>
    /// <param name="predicate">A predicate to filter consumers before registration</param>
    public static void RegisterAllConsumers(
        this IBusRegistrationConfigurator busConfigurator,
        ICollection<Assembly> assemblies,
        Func<Type, bool>? predicate = default)
    {
        // Get all implementations of consumer
        var consumers = assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => !type.IsAbstract
                           && type.GetInterfaces().Any(implementedInterface =>
                               implementedInterface.IsGenericType
                               && implementedInterface.GetGenericTypeDefinition() == typeof(IEventHandler<>)));

        foreach (var consumer in consumers)
            if (predicate is null || predicate(consumer))
                busConfigurator.AddConsumer(consumer);
    }
}