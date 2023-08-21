using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Repositories;
using drink_finder_restapi.Persistence.Contexts;

namespace drink_finder_restapi.Persistence.Repositories
{
    public class CityRepository : BaseRepository, ICityRepository
    {
        public CityRepository(DFDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<City>> ListAsync()
        {
            return await _context.cities.ToListAsync();
        }
    }
}
