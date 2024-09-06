using Backbone.Comms.Infra.Abstractions.Brokers;
using Backbone.Comms.Infra.Abstractions.Commands;
using Backbone.Comms.Infra.Abstractions.Queries;
using MediatR;

namespace Backbone.Comms.Infra.Mediator.MediatR.Brokers;

/// <summary>
/// Provides MediatR implementation as Mediator pipeline
/// </summary>
public class MediatRMediatorBroker : IMediatorBroker
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of <see cref="MediatRMediatorBroker"/>.
    /// </summary>
    /// <param name="mediator">The MediatR instance for handling commands and queries.</param>
    public MediatRMediatorBroker(IMediator mediator)
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
        return await _mediator.Send(command, cancellationToken);
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
        await _mediator.Send(command, cancellationToken);
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
        return await _mediator.Send(query, cancellationToken);
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
        await _mediator.Send(query, cancellationToken);
    }
}