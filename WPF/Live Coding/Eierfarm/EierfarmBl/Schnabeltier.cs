using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace EierfarmBl;

public class Schnabeltier : Saeugetier, IEiLeger, IDisposable
{
    public double Gewicht { get; set; }
    public ObservableCollection<Ei> Eier { get; set; } = new ObservableCollection<Ei>();

    public void Dispose()
    {
        //throw new NotImplementedException();
    }

    public void EiLegen()
    {
        if (this.Gewicht > 2000)
        {
            Ei ei = new Ei(this);
            this.Eier.Add(ei);
            this.Gewicht -= ei.Gewicht;
        }
    }

    public override void Saeugen()
    {
        throw new NotImplementedException();
    }
}