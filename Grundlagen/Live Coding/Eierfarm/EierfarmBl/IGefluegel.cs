
namespace EierfarmBl
{
    public interface IGefluegel
    {
        List<Ei> Eier { get; set; }
        double Gewicht { get; set; }

        void EiLegen();
    }
}