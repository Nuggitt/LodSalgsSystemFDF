using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Salgs
{
    [Authorize]
    public class GetSalgsModel : PageModel
    {
        private ISalgService _salgService;
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
    }
}
