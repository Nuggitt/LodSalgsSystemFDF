using LodSalgsSystemFDF.Models;

namespace LodSalgsSystemFDF.Services.ADOServices.Interfaces
{
    public interface IBornService

    {
        Task<IEnumerable<Born>> GetBorn();

        Task<Born> GetBorn(int id);
        Born CreateBorn(Born born);
        Born UpdateBorn(Born born);
        Born DeleteBorn(Born born);

        Born TildelLodsedler(Born born, int amount);

        IEnumerable<Born> GetAllBornNavnDescending();
        IEnumerable<Born> GetAllBornIDDescending();
        IEnumerable<Born> GetAllBornAntalSolgteLodseddelerDescending();
        IEnumerable<Born> GetAllBornGruppeIDDescending();

        IEnumerable<Born> GetAllBornNavnAscending();
        IEnumerable<Born> GetAllBornIDAscending();
        IEnumerable<Born> GetAllBornAntalSolgteLodseddelerAscending();
        IEnumerable<Born> GetAllBornGruppeIDAscending();

        IEnumerable<Born> GetGivetLodsedlerAscending();
        IEnumerable<Born> GetGivetLodsedlerDescending();

        //IEnumerable<Børn> GetAllBornItems(string Born, string Navn);
        IEnumerable<Born> GetBornByName(string Name);

        Task<IEnumerable<Born>> GetBornInBornegruppe(int id);

        Task<IEnumerable<Born>> GetBornInBornegruppeByID(int id);

        IEnumerable<Bornegruppe> GetBornegruppeOptions();

    }
}
