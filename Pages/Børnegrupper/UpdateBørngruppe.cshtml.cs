using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Børnegrupper
{
    public class UpdateBørngruppeModel : PageModel
    {
        private IBørnegruppeService IBS;
        [BindProperty]
        public Børnegruppe Børnegruppe { get; set; }

        public UpdateBørngruppeModel(IBørnegruppeService børnegruppeService)
        {
            IBS = børnegruppeService;
        }  
        public async Task OnGetAsync(int id)
        {
            Børnegruppe = await IBS.GetBørnegruppeIdAsync(id);
        }

        public IActionResult Onpost()
        {
            Børnegruppe = IBS.UpdateBørnegruppe(Børnegruppe);
            return RedirectToPage("GetBørnegrupper");
        }
    }
}
