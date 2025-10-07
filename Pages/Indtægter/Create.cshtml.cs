using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.IndtÃ¦gter
{
    public class CreateModel : PageModel
    {
        private IIndtÃ¦gtService _indtÃ¦gtService;

        [BindProperty]
        public IndtÃ¦gt IndtÃ¦gt { get; set; }

        public CreateModel(IIndtÃ¦gtService indtÃ¦gtService)
        {
            _indtÃ¦gtService = indtÃ¦gtService;
        }

        public IActionResult OnPost()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            IndtÃ¦gt = _indtÃ¦gtService.CreateIndtÃ¦gt(IndtÃ¦gt);
            return RedirectToPage("GetIndtÃ¦gter");
        }
    }
}


