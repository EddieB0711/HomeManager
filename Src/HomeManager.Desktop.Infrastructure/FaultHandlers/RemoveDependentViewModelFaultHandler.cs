using System;
using HomeManager.Desktop.Infrastructure.Commands;
using HomeManager.Infrastructure.Handlers;

namespace HomeManager.Desktop.Infrastructure.FaultHandlers
{
    public class RemoveDependentViewModelFaultHandler : IFaultHandler<RemoveDependentViewModelCommand>
    {
        public void Handle(RemoveDependentViewModelCommand command, string errors)
        {
            throw new Exception(string.Format("Error removing views from {0}. ", command.Region.Name), new Exception(errors));
        }
    }
}
