using LodSalgsSystemFDF.Models;

namespace LodSalgsSystemFDF.Services.ADOServices.Interfaces
{
    public interface IBornegruppeService
    {
        Task<IEnumerable<Bornegruppe>> GetBornegruppeAsync();
        Task<Bornegruppe>  GetBornegruppeIdAsync(int id);
        Bornegruppe CreateBornegruppe(Bornegruppe bornegruppe);
        Bornegruppe DeleteBornegruppe(Bornegruppe bornegruppe);
        Bornegruppe UpdateBornegruppe(Bornegruppe bornegruppe);
        IEnumerable<Bornegruppe> GetBornegruppeByName(string Name);
        IEnumerable<Bornegruppe> GetAllBornegruppeIDDESC();
        IEnumerable<Bornegruppe> GetAllBornegruppeIDASC();
        IEnumerable<Bornegruppe> SortAllGruppeNavnDESC();
        IEnumerable<Bornegruppe> SortAllGruppeNavnASC();
        IEnumerable<Bornegruppe> SortAllAntalBornDESC();
        IEnumerable<Bornegruppe> SortAllAntalBornASC();
        IEnumerable<Bornegruppe> SortAllLederIDDESC();
        IEnumerable<Bornegruppe> SortAllLederIDASC();
        IEnumerable<Bornegruppe> SortAllAntalLodSeddelerPrGruppeDESC();
        IEnumerable<Bornegruppe> SortAllAntalLodSeddelerPrGruppeASC();
        IEnumerable<Bornegruppe> SortAllAntalSolgtePrGruppeDESC();
        IEnumerable<Bornegruppe> SortAllAntalSolgtePrGruppeASC();

        Bornegruppe TildelLodsedlerBornegruppe(Bornegruppe bornegruppe, int amount);

        IEnumerable<Leder>GetLederOptions();

    }
}
