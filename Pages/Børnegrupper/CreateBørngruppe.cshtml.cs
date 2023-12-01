using LodSalgsSystemFDF.Models;
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
        //Exception
        //public class DuplicatedBørnegruppeIdException : Exception
        //{
        //    public DuplicatedBørnegruppeIdException()
        //    {
        //    }

        //    public DuplicatedBørnegruppeIdException(string message)
        //        : base(message)
        //    {
        //    }

        //    public DuplicatedBørnegruppeIdException(string message, Exception innerException)
        //        : base(message, innerException)
        //    {
        //    }
        //}
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Børnegrupper = _børnegruppeservice.CreateBørnegruppe(Børnegrupper);
            return RedirectToPage("GetBørnegrupper");

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
