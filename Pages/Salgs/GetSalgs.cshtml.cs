using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
using LodSalgsSystemFDF.Repository;
using LodSalgsSystemFDF.Services.ADOServices.ADOIndtægtService;
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
        IGenericRepository<Salg> _genericRepository;

        [BindProperty]
        public int IDSearch { get; set; }
        [BindProperty]
        public float MinPrice { get; set; }
        [BindProperty]
        public float MaxPrice { get; set; }
        public GetSalgsModel(ISalgService salgService, IGenericRepository<Salg> genericRepository)
        {
            _salgService = salgService;
            _genericRepository = genericRepository;
        }
        [BindProperty]
        public IEnumerable<Salg> Salgs { get; set; } = new List<Salg>();

        public void OnGet()
        {
            Salgs = _salgService.GetSalgs();
            //Salgs = _genericRepository.GetAll();
        }

        public void OnGetAntalSolgteLodseddelerDESC()
        {
            Salgs = _salgService.GetAntalSolgteLodseddelerDESC();
        }

        public void OnGetAntalSolgteLodseddelerASC()
        {
            Salgs = _salgService.GetAntalSolgteLodseddelerASC();
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
