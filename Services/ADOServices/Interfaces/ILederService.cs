using LodSalgsSystemFDF.Models;

namespace LodSalgsSystemFDF.Services.ADOServices.Interfaces
{
    public interface ILederService
    {
        Task<IEnumerable<Leder>> GetLederAsync();
        Leder CreateLeder(Leder leder);
        Leder DeleteLeder(Leder leder);
        Leder GetLederByID(int Leder_ID);
        Leder UpdateLeder(Leder leder);
        IEnumerable<Leder> GetLederByName(string Navn);
        IEnumerable<Leder> GetAllLederNavnDescending();

    }
}
