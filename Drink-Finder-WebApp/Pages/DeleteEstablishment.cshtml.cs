using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Drink_Finder_WebApp.Pages
{
    public class DeleteEstablishmentModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int establishmentId { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;

        public DeleteEstablishmentModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnGet()
        {
            if (Request.Cookies.TryGetValue("jwtToken", out var token) && !string.IsNullOrEmpty(token))
            {
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                await httpClient.DeleteAsync("https://localhost:7082/api/Users/establishments/" + establishmentId);

                return RedirectToPage("Establishments");
            }
            else
            {
                return RedirectToPage("Login");
            }
        }
    }
}
