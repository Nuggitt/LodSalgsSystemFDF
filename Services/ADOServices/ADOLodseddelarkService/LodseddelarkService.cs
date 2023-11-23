using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOSalgService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOLodseddelarkService
{
    public class LodseddelarkService : ILodseddelarkService

    {
        private AdonetLodseddelarkService _lodseddelarkservice;

        public LodseddelarkService(AdonetLodseddelarkService LodseddelSerive)
        {
            _lodseddelarkservice = LodseddelSerive;
        }
        public IEnumerable<Lodseddelark> GetLodseddelark()
        {
            return _lodseddelarkservice.GetAllLodseddelark();
        }
    }
}
