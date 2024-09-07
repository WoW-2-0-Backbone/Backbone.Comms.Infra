using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Backbone.Comms.Infra.Mediator.MassTransit.Configurations;

/// <summary>
/// Provides extension methods to configure the mediator pipeline using MassTransit.
/// </summary>
public static class InfraConfigurations
{
    /// <summary>
    /// Configures MassTransit as mediator pipeline allowing customization of bus configuration and in-memory bus configuration. 
    /// </summary>
    /// <param name="services">The service collection to add MassTransit to.</param>
    /// <param name="busConfiguration">The bus configuration.</param>
    /// <param name="inMemoryBusConfiguration">The in-memory bus configuration.</param>
    public static void AddMediatorWithMassTransit(
        this IServiceCollection services,
        Action<IBusRegistrationConfigurator> busConfiguration,
        Action<IInMemoryBusFactoryConfigurator> inMemoryBusConfiguration
    )
    {
        services.AddMassTransit(
            configuration =>
            {
                busConfiguration(configuration);

                configuration.UsingInMemory(
                    (context, config) =>
                    {
                        config.ConfigureEndpoints(context);
                        inMemoryBusConfiguration(config);
                    }
                );
            }
        );
    }
}