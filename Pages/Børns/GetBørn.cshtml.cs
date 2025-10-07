using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Repository;
using LodSalgsSystemFDF.Services.ADOServices.ADOBørnService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;

namespace LodSalgsSystemFDF.Pages.Børns
{
    [AllowAnonymous]
    public class GetBørnModel : PageModel
    {
        private IBørnService _børnService;
        private IGenericRepository<Børn> _genericRepository;
        BørnRepository _børnRepository;   

        [BindProperty]
        public string NameSearch { get; set; }
        public GetBørnModel(IBørnService børnService, IGenericRepository<Børn> genericRepository, BørnRepository børnRepository)
        {
            _børnService = børnService;
            _genericRepository = genericRepository;
            _børnRepository = børnRepository;
        }
        [BindProperty]
        public IEnumerable<Børn> Børns { get; set; } = new List<Børn>();
        public async Task OnGet() //Async almindelig
        {

            Børns = await _børnService.GetBørn();

        }

        //public IActionResult OnGet() //Generic
        //{
        //    //Børns = _børnRepository.GetAll();
        //    Børns = _genericRepository.GetAll();
        //    return Page();
        //}

        public async Task OnGetBørnInBørnegruppe(int id)
        {
            Børns = await _børnService.GetBørnInBørnegruppe(id);

        }

        public void OnGetNavnDescending()
        {
            Børns = _børnService.GetAllBørnNavnDescending();
        }

        public void OnGetIDDescending()
        {
            Børns = _børnService.GetAllBørnIDDescending();
        }

        public void OnGetAntalSolgteLodseddelerDescending()
        {
            Børns = _børnService.GetAllBørnAntalSolgteLodseddelerDescending();
        }

        public void OnGetGruppeIDDescending()
        {
            Børns = _børnService.GetAllBørnGruppeIDDescending();
        }

        public void OnGetNavnAscending()
        {
            Børns = _børnService.GetAllBørnNavnAscending();
        }

        public void OnGetIDAscending()
        {
            Børns = _børnService.GetAllBørnIDAscending();
        }

        public void OnGetAntalSolgteLodseddelerAscending()
        {
            Børns = _børnService.GetAllBørnAntalSolgteLodseddelerAscending();
        }

        public void OnGetGruppeIDAscending()
        {
            Børns = _børnService.GetAllBørnGruppeIDAscending();
        }

        public IActionResult OnPostBørnByName()
        {
            if (NameSearch != null)
            {
                Børns = _børnService.GetBørnByName(NameSearch);
                return Page();
            }
            
            return Page();
        }
        public void OnGetGivetLodsedlerDescending()
        {
            Børns = _børnService.GetGivetLodsedlerDescending();
        }

        public void OnGetGivetLodsedlerAscending()
        {
            Børns = _børnService.GetGivetLodsedlerAscending();
        }




        //public void OnGetAllBørnItems(string Børn, string Navn)
        //{
        //    Børns = _børnService.GetAllBørnItems(Børn, Navn);
        //}

    }
}
