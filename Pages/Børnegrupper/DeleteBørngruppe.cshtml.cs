using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Børnegrupper
{
    public class DeleteBørngruppeModel : PageModel
    {
        private IBørnegruppeService IBS;
        [BindProperty]
        public Børnegruppe Børnegrupper { get; set; }

        public DeleteBørngruppeModel(IBørnegruppeService børnegruppeService)
        {
            IBS = børnegruppeService;
        }
        public void OnGet(int id)
        {
            Børnegrupper = IBS.GetBørnegruppeId(id);
        }
        public IActionResult OnPost() 
        {
            Børnegrupper = IBS.DeleteBørnegruppe(Børnegrupper);
            return RedirectToPage("GetBørnegrupper");
        }
    }
}
