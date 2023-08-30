using AutoMapper;
using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Services;
using drink_finder_restapi.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using drink_finder_restapi.Extensions;
using drink_finder_restapi.Services;
using Microsoft.AspNetCore.Authorization;

namespace drink_finder_restapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DrinksController : Controller
    {

        private readonly IDrinkService _drinkService;
        private readonly IMapper _mapper;

        public DrinksController(IDrinkService drinkService, IMapper mapper)
        {
            _drinkService = drinkService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<DrinkResource>> GetAllAsync()
        {
            var drinks = await _drinkService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Drink>, IEnumerable<DrinkResource>>(drinks);
            return resources;        
        }

        [HttpGet("establishments/{establishmentId}")]
        public async Task<IEnumerable<DrinkResource>> GetAllDrinksInEstablishmentAsync(int establishmentId)
        {
            var drinks = await _drinkService.GetAllDrinksInEstablishmentAsync(establishmentId);
            var resources = _mapper.Map<IEnumerable<Drink>, IEnumerable<DrinkResource>>(drinks);
            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveDrinkResource resource)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            var drink = _mapper.Map<SaveDrinkResource, Drink>(resource);
            var result = await _drinkService.SaveAsync(drink);
            if(!result.Success)
            {
                return BadRequest(result.Message);
            }
            var drinkResource = _mapper.Map<Drink, DrinkResource>(result._drink);
            return Ok(drinkResource);
        }

        [HttpGet("establishments/{establishmentId}/page")]
        public async Task<PageResource<DrinkResource>> pageGetAllAsync(int establishmentId, int pageNumber = 1, int pageSize = 10, int category = -1, string sortBy = "name", string sort="asc")
        {
            return await _drinkService.pageGetAllAsync(establishmentId ,pageNumber, pageSize, category, sortBy, sort);
        }
    }
}
