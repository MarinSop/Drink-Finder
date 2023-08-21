using drink_finder_restapi.Domain.Models;

namespace drink_finder_restapi.Domain.Services
{
    public interface IDrinkCategoryService
    {
        Task<IEnumerable<DrinkCategory>> ListAsync();
    }
}
