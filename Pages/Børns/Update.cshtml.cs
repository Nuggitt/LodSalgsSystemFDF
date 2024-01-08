using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Repository;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Dapper.SqlMapper;

namespace LodSalgsSystemFDF.Pages.Børns
{
    public class UpdateModel : PageModel
    {
        private IBørnService _børnService;
        private IGenericRepository<Børn> _genericRepository;

        [BindProperty]
        public Børn Børn { get; set; }

        public UpdateModel(IBørnService børnService, IGenericRepository<Børn> genericRepository)
        {
            _børnService = børnService;
            _genericRepository = genericRepository;
        }
        //public async Task OnGet(int id) //Almindelig
        //{
        //    Børn = await _børnService.GetBørn(id);
            
        //}

        public IActionResult OnGet(int id) //Generic
        {
            Børn = _genericRepository.GetById(id);
            return Page();
        }

        //public IActionResult OnPost()
        //{

        //    Børn = _børnService.UpdateBørn(Børn);
        //    return RedirectToPage("GetBørn");
        //}

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    // Assuming repository is an instance of your data access logic
                    bool updateResult = _genericRepository.Update(Børn);

                    if (updateResult)
                    {
                        // Update successful
                        return RedirectToPage("GetBørn"); // Redirect to a success page
                    }
                    else
                    {
                        // Update failed
                        ModelState.AddModelError(string.Empty, "Update failed. Please try again.");
                        return Page();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception, log it, etc.
                    ModelState.AddModelError(string.Empty, "An error occurred during the update. Please try again.");
                    return Page();
                }
            }

            // Model state is not valid, return to the same page with validation errors
            return Page();
        }


    }

}
