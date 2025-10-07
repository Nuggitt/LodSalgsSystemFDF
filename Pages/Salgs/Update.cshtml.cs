using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Salgs
{
    public class UpdateModel : PageModel
    {
        private ISalgService _salgService;
        [BindProperty]
        public Salg Salg { get; set; }

        public UpdateModel(ISalgService salgService)
        {
            _salgService = salgService;
        }

        public void OnGet(int id)
        {
            Salg = _salgService.GetSalgById(id);
        }

        public IActionResult OnPost()
        {
            Salg = _salgService.UpdateSalg(Salg);
            return RedirectToPage("GetSalgs");
        }
    }
}
