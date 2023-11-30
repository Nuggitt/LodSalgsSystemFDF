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
        public async Task OnGetAsync(int id)
        {
                Børnegrupper = await IBS.GetBørnegruppeIdAsync(id) /*?? new Børnegruppe()*/;
            
        }
        public IActionResult OnPost() 
        {
            Børnegrupper = IBS.DeleteBørnegruppe(Børnegrupper);
            return RedirectToPage("GetBørnegrupper");
        }
    }
}
