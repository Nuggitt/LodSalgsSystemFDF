using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBørnService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Børns
{
    public class GetBørnModel : PageModel
    {
        private IBørnService _børnService;
        public GetBørnModel(IBørnService børnService)
        {
            _børnService = børnService;
        }
        [BindProperty]
        public IEnumerable<Børn> Børns { get; set; } = new List<Børn>();
        public void OnGet()
        {
            Børns = _børnService.GetBørn();
        }

        public void OnGetNavnDescending()
        {
            Børns = _børnService.GetAllBørnNavnDescending();
        }

        public void OnGetIDDescending()
        {
            Børns = _børnService.GetAllBørnIDDescending();
        }

        public void OnGetAntalSolgteLodseddelerDescending()
        {
            Børns = _børnService.GetAllBørnAntalSolgteLodseddelerDescending();
        }

        public void OnGetGruppeIDDescending()
        {
            Børns = _børnService.GetAllBørnGruppeIDDescending();
        }

        public void OnGetNavnAscending()
        {
            Børns = _børnService.GetAllBørnNavnAscending();
        }

        public void OnGetIDAscending()
        {
            Børns = _børnService.GetAllBørnIDAscending();
        }

        public void OnGetAntalSolgteLodseddelerAscending()
        {
            Børns = _børnService.GetAllBørnAntalSolgteLodseddelerAscending();
        }

        public void OnGetGruppeIDAscending()
        {
            Børns = _børnService.GetAllBørnGruppeIDAscending();
        }


        //public void OnGetAllBørnItems(string Børn, string Navn)
        //{
        //    Børns = _børnService.GetAllBørnItems(Børn, Navn);
        //}

    }
}
