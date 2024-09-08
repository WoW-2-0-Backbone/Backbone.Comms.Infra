using Backbone.Comms.Infra.Abstractions.Brokers;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Events;
using Backbone.Comms.Infra.Abstractions.Queries;
using MassTransit;
using MassTransit.Mediator;

namespace Backbone.Comms.Infra.Mediator.MassTransit.Brokers;

/// <summary>
/// Provides MassTransit implementation as Mediator pipeline
/// </summary>
public class MassTransitMediatorBroker(IMediator mediator, IBus bus) : IMediatorBroker
{
    /// <summary>
    /// Sends a command to MediatR with expected response.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to be executed.</typeparam>
    /// <typeparam name="TResponse">The type of the response returned by the command handler.</typeparam>
    /// <param name="command">The command to be sent.</param>
    /// <param name="commandOptions">The options to customize the execution.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The response from a command handler.</returns>
    public ValueTask<TResponse> SendAsync<TCommand, TResponse>(
        TCommand command, 
        CommandOptions commandOptions = default, 
        CancellationToken cancellationToken = default
    ) where TCommand : ICommand<TResponse>
    {
        // TODO: Implement command model from MassTransit interfaces
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Sends a command to MediatR without expecting a response.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to be executed.</typeparam>
    /// <param name="command">The command to be sent.</param>
    /// <param name="commandOptions">The options to customize the execution.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public ValueTask SendAsync<TCommand>(
        TCommand command, 
        CommandOptions commandOptions = default, 
        CancellationToken cancellationToken = default
    ) where TCommand : ICommand
    {
        // TODO: Implement command model from MassTransit interfaces
        throw new NotImplementedException();
    }

    /// <summary>
    /// Sends a query to MediatR and returns a response.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query to be executed.</typeparam>
    /// <typeparam name="TResponse">The type of the response returned by the query handler.</typeparam>
    /// <param name="query">The query to be sent.</param>
    /// <param name="queryOptions">The options to customize the execution.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The response from a query handler.</returns>
    public ValueTask<TResponse> SendAsync<TQuery, TResponse>(
        TQuery query, 
        QueryOptions queryOptions = default, 
        CancellationToken cancellationToken = default
    ) where TQuery : IQuery<TResponse>
    {
        // TODO: Implement query model from MassTransit interfaces
        throw new NotImplementedException();
    }

    /// <summary>
    /// Sends a query to MediatR without expecting a response.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query to be executed.</typeparam>
    /// <param name="eventContext">The query to be sent.</param>
    /// <param name="queryOptions">The options to customize the execution.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public ValueTask SendAsync<TQuery>(
        TQuery eventContext, 
        QueryOptions queryOptions = default, 
        CancellationToken cancellationToken = default
    ) where TQuery : IQuery
    {
        // TODO: Implement query model from MassTransit interfaces
        throw new NotImplementedException();
    }

    /// <summary>
    /// Sends an event without expecting a response.
    /// </summary>
    /// <typeparam name="TEvent">The type of the query to be executed.</typeparam>
    /// <param name="eventContext">The query to be sent.</param>
    /// <param name="eventOptions">The options to customize the execution.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public ValueTask SendAsync<TEvent>(
        TEvent eventContext, 
        EventOptions eventOptions = default,
        CancellationToken cancellationToken = default
    ) where TEvent : IEvent
    {
        // TODO: Implement event model from MassTransit interfaces
        throw new NotImplementedException();
    }
}