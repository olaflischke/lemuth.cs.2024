using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace EierfarmBl;

public abstract class Gefluegel : IEiLeger, IGefluegel, IEigenschaftGeaendert, INotifyPropertyChanged, IDataErrorInfo
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string propName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }

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

    public ObservableCollection<Ei> Eier { get; set; } = new ObservableCollection<Ei>();

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
            OnPropertyChanged();
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
            OnPropertyChanged();
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
            OnPropertyChanged();
        }
    }

    #region IDataErrorInfo
    public string Error { get; }

    public string this[string propertyName]
    {
        get
        {
            string? result = null!;

            if (propertyName == nameof(this.Name))
            {
                if (string.IsNullOrWhiteSpace(this.Name))
                {
                    result = "Name darf nicht leer bleiben!";
                }
            }

            if (propertyName == nameof(this.Gewicht))
            {
                if (this.Gewicht <= 0 || this.Gewicht > 5000)
                {
                    result = "Gewicht muss görßer al 0 und kleiner als 5.000 sein.";
                }
            }

            return result;
        }
    }
    #endregion

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