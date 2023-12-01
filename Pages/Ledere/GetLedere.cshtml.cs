using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Ledere
{
    public class GetLedereModel : PageModel
    {
        private ILederService _lederService;
        [BindProperty]
        public string LederSearch { get; set; }


        public GetLedereModel(ILederService lederService)
        {
            _lederService = lederService;
        }
        [BindProperty]
        public IEnumerable<Leder> Ledere { get; set; } = new List<Leder>();
        public async Task OnGetAsync()
        {
            Ledere = await _lederService.GetLederAsync();
        }
        public IActionResult OnPostLederByName()
        {
            Ledere = _lederService.GetLederByName(LederSearch);
            return Page();
        }
    }
}
