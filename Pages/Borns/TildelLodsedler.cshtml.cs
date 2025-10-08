using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBornegruppeService;
using LodSalgsSystemFDF.Services.ADOServices.ADOBornService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Borns
{
    public class TildelLodsedlerModel : PageModel
    {
        [BindProperty]
        public Born Born { get; set; }
        [BindProperty]
        public int Amount { get; set; }
        public IEnumerable<Born> Borns { get; set; } = new List<Born>();
        private IBornService _borneService { get; set; }

        [BindProperty]
        public string NameSearch { get; set; }

        public TildelLodsedlerModel(IBornService borneService)
        {
            _borneService = borneService;
        }
        public async Task OnGet(int id)
        {

            Born = await _borneService.GetBorn(id);
            Borns = await _borneService.GetBorn();

        }

        public async Task OnGetBornInBornegruppe(int id)
        {
            Born = await _borneService.GetBorn(id); //Henter modal
            Borns = await _borneService.GetBornInBornegruppe(id);
          
        }


        public IActionResult OnPost()
        {
            
            Born = _borneService.TildelLodsedler(Born, Amount);
            return RedirectToPage("GetBorn");
        }
    }
}
