using Backbone.Comms.Infra.Abstractions.Brokers;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Queries;
using MassTransit.Mediator;

namespace Backbone.Comms.Infra.Mediator.MassTransit.Brokers;

/// <summary>
/// Provides MassTransit implementation as Mediator pipeline
/// </summary>
public class MassTransitMediatorBroker : IMediatorBroker
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of <see cref="MassTransitMediatorBroker"/>.
    /// </summary>
    /// <param name="mediator">The MassTransit bus instance for handling commands and queries.</param>
    public MassTransitMediatorBroker(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Sends a command and returns the expected response.
    /// </summary>
    public async ValueTask<TResponse> SendAsync<TCommand, TResponse>(
        TCommand command, 
        CommandOptions commandOptions = default, 
        CancellationToken cancellationToken = default
    ) where TCommand : ICommand<TResponse>
    {
        // TODO: Implement command model from MassTransit interfaces
        throw new NotImplementedException();
    }

    /// <summary>
    /// Sends a command without expecting a response.
    /// </summary>
    public async ValueTask SendAsync<TCommand>(
        TCommand command, 
        CommandOptions commandOptions = default, 
        CancellationToken cancellationToken = default
    ) where TCommand : ICommand
    {
        // TODO: Implement command model from MassTransit interfaces
        throw new NotImplementedException();
    }

    /// <summary>
    /// Sends a query and returns the expected response.
    /// </summary>
    public async ValueTask<TResponse> SendAsync<TQuery, TResponse>(
        TQuery query, 
        QueryOptions queryOptions = default, 
        CancellationToken cancellationToken = default
    ) where TQuery : IQuery<TResponse>
    {
        // TODO: Implement query model from MassTransit interfaces
        throw new NotImplementedException();
    }

    /// <summary>
    /// Sends a query without expecting a response.
    /// </summary>
    public async ValueTask SendAsync<TQuery>(
        TQuery query, 
        QueryOptions queryOptions = default, 
        CancellationToken cancellationToken = default
    ) where TQuery : IQuery
    {
        // TODO: Implement query model from MassTransit interfaces
        throw new NotImplementedException();
    }
}