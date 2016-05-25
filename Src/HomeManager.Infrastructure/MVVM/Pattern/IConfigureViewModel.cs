namespace HomeManager.Infrastructure.MVVM.Pattern
{
    public interface IConfigureViewModel
    {
        string ViewModelFolder { get; }

        string ViewFolder { get; }

        string ViewModelName { get; }

        string ViewName { get; }
    }
}