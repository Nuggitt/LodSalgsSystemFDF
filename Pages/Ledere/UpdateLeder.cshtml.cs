using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Ledere
{
    public class UpdateLederModel : PageModel
    {
            private ILederService _lederService;

            [BindProperty]
            public Leder Leder { get; set; }

            public UpdateLederModel(ILederService lederService)
            {
                _lederService = lederService;
            }
            public void OnGet(int Leder_ID)
            {
                Leder = _lederService.GetLederByID(Leder_ID);
            }

            public IActionResult OnPost()
            {
                Leder = _lederService.UpdateLeder(Leder);
                return RedirectToPage("GetLedere");
            }
        }
}

