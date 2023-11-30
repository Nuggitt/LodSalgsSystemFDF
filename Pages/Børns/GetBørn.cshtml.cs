using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBørnService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Børns
{
    public class GetBørnModel : PageModel
    {
        private IBørnService _børnService;
        public GetBørnModel(IBørnService børnService)
        {
            _børnService = børnService;
        }
        [BindProperty]
        public IEnumerable<Børn> Børns { get; set; } = new List<Børn>();
        public void OnGet()
        {
            Børns = _børnService.GetBørn();
        }

        public void OnGetDescending()
        {
            Børns = _børnService.GetAllBørnDescending();
        }

    }
}
