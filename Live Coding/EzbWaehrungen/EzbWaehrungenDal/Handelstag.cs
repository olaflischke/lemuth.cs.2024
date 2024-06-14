using System.Xml.Linq;

namespace EzbWaehrungenDal;

public class Handelstag
{
    public Handelstag(XElement handelstagNode)
    {
        this.Datum = Convert.ToDateTime(handelstagNode.Attribute("time")?.Value);

        this.Waehrungen=handelstagNode.Elements(
    }

    public List<Waehrung> Waehrungen { get; set; } = new List<Waehrung>();

    public DateTime Datum { get; set; }
}