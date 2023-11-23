using LodSalgsSystemFDF.Models;

namespace LodSalgsSystemFDF.Services.ADOServices.Interfaces
{
    public interface ILodseddelarkService
    {
        IEnumerable<Lodseddelark> GetLodseddelark();
    }
}
