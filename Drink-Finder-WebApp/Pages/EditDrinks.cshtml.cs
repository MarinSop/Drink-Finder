using Drink_Finder_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Drink_Finder_WebApp.Pages
{
    public class EditDrinksModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int establishmentId { get; set; }
        public List<Drink> drinkList { get; set; }
        public List<Drink> allDrinks { get; set; }
        public List<DrinkCategory> categories { get; set; }
        public int totalPages { get; set; }
        public int totalItems { get; set; }
        public int pgNum { get; set; }
        public int pgSize { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;

        public EditDrinksModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task OnGet(int pageNumber = 1, int pageSize = 10, int category = -1, string sortBy = "name", string sort = "asc")
        {
            pgNum = pageNumber;
            pgSize = pageSize;
            var httpClient = _httpClientFactory.CreateClient();
            string apiUrl =
            "https://localhost:7082/api/drinks/establishments/" + establishmentId + "/page?pageNumber=" + pageNumber + "&pageSize=" + pageSize + "&category=" + category + "&sortBy=" + sortBy + "&sort=" + sort;
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                Page<Drink> drinkPage = JsonSerializer.Deserialize<Page<Drink>>(responseContent);
                drinkList = drinkPage.items;
                totalItems = drinkPage.totalItems;
                totalPages = drinkPage.totalPages;
            }

            HttpResponseMessage categoryResponse = await httpClient.GetAsync("https://localhost:7082/api/drinkcategories");
            if (categoryResponse.IsSuccessStatusCode)
            {
                string responseContent = await categoryResponse.Content.ReadAsStringAsync();
                categories = JsonSerializer.Deserialize<List<DrinkCategory>>(responseContent);
            }

            HttpResponseMessage allDrinksResponse = await httpClient.GetAsync("https://localhost:7082/api/drinks");
            if (allDrinksResponse.IsSuccessStatusCode)
            {
                string responseContent = await allDrinksResponse.Content.ReadAsStringAsync();
                allDrinks = JsonSerializer.Deserialize<List<Drink>>(responseContent);
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (Request.Cookies.TryGetValue("jwtToken", out var token) && !string.IsNullOrEmpty(token))
            {
                var establishmentDrink = new EstablishmentDrink
                {
                    establishemntId = establishmentId,
                    drinkId = int.Parse(Request.Form["drink-select"]),
                    price = double.Parse(Request.Form["price"])
                };
                if(establishmentDrink.drinkId < 0)
                {
                    return Redirect("/edit-drinks/" + establishmentId);
                }
                var json = JsonSerializer.Serialize(establishmentDrink);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var httpClient = _httpClientFactory.CreateClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                await httpClient.PostAsync("https://localhost:7082/api/users/establishmentDrinks", content);
                return Redirect("/edit-drinks/" + establishmentId);
            }
            else
            {
                return RedirectToPage("Login");
            }
        }
    }
}

