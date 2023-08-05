using drink_finder_restapi.Domain.Models;

namespace drink_finder_restapi.Domain.Repositories
{
    public interface IDrinkRepository
    {
        Task<IEnumerable<Drink>> ListAsync();
    }
}
