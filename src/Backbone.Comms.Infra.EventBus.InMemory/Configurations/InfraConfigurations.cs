using System.Reflection;
using Backbone.Comms.Infra.Abstractions.Brokers;
using Backbone.Comms.Infra.EventBus.InMemory.Brokers;
using Backbone.General.Serialization.Json.Newtonsoft.Brokers;
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
    /// <param name="useNewtonsoftJsonSerializer">Indicates whether to use Newtonsoft.JSON serializer.</param>
    public static void AddInMemoryEventBusWithMassTransit(
        this IBusRegistrationConfigurator busRegistrationConfigurator,
        IServiceCollection services,
        bool useNewtonsoftJsonSerializer = false
    )
    {
        busRegistrationConfigurator.UsingInMemory(
            (context, config) =>
            {
                config.ConfigureEndpoints(context);

                // Change the default serializer to NewtonsoftJson
                if (useNewtonsoftJsonSerializer)
                {
                    var serviceProvider = busRegistrationConfigurator.BuildServiceProvider();
                    var serializationSettingsProvider = serviceProvider.GetRequiredService<INewtonsoftJsonSerializationSettingsProvider>();

                    // Change the default serializer to NewtonsoftJson
                    config.UseNewtonsoftJsonSerializer();
                    config.UseNewtonsoftJsonDeserializer();

                    // Change the default serializer settings
                    config.ConfigureNewtonsoftJsonSerializer(settings => serializationSettingsProvider.ConfigureWithTypeHandling(settings));
                    config.ConfigureNewtonsoftJsonDeserializer(settings => serializationSettingsProvider.ConfigureWithTypeHandling(settings));
                }
            }
        );

        services.AddSingleton<IEventBusBroker, MasstransitInMemoryEventBusBroker>();
    }
}