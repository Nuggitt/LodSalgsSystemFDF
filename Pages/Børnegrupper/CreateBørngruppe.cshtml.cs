using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOIndtægtService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Børnegrupper
{
    public class CreateBørngruppeModel : PageModel
    {
        private IBørnegruppeService _børnegruppeservice;

        [BindProperty]
        public Børnegruppe Børnegrupper { get; set; }
        public CreateBørngruppeModel(IBørnegruppeService børnegruppeService)
        {
            _børnegruppeservice = børnegruppeService;
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Børnegrupper = _børnegruppeservice.CreateBørnegruppe(Børnegrupper);
            return RedirectToPage("GetBørnegrupper");

        }
    }
}
