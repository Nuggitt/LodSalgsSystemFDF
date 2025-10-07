using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Repository;
using LodSalgsSystemFDF.Services.ADOServices.ADOBÃ¸rnService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;

namespace LodSalgsSystemFDF.Pages.BÃ¸rns
{
    [AllowAnonymous]
    public class GetBÃ¸rnModel : PageModel
    {
        private IBÃ¸rnService _bÃ¸rnService;
        private IGenericRepository<BÃ¸rn> _genericRepository;
        BÃ¸rnRepository _bÃ¸rnRepository;   

        [BindProperty]
        public string NameSearch { get; set; }
        public GetBÃ¸rnModel(IBÃ¸rnService bÃ¸rnService, IGenericRepository<BÃ¸rn> genericRepository, BÃ¸rnRepository bÃ¸rnRepository)
        {
            _bÃ¸rnService = bÃ¸rnService;
            _genericRepository = genericRepository;
            _bÃ¸rnRepository = bÃ¸rnRepository;
        }
        [BindProperty]
        public IEnumerable<BÃ¸rn> BÃ¸rns { get; set; } = new List<BÃ¸rn>();
        public async Task OnGet() //Async almindelig
        {

            BÃ¸rns = await _bÃ¸rnService.GetBÃ¸rn();

        }

        //public IActionResult OnGet() //Generic
        //{
        //    //BÃ¸rns = _bÃ¸rnRepository.GetAll();
        //    BÃ¸rns = _genericRepository.GetAll();
        //    return Page();
        //}

        public async Task OnGetBÃ¸rnInBÃ¸rnegruppe(int id)
        {
            BÃ¸rns = await _bÃ¸rnService.GetBÃ¸rnInBÃ¸rnegruppe(id);

        }

        public void OnGetNavnDescending()
        {
            BÃ¸rns = _bÃ¸rnService.GetAllBÃ¸rnNavnDescending();
        }

        public void OnGetIDDescending()
        {
            BÃ¸rns = _bÃ¸rnService.GetAllBÃ¸rnIDDescending();
        }

        public void OnGetAntalSolgteLodseddelerDescending()
        {
            BÃ¸rns = _bÃ¸rnService.GetAllBÃ¸rnAntalSolgteLodseddelerDescending();
        }

        public void OnGetGruppeIDDescending()
        {
            BÃ¸rns = _bÃ¸rnService.GetAllBÃ¸rnGruppeIDDescending();
        }

        public void OnGetNavnAscending()
        {
            BÃ¸rns = _bÃ¸rnService.GetAllBÃ¸rnNavnAscending();
        }

        public void OnGetIDAscending()
        {
            BÃ¸rns = _bÃ¸rnService.GetAllBÃ¸rnIDAscending();
        }

        public void OnGetAntalSolgteLodseddelerAscending()
        {
            BÃ¸rns = _bÃ¸rnService.GetAllBÃ¸rnAntalSolgteLodseddelerAscending();
        }

        public void OnGetGruppeIDAscending()
        {
            BÃ¸rns = _bÃ¸rnService.GetAllBÃ¸rnGruppeIDAscending();
        }

        public IActionResult OnPostBÃ¸rnByName()
        {
            if (NameSearch != null)
            {
                BÃ¸rns = _bÃ¸rnService.GetBÃ¸rnByName(NameSearch);
                return Page();
            }
            
            return Page();
        }
        public void OnGetGivetLodsedlerDescending()
        {
            BÃ¸rns = _bÃ¸rnService.GetGivetLodsedlerDescending();
        }

        public void OnGetGivetLodsedlerAscending()
        {
            BÃ¸rns = _bÃ¸rnService.GetGivetLodsedlerAscending();
        }




        //public void OnGetAllBÃ¸rnItems(string BÃ¸rn, string Navn)
        //{
        //    BÃ¸rns = _bÃ¸rnService.GetAllBÃ¸rnItems(BÃ¸rn, Navn);
        //}

    }
}

