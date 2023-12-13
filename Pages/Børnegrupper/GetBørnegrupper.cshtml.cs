using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;

namespace LodSalgsSystemFDF.Pages.Børnegrupper
{
    [Authorize]
    public class GetBørnegruppeModel : PageModel
    {
        private IBørnegruppeService _IB;

        [BindProperty]
        public string NameSearch { get; set; }
        
        public GetBørnegruppeModel(IBørnegruppeService Børnegruppeservice) 
        {
            _IB = Børnegruppeservice;
        }

       public IEnumerable<Børnegruppe> Børnegrupper  { get; set; } = new List<Børnegruppe>();
       
        public async Task OnGetAsync()
        {
            Børnegrupper = await _IB.GetBørnegruppeAsync();
        }
        public IActionResult OnPostBørnegruppeByName() 
        {
            if (NameSearch != null)
            {
                Børnegrupper = _IB.GetBørnegruppeByName(NameSearch);
                return Page();
            }
            return Page();
        }
        public void OnGetBørngruppeIDAscending()
        {
            Børnegrupper = _IB.GetAllBørnegruppeIDASC();
        }
        public void OnGetBørnegruppeIDDescending()
        {
            Børnegrupper = _IB.GetAllBørnegruppeIDDESC();
        }

        public void OnGetSortAllGruppeNavnAscending()
        {
            Børnegrupper = _IB.SortAllGruppeNavnASC();
        }

        public void OnGetSortAllGruppeNavnDescending()
        {
            Børnegrupper = _IB.SortAllGruppeNavnDESC();
        }

        public void OnGetSortAntalBørnDescending()
        {
            Børnegrupper = _IB.SortAllAntalBørnDESC();
        }

        public void OnGetSortAntalBørnAscending()
        {
            Børnegrupper = _IB.SortAllAntalBørnASC();
        }
        public void OnGetSortAntalLederDescending()
        {
            Børnegrupper = _IB.SortAllLederIDDESC();
        }
        public void OnGetSortAntalLederAscending()
        {
            Børnegrupper = _IB.SortAllLederIDASC();
        }

        public void OnGetSortAllAntalLodSeddelerPrGruppeDescending()
        {
            Børnegrupper = _IB.SortAllAntalLodSeddelerPrGruppeDESC();
        }
        public void OnGetSortAllAntalLodSeddelerPrGruppeAscending()
        {
            Børnegrupper = _IB.SortAllAntalLodSeddelerPrGruppeASC();
        }

        public void OnGetSortAllAntalSolgtePrGruppeDescending()
        {
            Børnegrupper = _IB.SortAllAntalSolgtePrGruppeDESC();
        }
        public void OnGetSortAllAntalSolgtePrGruppeAscending()
        {
            Børnegrupper = _IB.SortAllAntalSolgtePrGruppeASC();
        }
    }
}
