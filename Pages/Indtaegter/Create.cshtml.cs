using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Indtaegter
{
    public class CreateModel : PageModel
    {
        private IIndtaegtService _indtaegtService;

        [BindProperty]
        public Indtaegt Indtaegt { get; set; }

        public CreateModel(IIndtaegtService indtaegtService)
        {
            _indtaegtService = indtaegtService;
        }

        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            Indtaegt = _indtaegtService.CreateIndtaegt(Indtaegt);
            return RedirectToPage("GetIndtaegter");
        }
    }
}

