using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBÃ¸rnService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.BÃ¸rns
{
    public class TildelLodsedlerBÃ¸rneIBÃ¸rnegruppeModel : PageModel
    {
        [BindProperty]
        public BÃ¸rn BÃ¸rn { get; set; }
        [BindProperty]
        public int Amount { get; set; }
        public IEnumerable<BÃ¸rn> BÃ¸rns { get; set; } = new List<BÃ¸rn>();
        private IBÃ¸rnService _bÃ¸rneService { get; set; }

        [BindProperty]
        public string NameSearch { get; set; }

        public TildelLodsedlerBÃ¸rneIBÃ¸rnegruppeModel(IBÃ¸rnService bÃ¸rneService)
        {
            _bÃ¸rneService = bÃ¸rneService;
        }
        public async Task OnGet(int id, int bid)
        {

            BÃ¸rn = await _bÃ¸rneService.GetBÃ¸rn(id);
            BÃ¸rns = await _bÃ¸rneService.GetBÃ¸rnInBÃ¸rnegruppe(bid);

        }

        public IActionResult OnPost()
        {

            BÃ¸rn = _bÃ¸rneService.TildelLodsedler(BÃ¸rn, Amount);
            return RedirectToPage("BÃ¸rnIBÃ¸rnegrupper", new { id = BÃ¸rn.BÃ¸rn_ID, bid = BÃ¸rn.BÃ¸rnegruppe_ID }); //Bliver pÃ¥ siden i de forskellige Lister af bÃ¸rn i grupper.
        }
    }
}

