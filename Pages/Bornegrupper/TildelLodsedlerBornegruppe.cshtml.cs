using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Bornegrupper
{
    public class TildelLodsedlerBornegruppeModel : PageModel
    {
        [BindProperty]
        public Bornegruppe Bornegruppe { get; set; }
        [BindProperty]
        public int Amount { get; set; }
        public IEnumerable<Bornegruppe> Bornegrupper { get; set; } = new List<Bornegruppe>();
        private IBornegruppeService _bornegruppeService { get; set; }

        [BindProperty]
        public string NameSearch { get; set; }

        public TildelLodsedlerBornegruppeModel(IBornegruppeService bornegruppeService)
        {
            _bornegruppeService = bornegruppeService;
        }
        public async Task OnGet(int id)
        {
            Bornegruppe = await _bornegruppeService.GetBornegruppeIdAsync(id);
            Bornegrupper = await _bornegruppeService.GetBornegruppeAsync();

        }

        public IActionResult OnPost()
        {

            Bornegruppe = _bornegruppeService.TildelLodsedlerBornegruppe(Bornegruppe, Amount);
            return RedirectToPage("GetBornegrupper");
        }
    }
}
