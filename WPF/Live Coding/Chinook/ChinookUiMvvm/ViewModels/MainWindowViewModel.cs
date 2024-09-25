using ChinookDal.Model;
using ChinookUiMvvm.Model;
using Microsoft.EntityFrameworkCore;
using System.Windows.Data;

namespace ChinookUiMvvm.ViewModels;

public class MainWindowViewModel
{
    private Artist selectedArtist;

    public MainWindowViewModel()
    {
        this.AddArtistCommand = new RelayCommand(a => AddArtist(), p => CanAddArtist());
        this.EditArtistCommand = new RelayCommand(a => EditArtist(), p => CanEditArtist());

        using ChinookContext context = GetContext(noTracking: true);

        var genresWithArtists = context.Genres.Select(g => new Model.Genre()
        {
            Name = g.Name,
            Artists = context.Tracks.Where(t => t.GenreId == g.GenreId).Select(t => t.Album.Artist).Distinct().ToList()
        });

        this.GenresWithArtists = genresWithArtists.ToList();
    }

    private void EditArtist()
    {
        throw new NotImplementedException();
    }

    private bool CanEditArtist()
    {
        throw new NotImplementedException();
    }

    private void AddArtist()
    {
        throw new NotImplementedException();
    }

    private bool CanAddArtist()
    {
        throw new NotImplementedException();
    }

    public RelayCommand AddArtistCommand { get; set; }
    public RelayCommand EditArtistCommand { get; set; }

    /// <summary>
    /// Enthält alle Genres mit den jew. dazugeh. Künstlern
    /// </summary>
    public List<Model.Genre> GenresWithArtists { get; set; }

    public Artist SelectedArtist
    {
        get
        {
            return selectedArtist;
        }
        set
        {
            using ChinookContext context = GetContext(noTracking: true);
            value.Albums = context.Albums.Where(al => al.ArtistId == value.ArtistId).ToList();

            selectedArtist = value;

            // TODO: OnPropertyChanged!
        }
    }
    public Album SelectedAlbum { get; set; }



    private ChinookContext GetContext(bool noTracking = false, Action<string> logAction = null)
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Chinook;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        //DbContextOptionsBuilder<ChinookContext> builder = new DbContextOptionsBuilder<ChinookContext>()
        //                                                        .UseSqlServer(Properties.Settings.Default.ChinookConnection);
        DbContextOptionsBuilder<ChinookContext> builder = new DbContextOptionsBuilder<ChinookContext>()
                                                                .UseSqlServer(connectionString);

        if (noTracking)
        {
            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        else
        {
            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        }

        if (logAction != null)
        {
            builder.LogTo(logAction, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        return new ChinookContext(builder.Options);
    }

}
