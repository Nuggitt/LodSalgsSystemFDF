using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
using LodSalgsSystemFDF.Services.ADOServices.ADOBornService;
using LodSalgsSystemFDF.Services.ADOServices.ADOIndtaegtService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Bornegrupper
{
    public class CreateBorngruppeModel : PageModel
    {
        private IBornegruppeService _børnegruppeservice;

        [BindProperty]
        public Bornegruppe Bornegrupper { get; set; }

        public IEnumerable<Leder> LederOptions { get; set; }

        public CreateBorngruppeModel(IBornegruppeService børnegruppeService)
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
        
                }

                Bornegrupper = _børnegruppeservice.CreateBornegruppe(Bornegrupper);
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
