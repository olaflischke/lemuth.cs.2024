namespace EierfarmBl;

public interface IEiLeger
{
    double Gewicht { get; set; }
    List<Ei> Eier { get; set; }

    void EiLegen();
}