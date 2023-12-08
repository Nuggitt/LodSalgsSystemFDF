using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBørnService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Børns
{
    [Authorize]
    public class GetBørnModel : PageModel
    {
        private IBørnService _børnService;
        
        [BindProperty]
        public string NameSearch { get; set; }
        public GetBørnModel(IBørnService børnService)
        {
            _børnService = børnService;
        }
        [BindProperty]
        public IEnumerable<Børn> Børns { get; set; } = new List<Børn>();
        public async Task OnGet()
        {
            Børns = await _børnService.GetBørn();
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

        public IActionResult OnPostBørnByName()
        {
            Børns = _børnService.GetBørnByName(NameSearch);
            return Page();
        }

        //public void OnGetAllBørnItems(string Børn, string Navn)
        //{
        //    Børns = _børnService.GetAllBørnItems(Børn, Navn);
        //}

    }
}
