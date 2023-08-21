using drink_finder_restapi.Domain.Models;

namespace drink_finder_restapi.Domain.Repositories
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> ListAsync();

    }
}
