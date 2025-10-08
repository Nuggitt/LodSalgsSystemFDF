using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBornegruppeService;
using LodSalgsSystemFDF.Services.ADOServices.ADOSalgService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOBornService
{
    public class BornService : IBornService
    {
        private AdonetBornService _bornService; //Instans felt


        public BornService(AdonetBornService bornservice) //Konstruktør
        {
            _bornService = bornservice;
        }

        public async Task<IEnumerable<Born>> GetBorn()
        {
            return await _bornService.GetAllBorn();
        }

        public async Task<Born> GetBorn(int id)
        {
            return await _bornService.GetBorn(id);
        }

        public Born CreateBorn(Born born)
        {
            return _bornService.CreateBorn(born);
        }

        public Born DeleteBorn(Born born)
        {
            return _bornService.DeleteBorn(born);
        }

        public Born UpdateBorn(Born born)
        {
            return _bornService.UpdateBorn(born);
        }

        public IEnumerable<Born> GetAllBornNavnDescending()
        {
            return _bornService.GetAllBornNavnDescending();
        }

        public IEnumerable<Born> GetAllBornIDDescending()
        {
            return _bornService.GetAllBornIDDescending();
        }

        public IEnumerable<Born> GetAllBornAntalSolgteLodseddelerDescending()
        {
            return _bornService.GetAllBornAntalSolgteLodseddelerDescending();
        }

        public IEnumerable<Born> GetAllBornGruppeIDDescending()
        {
            return _bornService.GetAllBornGruppeIDDescending();
        }

        public IEnumerable<Born> GetAllBornNavnAscending()
        {
            return _bornService.GetAllBornNavnAscending();
        }

        public IEnumerable<Born> GetAllBornIDAscending()
        {
            return _bornService.GetAllBornIDAscending();
        }

        public IEnumerable<Born> GetAllBornAntalSolgteLodseddelerAscending()
        {
            return _bornService.GetAllBornAntalSolgteLodseddelerAscending();
        }

        public IEnumerable<Born> GetAllBornGruppeIDAscending()
        {
            return _bornService.GetAllBornGruppeIDAscending();
        }

        public IEnumerable<Born> GetGivetLodsedlerDescending()
        {
            return _bornService.GetGivetLodsedlerDescending();
        }

        public IEnumerable<Born> GetGivetLodsedlerAscending()
        {
            return _bornService.GetGivetLodsedlerAscending();
        }

        //public IEnumerable<Born> GetAllBornItems(string Born, string Navn)
        //{
        //    return _bornService.GetAllBornItems(Born, Navn);
        //}
        public IEnumerable<Born> GetBornByName(string Name)
        {
            return _bornService.GetBornByName(Name);
        }

        public Born TildelLodsedler(Born born, int amount)
        {

            return _bornService.TildelLodsedler(born, amount);
        }

        public Task<IEnumerable<Born>> GetBornInBornegruppe(int id)
        {
            return _bornService.GetBornInBornegruppe(id);
        }

        public IEnumerable<Bornegruppe> GetBornegruppeOptions()
        {
            return _bornService.GetBornegruppeOptions();
        }

        public Task<IEnumerable<Born>> GetBornInBornegruppeByID(int id)
        {
            return _bornService.GetBornInBornegruppeByID(id);
        }

    }
}
