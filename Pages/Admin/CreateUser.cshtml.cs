using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.BrugerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private PasswordHasher<string> passwordHasher;
        public string Message { get; set; }

        public CreateUserModel(BrugerService brugerService)
        {
            _brugerService = brugerService;
            passwordHasher = new PasswordHasher<string>();
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
            _brugerService.AddBruger(new Bruger(BrugerNavn, passwordHasher.HashPassword(null, Password)));
            Message = $"Brugeren: {BrugerNavn} er oprettet.";
            return Page();
        }
    }
}
