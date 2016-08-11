using HomeManager.Desktop.Infrastructure.Commands;
using HomeManager.Infrastructure.Builders;

namespace HomeManager.Desktop.Infrastructure.Builders.CommandToCommand
{
    public class NothingDependentViewModelCommandToAddDependentViewModelCommandBuilder : 
        ICommandBuilder<NothingDependentViewModelCommand, AddDependentViewModelCommand>
    {
        public AddDependentViewModelCommand Create(NothingDependentViewModelCommand command, params object[] valueObjects)
        {
            return AddDependentViewModelCommand.Create(command.Region, command.NewItems, command.OldItems, command.Action);
        }
    }
}
