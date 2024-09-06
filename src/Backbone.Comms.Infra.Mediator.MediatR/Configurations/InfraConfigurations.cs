using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Backbone.Comms.Infra.Mediator.MediatR.Configurations;

/// <summary>
/// Provides extension methods to configure the MediatR pipeline in the infrastructure layer.
/// </summary>
public static class InfraConfigurations
{
    /// <summary>
    /// Configures MediatR as Mediator pipeline with the provided behaviors and assemblies to scan for handlers. 
    /// </summary>
    /// <param name="services">The service collection to add MediatR to.</param>
    /// <param name="pipelineBehaviors">An array of behaviors to be added to the MediatR pipeline.</param>
    /// <param name="assemblies">An array of assemblies to scan for MediatR handlers.</param>
    public static void AddMediatorWithMediatR(
        this IServiceCollection services,
        Type[] pipelineBehaviors,
        params Assembly[] assemblies)
    {
        services.AddMediatR(configuration =>
        {
            // Register each pipeline behavior from the provided array
            foreach (var behavior in pipelineBehaviors)
                configuration.AddBehavior(typeof(IPipelineBehavior<,>), behavior);

            // Register services from the provided assemblies
            configuration.RegisterServicesFromAssemblies(assemblies);
        });
    }
}