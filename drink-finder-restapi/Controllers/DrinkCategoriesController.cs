using AutoMapper;
using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Services;
using drink_finder_restapi.Resources;
using Microsoft.AspNetCore.Mvc;

namespace drink_finder_restapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrinkCategoriesController : Controller
    {
        private readonly IDrinkCategoryService _drinkCategoryService;
        private readonly IMapper _mapper;

        public DrinkCategoriesController(IDrinkCategoryService drinkCategoryService, IMapper mapper)
        {
            _drinkCategoryService = drinkCategoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<DrinkCategoryResource>> GetAllAsync()
        {
            var drinkCategories = await _drinkCategoryService.ListAsync();
            var resources = _mapper.Map<IEnumerable<DrinkCategory>, IEnumerable<DrinkCategoryResource>>(drinkCategories);
            return resources;
        }
    }
}
