using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Drink_Finder_WebApp.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            Response.Cookies.Delete("jwtToken");

            return RedirectToPage("Index");
        }
    }
}
