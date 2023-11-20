using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOSalgService
{
    public class SalgService : ISalgService
    {
        private AdonetSalgService _salgService;

        public SalgService(AdonetSalgService salgservice)
        {
            _salgService = salgservice;
        }

        public IEnumerable<Salg> GetSalgs()
        {
            return _salgService.GetAllSalgs();
        }
    }
}
