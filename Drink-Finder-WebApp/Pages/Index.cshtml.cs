using Drink_Finder_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;

namespace Drink_Finder_WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public List<Establishment> establishments { get; set; }
        public List<City> cities { get; set; }
        public int totalPages { get; set; }
        public int totalItems { get; set; }
        public int pgNum { get; set; }
        public int pgSize { get; set; }

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGet(int pageNumber = 1, int pageSize = 1, string query = "", string cityFilter = "", string sortBy = "name")
        {
            pgNum = pageNumber;
            pgSize = pageSize;
            var httpClient = _httpClientFactory.CreateClient();
            string apiUrl = "https://localhost:7082/api/establishments/page-search?" + "pageNumber=" + pageNumber + "&pageSize=" + pageSize + "&sortBy=" + sortBy;
            apiUrl = string.IsNullOrEmpty(query) ? apiUrl : apiUrl + "&query=" + query;
            apiUrl = string.IsNullOrEmpty(cityFilter) ? apiUrl : apiUrl + "&cityFilter=" + cityFilter;
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                Page<Establishment> establishmentPage = JsonSerializer.Deserialize<Page<Establishment>>(responseContent);
                establishments = establishmentPage.items;
                totalPages = establishmentPage.totalPages;
                totalItems = establishmentPage.totalItems;
           }

            HttpResponseMessage responseCities = await httpClient.GetAsync("https://localhost:7082/api/cities");
            if(responseCities.IsSuccessStatusCode)
            {
                string responseContent = await responseCities.Content.ReadAsStringAsync();
                cities = JsonSerializer.Deserialize<List<City>>(responseContent);
            }
        }

        public IActionResult OnPost()
        {
            string query = Request.Form["query"];
            string cityFilter = Request.Form["city-filter"];
            var parameters = new Dictionary<string, object>
            {
                { "query", query },
                { "cityFilter", cityFilter }
            };
                return RedirectToPage("Index", parameters);
        }
    }
}
