using ChinookDal.Model;
using Microsoft.EntityFrameworkCore;

namespace ChinookDalTests
{
    public class Tests
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Chinook;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void HatGenreTracks()
        {
            ChinookContext context = GetContext(queryTrackingBehavior: QueryTrackingBehavior.NoTracking, Console.WriteLine);

            var genres = context.Genres.Include(g => g.Tracks).ToList();

            Console.WriteLine($"Anzahl der Tracks mit Genre {genres.FirstOrDefault().Name}: {genres.FirstOrDefault().Tracks.Count}");

            Assert.Pass();
        }

        [Test]
        public void HatGenreTracksMitIR()
        {
            ChinookContext context = GetContext(queryTrackingBehavior: QueryTrackingBehavior.NoTrackingWithIdentityResolution, logAction: Console.WriteLine);

            var tracks = context.Tracks.ToList();

            var genres = context.Genres.ToList();

            Console.WriteLine($"Anzahl der Tracks mit Genre {genres.FirstOrDefault().Name}: {genres.FirstOrDefault().Tracks.Count}");

            Assert.Pass();
        }

        [Test]
        public void ArtistsEinesGenres()
        {
            ChinookContext context = GetContext(queryTrackingBehavior: QueryTrackingBehavior.TrackAll, logAction: Console.WriteLine);

            Genre? genre = context.Genres.FirstOrDefault();
            if (genre != null)
            {
                IQueryable<Artist> qArtists = context.Tracks
                                                    //.Include(tr => tr.Album)
                                                    //.ThenInclude(al => al.Artist)//.AsTracking() 
                                                    .Where(tr => tr.Genre.GenreId == genre.GenreId)
                                                    .Select(tr => tr.Album.Artist).Distinct();

                var alben = context.Albums.ToList();

                foreach (Artist artist in qArtists)
                {
                    Console.WriteLine($"{artist.Name} - {artist.Albums.Count}");

                }

                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }


        private ChinookContext GetContext(QueryTrackingBehavior queryTrackingBehavior = QueryTrackingBehavior.NoTracking, Action<string> logAction = null)
        {
            DbContextOptionsBuilder<ChinookContext> builder = new DbContextOptionsBuilder<ChinookContext>()
                                                                    .UseSqlServer(connectionString)
                                                                    .UseQueryTrackingBehavior(queryTrackingBehavior);

            if (logAction != null)
            {
                builder.LogTo(logAction, Microsoft.Extensions.Logging.LogLevel.Information);
            }

            return new ChinookContext(builder.Options);
        }

    }
}