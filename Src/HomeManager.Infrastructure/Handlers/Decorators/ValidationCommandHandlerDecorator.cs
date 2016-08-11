using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Results;

namespace HomeManager.Infrastructure.Handlers.Decorators
{
    public class ValidationCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
    {
        private readonly ICommandHandler<TCommand> _command;
        private readonly IFaultHandler<TCommand> _faultHandler;
        private readonly IValidator<TCommand> _validator;

        public ValidationCommandHandlerDecorator(
            ICommandHandler<TCommand> command, 
            IFaultHandler<TCommand> faultHandler, 
            IValidator<TCommand> validator)
        {
            _command = command;
            _faultHandler = faultHandler;
            _validator = validator;
        }

        public void Handle(TCommand command)
        {
            var result = _validator.Validate(command);

            if (result.IsValid)
            {
                _command.Handle(command);
            }
            else
            {
                var errors = AggregateErrors(result.Errors);
                _faultHandler.Handle(command, errors);
            }
        }

        private static string AggregateErrors(IList<ValidationFailure> errors)
        {
            var builder = new StringBuilder();
            var count = 1;

            foreach (var error in errors)
            {
                builder.AppendFormat("Error {0}: {1} \n", count, error.ErrorMessage);
            }

            return builder.ToString();
        }
    }
}
