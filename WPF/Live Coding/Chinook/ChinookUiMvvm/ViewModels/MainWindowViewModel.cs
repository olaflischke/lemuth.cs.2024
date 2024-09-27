using dal = ChinookDal.Model; // Benannte Namespaces, nützlich bei Doppeldeutigkeiten
using mdl = ChinookDataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System.Windows.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ChinookDal.Model;
using ChinookUiMvvm.Views;
using ChinookDataAccess; // Vorsicht: ViewModel kennt Views!

namespace ChinookUiMvvm.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private dal.Artist? _selectedArtist;
    private dal.Album? _selectedAlbum;

    public event PropertyChangedEventHandler? PropertyChanged;

    private ChinookDataService dataService;

    private void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public MainWindowViewModel(ChinookDataService dataService)
    {
        this.dataService = dataService;

        //this.dataService = new ChinookDataService(Properties.Settings.Default.ChinookConnection);

        this.AddArtistCommand = new RelayCommand(a => AddArtist(), p => CanAddArtist());
        this.EditArtistCommand = new RelayCommand(a => EditArtist(), p => CanEditArtist());


        this.GenresWithArtists = dataService.GetGenresWithArtists();
    }

    private void EditArtist()
    {
        // Für einen kleinen Dialog kann ShowDialog hier erlaubt werden, trotz MVVM
        AddEditArtist editArtist = new AddEditArtist(this.SelectedArtist);
        if (editArtist.ShowDialog() == true)
        {
            dataService.UpdateArtist(this.SelectedArtist);
        }
    }

    private bool CanEditArtist()
    {
        return this.SelectedArtist != null;
    }

    private void AddArtist()
    {
        throw new NotImplementedException();
    }

    private bool CanAddArtist()
    {
        return true;
    }

    public RelayCommand AddArtistCommand { get; set; }
    public RelayCommand EditArtistCommand { get; set; }

    /// <summary>
    /// Enthält alle Genres mit den jew. dazugeh. Künstlern
    /// </summary>
    public List<mdl.Genre> GenresWithArtists { get; set; }

    public dal.Artist SelectedArtist
    {
        get
        {
            return _selectedArtist;
        }
        set
        {
            if (value != null)
            {
                value.Albums = dataService.GetAlbumsForArtist(value);
            }
            _selectedArtist = value;

            OnPropertyChanged();
        }
    }


    public dal.Album? SelectedAlbum
    {
        get
        {
            return _selectedAlbum;
        }
        set
        {
            _selectedAlbum = value;
            if (_selectedAlbum != null)
            {
                _selectedAlbum.Tracks = dataService.GetTracksForAlbum(_selectedAlbum);
            }

            OnPropertyChanged();
        }
    }




}
