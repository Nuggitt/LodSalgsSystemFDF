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
       
        public void OnGet()
        {
            Børnegrupper = _IB.GetBørnegruppe();
        }
        public IActionResult OnPostBørnegruppeByName() 
        {
            Børnegrupper = _IB.GetBørnegruppeByName(NameSearch);
            return Page();
        }
        
    }
}
