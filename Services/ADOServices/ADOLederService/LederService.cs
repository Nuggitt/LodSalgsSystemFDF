using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBørnegruppeService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOLederService
{
    public class LederService : ILederService
    {
        private AdonetLederService lederService;

        public LederService(AdonetLederService service)
        {
            lederService = service;
        }
        public IEnumerable<Leder> GetLeder()
        {
            return lederService.GetAllLeder();
        }
    }
}
