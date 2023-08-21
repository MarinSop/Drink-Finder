using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Repositories;
using drink_finder_restapi.Domain.Services;

namespace drink_finder_restapi.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CityService(ICityRepository cityRepository, IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<City>> ListAsync()
        {
            return await _cityRepository.ListAsync();
        }
    }
}
