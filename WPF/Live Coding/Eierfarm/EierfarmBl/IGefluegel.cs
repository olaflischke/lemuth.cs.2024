
using System.Collections.ObjectModel;

namespace EierfarmBl
{
    public interface IGefluegel
    {
        ObservableCollection<Ei> Eier { get; set; }
        double Gewicht { get; set; }

        void EiLegen();
    }
}