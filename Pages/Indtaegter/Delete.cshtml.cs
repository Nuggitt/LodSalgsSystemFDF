using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Indtaegter
{
    public class DeleteModel : PageModel
    {
        private IIndtaegtService _indtaegtService;

        [BindProperty]
        public Indtaegt Indtaegt { get; set; }


        public DeleteModel(IIndtaegtService indtaegtService)
        {
            _indtaegtService = indtaegtService;
        }

        public void OnGet(int id)
        {
            Indtaegt = _indtaegtService.GetIndtaegtById(id);
        }

        public IActionResult OnPost()
        {
            Indtaegt = _indtaegtService.DeleteIndtaegt(Indtaegt);
            return RedirectToPage("GetIndtaegter");
        }
    }
}
