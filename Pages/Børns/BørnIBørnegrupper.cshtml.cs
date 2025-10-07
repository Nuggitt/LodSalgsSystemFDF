using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Børns
{
    [Authorize(Roles = "admin, lotteribestyrer, leder")]
    public class BørnIBørnegrupperModel : PageModel
    {
        
        private IBørnService _børnService;

        [BindProperty]
        public string NameSearch { get; set; }
        public BørnIBørnegrupperModel(IBørnService børnService)
        {
            _børnService = børnService;
        }
        [BindProperty]
        public IEnumerable<Børn> Børns { get; set; } = new List<Børn>();
        public async Task OnGet(int bid)
        {

            Børns = await _børnService.GetBørnInBørnegruppe(bid);


        }

        public async Task OnGetBørnInBørnegruppe(int id)
        {
            Børns = await _børnService.GetBørnInBørnegruppe(id);

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
            if (NameSearch != null)
            {
                Børns = _børnService.GetBørnByName(NameSearch);
                return Page();
            }

            return Page();
        }
        public void OnGetGivetLodsedlerDescending()
        {
            Børns = _børnService.GetGivetLodsedlerDescending();
        }

        public void OnGetGivetLodsedlerAscending()
        {
            Børns = _børnService.GetGivetLodsedlerAscending();
        }
    }
}
