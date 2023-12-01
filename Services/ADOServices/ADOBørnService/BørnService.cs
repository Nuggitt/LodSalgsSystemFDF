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
            public IEnumerable<Børn> GetBørn()
            {
                return _børnService.GetAllBørn();
        }

        public Børn GetBørn(int id)
        {
            return _børnService.GetBørn(id);
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



    }
}
