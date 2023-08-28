 using AutoMapper;
using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Repositories;
using drink_finder_restapi.Domain.Services;
using drink_finder_restapi.Resources;

namespace drink_finder_restapi.Services
{
    public class EstablishmentService : IEstablishmentService
    {
        private readonly IEstablishmentRepository _establishmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EstablishmentService(IEstablishmentRepository establishmentRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _establishmentRepository = establishmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Establishment>> ListAsync()
        {
            return await _establishmentRepository.ListAsync();
        }

        public async Task<IEnumerable<Establishment>> searchAsync(string query, string cityFilter, string sortBy)
        {
            return await _establishmentRepository.searchAsync(query, cityFilter, sortBy);
        }

        public async Task<PageResource<EstablishmentResource>> pageSearchAsync(int pageNumber, int pageSize, string query, string cityFilter, string sortBy, string sort)
        {
            var establishments = await _establishmentRepository.pageSearchAsync(pageNumber, pageSize, query, cityFilter, sortBy, sort);
            int totalItems = establishments.Count();
            establishments = establishments.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var resources = _mapper.Map<IEnumerable<Establishment>, IEnumerable<EstablishmentResource>>(establishments);
            var pageResource = new PageResource<EstablishmentResource>
            {
                Items = resources.ToList(),
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return pageResource;
        }
    }
}
