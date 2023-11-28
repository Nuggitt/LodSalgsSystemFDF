using LodSalgsSystemFDF.Models;

namespace LodSalgsSystemFDF.Services.ADOServices.Interfaces
{
    public interface ILederService
    {
        IEnumerable<Leder> GetLeder();
        Leder CreateLeder(Leder leder);
        Leder DeleteLeder(Leder leder);
        Leder GetLederByID(int Leder_ID);
    }
}
