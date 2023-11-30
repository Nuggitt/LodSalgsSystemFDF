using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.BrugerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace LodSalgsSystemFDF.Pages.Admin
{
    [Authorize(Roles = "admin")]
    public class CreateUserModel : PageModel
    {
        private BrugerService _brugerService;
        [BindProperty]
        public string BrugerNavn { get; set; }
        [BindProperty,DataType(DataType.Password)]
        public string Password { get; set; }

        public CreateUserModel(BrugerService brugerService)
        {
            _brugerService = brugerService;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _brugerService.AddBruger(new Bruger(BrugerNavn, Password));
            return RedirectToPage("/Børns/GetBørn");
        }
    }
}
