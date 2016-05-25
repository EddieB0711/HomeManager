using System.Windows;
using DevExpress.Xpf.Core;
using HomeManager.Desktop.ResolveDependencies;

namespace HomeManager.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ThemeManager.ApplicationThemeName = Theme.Office2013Name;

            var bootstrapper = new DefaultBootstrapper();

            bootstrapper.Run();
        }
    }
}