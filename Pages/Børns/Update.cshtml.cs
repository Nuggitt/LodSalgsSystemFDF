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

        public async Task OnGet(int id) //Almindelig
        {
            Børn = await _børnService.GetBørn(id);

        }

        //public IActionResult OnGet(int id) //Generic
        //{
        //    Børn = _genericRepository.GetById(id);
        //    return Page();
        //}

        public IActionResult OnPost() // Almindelig
        {

            Børn = _børnService.UpdateBørn(Børn);
            return RedirectToPage("GetBørn");
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
