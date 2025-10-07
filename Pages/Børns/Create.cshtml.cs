using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Models.Exceptions;
using LodSalgsSystemFDF.Repository;
using LodSalgsSystemFDF.Services.ADOServices.ADOBÃ¸rnegruppeService;
using LodSalgsSystemFDF.Services.ADOServices.ADOBÃ¸rnService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.BÃ¸rns
{
    public class CreateModel : PageModel
    {
        private IBÃ¸rnService _bÃ¸rnService;
        private IGenericRepository<BÃ¸rn> _genericRepository;

        [BindProperty]
        public BÃ¸rn BÃ¸rns { get; set; }

        public IEnumerable<BÃ¸rnegruppe> BÃ¸rnegruppeOptions { get; set; }

        public CreateModel(IBÃ¸rnService bÃ¸rnService, IGenericRepository<BÃ¸rn> genericRepository)
        {
            _bÃ¸rnService = bÃ¸rnService;
            _genericRepository = genericRepository;
        }

        public IActionResult OnGet()
        {

            BÃ¸rnegruppeOptions = _bÃ¸rnService.GetBÃ¸rnegruppeOptions();
            return Page();
        }

        //public IActionResult OnPost()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }
        //    BÃ¸rns = _bÃ¸rnService.CreateBÃ¸rn(BÃ¸rns);
        //    return RedirectToPage("GetBÃ¸rn");
        //}

        public IActionResult OnPost() //Almindelig
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    BÃ¸rnegruppeOptions = _bÃ¸rnService.GetBÃ¸rnegruppeOptions();

                }

                BÃ¸rns = _bÃ¸rnService.CreateBÃ¸rn(BÃ¸rns);
                return RedirectToPage("GetBÃ¸rn");
            }
            catch (NegativeAmountExceptioncs ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                BÃ¸rnegruppeOptions = _bÃ¸rnService.GetBÃ¸rnegruppeOptions();
                return Page();
            }

            catch (DuplicateKeyException ex)
            {
                ModelState.AddModelError("BÃ¸rns.BÃ¸rn_ID", ex.Message);
                BÃ¸rnegruppeOptions = _bÃ¸rnService.GetBÃ¸rnegruppeOptions();
                return Page();

            }

        }

        //public IActionResult OnPost() // GENERIC
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        bool addResult = _genericRepository.Add(BÃ¸rns);

        //        if (addResult)
        //        {
        //            // Addition was successful
        //            // Optionally, you can redirect to another page or return a success message
        //            BÃ¸rnegruppeOptions = _bÃ¸rnService.GetBÃ¸rnegruppeOptions(); // Include the line here

        //            // Assuming BÃ¸rns is the entity you added, you can retrieve its ID and redirect
        //            int addedEntityId = BÃ¸rns.BÃ¸rn_ID; // Adjust this based on your entity structure

        //            // Redirect to the "GetBÃ¸rn" page with the added entity ID as a route parameter
        //            return RedirectToPage("GetBÃ¸rn", new { id = addedEntityId });
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

