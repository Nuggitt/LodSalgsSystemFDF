using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LodSalgsSystemFDF.Pages.LogIn
{
    public class LogOutPageModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            LogInPageModel.LoggedInBruger = null;

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/index");
        }
    }
}
