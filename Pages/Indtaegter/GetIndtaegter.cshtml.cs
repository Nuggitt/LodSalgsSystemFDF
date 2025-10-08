using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBornService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Indtaegter
{
    [AllowAnonymous]
    public class GetIndtaegterModel : PageModel
    {
        private IIndtaegtService _indtaegtService;
        private IBornegruppeService _IB;

        [BindProperty]
        public string NameSearch { get; set; }
        public GetIndtaegterModel(IIndtaegtService indtaegtService)
        {
            _indtaegtService = indtaegtService;
        }
        [BindProperty]
        public IEnumerable<Indtaegt> Indtaegter { get; set; } = new List<Indtaegt>();

        public void OnGet()
        {
            Indtaegter = _indtaegtService.GetIndtaegter();
        }

        public void OnGetIndtaegtIDDESC()
        {
            Indtaegter = _indtaegtService.GetIndtaegtIDDESC();
        }

        public void OnGetIndtaegtIDASC()
        {
            Indtaegter = _indtaegtService.GetIndtaegtIDASC();
        }

        public void OnGetAntalSolgteLodseddelerDESC()
        {
            Indtaegter = _indtaegtService.GetAntalSolgteLodseddelerDESC();
        }

        public void OnGetAntalSolgteLodseddelerASC()
        {
            Indtaegter = _indtaegtService.GetAntalSolgteLodseddelerASC();
        }

        public void OnGetAntalSolgteLodseddlerForGruppenASC()
        {
            Indtaegter = _indtaegtService.GetAntalSolgteLodseddelerForGruppenASC();
        }

        public void OnGetAntalSolgteLodseddlerForGruppenDESC()
        {
            Indtaegter = _indtaegtService.GetAntalSolgteLodseddelerForGruppenDESC();
        }




    }
}
