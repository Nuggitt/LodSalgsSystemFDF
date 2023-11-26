using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBørnService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Børns
{
    public class CreateModel : PageModel
    {
        private IBørnService _børnService;

        [BindProperty]
        public Børn Børn { get; set; }

        public CreateModel(IBørnService børnService)
        {
            _børnService=børnService;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Børn = _børnService.CreateBørn(Børn);
            return RedirectToPage("GetBørn");
        }
    }
}
