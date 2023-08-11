using drink_finder_restapi.Domain.Services;
using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Repositories;
using drink_finder_restapi.Domain.Services.Communication;

namespace drink_finder_restapi.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DrinkService(IDrinkRepository drinkRepository, IUnitOfWork unitOfWork)
        {
            _drinkRepository = drinkRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Drink>> ListAsync()
        {
            return await _drinkRepository.ListAsync();
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
    }
}
