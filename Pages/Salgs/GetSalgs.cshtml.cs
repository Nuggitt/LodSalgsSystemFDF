using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOLederService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Salgs
{
    public class GetSalgsModel : PageModel
    {
        private ISalgService _salgService;
        private IB�rnegruppeService _IB;
        public string IDSearch { get; set; }
        public GetSalgsModel(ISalgService salgService)
        {
            _salgService = salgService;
        }
        [BindProperty]
        public IEnumerable<Salg> Salgs { get; set; } = new List<Salg>();

        public void OnGet()
        {
            Salgs = _salgService.GetSalgs();
        }
        public IActionResult OnPostB�rnegruppeByID()
        {
            {
                Salgs = _salgService.GetB�rnegruppeByID(IDSearch);
                return Page();
            }

        }
    }
}