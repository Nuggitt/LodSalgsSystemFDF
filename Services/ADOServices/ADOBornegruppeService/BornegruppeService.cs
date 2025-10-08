using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOBornegruppeService
{
    public class BornegruppeService : IBornegruppeService
    {
        private AdonetBornegruppeService bornegruppeService;
        public async Task<IEnumerable<Bornegruppe>> GetBornegruppeAsync()
        {
            return await bornegruppeService.GetAllBornegruppeAsync();
        }
        public BornegruppeService(AdonetBornegruppeService service)
        {
            bornegruppeService = service;
        }
        public async Task<Bornegruppe> GetBornegruppeIdAsync(int id)
        {
            return await bornegruppeService.GetBornegruppeById(id);
        }
        public Bornegruppe CreateBornegruppe(Bornegruppe bornegruppe)
        {
            return bornegruppeService.CreateBornegruppe(bornegruppe);
        }

        public Bornegruppe DeleteBornegruppe(Bornegruppe bornegruppe)
        {
            return bornegruppeService.DeleteBornegruppe(bornegruppe);
        }

        public Bornegruppe UpdateBornegruppe(Bornegruppe bornegruppe)
        {
            return bornegruppeService.UpdateBornegruppe(bornegruppe);
        }
        public IEnumerable<Bornegruppe> GetBornegruppeByName(string Name)
        {
            return bornegruppeService.GetBornegruppeByName(Name);
        }

        public IEnumerable<Bornegruppe> GetAllBornegruppeIDDESC()
        {
            return bornegruppeService.GetAllBornGruppeIDDescending();
        }

        public IEnumerable<Bornegruppe> GetAllBornegruppeIDASC()
        {
            return bornegruppeService.GetAllBornGruppeIDAscending();
        }

        public IEnumerable<Bornegruppe> SortAllGruppeNavnDESC()
        {
            return bornegruppeService.SortAllGruppeNavnDescending();
        }

        public IEnumerable<Bornegruppe> SortAllGruppeNavnASC()
        {
            return bornegruppeService.SortAllGruppeNavnAscending();
        }

        public IEnumerable<Bornegruppe> SortAllAntalBornDESC()
        {
            return bornegruppeService.SortAllAntalBornDescending();
        }

        public IEnumerable<Bornegruppe> SortAllAntalBornASC()
        {
            return bornegruppeService.SortAllAntalBornAscending();
        }

        public IEnumerable<Bornegruppe> SortAllLederIDDESC()
        {
            return bornegruppeService.SortAllLederIDDescending();
        }

        public IEnumerable<Bornegruppe> SortAllLederIDASC()
        {
            return bornegruppeService.SortAllLederIDAscending();
        }

        public IEnumerable<Bornegruppe> SortAllAntalLodSeddelerPrGruppeDESC()
        {
            return bornegruppeService.SortAllAntalLodSeddelerPrGruppeDescending();
        }

        public IEnumerable<Bornegruppe> SortAllAntalLodSeddelerPrGruppeASC()
        {
            return bornegruppeService.SortAllAntalLodSeddelerPrGruppeAscending();
        }

        public IEnumerable<Bornegruppe> SortAllAntalSolgtePrGruppeDESC()
        {
            return bornegruppeService.SortAllAntalSolgtePrGruppeDescending();
        }

        public IEnumerable<Bornegruppe> SortAllAntalSolgtePrGruppeASC()
        {
            return bornegruppeService.SortAllAntalSolgtePrGruppeAscending();
        }

        public Bornegruppe TildelLodsedlerBornegruppe(Bornegruppe bornegruppe, int amount)
        {
            return bornegruppeService.TildelLodsedlerBornegruppe(bornegruppe, amount);
        }

        public IEnumerable<Leder> GetLederOptions()
        {
            return bornegruppeService.GetLederOptions();
        }

        
    }
}
