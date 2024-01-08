using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Repository;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.Børns
{
    public class DeleteModel : PageModel
    {
        private IBørnService _børnService;
        IGenericRepository<Børn> _genericRepository;
        BørnRepository _børnRepository;

        [BindProperty]
        public Børn Børn { get; set; }

        public DeleteModel(IBørnService børnService, IGenericRepository<Børn> genericRepository, BørnRepository børnRepository)
        {
            _børnService = børnService;
            _genericRepository = genericRepository;
            _børnRepository = børnRepository;
        }

        public async Task OnGet(int id) //Async Almindelig
        {
            Børn = await _børnService.GetBørn(id);
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
            Børn = _børnService.DeleteBørn(Børn);
            return RedirectToPage("GetBørn");
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
