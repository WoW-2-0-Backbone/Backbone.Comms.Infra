using Backbone.Comms.Infra.Abstractions.Brokers;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Events;
using Backbone.Comms.Infra.Abstractions.Queries;
using MediatR;

namespace Backbone.Comms.Infra.Mediator.MediatR.Brokers;

/// <summary>
/// Provides MediatR implementation as Mediator pipeline
/// </summary>
public class MediatRMediatorBroker(IMediator mediator) : IMediatorBroker
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
    public async ValueTask<TResponse> SendAsync<TCommand, TResponse>(
        TCommand command,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) where TCommand : ICommand<TResponse>
    {
        return await mediator.Send(command, cancellationToken);
    }

    /// <summary>
    /// Sends a command to MediatR without expecting a response.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to be executed.</typeparam>
    /// <param name="command">The command to be sent.</param>
    /// <param name="commandOptions">The options to customize the execution.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async ValueTask SendAsync<TCommand>(
        TCommand command,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) where TCommand : ICommand
    {
        await mediator.Send(command, cancellationToken);
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
    public async ValueTask<TResponse> SendAsync<TQuery, TResponse>(
        TQuery query,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    ) where TQuery : IQuery<TResponse>
    {
        return await mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Sends a query to MediatR without expecting a response.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query to be executed.</typeparam>
    /// <param name="eventContext">The query to be sent.</param>
    /// <param name="queryOptions">The options to customize the execution.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async ValueTask SendAsync<TQuery>(
        TQuery eventContext,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    ) where TQuery : IQuery
    {
        await mediator.Send(eventContext, cancellationToken);
    }

    /// <summary>
    /// Sends an event without expecting a response.
    /// </summary>
    /// <typeparam name="TEvent">The type of the query to be executed.</typeparam>
    /// <param name="eventContext">The query to be sent.</param>
    /// <param name="eventOptions">The options to customize the execution.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async ValueTask SendAsync<TEvent>(
        TEvent eventContext, 
        EventOptions eventOptions = default,
        CancellationToken cancellationToken = default
    ) where TEvent : IEvent
    {
        await mediator.Publish(eventContext, cancellationToken);
    }
}