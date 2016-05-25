using System;
using HomeManager.Infrastructure.MVVM.Events;
using Prism.Events;

namespace HomeManager.Infrastructure.Events
{
    public class ModuleChangingEvent : AsyncPubSubEvent<DataEventArgs<Type>>
    {
    }
}