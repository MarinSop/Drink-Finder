using Drink_Finder_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Drink_Finder_WebApp.Pages
{
    public class AddEstablishmentsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public List<City> cities { get; set; }

        public AddEstablishmentsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> OnGet()
        {
            if (Request.Cookies.TryGetValue("jwtToken", out var token) && !string.IsNullOrEmpty(token))
            {
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage responseCities = await httpClient.GetAsync("https://localhost:7082/api/cities");
                if (responseCities.IsSuccessStatusCode)
                {
                    string responseContent = await responseCities.Content.ReadAsStringAsync();
                    cities = JsonSerializer.Deserialize<List<City>>(responseContent);
                }
                return Page();
            }
            return RedirectToPage("Login");
        }

        public async Task<IActionResult> OnPost()
        {
            if (Request.Cookies.TryGetValue("jwtToken", out var token) && !string.IsNullOrEmpty(token))
            {
                var establishment = new Establishment
                {
                    name = Request.Form["name"],
                    address = Request.Form["address"],
                    cityId = int.Parse(Request.Form["city-select"])
                }; 
                if(establishment.cityId < 0)
                {
                    return RedirectToPage("AddEstablishments");
                }
                var json = JsonSerializer.Serialize(establishment);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                await httpClient.PostAsync("https://localhost:7082/api/users/establishments",content);
                return RedirectToPage("Establishments");
            }
            else
            {
                return RedirectToPage("Login");
            }
        }
    }
}
