using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Repositories;
using drink_finder_restapi.Domain.Services;

namespace drink_finder_restapi.Services
{
    public class DrinkCategoryService : IDrinkCategoryService
    {
        private readonly IDrinkCategoryRepository _drinkCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DrinkCategoryService(IDrinkCategoryRepository drinkCategoryRepository, IUnitOfWork unitOfWork)
        {
            _drinkCategoryRepository = drinkCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DrinkCategory>> ListAsync()
        {
            return await _drinkCategoryRepository.ListAsync();
        }
    }
}
