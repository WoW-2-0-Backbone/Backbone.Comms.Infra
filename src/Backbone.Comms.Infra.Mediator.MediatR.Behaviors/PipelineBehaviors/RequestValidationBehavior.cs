using Backbone.General.Validations.Abstractions.Exceptions;
using FluentValidation;
using FluentValidation.TestHelper;
using MediatR;

namespace Backbone.Comms.Infra.Mediator.MediatR.Behaviors.PipelineBehaviors;

/// <summary>
/// Represents a request validation behavior
/// </summary>
public class RequestValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Validate request
        var context = new ValidationContext<TRequest>(request);
        var failures = (await Task.WhenAll(validators
                .Select(v => v.ValidateAsync(context, cancellationToken))))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Any())
            throw new AppValidationException(new ValidationException(failures));

        return await next();
    }
}

/// <summary>
/// Represents a void request validation behavior
/// </summary>
public class VoidRequestValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, Unit>
    where TRequest : IRequest
{
    /// <inheritdoc />
    public async Task<Unit> Handle(TRequest request, RequestHandlerDelegate<Unit> next, CancellationToken cancellationToken)
    {
        // Validate request
        var context = new ValidationContext<TRequest>(request);
        var failures = (await Task.WhenAll(validators
                .Select(v => v.ValidateAsync(context, cancellationToken))))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Any())
            throw new AppValidationException(new ValidationException(failures));

        return await next();
    }
}