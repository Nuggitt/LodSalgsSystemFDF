using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOBørnegruppeService
{
    public class BørnegruppeService : IBørnegruppeService
    {
        private AdonetBørnegruppeService børnegruppeService;
        public async Task<IEnumerable<Børnegruppe>> GetBørnegruppeAsync()
        {
            return await børnegruppeService.GetAllBørnegruppeAsync();
        }
        public BørnegruppeService(AdonetBørnegruppeService service)
        {
            børnegruppeService = service;
        }
        public async Task<Børnegruppe> GetBørnegruppeIdAsync(int id)
        {
            return await børnegruppeService.GetBørnegruppeById(id);
        }
        public Børnegruppe CreateBørnegruppe(Børnegruppe børnegruppe)
        {
            return børnegruppeService.CreateBørnegruppe(børnegruppe);
        }

        public Børnegruppe DeleteBørnegruppe(Børnegruppe børnegruppe)
        {
            return børnegruppeService.DeleteBørnegruppe(børnegruppe);
        }

         public Børnegruppe UpdateBørnegruppe(Børnegruppe børnegruppe)
        {
            return børnegruppeService.UpdateBørnegruppe(børnegruppe);
        }
         public IEnumerable<Børnegruppe> GetBørnegruppeByName(string Name)
        {
            return børnegruppeService.GetBørnegruppeByName(Name);
        }

        public IEnumerable<Børnegruppe> GetAllBørnegruppeIDDESC()
        {
            return børnegruppeService.GetAllBørnGruppeIDDescending();
        }

        public IEnumerable<Børnegruppe> GetAllBørnegruppeIDASC()
        {
            return børnegruppeService.GetAllBørnGruppeIDAscending();
        }

        public IEnumerable<Børnegruppe> SortAllGruppeNavnDESC()
        {
            return børnegruppeService.SortAllGruppeNavnDescending();
        }

        public IEnumerable<Børnegruppe> SortAllGruppeNavnASC()
        {
            return børnegruppeService.SortAllGruppeNavnAscending();
        }

        public IEnumerable<Børnegruppe> SortAllAntalBørnDESC()
        {
            return børnegruppeService.SortAllAntalBørnDescending();
        }

        public IEnumerable<Børnegruppe> SortAllAntalBørnASC()
        {
            return børnegruppeService.SortAllAntalBørnAscending();
        }

        public IEnumerable<Børnegruppe> SortAllLederIDDESC()
        {
            return børnegruppeService.SortAllLederIDDescending();
        }

        public IEnumerable<Børnegruppe> SortAllLederIDASC()
        {
            return børnegruppeService.SortAllLederIDAscending();
        }

        public IEnumerable<Børnegruppe> SortAllAntalLodSeddelerPrGruppeDESC()
        {
            return børnegruppeService.SortAllAntalLodSeddelerPrGruppeDescending();
        }

        public IEnumerable<Børnegruppe> SortAllAntalLodSeddelerPrGruppeASC()
        {
            return børnegruppeService.SortAllAntalLodSeddelerPrGruppeAscending();
        }

        public IEnumerable<Børnegruppe> SortAllAntalSolgtePrGruppeDESC()
        {
            return børnegruppeService.SortAllAntalSolgtePrGruppeDescending();
        }

        public IEnumerable<Børnegruppe> SortAllAntalSolgtePrGruppeASC()
        {
            return børnegruppeService.SortAllAntalSolgtePrGruppeAscending();
        }

        public Børnegruppe TildelLodsedlerBørnegruppe(Børnegruppe børnegruppe, int amount)
        {
            return børnegruppeService.TildelLodsedlerBørnegruppe(børnegruppe,amount);
        }
    }
}
