using MediatR;
using Microsoft.Extensions.Logging;

namespace Backbone.Comms.Infra.Mediator.MediatR.Behaviors.PipelineBehaviors;

/// <summary>
/// Represents a logging behavior.
/// </summary>
public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {RequestName} {@Request}", typeof(TRequest).Name, request);
        var response = await next();
        logger.LogInformation("Handled {RequestName} with response {@Response}", typeof(TRequest).Name, typeof(TResponse).Name);
        
        return response;
    }
}