using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;

namespace LodSalgsSystemFDF.Pages.Bornegrupper
{
    [AllowAnonymous]
    public class GetBornegruppeModel : PageModel
    {
        private IBornegruppeService _IB;

        [BindProperty]
        public string NameSearch { get; set; }
        
        public GetBornegruppeModel(IBornegruppeService Bornegruppeservice) 
        {
            _IB = Bornegruppeservice;
        }

       public IEnumerable<Bornegruppe> Bornegrupper  { get; set; } = new List<Bornegruppe>();
       
        public async Task OnGetAsync()
        {
            Bornegrupper = await _IB.GetBornegruppeAsync();
        }
        public IActionResult OnPostBornegruppeByName() 
        {
            if (NameSearch != null)
            {
                Bornegrupper = _IB.GetBornegruppeByName(NameSearch);
                return Page();
            }
            return Page();
        }
        public void OnGetBorngruppeIDAscending()
        {
            Bornegrupper = _IB.GetAllBornegruppeIDASC();
        }
        public void OnGetBornegruppeIDDescending()
        {
            Bornegrupper = _IB.GetAllBornegruppeIDDESC();
        }

        public void OnGetSortAllGruppeNavnAscending()
        {
            Bornegrupper = _IB.SortAllGruppeNavnASC();
        }

        public void OnGetSortAllGruppeNavnDescending()
        {
            Bornegrupper = _IB.SortAllGruppeNavnDESC();
        }

        public void OnGetSortAntalBornDescending()
        {
            Bornegrupper = _IB.SortAllAntalBornDESC();
        }

        public void OnGetSortAntalBornAscending()
        {
            Bornegrupper = _IB.SortAllAntalBornASC();
        }
        public void OnGetSortAntalLederDescending()
        {
            Bornegrupper = _IB.SortAllLederIDDESC();
        }
        public void OnGetSortAntalLederAscending()
        {
            Bornegrupper = _IB.SortAllLederIDASC();
        }

        public void OnGetSortAllAntalLodSeddelerPrGruppeDescending()
        {
            Bornegrupper = _IB.SortAllAntalLodSeddelerPrGruppeDESC();
        }
        public void OnGetSortAllAntalLodSeddelerPrGruppeAscending()
        {
            Bornegrupper = _IB.SortAllAntalLodSeddelerPrGruppeASC();
        }

        public void OnGetSortAllAntalSolgtePrGruppeDescending()
        {
            Bornegrupper = _IB.SortAllAntalSolgtePrGruppeDESC();
        }
        public void OnGetSortAllAntalSolgtePrGruppeAscending()
        {
            Bornegrupper = _IB.SortAllAntalSolgtePrGruppeASC();
        }
    }
}
