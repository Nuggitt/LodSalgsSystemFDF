using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBornService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Borns
{
    public class TildelLodsedlerBorneIBornegruppeModel : PageModel
    {
        [BindProperty]
        public Born Born { get; set; }
        [BindProperty]
        public int Amount { get; set; }
        public IEnumerable<Born> Borns { get; set; } = new List<Born>();
        private IBornService _borneService { get; set; }

        [BindProperty]
        public string NameSearch { get; set; }

        public TildelLodsedlerBorneIBornegruppeModel(IBornService borneService)
        {
            _borneService = borneService;
        }
        public async Task OnGet(int id, int bid)
        {

            Born = await _borneService.GetBorn(id);
            Borns = await _borneService.GetBornInBornegruppe(bid);

        }

        public IActionResult OnPost()
        {

            Born = _borneService.TildelLodsedler(Born, Amount);
            return RedirectToPage("BornIBornegrupper", new { id = Born.Born_ID, bid = Born.Bornegruppe_ID }); //Bliver på siden i de forskellige Lister af børn i grupper.
        }
    }
}
