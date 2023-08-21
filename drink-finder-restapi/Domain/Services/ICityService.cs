using drink_finder_restapi.Domain.Models;

namespace drink_finder_restapi.Domain.Services
{
    public interface ICityService
    {
        Task<IEnumerable<City>> ListAsync();
    }
}
