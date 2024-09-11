using System.Reflection;
using Backbone.Comms.Infra.Abstractions.Brokers;
using Backbone.Comms.Infra.Mediator.MediatR.Brokers;
using Microsoft.Extensions.DependencyInjection;

namespace Backbone.Comms.Infra.Mediator.MediatR.Configurations;

/// <summary>
/// Provides extension methods to configure the mediator pipeline using MediatR.
/// </summary>
public static class InfraConfigurations
{
    /// <summary>
    /// Configures MediatR services with the provided assemblies to scan for handlers.
    /// </summary>
    /// <param name="services">The service collection to add MediatR to.</param>
    /// <param name="assemblies">An array of assemblies to scan for MediatR handlers.</param>
    /// <param name="configuration">An action to configure MediatR services.</param>
    public static IServiceCollection AddMediatRServices(
        this IServiceCollection services,
        ICollection<Assembly> assemblies,
        Action<MediatRServiceConfiguration, IServiceCollection>? configuration = null
    )
    {
        services.AddMediatR(mediatrConfiguration =>
        {
            // Register services from the provided assemblies
            mediatrConfiguration.RegisterServicesFromAssemblies(assemblies.ToArray());

            configuration?.Invoke(mediatrConfiguration, services);
        });

        return services;
    }

    /// <summary>
    /// Configures MediatR as a mediator pipeline with the provided behaviors and assemblies to scan for handlers. 
    /// </summary>
    /// <param name="services">The service collection to add MediatR to.</param>
    public static IServiceCollection AddMediatorWithMediatR(this IServiceCollection services)
    {
        services.AddSingleton<IMediatorBroker, MediatRMediatorBroker>();

        return services;
    }
}