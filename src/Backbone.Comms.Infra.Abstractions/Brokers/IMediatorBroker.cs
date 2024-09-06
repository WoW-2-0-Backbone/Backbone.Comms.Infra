using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Queries;

namespace Backbone.Comms.Infra.Abstractions.Brokers;

/// <summary>
/// Defines the broker functionality for sending commands and queries through the mediator pipeline.
/// </summary>
public interface IMediatorBroker
{
    /// <summary>
    /// Sends a command with expected response.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to be executed.</typeparam>
    /// <typeparam name="TResponse">The type of the response returned by the command handler.</typeparam>
    /// <param name="command">The command to be sent.</param>
    /// <param name="commandOptions">The options to customize the execution.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The response from a command handler.</returns>
    ValueTask<TResponse> SendAsync<TCommand, TResponse>(
        TCommand command,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) where TCommand : ICommand<TResponse>;

    /// <summary>
    /// Sends a command asynchronously without expecting a response.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to be executed.</typeparam>
    /// <param name="command">The command to be sent.</param>
    /// <param name="commandOptions">The options to customize the execution.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    ValueTask SendAsync<TCommand>(
        TCommand command,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    ) where TCommand : ICommand;

    /// <summary>
    /// Sends a query asynchronously and returns a response.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query to be executed.</typeparam>
    /// <typeparam name="TResponse">The type of the response returned by the query handler.</typeparam>
    /// <param name="query">The query to be sent.</param>
    /// <param name="queryOptions">The options to customize the execution.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The response from a query handler.</returns>
    ValueTask<TResponse> SendAsync<TQuery, TResponse>(
        TQuery query,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    ) where TQuery : IQuery<TResponse>;

    /// <summary>
    /// Sends a query asynchronously without expecting a response.
    /// </summary>
    /// <typeparam name="TQuery">The type of the query to be executed.</typeparam>
    /// <param name="query">The query to be sent.</param>
    /// <param name="queryOptions">The options to customize the execution.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    ValueTask SendAsync<TQuery>(
        TQuery query,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default
    ) where TQuery : IQuery;
}