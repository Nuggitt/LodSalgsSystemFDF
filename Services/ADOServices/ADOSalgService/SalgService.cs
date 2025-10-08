using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBornegruppeService;
using LodSalgsSystemFDF.Services.ADOServices.ADOIndtaegtService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Server.IIS.Core;

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

        public Salg GetSalgById(int id)
        {
            return _salgService.GetSalgById(id);
        }

        public Salg CreateSalg(Salg salg)
        {
            return _salgService.CreateSalg(salg);
        }

        public Salg DeleteSalg(Salg salg)
        {
            return _salgService.DeleteSalg(salg);
        }

        public Salg UpdateSalg(Salg salg)
        {
            return _salgService.UpdateSalg(salg);
        }
        public IEnumerable<Salg> GetBornegruppeByID(int ID)
        {
            return _salgService.GetBornegruppeByID(ID);
        }

        public IEnumerable<Salg> PriceFilter(float maxPrice, float minPrice)
        {
            return _salgService.PriceFilters(maxPrice, minPrice);
        }

        public IEnumerable<Salg> GetBornById(int id, int bid)
        {
            return _salgService.GetBornById(id, bid);
        }

        public IEnumerable<Leder> GetLederOptions()
        {
            return _salgService.GetLederOptions();
        }

        public IEnumerable<Salg> GetAntalSolgteLodseddelerDESC()
        {
            return _salgService.GetAntalSolgteLodseddelerDESC();
        }

        public IEnumerable<Salg> GetAntalSolgteLodseddelerASC()
        {
            return _salgService.GetAntalSolgteLodseddelerASC();
        }
    }
}
