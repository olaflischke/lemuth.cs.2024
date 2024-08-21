using System.Collections.ObjectModel;

namespace EierfarmBl;

public interface IEiLeger
{
    double Gewicht { get; set; }
    ObservableCollection<Ei> Eier { get; set; }

    void EiLegen();
}