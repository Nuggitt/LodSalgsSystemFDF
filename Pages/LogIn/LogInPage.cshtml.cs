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
        public static Bruger LoggedInBruger { get; set; } = null;
        private BrugerService _brugerService;

        [BindProperty]
        public string BrugerNavn { get; set; }

        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }

        public string Message { get; set; }

        public LogInPageModel(BrugerService brugerService)
        {
            _brugerService = brugerService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {

            List<Bruger> brugere = _brugerService.Bruger;
            foreach (Bruger bruger in brugere)
            {

                if (BrugerNavn == bruger.BrugerNavn)
                {
                    var passwordHasher = new PasswordHasher<string>();

                    if (passwordHasher.VerifyHashedPassword(null, bruger.Password, Password) == PasswordVerificationResult.Failed)
                    {
                        LoggedInBruger = bruger;

                        var claims = new List<Claim> { new Claim(ClaimTypes.Name, BrugerNavn) };
                        
                        
                        if (BrugerNavn == "admin") claims.Add(new Claim(ClaimTypes.Role, "admin"));
                        if (BrugerNavn == "leder") claims.Add(new Claim(ClaimTypes.Role, "leder"));
                        if (BrugerNavn == "lotteribestyrer") claims.Add(new Claim(ClaimTypes.Role, "lotteribestyrer"));
                        if (BrugerNavn == "bestyrer") claims.Add(new Claim(ClaimTypes.Role, "bestyrer"));
                        
                            
                        



                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                        return RedirectToPage("/Index");
                    }

                }

            }

            Message = "Ugyldigt Login";
            return Page();
        }
    }
}
