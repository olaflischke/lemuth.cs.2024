using ChinookDal.Model;
using ChinookDataAccess;
using ChinookUiMvvmToolkit.Infrastructure;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using System.Windows.Input;

namespace ChinookUiMvvmToolkit.ViewModels;

public partial class MainWindowViewModel : ObservableRecipient
{
    ChinookDataService dataService; //= new ChinookDataService(Properties.Settings.Default.ChinookConnection);

    private ILogger logger;
    private readonly IMessenger messenger;

    public MainWindowViewModel(ILogger<MainWindowViewModel> logger, IMessenger messenger, ChinookDataService dataService)
    {
        this.logger = logger;
        this.messenger = messenger;
        this.dataService = dataService;

        messenger.Register<MainWindowViewModel, ArtistChangedMessage>(this, (recipient, message) => recipient.Receive(message));

        this.AddArtistCommand = new RelayCommand(AddArtist);

        this.GenresWithArtists = dataService.GetGenresWithArtists();

        logger.LogInformation("MainWindowViewModel initialisiert.");
    }

    private void Receive(ArtistChangedMessage m)
    {
        dataService.UpdateArtist(this.SelectedArtist);
    }

    private void AddArtist()
    {
        throw new NotImplementedException();
    }

    //private Artist _selectedArtist;

    //public Artist SelectedArtist
    //{
    //    get { return _selectedArtist; }
    //    set { SetProperty(ref _selectedArtist, value); }
    //}

    //[ObservableProperty]
    //[NotifyCanExecuteChangedFor(nameof(EditArtistCommand))]
    private Artist? _selectedArtist;

    public Artist? SelectedArtist
    {
        get => _selectedArtist;
        set
        {
            //try
            //{
            if (value != null)
            {
                logger.LogDebug($"Ausgewählter Artist: {value.Name} (ID: {value.ArtistId})");
                value.Albums = dataService.GetAlbumsForArtist(value);
                //throw new Exception("Generierter Fehler", new Exception("Noch eine Exception"));
            }
            SetProperty(ref _selectedArtist, value);
            EditArtistCommand.NotifyCanExecuteChanged();

        }
        //catch (Exception ex)
        //{
        //    logger.LogError($"Fehler beim Artistauswählen: {ex.ToString()}: {ex.Message}\n\r{ex.InnerException?.ToString()}: {ex.InnerException?.Message}");
        //}
        //}
    }

    [ObservableProperty]
    private Album? _selectedAlbum;

    [RelayCommand(CanExecute = nameof(CanEditArtist))]
    private void EditArtist()
    {
        messenger.Send(new ShowArtistEditDialogMessage(this.SelectedArtist));
    }

    private bool CanEditArtist()
    {
        return this.SelectedArtist != null;
    }

    private List<ChinookDataAccess.Model.Genre> _genre;
    public List<ChinookDataAccess.Model.Genre> GenresWithArtists
    {
        get => _genre;
        set => SetProperty(ref _genre, value);
    }

    public ICommand AddArtistCommand { get; set; }
}
