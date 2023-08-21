using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Resources;

namespace drink_finder_restapi.Domain.Repositories
{
    public interface IDrinkRepository
    {
        Task<IEnumerable<Drink>> ListAsync();
        Task<IEnumerable<Drink>> GetAllDrinksInEstablishmentAsync(int establishmentId);
        Task AddAsync(Drink drink);
    }
}
