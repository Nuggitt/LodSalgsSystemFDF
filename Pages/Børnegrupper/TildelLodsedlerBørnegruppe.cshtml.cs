using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Børnegrupper
{
    public class TildelLodsedlerBørnegruppeModel : PageModel
    {
        [BindProperty]
        public Børnegruppe Børnegruppe { get; set; }
        [BindProperty]
        public int Amount { get; set; }
        public IEnumerable<Børnegruppe> Børnegrupper { get; set; } = new List<Børnegruppe>();
        private IBørnegruppeService _børnegruppeService { get; set; }

        [BindProperty]
        public string NameSearch { get; set; }

        public TildelLodsedlerBørnegruppeModel(IBørnegruppeService børnegruppeService)
        {
            _børnegruppeService = børnegruppeService;
        }
        public async Task OnGet(int id)
        {
            Børnegruppe = await _børnegruppeService.GetBørnegruppeIdAsync(id);
            Børnegrupper = await _børnegruppeService.GetBørnegruppeAsync();

        }

        public IActionResult OnPost()
        {

            Børnegruppe = _børnegruppeService.TildelLodsedlerBørnegruppe(Børnegruppe, Amount);
            return Page();
        }
    }
}
