using System.ComponentModel.DataAnnotations;

namespace EierfarmBl; // File-scoped namespce = Namespace gilt für gesamte Datei

public class Ei
{
    // Öffentliches Feld  - meinEi.Gewicht2
    public double Gewicht2;

    // Konstruktor für ein Ei
    public Ei(IEiLeger mutter)
    {
        Random zufall = new Random();

        //_gewicht = 65;
        this.Gewicht = zufall.Next(45, 81);

        this.Legedatum = DateTime.Now;

        this.Farbe = (EiFarbe)zufall.Next(Enum.GetNames(typeof(EiFarbe)).Length);

        this.Mutter = mutter;
    }

    public Ei(double gewicht)// : this()
    {
        this.Gewicht = gewicht;
    }

    // Full-qualified Property
    // Vollständige Eigenschaft

    // Backing Field
    // Speicherfeld im Hintergrund
    private double _gewicht;


    public double Gewicht
    {
        // var g = meinEi.Gewicht;
        get
        {
            return _gewicht;
        }

        // meinEi.Gewicht=65;
        set
        {
            if (value > 0)
            {
                _gewicht = value;
            }
        }

    }

    // Auto-Property (Property mit durch den Compiler automatzisch generiertem Backing Field)
    public DateTime Legedatum { get; set; }
    public DateTime Verfallsdatum
    {
        get
        {
            return Legedatum.AddDays(28);
        }
        private set { }
    }

    public Guid Id { get; set; } = Guid.NewGuid(); // Auto-Property Initializer

    public EiFarbe Farbe { get; set; }

    public IEiLeger Mutter { get; set; }
}

public enum EiFarbe
{
    Hell,
    Dunkel,
    Gruen
}
