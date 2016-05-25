using System;

namespace HomeManager.Infrastructure.MVVM.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependentViewModelAttribute : Attribute
    {
        public DependentViewModelAttribute(Type viewType, string region)
        {
            Type = viewType;
            Region = region;
        }

        public Type Type { get; private set; }

        public string Region { get; private set; }
    }
}