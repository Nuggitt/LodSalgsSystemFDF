using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.IndtÃ¦gter
{
    public class DeleteModel : PageModel
    {
        private IIndtÃ¦gtService _indtÃ¦gtService;

        [BindProperty]
        public IndtÃ¦gt IndtÃ¦gt { get; set; }


        public DeleteModel(IIndtÃ¦gtService indtÃ¦gtService)
        {
            _indtÃ¦gtService = indtÃ¦gtService;
        }

        public void OnGet(int id)
        {
            IndtÃ¦gt = _indtÃ¦gtService.GetIndtÃ¦gtById(id);
        }

        public IActionResult OnPost()
        {
            IndtÃ¦gt = _indtÃ¦gtService.DeleteIndtÃ¦gt(IndtÃ¦gt);
            return RedirectToPage("GetIndtÃ¦gter");
        }
    }
}

