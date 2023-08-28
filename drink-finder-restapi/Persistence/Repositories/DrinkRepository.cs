using drink_finder_restapi.Domain.Repositories;
using drink_finder_restapi.Domain.Models;
using System;
using drink_finder_restapi.Persistence.Contexts;
using drink_finder_restapi.Resources;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Drink>> GetAllDrinksInEstablishmentAsync(int establishmentId)
        {
            return await _context.establishmentDrinks.Where(ed => ed.EstablishemntId == establishmentId).Select(d => d.drink).ToListAsync();
        }

        public async Task AddAsync(Drink drink)
        {
            await _context.drinks.AddAsync(drink);
        }

        public async Task<IEnumerable<Drink>> pageGetAllAsync(int establishmentId, int pageNumber, int pageSize, int? category, string sortBy = "name", string sort = "asc")
        {
            var drinks = _context.establishmentDrinks.Where(ed => ed.EstablishemntId == establishmentId).Select(d => d.drink);


            if (category > -1)
            {
                drinks = drinks.Where(d => d.drinkCategoryId == category);
            }


            switch (sortBy)
            {
                case "name":
                    drinks = sort == "asc" ? drinks.OrderBy(d => d.Name) : drinks.OrderByDescending(d => d.Name);
                    break;
                case "volume":
                    drinks = sort == "asc" ? drinks.OrderBy(d => d.Volume) : drinks.OrderByDescending(d => d.Volume);
                    break;
                default:
                    break;
            }

            return await drinks.ToListAsync();
        }

    }
}
