using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Ledere
{
    public class DeleteLederModel : PageModel
    {
        private ILederService _lederService;

        [BindProperty]
        public Leder Leder { get; set; }

        public IEnumerable<Leder> Ledere { get; set; } = new List<Leder>();

        public DeleteLederModel(ILederService lederService)
        {
            _lederService = lederService;
        }

        public void OnGet(int Leder_ID)
        {
            Leder = _lederService.GetLederByID(Leder_ID);
        }

        public IActionResult OnPost()
        {
            Leder = _lederService.DeleteLeder(Leder);
            return RedirectToPage("GetLedere");
        }
    }
}
