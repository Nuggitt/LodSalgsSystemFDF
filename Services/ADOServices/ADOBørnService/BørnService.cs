using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBørnegruppeService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOBørnService
{
    public class BørnService 
    {
            private AdonetBørnService børnService;

            public BørnService(AdonetBørnService service)
            {
                børnService = service;
            }
            public IEnumerable<Børn> GetBørn()
            {
                return børnService.GetAllBørn();
            }
        
    }
}
