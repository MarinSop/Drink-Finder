using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Drink_Finder_WebApp.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];

            var httpClient = _httpClientFactory.CreateClient();
            var url = "https://localhost:7082/api/Users/register?username=" + username + "&password=" + password;
            var response = await httpClient.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Login");
            }
            return RedirectToPage("Register", new { error = "Bad credentials." });

        }
    }
}
