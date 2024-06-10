namespace EierfarmBl;

public class Huhn
{
    public Huhn()
    {
        this.Name = "Neues Huhn";
        this.Gewicht = 1000;
    }

    
    public string Name { get; set; }
    public Guid Id { get; set; } = Guid.NewGuid();
    public double Gewicht { get; set; }
    public List<Ei> Eier { get; set; }

    public void Fressen()
    {
        if (this.Gewicht < 3000)
        {
            //this.Gewicht = this.Gewicht + 100;
            this.Gewicht += 100;
        }
    }

    public void EiLegen()
    {
        if (this.Gewicht > 2000)
        {
            Ei ei = new Ei(this);
            //{
            //    Mutter = this,
            //};
            //ei.Mutter = this;

            this.Gewicht -= ei.Gewicht;
            this.Eier.Add(ei);
        }
    }
}

