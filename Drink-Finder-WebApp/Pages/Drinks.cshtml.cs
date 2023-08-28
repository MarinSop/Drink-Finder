using Drink_Finder_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Net.Http;
using System.Text.Json;

namespace Drink_Finder_WebApp.Pages
{
    public class DrinksModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int establishmentId { get; set; }
        public List<Drink> drinkList { get; set; }
        public List<DrinkCategory> categories { get; set; }
        public int totalPages { get; set; }
        public int totalItems { get; set; }
        public int pgNum { get; set; }
        public int pgSize { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;

        public DrinksModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task OnGet(int pageNumber = 1, int pageSize = 10, int category = -1, string sortBy = "name", string sort="asc")
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
                foreach(var drink in drinkList)
                {
                    Console.WriteLine(drink.name);
                }
                Console.WriteLine(totalPages);
                Console.WriteLine(totalItems);
            }

            HttpResponseMessage categoryResponse = await httpClient.GetAsync("https://localhost:7082/api/drinkcategories");
            if (categoryResponse.IsSuccessStatusCode)
            {
                string responseContent = await categoryResponse.Content.ReadAsStringAsync();
                categories = JsonSerializer.Deserialize<List<DrinkCategory>>(responseContent);
            }
        }
    }
}
