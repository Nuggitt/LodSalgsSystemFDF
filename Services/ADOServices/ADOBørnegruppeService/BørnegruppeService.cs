using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOBørnegruppeService
{
    public class BørnegruppeService : IBørnegruppeService
    {
        private AdonetBørnegruppeService børnegruppeService;
        public IEnumerable<Børnegruppe> GetBørnegruppe()
        {
            return børnegruppeService.GetAllBørnegruppe();
        }
        public BørnegruppeService(AdonetBørnegruppeService service)
        {
            børnegruppeService = service;
        }
        public Børnegruppe GetBørnegruppeId(int id)
        {
            return børnegruppeService.GetBørnegruppeById(id);
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
    }
}
