using drink_finder_restapi.Domain.Repositories;
using drink_finder_restapi.Domain.Models;
using System;
using drink_finder_restapi.Persistence.Contexts;

namespace drink_finder_restapi.Persistence.Repositories
{
    public class DrinkRepository : BaseRepository, IDrinkRepository
    {
        public DrinkRepository(DFDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Drink>> ListAsync()
        {
            return await _context.drinks.ToListAsync();
        }
    }
}
