using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Indtægter
{
    public class DeleteModel : PageModel
    {
        private IIndtægtService _indtægtService;

        [BindProperty]
        public Indtægt Indtægt { get; set; }


        public DeleteModel(IIndtægtService indtægtService)
        {
            _indtægtService = indtægtService;
        }

        public void OnGet(int id)
        {
            Indtægt = _indtægtService.GetIndtægtById(id);
        }

        public IActionResult OnPost()
        {
            Indtægt = _indtægtService.DeleteIndtægt(Indtægt);
            return RedirectToPage("GetIndtægter");
        }
    }
}
