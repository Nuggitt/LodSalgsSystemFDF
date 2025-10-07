using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.BÃ¸rnegrupper
{
    public class DeleteBÃ¸rngruppeModel : PageModel
    {
        private IBÃ¸rnegruppeService IBS;
        [BindProperty]
        public BÃ¸rnegruppe BÃ¸rnegrupper { get; set; }

        public DeleteBÃ¸rngruppeModel(IBÃ¸rnegruppeService bÃ¸rnegruppeService)
        {
            IBS = bÃ¸rnegruppeService;
        }
        public async Task OnGetAsync(int id)
        {
                BÃ¸rnegrupper = await IBS.GetBÃ¸rnegruppeIdAsync(id) /*?? new BÃ¸rnegruppe()*/;
            
        }
        public IActionResult OnPost() 
        {
            BÃ¸rnegrupper = IBS.DeleteBÃ¸rnegruppe(BÃ¸rnegrupper);
            return RedirectToPage("GetBÃ¸rnegrupper");
        }
    }
}

