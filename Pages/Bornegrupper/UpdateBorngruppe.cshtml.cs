using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Bornegrupper
{
    public class UpdateBorngruppeModel : PageModel
    {
        private IBornegruppeService IBS;
        [BindProperty]
        public Bornegruppe Bornegruppe { get; set; }

        public UpdateBorngruppeModel(IBornegruppeService bornegruppeService)
        {
            IBS = bornegruppeService;
        }  
        public async Task OnGetAsync(int id)
        {
            Bornegruppe = await IBS.GetBornegruppeIdAsync(id);
        }

        public IActionResult Onpost()
        {
            Bornegruppe = IBS.UpdateBornegruppe(Bornegruppe);
            return RedirectToPage("GetBornegrupper");
        }
    }
}
