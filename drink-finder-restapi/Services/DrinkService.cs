using drink_finder_restapi.Domain.Services;
using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Repositories;
using drink_finder_restapi.Domain.Services.Communication;
using drink_finder_restapi.Resources;
using drink_finder_restapi.Persistence.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using AutoMapper;

namespace drink_finder_restapi.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DrinkService(IDrinkRepository drinkRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _drinkRepository = drinkRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Drink>> ListAsync()
        {
            return await _drinkRepository.ListAsync();
        }

        public async Task<IEnumerable<Drink>> GetAllDrinksInEstablishmentAsync(int establishmentId)
        {
            return await _drinkRepository.GetAllDrinksInEstablishmentAsync(establishmentId);
        }

        public async Task<SaveDrinkResponse> SaveAsync(Drink drink)
        {
            try 
            {
                await _drinkRepository.AddAsync(drink);
                await _unitOfWork.CompleteAsync();

                return new SaveDrinkResponse(drink);
            }
            catch (Exception ex)
            {
                return new SaveDrinkResponse($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<PageResource<DrinkResource>> pageGetAllAsync(int establishmentId, int pageNumber, int pageSize, int? category, string sortBy = "name", string sort = "asc")
        {
            var drinks = await _drinkRepository.pageGetAllAsync(establishmentId, pageNumber, pageSize, category, sortBy, sort);
            int totalItems = drinks.Count();
            drinks = drinks.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var resources = _mapper.Map<IEnumerable<Drink>, IEnumerable<DrinkResource>>(drinks);
            var pageResource = new PageResource<DrinkResource>
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
