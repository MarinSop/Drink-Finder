using Drink_Finder_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Drink_Finder_WebApp.Pages.Delete
{
    public class DeleteDrinksModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int establishmentId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int drinkId { get; set; }
        private readonly IHttpClientFactory _httpClientFactory;

        public DeleteDrinksModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> OnGet()
        {
            if (Request.Cookies.TryGetValue("jwtToken", out var token) && !string.IsNullOrEmpty(token))
            {
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                string apiUrl =
                "https://localhost:7082/api/users/establishmentDrinks?establishmentId=" + establishmentId + "&drinkId=" + drinkId;
                HttpResponseMessage response = await httpClient.DeleteAsync(apiUrl);
                return Redirect("/edit-drinks/" + establishmentId);
            }
            return RedirectToPage("Login");
        }
    }
}
