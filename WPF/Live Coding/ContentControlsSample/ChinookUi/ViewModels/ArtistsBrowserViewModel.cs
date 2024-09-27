using ChinookDal.Model;
using System.Security.Policy;

namespace ChinookUi.ViewModels;

public class ArtistsBrowserViewModel : BaseViewModel
{
    public ArtistsBrowserViewModel()
    {
        using ChinookContext context = GetContext(noTracking: true);

        var genresWithArtists = context.Genres.Select(g => new Model.Genre()
        {
            Name = g.Name,
            Artists = context.Tracks.Where(t => t.GenreId == g.GenreId).Select(t => t.Album.Artist).Distinct().ToList()
        });

        this.GenresWithArtists = genresWithArtists.ToList();
    }

    public List<Model.Genre> GenresWithArtists { get; set; }

    private Artist? _selectedArtist;

    public Artist? SelectedArtist
    {
        get
        {
            return _selectedArtist;
        }
        set
        {
            _selectedArtist = value;
            if (_selectedArtist != null)
            {
                using ChinookContext context = GetContext(noTracking: true);
                _selectedArtist.Albums = context.Albums.Where(a => a.ArtistId == _selectedArtist.ArtistId).ToList();
            }
            OnPropertyChanged();
        }
    }

    private Album _selectedAlbum;

    public Album SelectedAlbum
    {
        get { return _selectedAlbum; }
        set
        {
            _selectedAlbum = value;
            if (_selectedAlbum != null)
            {
                using ChinookContext context = GetContext(noTracking: true);
                _selectedAlbum.Tracks = context.Tracks.Where(t => t.AlbumId == _selectedAlbum.AlbumId).ToList();
            }
            OnPropertyChanged();
        }
    }

}
