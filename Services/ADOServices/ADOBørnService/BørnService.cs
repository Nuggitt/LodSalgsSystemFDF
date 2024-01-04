using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBørnegruppeService;
using LodSalgsSystemFDF.Services.ADOServices.ADOSalgService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOBørnService
{
    public class BørnService : IBørnService
    {
        private AdonetBørnService _børnService;

        public BørnService(AdonetBørnService børnservice)
        {
            _børnService = børnservice;
        }

        public async Task<IEnumerable<Børn>> GetBørn()
        {
            return await _børnService.GetAllBørn();
        }

        public async Task<Børn> GetBørn(int id)
        {
            return await _børnService.GetBørn(id);
        }

        public Børn CreateBørn(Børn børn)
        {
            return _børnService.CreateBørn(børn);
        }

        public Børn DeleteBørn(Børn børn)
        {
            return _børnService.DeleteBørn(børn);
        }

        public Børn UpdateBørn(Børn børn)
        {
            return _børnService.UpdateBørn(børn);
        }

        public IEnumerable<Børn> GetAllBørnNavnDescending()
        {
            return _børnService.GetAllBørnNavnDescending();
        }

        public IEnumerable<Børn> GetAllBørnIDDescending()
        {
            return _børnService.GetAllBørnIDDescending();
        }

        public IEnumerable<Børn> GetAllBørnAntalSolgteLodseddelerDescending()
        {
            return _børnService.GetAllBørnAntalSolgteLodseddelerDescending();
        }

        public IEnumerable<Børn> GetAllBørnGruppeIDDescending()
        {
            return _børnService.GetAllBørnGruppeIDDescending();
        }

        public IEnumerable<Børn> GetAllBørnNavnAscending()
        {
            return _børnService.GetAllBørnNavnAscending();
        }

        public IEnumerable<Børn> GetAllBørnIDAscending()
        {
            return _børnService.GetAllBørnIDAscending();
        }

        public IEnumerable<Børn> GetAllBørnAntalSolgteLodseddelerAscending()
        {
            return _børnService.GetAllBørnAntalSolgteLodseddelerAscending();
        }

        public IEnumerable<Børn> GetAllBørnGruppeIDAscending()
        {
            return _børnService.GetAllBørnGruppeIDAscending();
        }

        public IEnumerable<Børn> GetGivetLodsedlerDescending()
        {
            return _børnService.GetGivetLodsedlerDescending();
        }

        public IEnumerable<Børn> GetGivetLodsedlerAscending()
        {
            return _børnService.GetGivetLodsedlerAscending();
        }

        //public IEnumerable<Børn> GetAllBørnItems(string Børn, string Navn)
        //{
        //    return _børnService.GetAllBørnItems(Børn, Navn);
        //}
        public IEnumerable<Børn> GetBørnByName(string Name)
        {
            return _børnService.GetBørnByName(Name);
        }

        public Børn TildelLodsedler(Børn børn, int amount)
        {

            return _børnService.TildelLodsedler(børn, amount);
        }

        public Task<IEnumerable<Børn>> GetBørnInBørnegruppe(int id)
        {
            return _børnService.GetBørnInBørnegruppe(id);
        }

        public IEnumerable<Børnegruppe> GetBørnegruppeOptions()
        {
            return _børnService.GetBørnegruppeOptions();
        }

    }
}
