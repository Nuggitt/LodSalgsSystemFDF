using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
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
        public CreateBørngruppeModel(IBørnegruppeService børnegruppeService)
        {
            _børnegruppeservice = børnegruppeService;
        }
        

        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                Børnegrupper = _børnegruppeservice.CreateBørnegruppe(Børnegrupper);
                return RedirectToPage("GetBørnegrupper");
            }
            catch (NegativeAmountExceptioncs ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

            catch (DuplicateKeyException ex)
            {
                ModelState.AddModelError("Børnegrupper.Børnegruppe_ID", ex.Message);
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
