using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Børnegrupper
{
    public class GetBørnegruppeModel : PageModel
    {
        private IBørnegruppeService _IB;
        
        public GetBørnegruppeModel(IBørnegruppeService Børnegruppeservice) 
        {
            _IB = Børnegruppeservice;
        }

       public IEnumerable<Børnegruppe> Børnegrupper  { get; set; } = new List<Børnegruppe>();
       
        public void OnGet()
        {
            Børnegrupper = _IB.GetBørnegruppe();
        }
    }
}
