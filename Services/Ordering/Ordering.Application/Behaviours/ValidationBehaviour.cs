using FluentValidation;
using MediatR;

namespace Ordering.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any()) return await next();
            var context = new ValidationContext<TRequest>(request);
            var validationResult =
                await Task.WhenAll(_validators.Select(p => p.ValidateAsync(context, cancellationToken)));
            var failures = validationResult.SelectMany(r => r.Errors).Where(r => r != null);
            if (failures.Any())
                throw new Exceptions.ValidationException(failures);
            return await next();
        }
    }
}
