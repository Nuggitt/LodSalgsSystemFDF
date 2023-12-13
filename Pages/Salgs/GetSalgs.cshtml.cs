using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
using LodSalgsSystemFDF.Services.ADOServices.ADOLederService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Salgs
{
    [Authorize]
    public class GetSalgsModel : PageModel
    {
        private ISalgService _salgService;
        private IBørnegruppeService _IB;
        [BindProperty]
        public int IDSearch { get; set; }
        [BindProperty]
        public float MinPrice { get; set; }
        [BindProperty]
        public float MaxPrice { get; set; }
        public GetSalgsModel(ISalgService salgService)
        {
            _salgService = salgService;
        }
        [BindProperty]
        public IEnumerable<Salg> Salgs { get; set; } = new List<Salg>();

        public void OnGet()
        {
            Salgs = _salgService.GetSalgs();
        }
        public IActionResult OnPostBørnegruppeByID()
        {
            if (IDSearch != 0)
            {
                Salgs = _salgService.GetBørnegruppeByID(IDSearch);
                return Page();
            }
            return Page();

        }
        public IActionResult OnpostPriceFilter()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                 Salgs = _salgService.PriceFilter(MaxPrice, MinPrice);
                 return Page();
            }
            catch (NegativeAmountExceptioncs ex) 
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
           
        }
    }
}