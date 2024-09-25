using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml.Linq;

namespace EzbWaehrungenDal;

public class Handelstag
{
    public Handelstag()
    {

    }

    public Handelstag(XElement handelstagNode)
    {
        this.Datum = Convert.ToDateTime(handelstagNode.Attribute("time")?.Value);

        //CultureInfo ciEzb=new CultureInfo("en-US");
        //NumberFormatInfo nfiEzb = ciEzb.NumberFormat;
        NumberFormatInfo nfiEzb = new NumberFormatInfo() { NumberDecimalSeparator = "." }; // zu lesendes Format beschreiben

        var q = handelstagNode.Elements()
                                // Projektion
                                .Select(nd => new Waehrung()
                                {
                                    Symbol = nd.Attribute("currency").Value,
                                    EuroKurs = Convert.ToDouble(nd.Attribute("rate").Value, nfiEzb), //CultureInfo.InvariantCulture)
                                    Handelstag = this
                                });

        this.Waehrungen = new ObservableCollection<Waehrung>(q);
    }

    public int Id { get; set; }

    public ObservableCollection<Waehrung> Waehrungen { get; set; } = new ObservableCollection<Waehrung>();

    public DateTime Datum { get; set; }
}