using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
using LodSalgsSystemFDF.Repository;
using LodSalgsSystemFDF.Services.ADOServices.ADOBornegruppeService;
using LodSalgsSystemFDF.Services.ADOServices.ADOBornService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Borns
{
    public class CreateModel : PageModel
    {
        private IBornService _bornService;
        private IGenericRepository<Born> _genericRepository;

        [BindProperty]
        public Born Borns { get; set; }

        public IEnumerable<Bornegruppe> BornegruppeOptions { get; set; }

        public CreateModel(IBornService bornService, IGenericRepository<Born> genericRepository)
        {
            _bornService = bornService;
            _genericRepository = genericRepository;
        }

        public IActionResult OnGet()
        {

            BornegruppeOptions = _bornService.GetBornegruppeOptions();
            return Page();
        }

        //public IActionResult OnPost()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }
        //    Borns = _bornService.CreateBorn(Borns);
        //    return RedirectToPage("GetBorn");
        //}

        public IActionResult OnPost() //Almindelig
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BornegruppeOptions = _bornService.GetBornegruppeOptions();

                }

                Borns = _bornService.CreateBorn(Borns);
                return RedirectToPage("GetBorn");
            }
            catch (NegativeAmountExceptioncs ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                BornegruppeOptions = _bornService.GetBornegruppeOptions();
                return Page();
            }

            catch (DuplicateKeyException ex)
            {
                ModelState.AddModelError("Borns.Born_ID", ex.Message);
                BornegruppeOptions = _bornService.GetBornegruppeOptions();
                return Page();

            }

        }

        //public IActionResult OnPost() // GENERIC
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        bool addResult = _genericRepository.Add(Borns);

        //        if (addResult)
        //        {
        //            // Addition was successful
        //            // Optionally, you can redirect to another page or return a success message
        //            BornegruppeOptions = _bornService.GetBornegruppeOptions(); // Include the line here

        //            // Assuming Borns is the entity you added, you can retrieve its ID and redirect
        //            int addedEntityId = Borns.Born_ID; // Adjust this based on your entity structure

        //            // Redirect to the "GetBorn" page with the added entity ID as a route parameter
        //            return RedirectToPage("GetBorn", new { id = addedEntityId });
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
