
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace EzbWaehrungenDal;

public class Archiv
{
    private ILogger? _logger;

    //public Archiv(string url)
    //{
    //    this.Handelstage = GetData(url);
    //}

    public Archiv(string url, ILogger logger = null)
    {
        _logger = logger;
        this.Handelstage = GetData(url);

        SaveToDb();
    }

    private void SaveToDb()
    {
        DbContextOptionsBuilder<WaehrungsContext> builder = new DbContextOptionsBuilder<WaehrungsContext>().UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WaehrungenDb;Integrated Security=True");

        WaehrungsContext context = new WaehrungsContext(builder.Options);

        context.Database.EnsureCreated();

        context.Handelstage.AddRange(this.Handelstage);
        context.SaveChanges();
    }

    public List<Handelstag>? Handelstage { get; set; }

    private List<Handelstag>? GetData(string url)
    {
        try
        {
            XDocument document = XDocument.Load(url);

            var q = document.Root?.Descendants()
                                .Where(xe => xe.Name.LocalName == "Cube" && xe.Attributes().Count() == 1)
                                // Projektion
                                .Select(xe => new Handelstag(xe));
            //.Select(xe => new Handelstag() {
            //            Datum = Convert.ToDateTime(xe.Attribute("time")?.Value)
            //            });


            //List<Handelstag> tage = new List<Handelstag>();

            //foreach (XElement element in q)
            //{
            //    Handelstag tag = new Handelstag() { Datum = Convert.ToDateTime(element.Attribute("time")?.Value) };
            //    tage.Add(tag);
            //}

            List<Handelstag> tage = q.ToList();

            return tage;
        }
        catch (Exception ex)
        {
            _logger?.LogError("Fehler beim Datenlesen");
            throw new EzbWaehrungenDalException("Fehler beim Datenlesen", ex);
        }
    }

    // Beispiel einer Log-Methode mit Absender und Zeilennummer
    private void Log(string message, [CallerMemberName] string sender = "", [CallerLineNumber] int zeile = 0)
    {

    }
}
