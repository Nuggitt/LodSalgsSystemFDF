using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
using LodSalgsSystemFDF.Services.ADOServices.ADOBørnegruppeService;
using LodSalgsSystemFDF.Services.ADOServices.ADOBørnService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Børns
{
    public class CreateModel : PageModel
    {
        private IBørnService _børnService;

        [BindProperty]
        public Børn Børns { get; set; }

        public IEnumerable<Børnegruppe> BørnegruppeOptions { get; set; }

        public CreateModel(IBørnService børnService)
        {
            _børnService=børnService;
        }

        public IActionResult OnGet()
        {
            // Load BørnegruppeOptions from your service or repository
            BørnegruppeOptions = _børnService.GetBørnegruppeOptions(); // Replace with your actual method to get Børnegruppe options
            return Page();
        }

        //public IActionResult OnPost()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }
        //    Børns = _børnService.CreateBørn(Børns);
        //    return RedirectToPage("GetBørn");
        //}

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BørnegruppeOptions = _børnService.GetBørnegruppeOptions();
                    
                }

                Børns = _børnService.CreateBørn(Børns);
                return RedirectToPage("GetBørn");
            }
            catch (NegativeAmountExceptioncs ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                BørnegruppeOptions = _børnService.GetBørnegruppeOptions();
                return Page();
            }

            catch (DuplicateKeyException ex)
            {
                ModelState.AddModelError("Børns.Børn_ID", ex.Message);
                BørnegruppeOptions = _børnService.GetBørnegruppeOptions();
                return Page();

            }

        }
    }
}
