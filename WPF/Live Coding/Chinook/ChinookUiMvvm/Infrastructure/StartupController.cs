using ChinookDataAccess;
using ChinookUiMvvm.ViewModels;
using ChinookUiMvvm.Views;
using Microsoft.Extensions.DependencyInjection; // nuget-Paket

namespace ChinookUiMvvm.Infrastructure
{
    public class StartupController
    {
        string connectionString;

        public StartupController()
        {
            connectionString = Properties.Settings.Default.ChinookConnection;

            ServiceCollection services = new ServiceCollection();

            // Fenster
            services.AddSingleton<MainWindow>();

            // ViewModels
            services.AddSingleton<MainWindowViewModel>();

            // Services
            services.AddSingleton<ChinookDataService>(p => new ChinookDataService(connectionString));

            this.Services = services.BuildServiceProvider();
        }

        public IServiceProvider Services { get; internal set; }



        internal void Run() // ersetzt die StartupUri in der App.xaml
        {
            MainWindow mainWindow = this.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
