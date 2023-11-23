using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOBørnegruppeService
{
    public class BørnegruppeService : IBørnegruppeService
    {
        private AdonetBørnegruppeService børnegruppeService;

        public BørnegruppeService(AdonetBørnegruppeService service)
        {
            børnegruppeService = service;
        }
        public IEnumerable<Børnegruppe> GetBørnegruppe()
        {
            return børnegruppeService.GetAllBørnegruppe();
        }
    }
}
