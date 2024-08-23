namespace EzbWaehrungenDal;

public class Waehrung
{

    public int Id { get; set; }
    public string Symbol { get; set; }
    public double EuroKurs { get; set; }
    public Handelstag Handelstag { get; set; }
}
