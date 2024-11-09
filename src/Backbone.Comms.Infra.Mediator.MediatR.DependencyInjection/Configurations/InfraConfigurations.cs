using System.Reflection;
using Backbone.Comms.Infra.Abstractions.Brokers;
using Backbone.Comms.Infra.Mediator.MediatR.Behaviors.PipelineBehaviors;
using Backbone.Comms.Infra.Mediator.MediatR.Brokers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Backbone.Comms.Infra.Mediator.MediatR.DependencyInjection.Configurations;

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
        services.AddScoped<IMediatorBroker, MediatRMediatorBroker>();

        return services;
    }
    
    /// <summary>
    /// Configures MediatR pipeline to register pipeline behaviors. 
    /// </summary>
    /// <param name="configuration">MediatR pipeline configuration to register pipeline behaviors to.</param>
    public static void AddMediatRPipelineBehaviors(this MediatRServiceConfiguration configuration)
    {
        configuration
            .AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
            .AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
            .AddBehavior(typeof(IPipelineBehavior<,>), typeof(VoidRequestValidationBehavior<,>));
    }
}