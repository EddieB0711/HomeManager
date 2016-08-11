using HomeManager.Desktop.Infrastructure.Commands;
using HomeManager.Infrastructure.Builders;

namespace HomeManager.Desktop.Infrastructure.Builders.CommandToCommand
{
    public class AddDependentViewModelToRemoveDependentViewModelCommandBuilder : ICommandBuilder<AddDependentViewModelCommand, RemoveDependentViewModelCommand>
    {
        public RemoveDependentViewModelCommand Create(AddDependentViewModelCommand command, params object[] valueObjects)
        {
            return RemoveDependentViewModelCommand.Create(command.Region, command.OldItems, command.Action);
        }
    }
}
