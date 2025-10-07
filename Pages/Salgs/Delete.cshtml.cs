using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Repository;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Salgs
{
    public class DeleteModel : PageModel
    {
        private ISalgService _salgService;

        [BindProperty]
        public Salg Salg { get; set; }

        public IEnumerable<Salg> Salgs { get; set; } = new List<Salg>();

        public DeleteModel(ISalgService salgService)
        {
            _salgService = salgService;
        }

        public void OnGet(int id)
        {
            Salg = _salgService.GetSalgById(id);
        }

        public IActionResult OnPost()
        {
            Salg = _salgService.DeleteSalg(Salg);
            return RedirectToPage("GetSalgs");
        }
    }
}

