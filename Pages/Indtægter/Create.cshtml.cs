using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Indtægter
{
    public class CreateModel : PageModel
    {
        private IIndtægtService _indtægtService;

        [BindProperty]
        public Indtægt Indtægt { get; set; }

        public CreateModel(IIndtægtService indtægtService)
        {
            _indtægtService = indtægtService;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Indtægt = _indtægtService.CreateIndtægt(Indtægt);
            return RedirectToPage("GetIndtægter");
        }
    }
}

