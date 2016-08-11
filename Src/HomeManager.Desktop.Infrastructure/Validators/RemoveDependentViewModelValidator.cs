using FluentValidation;
using HomeManager.Desktop.Infrastructure.Commands;
using HomeManager.Desktop.Infrastructure.Constants;

namespace HomeManager.Desktop.Infrastructure.Validators
{
    public class RemoveDependentViewModelValidator : AbstractValidator<RemoveDependentViewModelCommand>
    {
        public RemoveDependentViewModelValidator()
        {
            RuleFor(x => x.Action).Must(a => a == DependentViewModelActions.Remove);
            RuleFor(x => x.OldItems).NotNull();
        }
    }
}
