using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

namespace LodSalgsSystemFDF.Services.ADOServices.BrugerService
{
    public class BrugerService
    {
        public List<Bruger> Bruger { get; set; }
        private AdonetBrugerService _adonetBrugerService;

        public BrugerService(AdonetBrugerService adonetBrugerService)
        {

            _adonetBrugerService = adonetBrugerService;
            Bruger = _adonetBrugerService.GetAllBrugere();

        }

        public Bruger AddBruger(Bruger bruger)
        {
            return _adonetBrugerService.AddBruger(bruger);
        }
    }
}

