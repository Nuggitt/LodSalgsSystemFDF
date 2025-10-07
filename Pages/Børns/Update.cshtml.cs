using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Repository;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static Dapper.SqlMapper;

namespace LodSalgsSystemFDF.Pages.BÃ¸rns
{
    public class UpdateModel : PageModel
    {
        private IBÃ¸rnService _bÃ¸rnService;
        private IGenericRepository<BÃ¸rn> _genericRepository;

        [BindProperty]
        public BÃ¸rn BÃ¸rn { get; set; }

        public UpdateModel(IBÃ¸rnService bÃ¸rnService, IGenericRepository<BÃ¸rn> genericRepository)
        {
            _bÃ¸rnService = bÃ¸rnService;
            _genericRepository = genericRepository;
        }

        public async Task OnGet(int id) //Almindelig
        {
            BÃ¸rn = await _bÃ¸rnService.GetBÃ¸rn(id);

        }

        //public IActionResult OnGet(int id) //Generic
        //{
        //    BÃ¸rn = _genericRepository.GetById(id);
        //    return Page();
        //}

        public IActionResult OnPost() // Almindelig
        {

            BÃ¸rn = _bÃ¸rnService.UpdateBÃ¸rn(BÃ¸rn);
            return RedirectToPage("GetBÃ¸rn");
        }

        //public IActionResult OnPost()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        try
        //        {
        //            
        //            bool updateResult = _genericRepository.Update(BÃ¸rn);

        //            if (updateResult)
        //            {
        //                
        //                return RedirectToPage("GetBÃ¸rn"); // Redirect to a success page
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

