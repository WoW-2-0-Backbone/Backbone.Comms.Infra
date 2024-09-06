using System.Reflection;
using Backbone.Comms.Infra.Abstractions.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Backbone.Comms.Infra.Mediator.MassTransit.Configurations;

/// <summary>
/// Provides extension methods to configure the MediatR pipeline in the infrastructure layer.
/// </summary>
public static class InfraConfigurations
{
    /// <summary>
    /// Configures MediatR as Mediator pipeline with the provided behaviors and assemblies to scan for handlers. 
    /// </summary>
    /// <param name="services">The service collection to add MediatR to.</param>
    /// <param name="assemblies">An array of assemblies to scan for MediatR handlers.</param>
    public static void AddMediatorWithMassTransit(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        // TODO: Extract json serialization package
        throw new NotImplementedException();
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