using drink_finder_restapi.Domain.Services;
using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Repositories;
namespace drink_finder_restapi.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IDrinkRepository _drinkRepository;

        public DrinkService(IDrinkRepository drinkRepository)
        {
            this._drinkRepository = drinkRepository;
        }

        public async Task<IEnumerable<Drink>> ListAsync()
        {
            return await _drinkRepository.ListAsync();
        }
    }
}
