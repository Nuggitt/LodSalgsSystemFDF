using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Børns
{
    public class UpdateModel : PageModel
    {
        private IBørnService _børnService;

        [BindProperty]
        public Børn Børn { get; set; }

        public UpdateModel(IBørnService børnService)
        {
            _børnService=børnService;
        }
        public void OnGet(int id)
        {
            Børn = _børnService.GetBørn(id);
        }

        public IActionResult OnPost()
        {
            Børn = _børnService.UpdateBørn(Børn);
            return RedirectToPage("GetBørn");
        }
    }
}
