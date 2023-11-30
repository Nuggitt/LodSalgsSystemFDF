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

    }
}
