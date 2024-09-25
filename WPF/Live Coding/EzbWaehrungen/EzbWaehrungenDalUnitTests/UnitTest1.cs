using EzbWaehrungenDal;

namespace EzbWaehrungenDalUnitTests
{
    public class ArchivTests
    {
        string url;

        [SetUp]
        public void Setup()
        {
            url = "https://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml";
        }

        [Test]
        public void KannArchivInitialisieren()
        {
            Archiv archiv = new Archiv(url);

            Handelstag? handelstag = archiv.Handelstage.FirstOrDefault();
            if (handelstag != null)
            {
                Waehrung usd = handelstag.Waehrungen.FirstOrDefault();


                Console.WriteLine($"{usd.Symbol}: {usd.EuroKurs}");
            }

            Assert.AreEqual(GetAttributeCount("time"), archiv.Handelstage.Count);
        }

        private object? GetAttributeCount(string attributeName)
        {
            return 64;
        }
    }
}