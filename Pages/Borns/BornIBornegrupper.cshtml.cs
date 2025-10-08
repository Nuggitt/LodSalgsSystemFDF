using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Borns
{
    [Authorize(Roles = "admin, lotteribestyrer, leder")]
    public class BornIBornegrupperModel : PageModel
    {
        
        private IBornService _bornService;

        [BindProperty]
        public string NameSearch { get; set; }
        public BornIBornegrupperModel(IBornService bornService)
        {
            _bornService = bornService;
        }
        [BindProperty]
        public IEnumerable<Born> Borns { get; set; } = new List<Born>();
        public async Task OnGet(int bid)
        {

            Borns = await _bornService.GetBornInBornegruppe(bid);


        }

        public async Task OnGetBornInBornegruppe(int id)
        {
            Borns = await _bornService.GetBornInBornegruppe(id);

        }

        public void OnGetNavnDescending()
        {
            Borns = _bornService.GetAllBornNavnDescending();
        }

        public void OnGetIDDescending()
        {
            Borns = _bornService.GetAllBornIDDescending();
        }

        public void OnGetAntalSolgteLodseddelerDescending()
        {
            Borns = _bornService.GetAllBornAntalSolgteLodseddelerDescending();
        }

        public void OnGetGruppeIDDescending()
        {
            Borns = _bornService.GetAllBornGruppeIDDescending();
        }

        public void OnGetNavnAscending()
        {
            Borns = _bornService.GetAllBornNavnAscending();
        }

        public void OnGetIDAscending()
        {
            Borns = _bornService.GetAllBornIDAscending();
        }

        public void OnGetAntalSolgteLodseddelerAscending()
        {
            Borns = _bornService.GetAllBornAntalSolgteLodseddelerAscending();
        }

        public void OnGetGruppeIDAscending()
        {
            Borns = _bornService.GetAllBornGruppeIDAscending();
        }

        public IActionResult OnPostBornByName()
        {
            if (NameSearch != null)
            {
                Borns = _bornService.GetBornByName(NameSearch);
                return Page();
            }

            return Page();
        }
        public void OnGetGivetLodsedlerDescending()
        {
            Borns = _bornService.GetGivetLodsedlerDescending();
        }

        public void OnGetGivetLodsedlerAscending()
        {
            Borns = _bornService.GetGivetLodsedlerAscending();
        }
    }
}
