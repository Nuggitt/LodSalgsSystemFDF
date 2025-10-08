using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Repository;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Borns
{
    public class DeleteModel : PageModel
    {
        private IBornService _bornService;
        IGenericRepository<Born> _genericRepository;
        BornRepository _bornRepository;

        [BindProperty]
        public Born Born { get; set; }

        public DeleteModel(IBornService bornService, IGenericRepository<Born> genericRepository, BornRepository bornRepository)
        {
            _bornService = bornService;
            _genericRepository = genericRepository;
            _bornRepository = bornRepository;
        }

        public async Task OnGet(int id) //Async Almindelig
        {
            Born = await _bornService.GetBorn(id);
        }

        //--------------------------------------------------------------

        //public IActionResult OnGet(int id) // GENERIC
        //{
        //    Børn = _børnRepository.GetById(id);
        //    Børn = _genericRepository.GetById(id);
        //    return Page();
        //}

        //----------------------------------------------------------

        public IActionResult OnPost() //Almindelig
        {
            Born = _bornService.DeleteBorn(Born);
            return RedirectToPage("GetBorn");
        }

        //public IActionResult OnPost(int id) //Generic!!!
        //{
        //    Børn entityToDelete = _genericRepository.GetById(id);

        //    if (entityToDelete == null)
        //    {

        //        return NotFound();
        //    }

        //    bool deletionResult = _genericRepository.Delete(entityToDelete);

        //    if (deletionResult)
        //    {


        //        return RedirectToPage("GetBørn");
        //    }
        //    else
        //    {

        //        return Page();
        //    }
        //}
    }
}
