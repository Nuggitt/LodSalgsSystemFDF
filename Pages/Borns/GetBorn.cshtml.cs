using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Repository;
using LodSalgsSystemFDF.Services.ADOServices.ADOBornService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;

namespace LodSalgsSystemFDF.Pages.Borns
{
    [AllowAnonymous]
    public class GetBornModel : PageModel
    {
        private IBornService _bornService;
        private IGenericRepository<Born> _genericRepository;
        BornRepository _bornRepository;   

        [BindProperty]
        public string NameSearch { get; set; }
        public GetBornModel(IBornService bornService, IGenericRepository<Born> genericRepository, BornRepository bornRepository)
        {
            _bornService = bornService;
            _genericRepository = genericRepository;
            _bornRepository = bornRepository;
        }
        [BindProperty]
        public IEnumerable<Born> Borns { get; set; } = new List<Born>();
        public async Task OnGet() //Async almindelig
        {

            Borns = await _bornService.GetBorn();

        }

        //public IActionResult OnGet() //Generic
        //{
        //    //Borns = _bornRepository.GetAll();
        //    Borns = _genericRepository.GetAll();
        //    return Page();
        //}

        public async Task OnGetBornInBornegruppe(int id)
        {
            Borns = await _bornService.GetBornInBornegruppe(id);

        }

        public void OnGetNavnDescending()
        {
            Borns = _bornService.GetAllBornNavnDescending();
        }

        public void OnGetIDDescending()
        {
            Borns = _bornService.GetAllBornIDDescending();
        }

        public void OnGetAntalSolgteLodseddelerDescending()
        {
            Borns = _bornService.GetAllBornAntalSolgteLodseddelerDescending();
        }

        public void OnGetGruppeIDDescending()
        {
            Borns = _bornService.GetAllBornGruppeIDDescending();
        }

        public void OnGetNavnAscending()
        {
            Borns = _bornService.GetAllBornNavnAscending();
        }

        public void OnGetIDAscending()
        {
            Borns = _bornService.GetAllBornIDAscending();
        }

        public void OnGetAntalSolgteLodseddelerAscending()
        {
            Borns = _bornService.GetAllBornAntalSolgteLodseddelerAscending();
        }

        public void OnGetGruppeIDAscending()
        {
            Borns = _bornService.GetAllBornGruppeIDAscending();
        }

        public IActionResult OnPostBornByName()
        {
            if (NameSearch != null)
            {
                Borns = _bornService.GetBornByName(NameSearch);
                return Page();
            }
            
            return Page();
        }
        public void OnGetGivetLodsedlerDescending()
        {
            Borns = _bornService.GetGivetLodsedlerDescending();
        }

        public void OnGetGivetLodsedlerAscending()
        {
            Borns = _bornService.GetGivetLodsedlerAscending();
        }




        //public void OnGetAllBornItems(string Born, string Navn)
        //{
        //    Borns = _bornService.GetAllBornItems(Born, Navn);
        //}

    }
}
