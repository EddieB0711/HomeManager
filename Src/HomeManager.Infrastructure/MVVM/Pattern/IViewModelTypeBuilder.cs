using System;

namespace HomeManager.Infrastructure.MVVM.Pattern
{
    public interface IViewModelTypeBuilder
    {
        Type CreateViewModelType(Type viewType);
    }
}