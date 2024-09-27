using ChinookDataAccess;

namespace ChinookDataAccessUnitTests
{
    public class Tests
    {
        string conn = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Chinook;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanGetGenresWithArtists()
        {
            ChinookDataService service = new ChinookDataService(conn);

            List<ChinookDataAccess.Model.Genre> result = service.GetGenresWithArtists();

            Assert.NotNull(result[0].Artists);
            Assert.AreEqual(25, result.Count());
        }
    }
}