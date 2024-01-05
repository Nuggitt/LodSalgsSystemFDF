using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Salgs
{
    public class CreateModel : PageModel
    {
        private ISalgService _salgService;

        [BindProperty]
        public Salg Salg { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Bid { get; set; }

        public IEnumerable<Leder> LederOptions { get; set; }

        public CreateModel(ISalgService salgService)
        {
            _salgService = salgService;
        }

        public IActionResult OnGet()
        {
            _salgService.GetB�rnById(Id, Bid);
            LederOptions = _salgService.GetLederOptions();
            return Page();
            
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                LederOptions = _salgService.GetLederOptions();
                
            }
            Salg.B�rn_ID = Id;
            Salg.B�rnegruppe_ID = Bid;
            Salg = _salgService.CreateSalg(Salg);
            return RedirectToPage("GetSalgs");
        }
    }
}
