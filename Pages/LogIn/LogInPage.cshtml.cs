using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.BrugerService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LodSalgsSystemFDF.Pages.LogIn
{
    public class LogInPageModel : PageModel
    {
        private readonly AdonetBrugerService _adonetBrugerService;
        public static Bruger? LoggedInBruger { get; set; }

        [BindProperty]
        public string BrugerNavn { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public LogInPageModel(AdonetBrugerService adonetBrugerService)
        {
            _adonetBrugerService = adonetBrugerService;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            var navn = (BrugerNavn ?? "").Trim();
            var rawPwd = (Password ?? "").Trim();

            var brugere = _adonetBrugerService.GetAllBrugere();
            var bruger = brugere.FirstOrDefault(b =>
                string.Equals(b.BrugerNavn?.Trim(), navn, StringComparison.OrdinalIgnoreCase));

            if (bruger is null)
            {
                Message = "Ugyldigt login (bruger findes ikke)";
                return Page();
            }

            // Her bruger vi nu din VerifyPassword
            bool ok = _adonetBrugerService.VerifyPassword(navn, rawPwd, bruger.Password);

            if (!ok)
            {
                Message = "Ugyldigt login (forkert kode)";
                return Page();
            }

            // --- Login success ---
            LoggedInBruger = bruger;
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, bruger.BrugerNavn) };

            switch (navn.ToLowerInvariant())
            {
                case "admin": claims.Add(new Claim(ClaimTypes.Role, "admin")); break;
                case "leder": claims.Add(new Claim(ClaimTypes.Role, "leder")); break;
                case "lotteribestyrer": claims.Add(new Claim(ClaimTypes.Role, "lotteribestyrer")); break;
                case "bestyrer": claims.Add(new Claim(ClaimTypes.Role, "bestyrer")); break;
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

            return RedirectToPage("/Index");
        }
    }
}
