using System.Windows;
using System.Windows.Input;
using HomeManager.Infrastructure.MVVM.Commands;

namespace HomeManager.Desktop.Infrastructure.Behaviours
{
    public static class ViewModelBehaviour
    {
        public static readonly DependencyProperty LoadedCommandProperty = 
            DependencyProperty.RegisterAttached("LoadedCommand", typeof(ICommand), 
                typeof(ViewModelBehaviour), new PropertyMetadata(null, OnLoadedCommandChanged));

        public static ICommand GetLoadedCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(LoadedCommandProperty);
        }

        public static void SetLoadedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(LoadedCommandProperty, value);
        }

        private static void OnLoadedCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var frameworkElement = d as FrameworkElement;
            if (frameworkElement != null && e.NewValue is ICommand)
            {
                frameworkElement.Initialized += async (o, args) =>
                {
                    var command = e.NewValue as DelegateAsyncCommand<object>;
                    if (command != null) await command.ExecuteAsync(null);
                };
            }
        }
    }
}