namespace HomeManager.Infrastructure.MVVM.Pattern
{
    public class DefaultViewModelConfiguration : IConfigureViewModel
    {
        public string ViewFolder
        {
            get
            {
                return "Views";
            }
        }

        public string ViewModelFolder
        {
            get
            {
                return "ViewModels";
            }
        }

        public string ViewModelName
        {
            get
            {
                return "Model";
            }
        }

        public string ViewName
        {
            get
            {
                return "View";
            }
        }
    }
}