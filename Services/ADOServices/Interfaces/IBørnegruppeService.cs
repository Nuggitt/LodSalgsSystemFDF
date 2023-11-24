using LodSalgsSystemFDF.Models;

namespace LodSalgsSystemFDF.Services.ADOServices.Interfaces
{
    public interface IBørnegruppeService
    {
        IEnumerable<Børnegruppe> GetBørnegruppe();
        Børnegruppe GetBørnegruppeId(int id);
        Børnegruppe CreateBørnegruppe(Børnegruppe børnegruppe);
        Børnegruppe DeleteBørnegruppe(Børnegruppe børnegruppe);
        Børnegruppe UpdateBørnegruppe(Børnegruppe børnegruppe);
    }
}
