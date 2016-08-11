using FluentValidation;
using HomeManager.Desktop.Infrastructure.Commands;
using HomeManager.Desktop.Infrastructure.Constants;

namespace HomeManager.Desktop.Infrastructure.Validators
{
    public class NothingDependentViewModelValidator : AbstractValidator<NothingDependentViewModelCommand>
    {
        public NothingDependentViewModelValidator()
        {
            RuleFor(x => x.Action).Must(a => a == DependentViewModelActions.Nothing);
        }
    }
}
