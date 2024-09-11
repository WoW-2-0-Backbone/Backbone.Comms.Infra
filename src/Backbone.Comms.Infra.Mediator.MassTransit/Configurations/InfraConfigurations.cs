using System.Reflection;
using Backbone.Comms.Infra.Abstractions.Brokers;
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
            massTransitConfiguration.AddConsumers(assemblies.ToArray());

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
}