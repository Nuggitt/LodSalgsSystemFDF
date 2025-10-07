using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.BÃ¸rns
{
    [Authorize(Roles = "admin, lotteribestyrer, leder")]
    public class BÃ¸rnIBÃ¸rnegrupperModel : PageModel
    {
        
        private IBÃ¸rnService _bÃ¸rnService;

        [BindProperty]
        public string NameSearch { get; set; }
        public BÃ¸rnIBÃ¸rnegrupperModel(IBÃ¸rnService bÃ¸rnService)
        {
            _bÃ¸rnService = bÃ¸rnService;
        }
        [BindProperty]
        public IEnumerable<BÃ¸rn> BÃ¸rns { get; set; } = new List<BÃ¸rn>();
        public async Task OnGet(int bid)
        {

            BÃ¸rns = await _bÃ¸rnService.GetBÃ¸rnInBÃ¸rnegruppe(bid);


        }

        public async Task OnGetBÃ¸rnInBÃ¸rnegruppe(int id)
        {
            BÃ¸rns = await _bÃ¸rnService.GetBÃ¸rnInBÃ¸rnegruppe(id);

        }

        public void OnGetNavnDescending()
        {
            BÃ¸rns = _bÃ¸rnService.GetAllBÃ¸rnNavnDescending();
        }

        public void OnGetIDDescending()
        {
            BÃ¸rns = _bÃ¸rnService.GetAllBÃ¸rnIDDescending();
        }

        public void OnGetAntalSolgteLodseddelerDescending()
        {
            BÃ¸rns = _bÃ¸rnService.GetAllBÃ¸rnAntalSolgteLodseddelerDescending();
        }

        public void OnGetGruppeIDDescending()
        {
            BÃ¸rns = _bÃ¸rnService.GetAllBÃ¸rnGruppeIDDescending();
        }

        public void OnGetNavnAscending()
        {
            BÃ¸rns = _bÃ¸rnService.GetAllBÃ¸rnNavnAscending();
        }

        public void OnGetIDAscending()
        {
            BÃ¸rns = _bÃ¸rnService.GetAllBÃ¸rnIDAscending();
        }

        public void OnGetAntalSolgteLodseddelerAscending()
        {
            BÃ¸rns = _bÃ¸rnService.GetAllBÃ¸rnAntalSolgteLodseddelerAscending();
        }

        public void OnGetGruppeIDAscending()
        {
            BÃ¸rns = _bÃ¸rnService.GetAllBÃ¸rnGruppeIDAscending();
        }

        public IActionResult OnPostBÃ¸rnByName()
        {
            if (NameSearch != null)
            {
                BÃ¸rns = _bÃ¸rnService.GetBÃ¸rnByName(NameSearch);
                return Page();
            }

            return Page();
        }
        public void OnGetGivetLodsedlerDescending()
        {
            BÃ¸rns = _bÃ¸rnService.GetGivetLodsedlerDescending();
        }

        public void OnGetGivetLodsedlerAscending()
        {
            BÃ¸rns = _bÃ¸rnService.GetGivetLodsedlerAscending();
        }
    }
}

