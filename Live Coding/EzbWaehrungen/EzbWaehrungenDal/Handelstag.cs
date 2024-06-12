namespace EzbWaehrungenDal;

public class Handelstag
{
    public List<Waehrung> Waehrungen { get; set; } = new List<Waehrung>();

    public DateTime Datum { get; set; }
}