using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Services.Communication;
using drink_finder_restapi.Resources;

namespace drink_finder_restapi.Domain.Services
{
    public interface IDrinkService
    {
        Task<IEnumerable<Drink>> ListAsync();
        Task<IEnumerable<Drink>> GetAllDrinksInEstablishmentAsync(int establishmentId);
        Task<SaveDrinkResponse> SaveAsync(Drink drink);
        Task<PageResource<DrinkResource>> pageGetAllAsync(int establishmentId, int pageNumber, int pageSize, int? category, string sortBy = "name", string sort = "asc");

    }
}
