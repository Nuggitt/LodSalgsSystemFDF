using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBørnegruppeService;
using LodSalgsSystemFDF.Services.ADOServices.ADOSalgService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOLederService
{
    public class LederService : ILederService
    {
        private AdonetLederService _lederService;

        public LederService(AdonetLederService lederService)
        {
            _lederService = lederService;
        }
        public async Task<IEnumerable<Leder>> GetLederAsync()
        {
            return await _lederService.GetAllLederAsync();
        }
        public Leder CreateLeder(Leder leder)
        {
            return _lederService.CreateLeder(leder);
        }
        public Leder DeleteLeder(Leder leder) 
        {
            return _lederService.DeleteLeder(leder);
        }
        public Leder GetLederByID(int Leder_ID)
        {
            return _lederService.GetLederByID(Leder_ID);
        }
        public Leder UpdateLeder(Leder leder)
        {
            return _lederService.UpdateLeder(leder);
        }
        public IEnumerable<Leder> GetLederByName(string Navn)
        {
            return _lederService.GetLederByName(Navn);
        }
        public IEnumerable<Leder> GetAllLederNavnDescending()
        {
            return _lederService.GelAllLederNavnDescending();
        }
        public IEnumerable<Leder> GetAllLederNavnAscending()
        {
            return _lederService.GetAllLederNavnAscending();
        }
        public IEnumerable<Børnegruppe> GetBørneIDOptions()
        {
            return _lederService.GetBørneIDOptions();
        }
    }
}

