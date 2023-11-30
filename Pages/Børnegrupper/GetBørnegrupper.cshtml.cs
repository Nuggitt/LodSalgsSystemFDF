using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;

namespace LodSalgsSystemFDF.Pages.Børnegrupper
{
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
            Børnegrupper = _IB.GetBørnegruppeByName(NameSearch);
            return Page();
        }
        
    }
}
