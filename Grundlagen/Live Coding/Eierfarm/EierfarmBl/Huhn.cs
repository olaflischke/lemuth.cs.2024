namespace EierfarmBl;

public class Huhn : Gefluegel
{
    public Huhn() : base("Neues Huhn")
    {
        this.Gewicht = 1000;
    }

    public Huhn(string name) : this()
    {
        this.Name = name;
    }


    public override void Fressen()
    {
        if (this.Gewicht < 3000)
        {
            //this.Gewicht = this.Gewicht + 100;
            this.Gewicht += 100;
        }
    }

    public override void EiLegen()
    {
        if (this.Gewicht > 1500)
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

