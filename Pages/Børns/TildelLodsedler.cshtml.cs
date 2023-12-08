using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBørnegruppeService;
using LodSalgsSystemFDF.Services.ADOServices.ADOBørnService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Børns
{
    public class TildelLodsedlerModel : PageModel
    {
        [BindProperty]
        public Børn Børn { get; set; }
        [BindProperty]
        public int Amount { get; set; }
        public IEnumerable<Børn> Børns { get; set; } = new List<Børn>();
        private IBørnService _børneService { get; set; }

        [BindProperty]
        public string NameSearch { get; set; }

        public TildelLodsedlerModel(IBørnService børneService)
        {
            _børneService = børneService;
        }
        public async Task  OnGet(int id)
        {

            Børn = await _børneService.GetBørn(id);
            Børns = await _børneService.GetBørn();

        }


        public IActionResult OnPost()
        {
            
            Børn = _børneService.TildelLodsedler(Børn, Amount);
            return RedirectToPage();
        }
    }
}
