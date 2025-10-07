using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Ledere
{
    public class CreateLederModel : PageModel
    {
        private ILederService _lederService;

        [BindProperty]
        public Leder Leder { get; set; }
        public IEnumerable<Børnegruppe> BørneIDOptions { get; set; }
        public CreateLederModel(ILederService lederService)
        {
            _lederService = lederService;
        }
        public IActionResult OnGet()
        {
            BørneIDOptions = _lederService.GetBørneIDOptions();
            return Page();
        }

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                   BørneIDOptions =  _lederService.GetBørneIDOptions();
                    
                }
                Leder = _lederService.CreateLeder(Leder);
                return RedirectToPage("GetLedere");
            }
            catch (DuplicateKeyException ex)
            {
                ModelState.AddModelError("Leder.Leder_ID", ex.Message);
                return Page();
            }
            catch (NegativeAmountExceptioncs ex)
            {
                ModelState.AddModelError("Leder.Leder_ID", ex.Message);
                return Page();
            }
        }
    }
}
