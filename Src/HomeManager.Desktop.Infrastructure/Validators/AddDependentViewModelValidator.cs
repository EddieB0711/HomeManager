using FluentValidation;
using HomeManager.Desktop.Infrastructure.Commands;
using HomeManager.Desktop.Infrastructure.Constants;

namespace HomeManager.Desktop.Infrastructure.Validators
{
    public class AddDependentViewModelValidator : AbstractValidator<AddDependentViewModelCommand>
    {
        public AddDependentViewModelValidator()
        {
            RuleFor(x => x.Action).Must(a => a == DependentViewModelActions.Add);
            RuleFor(x => x.NewItems).NotNull();
        }
    }
}
