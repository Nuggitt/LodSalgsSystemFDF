using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBÃ¸rnService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.IndtÃ¦gter
{
    [AllowAnonymous]
    public class GetIndtÃ¦gterModel : PageModel
    {
        private IIndtÃ¦gtService _indtÃ¦gtService;
        private IBÃ¸rnegruppeService _IB;

        [BindProperty]
        public string NameSearch { get; set; }
        public GetIndtÃ¦gterModel(IIndtÃ¦gtService indtÃ¦gtService)
        {
            _indtÃ¦gtService = indtÃ¦gtService;
        }
        [BindProperty]
        public IEnumerable<IndtÃ¦gt> IndtÃ¦gter { get; set; } = new List<IndtÃ¦gt>();

        public void OnGet()
        {
            IndtÃ¦gter = _indtÃ¦gtService.GetIndtÃ¦gter();
        }

        public void OnGetIndtÃ¦gtIDDESC()
        {
            IndtÃ¦gter = _indtÃ¦gtService.GetIndtÃ¦gtIDDESC();
        }

        public void OnGetIndtÃ¦gtIDASC()
        {
            IndtÃ¦gter = _indtÃ¦gtService.GetIndtÃ¦gtIDASC();
        }

        public void OnGetAntalSolgteLodseddelerDESC()
        {
            IndtÃ¦gter = _indtÃ¦gtService.GetAntalSolgteLodseddelerDESC();
        }

        public void OnGetAntalSolgteLodseddelerASC()
        {
            IndtÃ¦gter = _indtÃ¦gtService.GetAntalSolgteLodseddelerASC();
        }

        public void OnGetAntalSolgteLodseddlerForGruppenASC()
        {
            IndtÃ¦gter = _indtÃ¦gtService.GetAntalSolgteLodseddelerForGruppenASC();
        }

        public void OnGetAntalSolgteLodseddlerForGruppenDESC()
        {
            IndtÃ¦gter = _indtÃ¦gtService.GetAntalSolgteLodseddelerForGruppenDESC();
        }




    }
}

