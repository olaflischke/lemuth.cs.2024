
namespace EzbWaehrungenDal;

public class Archiv
{
    public Archiv(string url)
    {
        this.Handelstage = GetData(url);
    }

    private List<Handelstag> GetData(string url)
    {
        throw new NotImplementedException();
    }

    public List<Handelstag> Handelstage { get; set; } 

}
