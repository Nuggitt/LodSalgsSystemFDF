using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Indtægter
{
    public class UpdateModel : PageModel
    {
        private IIndtægtService _indtægtService;
        [BindProperty]
        public Indtægt Indtægt { get; set; }

        public UpdateModel(IIndtægtService índtægtService)
        {
            _indtægtService = índtægtService;
        }

        public void OnGet(int id)
        {
            Indtægt = _indtægtService.GetIndtægtById(id);
        }

        public IActionResult OnPost()
        {
            Indtægt = _indtægtService.UpdateIndtægt(Indtægt);
            return RedirectToPage("GetIndtægter");
        }
    }
}
