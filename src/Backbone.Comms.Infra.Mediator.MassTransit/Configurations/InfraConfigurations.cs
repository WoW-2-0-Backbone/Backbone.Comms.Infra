using System.Reflection;
using Backbone.Comms.Infra.Abstractions.Brokers;
using Backbone.Comms.Infra.Abstractions.Events;
using Backbone.Comms.Infra.Mediator.MassTransit.Brokers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Backbone.Comms.Infra.Mediator.MassTransit.Configurations;

/// <summary>
/// Provides extension methods to configure the mediator pipeline using MassTransit.
/// </summary>
public static class InfraConfigurations
{
    /// <summary>
    /// Configures MassTransit services with the provided assemblies to scan for consumers.
    /// </summary>
    /// <param name="services">The service collection to add MassTransit to.</param>
    /// <param name="assemblies">An array of assemblies to scan for MassTransit consumers.</param>
    /// <param name="configuration">An action to configure MassTransit services.</param>
    public static IServiceCollection AddMassTransitServices(
        this IServiceCollection services,
        ICollection<Assembly> assemblies,
        Action<IBusRegistrationConfigurator, IServiceCollection>? configuration = null
    )
    {
        services.AddMassTransit(massTransitConfiguration =>
        {
            // Register services from the provided assemblies
            massTransitConfiguration.RegisterAllConsumers(assemblies);

            configuration?.Invoke(massTransitConfiguration, services);
        });

        return services;
    }

    /// <summary>
    /// Configures MassTransit as a mediator pipeline with the provided bus configuration and in-memory bus configuration.
    /// </summary>
    /// <param name="services">The service collection to add MassTransit to.</param>
    public static IServiceCollection AddMediatorWithMassTransit(this IServiceCollection services)
    {
        services.AddSingleton<IMediatorBroker, MassTransitMediatorBroker>();

        return services;
    }

    /// <summary>
    /// Registers all the consumers form the assembly
    /// </summary>
    /// <param name="busConfigurator">MassTransit bus configuration</param>
    /// <param name="assemblies">Collection of assemblies to get consumers from</param>
    /// <param name="predicate">A predicate to filter consumers before registration</param>
    public static IBusRegistrationConfigurator RegisterAllConsumers(
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

        return busConfigurator;
    }
}