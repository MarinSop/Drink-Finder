using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Repositories;
using drink_finder_restapi.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace drink_finder_restapi.Persistence.Repositories
{
    public class DrinkCategoryRepository : BaseRepository, IDrinkCategoryRepository
    {
        public DrinkCategoryRepository(DFDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DrinkCategory>> ListAsync()
        {
            return await _context.drinkCategories.ToListAsync();
        }
    }

}

