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

        IEnumerable<Salg> GetBørnegruppeByID(int ID);
        IEnumerable<Salg> PriceFilter(float maxPrice, float minPrice);

        IEnumerable<Salg> GetBørnById(int id, int bid);

        IEnumerable<Leder> GetLederOptions();

        IEnumerable<Salg> GetAntalSolgteLodseddelerDESC();

        IEnumerable<Salg> GetAntalSolgteLodseddelerASC();
    }
}

