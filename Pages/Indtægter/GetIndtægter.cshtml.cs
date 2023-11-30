using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Indtægter
{
    [Authorize]
    public class GetIndtægterModel : PageModel
    {
        private IIndtægtService _indtægtService;
        public GetIndtægterModel(IIndtægtService indtægtService)
        {
            _indtægtService = indtægtService;
        }
        [BindProperty]
        public IEnumerable<Indtægt> Indtægter { get; set; } = new List<Indtægt>();

        public void OnGet()
        {
            Indtægter = _indtægtService.GetIndtægter();
        }
    }
}
