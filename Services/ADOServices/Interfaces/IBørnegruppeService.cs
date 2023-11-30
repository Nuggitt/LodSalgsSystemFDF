using LodSalgsSystemFDF.Models;

namespace LodSalgsSystemFDF.Services.ADOServices.Interfaces
{
    public interface IBørnegruppeService
    {
        Task<IEnumerable<Børnegruppe>> GetBørnegruppeAsync();
        Task<Børnegruppe>  GetBørnegruppeIdAsync(int id);
        Børnegruppe CreateBørnegruppe(Børnegruppe børnegruppe);
        Børnegruppe DeleteBørnegruppe(Børnegruppe børnegruppe);
        Børnegruppe UpdateBørnegruppe(Børnegruppe børnegruppe);
        IEnumerable<Børnegruppe> GetBørnegruppeByName(string Name);
    }
}
