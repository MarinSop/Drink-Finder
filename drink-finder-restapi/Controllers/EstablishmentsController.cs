using AutoMapper;
using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Services;
using drink_finder_restapi.Resources;
using drink_finder_restapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace drink_finder_restapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstablishmentsController : Controller
    {
        private readonly IEstablishmentService _establishmentService;
        private readonly IMapper _mapper;

        public EstablishmentsController(IEstablishmentService establishmentService, IMapper mapper)
        {
            _establishmentService = establishmentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<EstablishmentResource>> GetAllAsync()
        {
            var establishments = await _establishmentService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Establishment>, IEnumerable<EstablishmentResource>>(establishments);
            return resources;
        }

        [HttpGet("search")]
        public async Task<IEnumerable<EstablishmentResource>> searchAsync(string? query = "", string? cityFilter = "", string sortBy = "name")
        {
            var establishments = await _establishmentService.searchAsync(query,cityFilter,sortBy);
            var resources = _mapper.Map<IEnumerable<Establishment>, IEnumerable<EstablishmentResource>>(establishments);
            return resources;
        }

        [HttpGet("page-search")]
        public async Task<PageResource<EstablishmentResource>> pageSearchAsync(int pageNumber = 1, int pageSize = 10, string? query = "", string? cityFilter = "", string sortBy = "name", string sort = "asc")
        {
            return await _establishmentService.pageSearchAsync(pageNumber,pageSize,query,cityFilter,sortBy,sort);
        }

    }
}
