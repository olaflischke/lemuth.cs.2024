using ChinookDataAccess;
using ChinookUiMvvmToolkit.ViewModels;
using ChinookUiMvvmToolkit.Views;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging; // nuget-Paket

namespace ChinookUiMvvmToolkit.Infrastructure
{
    public class StartupController
    {
        string connectionString;

        public StartupController()
        {
            connectionString = Properties.Settings.Default.ChinookConnection;

            ServiceCollection services = new ServiceCollection();

            // Logging
            services.AddLogging(builder =>
            {
                builder.AddFileLogger(Properties.Settings.Default.LogPath);
                builder.SetMinimumLevel(LogLevel.Information);
            });

            // Messaging
            services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);

            // Fenster
            services.AddSingleton<MainWindow>();
            services.AddSingleton<AddEditArtist>();

            // ViewModels
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<AddEditArtistViewModel>();

            // Services
            services.AddSingleton<ChinookDataService>(p => new ChinookDataService(connectionString));


            this.Services = services.BuildServiceProvider();
        }

        public IServiceProvider Services { get; }



        internal void Run() // ersetzt die StartupUri in der App.xaml
        {
            MainWindow mainWindow = this.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
