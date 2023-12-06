using LodSalgsSystemFDF.Models;

namespace LodSalgsSystemFDF.Services.ADOServices.Interfaces
{
    public interface ISalgService
    {
        IEnumerable<Salg> GetSalgs();

        Salg GetSalgById(int id);

        Salg CreateSalg(Salg salg);

        Salg DeleteSalg(Salg salg);

        Salg UpdateSalg(Salg salg);

        IEnumerable<Salg> GetBørnegruppeByID(string ID);
    }
}
