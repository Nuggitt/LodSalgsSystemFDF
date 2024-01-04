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
        IEnumerable<Børnegruppe> GetAllBørnegruppeIDDESC();
        IEnumerable<Børnegruppe> GetAllBørnegruppeIDASC();
        IEnumerable<Børnegruppe> SortAllGruppeNavnDESC();
        IEnumerable<Børnegruppe> SortAllGruppeNavnASC();
        IEnumerable<Børnegruppe> SortAllAntalBørnDESC();
        IEnumerable<Børnegruppe> SortAllAntalBørnASC();
        IEnumerable<Børnegruppe> SortAllLederIDDESC();
        IEnumerable<Børnegruppe> SortAllLederIDASC();
        IEnumerable<Børnegruppe> SortAllAntalLodSeddelerPrGruppeDESC();
        IEnumerable<Børnegruppe> SortAllAntalLodSeddelerPrGruppeASC();
        IEnumerable<Børnegruppe> SortAllAntalSolgtePrGruppeDESC();
        IEnumerable<Børnegruppe> SortAllAntalSolgtePrGruppeASC();

        Børnegruppe TildelLodsedlerBørnegruppe(Børnegruppe børnegruppe, int amount);

        IEnumerable<Leder>GetLederOptions();

    }
}
