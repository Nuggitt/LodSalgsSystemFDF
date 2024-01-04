using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Salgs
{
    public class CreateModel : PageModel
    {
        private ISalgService _salgService;

        [BindProperty]
        public Salg Salg { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Bid { get; set; }

        public CreateModel(ISalgService salgService)
        {
            _salgService = salgService;
        }

        public void OnGet(int id, int bid)
        {
            _salgService.GetBørnById(Id, Bid);
            
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Salg = _salgService.CreateSalg(Salg);
            return RedirectToPage("GetSalgs");
        }
    }
}
