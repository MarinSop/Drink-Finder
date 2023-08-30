using drink_finder_restapi.Domain.Models;

namespace drink_finder_restapi.Domain.Repositories
{
    public interface IEstablishmentRepository
    {
        Task<IEnumerable<Establishment>> ListAsync();
        Task<IEnumerable<Establishment>> searchAsync(string query, string cityFilter, string sortBy);
        Task<IEnumerable<Establishment>> pageSearchAsync(string query, string cityFilter, string sortBy, string sort);
    }
}
