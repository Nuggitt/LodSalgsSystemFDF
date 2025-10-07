using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Repository;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.BÃ¸rns
{
    public class DeleteModel : PageModel
    {
        private IBÃ¸rnService _bÃ¸rnService;
        IGenericRepository<BÃ¸rn> _genericRepository;
        BÃ¸rnRepository _bÃ¸rnRepository;

        [BindProperty]
        public BÃ¸rn BÃ¸rn { get; set; }

        public DeleteModel(IBÃ¸rnService bÃ¸rnService, IGenericRepository<BÃ¸rn> genericRepository, BÃ¸rnRepository bÃ¸rnRepository)
        {
            _bÃ¸rnService = bÃ¸rnService;
            _genericRepository = genericRepository;
            _bÃ¸rnRepository = bÃ¸rnRepository;
        }

        public async Task OnGet(int id) //Async Almindelig
        {
            BÃ¸rn = await _bÃ¸rnService.GetBÃ¸rn(id);
        }

        //--------------------------------------------------------------

        //public IActionResult OnGet(int id) // GENERIC
        //{
        //    BÃ¸rn = _bÃ¸rnRepository.GetById(id);
        //    BÃ¸rn = _genericRepository.GetById(id);
        //    return Page();
        //}

        //----------------------------------------------------------

        public IActionResult OnPost() //Almindelig
        {
            BÃ¸rn = _bÃ¸rnService.DeleteBÃ¸rn(BÃ¸rn);
            return RedirectToPage("GetBÃ¸rn");
        }

        //public IActionResult OnPost(int id) //Generic!!!
        //{
        //    BÃ¸rn entityToDelete = _genericRepository.GetById(id);

        //    if (entityToDelete == null)
        //    {

        //        return NotFound();
        //    }

        //    bool deletionResult = _genericRepository.Delete(entityToDelete);

        //    if (deletionResult)
        //    {


        //        return RedirectToPage("GetBÃ¸rn");
        //    }
        //    else
        //    {

        //        return Page();
        //    }
        //}
    }
}

