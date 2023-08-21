using drink_finder_restapi.Domain.Models;

namespace drink_finder_restapi.Domain.Repositories
{
    public interface IDrinkCategoryRepository
    {
        Task<IEnumerable<DrinkCategory>> ListAsync();
    }
}
