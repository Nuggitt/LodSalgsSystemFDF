using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBørnService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Indtægter
{
    [Authorize]
    public class GetIndtægterModel : PageModel
    {
        private IIndtægtService _indtægtService;
        private IBørnegruppeService _IB;

        [BindProperty]
        public string NameSearch { get; set; }
        public GetIndtægterModel(IIndtægtService indtægtService)
        {
            _indtægtService = indtægtService;
        }
        [BindProperty]
        public IEnumerable<Indtægt> Indtægter { get; set; } = new List<Indtægt>();

        public void OnGet()
        {
            Indtægter = _indtægtService.GetIndtægter();
        }

        public void OnGetIndtægtIDDESC()
        {
            Indtægter = _indtægtService.GetIndtægtIDDESC();
        }

        public void OnGetIndtægtIDASC()
        {
            Indtægter = _indtægtService.GetIndtægtIDASC();
        }

        public void OnGetAntalSolgteLodseddelerDESC()
        {
            Indtægter = _indtægtService.GetAntalSolgteLodseddelerDESC();
        }

        public void OnGetAntalSolgteLodseddelerASC()
        {
            Indtægter = _indtægtService.GetAntalSolgteLodseddelerASC();
        }

        public void OnGetAntalSolgteLodseddlerForGruppenASC()
        {
            Indtægter = _indtægtService.GetAntalSolgteLodseddelerForGruppenASC();
        }

        public void OnGetAntalSolgteLodseddlerForGruppenDESC()
        {
            Indtægter = _indtægtService.GetAntalSolgteLodseddelerForGruppenDESC();
        }




    }
}
