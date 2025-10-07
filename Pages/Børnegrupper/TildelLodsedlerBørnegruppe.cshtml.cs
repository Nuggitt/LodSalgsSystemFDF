using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.BÃ¸rnegrupper
{
    public class TildelLodsedlerBÃ¸rnegruppeModel : PageModel
    {
        [BindProperty]
        public BÃ¸rnegruppe BÃ¸rnegruppe { get; set; }
        [BindProperty]
        public int Amount { get; set; }
        public IEnumerable<BÃ¸rnegruppe> BÃ¸rnegrupper { get; set; } = new List<BÃ¸rnegruppe>();
        private IBÃ¸rnegruppeService _bÃ¸rnegruppeService { get; set; }

        [BindProperty]
        public string NameSearch { get; set; }

        public TildelLodsedlerBÃ¸rnegruppeModel(IBÃ¸rnegruppeService bÃ¸rnegruppeService)
        {
            _bÃ¸rnegruppeService = bÃ¸rnegruppeService;
        }
        public async Task OnGet(int id)
        {
            BÃ¸rnegruppe = await _bÃ¸rnegruppeService.GetBÃ¸rnegruppeIdAsync(id);
            BÃ¸rnegrupper = await _bÃ¸rnegruppeService.GetBÃ¸rnegruppeAsync();

        }

        public IActionResult OnPost()
        {

            BÃ¸rnegruppe = _bÃ¸rnegruppeService.TildelLodsedlerBÃ¸rnegruppe(BÃ¸rnegruppe, Amount);
            return RedirectToPage("GetBÃ¸rnegrupper");
        }
    }
}

