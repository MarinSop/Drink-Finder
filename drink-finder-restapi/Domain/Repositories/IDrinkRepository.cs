using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Resources;

namespace drink_finder_restapi.Domain.Repositories
{
    public interface IDrinkRepository
    {
        Task<IEnumerable<Drink>> ListAsync();
        Task<IEnumerable<Drink>> GetAllDrinksInEstablishmentAsync(int establishmentId);
        Task AddAsync(Drink drink);
        Task<IEnumerable<Drink>> pageGetAllAsync(int establishmentId,int pageNumber, int pageSize, int? category, string sortBy = "name", string sort = "asc");
    }
}
