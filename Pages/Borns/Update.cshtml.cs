using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Repository;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Dapper.SqlMapper;

namespace LodSalgsSystemFDF.Pages.Borns
{
    public class UpdateModel : PageModel
    {
        private IBornService _bornService;
        private IGenericRepository<Born> _genericRepository;

        [BindProperty]
        public Born Born { get; set; }

        public UpdateModel(IBornService børnService, IGenericRepository<Born> genericRepository)
        {
            _bornService = børnService;
            _genericRepository = genericRepository;
        }

        public async Task OnGet(int id) //Almindelig
        {
            Born = await _bornService.GetBorn(id);

        }

        //public IActionResult OnGet(int id) //Generic
        //{
        //    Børn = _genericRepository.GetById(id);
        //    return Page();
        //}

        public IActionResult OnPost() // Almindelig
        {

            Born = _bornService.UpdateBorn(Born);
            return RedirectToPage("GetBorn");
        }

        //public IActionResult OnPost()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        try
        //        {
        //            
        //            bool updateResult = _genericRepository.Update(Børn);

        //            if (updateResult)
        //            {
        //                
        //                return RedirectToPage("GetBørn"); // Redirect to a success page
        //            }
        //            else
        //            {
        //                
        //                ModelState.AddModelError(string.Empty, "Update failed. Please try again.");
        //                return Page();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            
        //            ModelState.AddModelError(string.Empty, "An error occurred during the update. Please try again.");
        //            return Page();
        //        }
        //    }

        //    
        //    return Page();
        //}


    }

}
