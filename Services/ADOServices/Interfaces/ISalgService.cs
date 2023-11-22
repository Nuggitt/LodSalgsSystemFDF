using LodSalgsSystemFDF.Models;

namespace LodSalgsSystemFDF.Services.ADOServices.Interfaces
{
    public interface ISalgService
    {
        IEnumerable<Salg> GetSalgs();

        Salg CreateSalg(Salg salg);
    }
}
