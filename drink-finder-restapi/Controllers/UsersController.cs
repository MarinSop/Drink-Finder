using AutoMapper;
using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Services;
using drink_finder_restapi.Extensions;
using drink_finder_restapi.Resources;
using drink_finder_restapi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace drink_finder_restapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> authenticate(string username, string password)
        {
            var token = await _userService.authenticate(username, password);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<IActionResult> register(string username, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            var result = await _userService.register(username, password);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var userResource = _mapper.Map<User, UserResource>(result._user);
            return Ok(userResource);
        }

        [HttpGet("establishments/{establishmentId}")]
        [Authorize]
        public async Task<IActionResult> getEstablishmentByIdAndUserId(int establishmentId)
        {
            var establishment = await _userService.getEstablishmentByIdAndUserId(establishmentId);
            if (establishment == null)
            {
                return NotFound();
            }
            var resource = _mapper.Map<Establishment, EstablishmentResource>(establishment);
            return Ok(resource);
        }

        [HttpGet("establishments/page")]
        [Authorize]
        public async Task<PageResource<EstablishmentResource>> getAllEstablishmentsPageAsync(int pageNumber = 1, int pageSize = 10, string? query = "", string? cityFilter = "", string? sortBy = "name", string? sort = "asc")
        {
            return await _userService.getAllEstablishmentsPageAsync(pageNumber,pageSize,query,cityFilter,sortBy,sort);
        }

        [HttpPost("establishments")]
        [Authorize]
        public async Task<IActionResult> PostEstablishmentAsync([FromBody] SaveEstablishmentResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            var establishment = _mapper.Map<SaveEstablishmentResource, Establishment>(resource);
            var result = await _userService.SaveEstablishmentAsync(establishment);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var drinkResource = _mapper.Map<Establishment, EstablishmentResource>(result._establishment);
            return Ok(drinkResource);
        }
        [HttpPost("establishmentDrinks")]
        [Authorize]
        public async Task<IActionResult> PostEstablishmentDrinkAsync([FromBody] SaveEstablishmentDrinkResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            var establishmentDrink = _mapper.Map<SaveEstablishmentDrinkResource, EstablishmentDrink>(resource);
            var result = await _userService.SaveEstablishmentDrinkAsync(establishmentDrink);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var drinkResource = _mapper.Map<EstablishmentDrink, EstablishmentDrinkResource>(result._establishmentDrink);
            return Ok(drinkResource);
        }

        [HttpPut("establishments/{id}")]
        [Authorize]
        public async Task<IActionResult> PutEstablishmentAsync(int id, [FromBody] SaveEstablishmentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var establishment = _mapper.Map<SaveEstablishmentResource, Establishment>(resource);
            var result = await _userService.UpdateEstablishmentAsync(id, establishment);

            if (!result.Success)
                return BadRequest(result.Message);

            var establishmentResource = _mapper.Map<Establishment, EstablishmentResource>(result._establishment);
            return Ok(establishmentResource);
        }

        [HttpDelete("establishments/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteEstablishmentAsync(int id)
        {
            var result = await _userService.DeleteEstablishmentAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var establishmentResource = _mapper.Map<Establishment, EstablishmentResource>(result._establishment);
            return Ok(establishmentResource);
        }
        [HttpDelete("establishmentDrinks")]
        [Authorize]
        public async Task<IActionResult> DeleteEstablishmentDrinkAsync(int establishmentId, int drinkId)
        {
            var result = await _userService.DeleteEstablishmentDrinkAsync(establishmentId, drinkId);

            if (!result.Success)
                return BadRequest(result.Message);

            var establishmentDrinkResource = _mapper.Map<EstablishmentDrink, EstablishmentDrinkResource>(result._establishmentDrink);
            return Ok(establishmentDrinkResource);
        }
    }
}
