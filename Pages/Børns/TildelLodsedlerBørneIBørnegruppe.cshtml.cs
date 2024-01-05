using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBørnService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Børns
{
    public class TildelLodsedlerBørneIBørnegruppeModel : PageModel
    {
        [BindProperty]
        public Børn Børn { get; set; }
        [BindProperty]
        public int Amount { get; set; }
        public IEnumerable<Børn> Børns { get; set; } = new List<Børn>();
        private IBørnService _børneService { get; set; }

        [BindProperty]
        public string NameSearch { get; set; }

        public TildelLodsedlerBørneIBørnegruppeModel(IBørnService børneService)
        {
            _børneService = børneService;
        }
        public async Task OnGet(int id, int bid)
        {

            Børn = await _børneService.GetBørn(id);
            Børns = await _børneService.GetBørnInBørnegruppe(bid);

        }

        public IActionResult OnPost()
        {

            Børn = _børneService.TildelLodsedler(Børn, Amount);
            return RedirectToPage("BørnIBørnegrupper");
        }
    }
}
