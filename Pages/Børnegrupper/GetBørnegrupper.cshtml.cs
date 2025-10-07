using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;

namespace LodSalgsSystemFDF.Pages.BÃ¸rnegrupper
{
    [AllowAnonymous]
    public class GetBÃ¸rnegruppeModel : PageModel
    {
        private IBÃ¸rnegruppeService _IB;

        [BindProperty]
        public string NameSearch { get; set; }
        
        public GetBÃ¸rnegruppeModel(IBÃ¸rnegruppeService BÃ¸rnegruppeservice) 
        {
            _IB = BÃ¸rnegruppeservice;
        }

       public IEnumerable<BÃ¸rnegruppe> BÃ¸rnegrupper  { get; set; } = new List<BÃ¸rnegruppe>();
       
        public async Task OnGetAsync()
        {
            BÃ¸rnegrupper = await _IB.GetBÃ¸rnegruppeAsync();
        }
        public IActionResult OnPostBÃ¸rnegruppeByName() 
        {
            if (NameSearch != null)
            {
                BÃ¸rnegrupper = _IB.GetBÃ¸rnegruppeByName(NameSearch);
                return Page();
            }
            return Page();
        }
        public void OnGetBÃ¸rngruppeIDAscending()
        {
            BÃ¸rnegrupper = _IB.GetAllBÃ¸rnegruppeIDASC();
        }
        public void OnGetBÃ¸rnegruppeIDDescending()
        {
            BÃ¸rnegrupper = _IB.GetAllBÃ¸rnegruppeIDDESC();
        }

        public void OnGetSortAllGruppeNavnAscending()
        {
            BÃ¸rnegrupper = _IB.SortAllGruppeNavnASC();
        }

        public void OnGetSortAllGruppeNavnDescending()
        {
            BÃ¸rnegrupper = _IB.SortAllGruppeNavnDESC();
        }

        public void OnGetSortAntalBÃ¸rnDescending()
        {
            BÃ¸rnegrupper = _IB.SortAllAntalBÃ¸rnDESC();
        }

        public void OnGetSortAntalBÃ¸rnAscending()
        {
            BÃ¸rnegrupper = _IB.SortAllAntalBÃ¸rnASC();
        }
        public void OnGetSortAntalLederDescending()
        {
            BÃ¸rnegrupper = _IB.SortAllLederIDDESC();
        }
        public void OnGetSortAntalLederAscending()
        {
            BÃ¸rnegrupper = _IB.SortAllLederIDASC();
        }

        public void OnGetSortAllAntalLodSeddelerPrGruppeDescending()
        {
            BÃ¸rnegrupper = _IB.SortAllAntalLodSeddelerPrGruppeDESC();
        }
        public void OnGetSortAllAntalLodSeddelerPrGruppeAscending()
        {
            BÃ¸rnegrupper = _IB.SortAllAntalLodSeddelerPrGruppeASC();
        }

        public void OnGetSortAllAntalSolgtePrGruppeDescending()
        {
            BÃ¸rnegrupper = _IB.SortAllAntalSolgtePrGruppeDESC();
        }
        public void OnGetSortAllAntalSolgtePrGruppeAscending()
        {
            BÃ¸rnegrupper = _IB.SortAllAntalSolgtePrGruppeASC();
        }
    }
}

