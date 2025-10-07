using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.BÃ¸rnegrupper
{
    public class UpdateBÃ¸rngruppeModel : PageModel
    {
        private IBÃ¸rnegruppeService IBS;
        [BindProperty]
        public BÃ¸rnegruppe BÃ¸rnegruppe { get; set; }

        public UpdateBÃ¸rngruppeModel(IBÃ¸rnegruppeService bÃ¸rnegruppeService)
        {
            IBS = bÃ¸rnegruppeService;
        }  
        public async Task OnGetAsync(int id)
        {
            BÃ¸rnegruppe = await IBS.GetBÃ¸rnegruppeIdAsync(id);
        }

        public IActionResult Onpost()
        {
            BÃ¸rnegruppe = IBS.UpdateBÃ¸rnegruppe(BÃ¸rnegruppe);
            return RedirectToPage("GetBÃ¸rnegrupper");
        }
    }
}

