using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
using LodSalgsSystemFDF.Services.ADOServices.ADOBÃ¸rnService;
using LodSalgsSystemFDF.Services.ADOServices.ADOIndtÃ¦gtService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.BÃ¸rnegrupper
{
    public class CreateBÃ¸rngruppeModel : PageModel
    {
        private IBÃ¸rnegruppeService _bÃ¸rnegruppeservice;

        [BindProperty]
        public BÃ¸rnegruppe BÃ¸rnegrupper { get; set; }

        public IEnumerable<Leder> LederOptions { get; set; }

        public CreateBÃ¸rngruppeModel(IBÃ¸rnegruppeService bÃ¸rnegruppeService)
        {
            _bÃ¸rnegruppeservice = bÃ¸rnegruppeService;
        }

        public IActionResult OnGet()
        {
            LederOptions = _bÃ¸rnegruppeservice.GetLederOptions();
            return Page();
        }


        public IActionResult OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    LederOptions = _bÃ¸rnegruppeservice.GetLederOptions();
        
                }

                BÃ¸rnegrupper = _bÃ¸rnegruppeservice.CreateBÃ¸rnegruppe(BÃ¸rnegrupper);
                return RedirectToPage("GetBÃ¸rnegrupper");
            }
            catch (NegativeAmountExceptioncs ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                LederOptions = _bÃ¸rnegruppeservice.GetLederOptions();
                return Page();
            }

            catch (DuplicateKeyException ex)
            {
                ModelState.AddModelError("BÃ¸rnegrupper.BÃ¸rnegruppe_ID", ex.Message);
                LederOptions = _bÃ¸rnegruppeservice.GetLederOptions();
                return Page();

                //ModelState.AddModelError(string.Empty, ex.Message);
                //return Page();
        
            }
            


            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            //BÃ¸rnegrupper = _bÃ¸rnegruppeservice.CreateBÃ¸rnegruppe(BÃ¸rnegrupper);
            //return RedirectToPage("GetBÃ¸rnegrupper");

        }
        //public IActionResult OnPost()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    try
        //    {
        //        // Try to create the BÃ¸rnegruppe
        //        BÃ¸rnegrupper = _bÃ¸rnegruppeservice.CreateBÃ¸rnegruppe(BÃ¸rnegrupper);
        //        return RedirectToPage("GetBÃ¸rnegrupper");
        //    }
        //    catch (DuplicatedBÃ¸rnegruppeIdException ex)
        //    {
        //        ModelState.AddModelError("BÃ¸rnegrupper.BÃ¸rnegruppe_ID", ex.Message);
        //        return Page();
        //    }
        //}
    }
}

