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
        private readonly BrugerService _brugerService;

        [BindProperty] public string BrugerNavn { get; set; } = "";
        [BindProperty, DataType(DataType.Password)] public string Password { get; set; } = "";
        public string Message { get; set; } = "";

        public CreateUserModel(BrugerService brugerService)
        {
            _brugerService = brugerService;
        }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            var navn = (BrugerNavn ?? "").Trim();
            var raw = (Password ?? "").Trim();

            if (string.IsNullOrWhiteSpace(navn) || string.IsNullOrWhiteSpace(raw))
            {
                ModelState.AddModelError("", "Brugernavn og password er påkrævet.");
                return Page();
            }

            _brugerService.AddBruger(new Bruger { BrugerNavn = navn, Password = raw }); // <-- rå password
            Message = $"Brugeren: {navn} er oprettet.";
            return Page();
        }
    }
}

