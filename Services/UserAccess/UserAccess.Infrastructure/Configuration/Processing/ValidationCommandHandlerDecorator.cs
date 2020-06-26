using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Base.Application;
using FluentValidation;
using MediatR;
using UserAccess.Application.Configuration.Commands;
using UserAccess.Application.Contracts;

namespace UserAccess.Infrastructure.Configuration.Processing
{
    internal class ValidationCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
    {
        private readonly ICommandHandler<T> _decorated;
        private readonly IList<IValidator<T>> _validators;

        public ValidationCommandHandlerDecorator(
            IList<IValidator<T>> validators,
            ICommandHandler<T> decorated)
        {
            _validators = validators;
            _decorated = decorated;
        }

        public Task<Unit> Handle(T command, CancellationToken cancellationToken)
        {
            var errors = _validators
                .Select(v => v.Validate(command))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (errors.Any())
            {
                var errorBuilder = new StringBuilder();

                errorBuilder.AppendLine("Invalid command, reason: ");

                foreach (var error in errors) errorBuilder.AppendLine(error.ErrorMessage);

                throw new InvalidCommandException(errorBuilder.ToString(), null);
            }

            return _decorated.Handle(command, cancellationToken);
        }
    }
}