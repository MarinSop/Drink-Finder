using AutoMapper;
using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Services;
using drink_finder_restapi.Resources;
using Microsoft.AspNetCore.Mvc;

namespace drink_finder_restapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CitiesController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CityResource>> GetAllAsync()
        {
            var cities = await _cityService.ListAsync();
            var resources = _mapper.Map<IEnumerable<City>, IEnumerable<CityResource>>(cities);
            return resources;
        }
    }
}
