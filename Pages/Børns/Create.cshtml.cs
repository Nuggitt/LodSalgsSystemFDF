using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
using LodSalgsSystemFDF.Repository;
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
        private IGenericRepository<Børn> _genericRepository;

        [BindProperty]
        public Børn Børns { get; set; }

        public IEnumerable<Børnegruppe> BørnegruppeOptions { get; set; }

        public CreateModel(IBørnService børnService, IGenericRepository<Børn> genericRepository)
        {
            _børnService = børnService;
            _genericRepository = genericRepository;
        }

        public IActionResult OnGet()
        {

            BørnegruppeOptions = _børnService.GetBørnegruppeOptions();
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

        public IActionResult OnPost() //Almindelig
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

        //public IActionResult OnPost() // GENERIC
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        bool addResult = _genericRepository.Add(Børns);

        //        if (addResult)
        //        {
        //            // Addition was successful
        //            // Optionally, you can redirect to another page or return a success message
        //            BørnegruppeOptions = _børnService.GetBørnegruppeOptions(); // Include the line here

        //            // Assuming Børns is the entity you added, you can retrieve its ID and redirect
        //            int addedEntityId = Børns.Børn_ID; // Adjust this based on your entity structure

        //            // Redirect to the "GetBørn" page with the added entity ID as a route parameter
        //            return RedirectToPage("GetBørn", new { id = addedEntityId });
        //        }
        //        else
        //        {
        //            // Addition failed
        //            // Optionally, you can return an error message or handle the failure in another way
        //            return Page(); // Stay on the current page, or return an error view
        //        }
        //    }

        //    // ModelState is not valid, return to the current page with validation errors
        //    return Page();
        //}

    }
}
