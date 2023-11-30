using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Ledere
{
    [Authorize]
    public class GetLedereModel : PageModel
    {
        private ILederService _lederService;

        public GetLedereModel(ILederService lederService)
        {
            _lederService = lederService;
        }
        [BindProperty]
        public IEnumerable<Leder> Ledere { get; set; } = new List<Leder>();
        public void OnGet()
        {
            Ledere = _lederService.GetLeder();
        }
    }
}
