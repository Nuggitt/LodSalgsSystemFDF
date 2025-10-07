using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.IndtÃ¦gter
{
    public class UpdateModel : PageModel
    {
        private IIndtÃ¦gtService _indtÃ¦gtService;
        [BindProperty]
        public IndtÃ¦gt IndtÃ¦gt { get; set; }

        public UpdateModel(IIndtÃ¦gtService Ã­ndtÃ¦gtService)
        {
            _indtÃ¦gtService = Ã­ndtÃ¦gtService;
        }

        public void OnGet(int id)
        {
            IndtÃ¦gt = _indtÃ¦gtService.GetIndtÃ¦gtById(id);
        }

        public IActionResult OnPost()
        {
            IndtÃ¦gt = _indtÃ¦gtService.UpdateIndtÃ¦gt(IndtÃ¦gt);
            return RedirectToPage("GetIndtÃ¦gter");
        }
    }
}

