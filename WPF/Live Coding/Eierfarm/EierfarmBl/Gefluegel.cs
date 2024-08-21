using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EierfarmBl;

public abstract class Gefluegel : IEiLeger, IGefluegel
{
    public event EventHandler<GefluegelEventArgs> EigenschaftGeaendert;

    private void OnEigenschaftGeaendert(string propName)
    {
        if (EigenschaftGeaendert != null)
        {
            EigenschaftGeaendert(this, new GefluegelEventArgs(propName));
        }
    }


    public Gefluegel(string name)
    {
        this.Name = name;
    }

    public List<Ei> Eier { get; set; } = new List<Ei>();

    private double _gewicht;
    private Guid id = Guid.NewGuid();
    private string name;

    public double Gewicht
    {
        get
        {
            return _gewicht;
        }
        set
        {
            _gewicht = value;
            OnEigenschaftGeaendert("Gewicht");
        }
    }


    public Guid Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
            OnEigenschaftGeaendert(nameof(this.Id));
        }
    }
    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
            OnEigenschaftGeaendert("Name");
        }
    }

    public abstract void Fressen();
    public abstract void EiLegen();

    //public override string ToString()
    //{
    //    return $"Ich heiße {Name}";
    //}
}

public class GefluegelEventArgs : EventArgs
{
    public GefluegelEventArgs(string geaenderteEigenschaft)
    {
        this.GeaenderteEigenschaft = geaenderteEigenschaft;
    }

    public string GeaenderteEigenschaft { get; set; }
}