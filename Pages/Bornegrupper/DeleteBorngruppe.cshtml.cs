using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Bornegrupper
{
    public class DeleteBorngruppeModel : PageModel
    {
        private IBornegruppeService IBS;
        [BindProperty]
        public Bornegruppe Bornegrupper { get; set; }

        public DeleteBorngruppeModel(IBornegruppeService bornegruppeService)
        {
            IBS = bornegruppeService;
        }
        public async Task OnGetAsync(int id)
        {
                Bornegrupper = await IBS.GetBornegruppeIdAsync(id) /*?? new Børnegruppe()*/;
            
        }
        public IActionResult OnPost() 
        {
            Bornegrupper = IBS.DeleteBornegruppe(Bornegrupper);
            return RedirectToPage("GetBornegrupper");
        }
    }
}
