using Drink_Finder_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;

namespace Drink_Finder_WebApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(IHttpClientFactory httpClientFactory)
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
            var url = "https://localhost:7082/api/Users/authenticate?username=" + username + "&password=" + password;
            var response = await httpClient.PostAsync(url, null);

            if (response.IsSuccessStatusCode)
            {
                string token = await response.Content.ReadAsStringAsync();
                Console.WriteLine(token);
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddHours(1),
                    HttpOnly = true,
                    Secure = true,
                };

                Response.Cookies.Append("jwtToken", token, cookieOptions);
                return RedirectToPage("Index");
            }
            return RedirectToPage("Login", new { error = "Bad credentials." });

        }
    }
}
