using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.BrugerService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

namespace LodSalgsSystemFDF.Pages.LogIn
{
    public class LogInPageModel : PageModel
    {
        public static Bruger LoggedInBruger { get; set; } = null;
        private BrugerService _brugerService;

        [BindProperty]
        public string BrugerNavn { get; set; }

        [BindProperty,DataType(DataType.Password)]
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

                if (BrugerNavn == bruger.BrugerNavn && Password == bruger.Password)
                {

                    LoggedInBruger = bruger;

                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, BrugerNavn) };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToPage("/Børnegrupper/GetBørnegrupper");

                }

            }

            Message = "Invalid attempt";
            return Page();
        }
    }
}
