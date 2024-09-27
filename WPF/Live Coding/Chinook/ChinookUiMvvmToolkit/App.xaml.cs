using ChinookUiMvvmToolkit.Infrastructure;
using ChinookUiMvvmToolkit.Views;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ChinookUiMvvmToolkit
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        StartupController startupController;

        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get => startupController.Services; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Dependency-Injection (DI) Controller instanziieren
            startupController = new StartupController();
            startupController.Run();

            // Messages registieren
            WeakReferenceMessenger.Default.Register<ShowArtistEditDialogMessage>(this, (recipient, message) =>
            {
                // Dialog anzeigen
                AddEditArtist editArtist = new AddEditArtist(message.Artist, startupController.Services.GetRequiredService<IMessenger>());
                editArtist.ShowDialog();
            });

        }

    }

}
