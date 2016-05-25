using System;
using System.Reflection;
using System.Text.RegularExpressions;
using HomeManager.Infrastructure.Extensions;

namespace HomeManager.Infrastructure.MVVM.Pattern
{
    public class DefaultViewModelBuilder : IViewModelTypeBuilder
    {
        private readonly IConfigureViewModel _configuration;

        public DefaultViewModelBuilder(IConfigureViewModel configuration)
        {
            configuration.NullGuard();

            _configuration = configuration;
        }

        public Type CreateViewModelType(Type viewType)
        {
            viewType.NullGuard();
            
            Assembly asm = viewType.Assembly;

            var viewModel = asm.GetType(Regex.Replace(viewType.FullName,
                _configuration.ViewFolder,
                _configuration.ViewModelFolder) + _configuration.ViewModelName);

            return viewModel;
        }
    }
}