using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Resources;

namespace drink_finder_restapi.Domain.Services
{
    public interface IEstablishmentService
    {
        Task<IEnumerable<Establishment>> ListAsync();
        Task<IEnumerable<Establishment>> searchAsync(string query, string cityFilter, string sortBy);
        Task<PageResource<EstablishmentResource>> pageSearchAsync(int pageNumber, int pageSize, string query, string cityFilter, string sortBy);
    }
}
