using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace drink_finder_restapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrinksController : Controller
    {

        private readonly IDrinkService _drinkService;

        public DrinksController(IDrinkService drinkService)
        {
            _drinkService = drinkService;
        }

        [HttpGet]
        public async Task<IEnumerable<Drink>> GetAllAsync()
        {
            var drinks = await _drinkService.ListAsync();
            return drinks;
        }
    }
}
