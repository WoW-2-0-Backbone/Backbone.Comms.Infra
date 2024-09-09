using Backbone.Comms.Infra.Mediator.MediatR.Behaviors.PipelineBehaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Backbone.Comms.Infra.Mediator.MediatR.Behaviors.Configurations;

/// <summary>
/// Provides extension methods to configure the mediator pipeline.
/// </summary>
public static class InfraConfigurations
{
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