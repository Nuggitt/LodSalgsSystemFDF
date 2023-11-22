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

        public CreateModel(ISalgService salgService)
        {
            _salgService = salgService;
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
