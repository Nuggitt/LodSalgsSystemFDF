using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
using LodSalgsSystemFDF.Services.ADOServices.ADOBørnService;
using LodSalgsSystemFDF.Services.ADOServices.ADOIndtægtService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Børnegrupper
{
    public class CreateBørngruppeModel : PageModel
    {
        private IBørnegruppeService _børnegruppeservice;

        [BindProperty]
        public Børnegruppe Børnegrupper { get; set; }

        public IEnumerable<Leder> LederOptions { get; set; }

        public CreateBørngruppeModel(IBørnegruppeService børnegruppeService)
        {
            _børnegruppeservice = børnegruppeService;
        }

        public IActionResult OnGet()
        {
            LederOptions = _børnegruppeservice.GetLederOptions();
            return Page();
        }


        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LederOptions = _børnegruppeservice.GetLederOptions();
                    return Page();
                }

                Børnegrupper = _børnegruppeservice.CreateBørnegruppe(Børnegrupper);
                return RedirectToPage("GetBørnegrupper");
            }
            catch (NegativeAmountExceptioncs ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                LederOptions = _børnegruppeservice.GetLederOptions();
                return Page();
            }

            catch (DuplicateKeyException ex)
            {
                ModelState.AddModelError("Børnegrupper.Børnegruppe_ID", ex.Message);
                LederOptions = _børnegruppeservice.GetLederOptions();
                return Page();

                //ModelState.AddModelError(string.Empty, ex.Message);
                //return Page();
        
            }
            


            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            //Børnegrupper = _børnegruppeservice.CreateBørnegruppe(Børnegrupper);
            //return RedirectToPage("GetBørnegrupper");

        }
        //public IActionResult OnPost()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    try
        //    {
        //        // Try to create the Børnegruppe
        //        Børnegrupper = _børnegruppeservice.CreateBørnegruppe(Børnegrupper);
        //        return RedirectToPage("GetBørnegrupper");
        //    }
        //    catch (DuplicatedBørnegruppeIdException ex)
        //    {
        //        ModelState.AddModelError("Børnegrupper.Børnegruppe_ID", ex.Message);
        //        return Page();
        //    }
        //}
    }
}
