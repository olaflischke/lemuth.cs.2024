using ChinookUiMvvm.Infrastructure;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ChinookUiMvvm
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        StartupController startupController;
        public IServiceProvider Services { get => startupController.Services; }

        public new static App Current => (App)Application.Current;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            startupController = new StartupController(); // liefert den DI-Container
            startupController.Run(); // zeigt das erste Fenster an
        }
    }

}
