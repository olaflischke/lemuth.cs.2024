using ChinookDal.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace ChinookDataAccess;

public class ChinookDataService
{
    private readonly string connectionString;

    public ChinookDataService(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public List<Model.Genre> GetGenresWithArtists()
    {
        using ChinookContext context = GetContext(noTracking: true);

        IQueryable<Model.Genre> genresWithArtists = context.Genres.Select(g => new Model.Genre()
        {
            Name = g.Name,
            Artists = context.Tracks.Where(t => t.GenreId == g.GenreId).Select(t => t.Album.Artist).Distinct().ToList()
        });

        return genresWithArtists.ToList();
    }

    public void UpdateArtist(Artist artist)
    {
        using ChinookContext context = GetContext();
        context.Update(artist);
        context.SaveChanges();
    }

    public List<Album> GetAlbumsForArtist(Artist artist)
    {
        using ChinookContext context = GetContext(noTracking: true);
        IQueryable<Album> qAlbums = context.Albums.Where(al => al.ArtistId == artist.ArtistId);
        return qAlbums.ToList();
    }

    public List<Track> GetTracksForAlbum(Album album)
    {
        using ChinookContext context = GetContext(noTracking: true);
        IQueryable<Track> qTracks = context.Tracks.Where(tr => tr.AlbumId == album.AlbumId);
        return qTracks.ToList();

    }

    private ChinookContext GetContext(bool noTracking = false, Action<string> logAction = null)
    {
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
