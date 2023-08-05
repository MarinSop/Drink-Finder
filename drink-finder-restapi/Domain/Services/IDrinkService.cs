using drink_finder_restapi.Domain.Models;

namespace drink_finder_restapi.Domain.Services
{
    public interface IDrinkService
    {
        Task<IEnumerable<Drink>> ListAsync();
    }
}
