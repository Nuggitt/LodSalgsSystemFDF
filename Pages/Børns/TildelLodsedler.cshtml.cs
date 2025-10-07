using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBÃ¸rnegruppeService;
using LodSalgsSystemFDF.Services.ADOServices.ADOBÃ¸rnService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.BÃ¸rns
{
    public class TildelLodsedlerModel : PageModel
    {
        [BindProperty]
        public BÃ¸rn BÃ¸rn { get; set; }
        [BindProperty]
        public int Amount { get; set; }
        public IEnumerable<BÃ¸rn> BÃ¸rns { get; set; } = new List<BÃ¸rn>();
        private IBÃ¸rnService _bÃ¸rneService { get; set; }

        [BindProperty]
        public string NameSearch { get; set; }

        public TildelLodsedlerModel(IBÃ¸rnService bÃ¸rneService)
        {
            _bÃ¸rneService = bÃ¸rneService;
        }
        public async Task OnGet(int id)
        {

            BÃ¸rn = await _bÃ¸rneService.GetBÃ¸rn(id);
            BÃ¸rns = await _bÃ¸rneService.GetBÃ¸rn();

        }

        public async Task OnGetBÃ¸rnInBÃ¸rnegruppe(int id)
        {
            BÃ¸rn = await _bÃ¸rneService.GetBÃ¸rn(id); //Henter modal
            BÃ¸rns = await _bÃ¸rneService.GetBÃ¸rnInBÃ¸rnegruppe(id);
          
        }


        public IActionResult OnPost()
        {
            
            BÃ¸rn = _bÃ¸rneService.TildelLodsedler(BÃ¸rn, Amount);
            return RedirectToPage("GetBÃ¸rn");
        }
    }
}

