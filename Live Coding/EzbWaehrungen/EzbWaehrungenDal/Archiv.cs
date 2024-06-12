
using System.Xml.Linq;

namespace EzbWaehrungenDal;

public class Archiv
{
    public Archiv(string url)
    {
        this.Handelstage = GetData(url);
    }

    public List<Handelstag> Handelstage { get; set; }

    private List<Handelstag> GetData(string url)
    {
        XDocument document = XDocument.Load(url);

        var q = document.Root?.Descendants()
                            .Where(xe => xe.Name.LocalName == "Cube" && xe.Attributes().Count() == 1)
                            // Projektion
                            .Select(xe => new Handelstag() { Datum = Convert.ToDateTime(xe.Attribute("time")?.Value) });


        //List<Handelstag> tage = new List<Handelstag>();

        //foreach (XElement element in q)
        //{
        //    Handelstag tag = new Handelstag() { Datum = Convert.ToDateTime(element.Attribute("time")?.Value) };
        //    tage.Add(tag);
        //}

        List<Handelstag> tage = q.ToList();

        return tage;
    }


}
