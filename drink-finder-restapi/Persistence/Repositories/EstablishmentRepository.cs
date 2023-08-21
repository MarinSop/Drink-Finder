using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Repositories;
using drink_finder_restapi.Persistence.Contexts;
using drink_finder_restapi.Resources;
using Microsoft.EntityFrameworkCore;

namespace drink_finder_restapi.Persistence.Repositories
{
    public class EstablishmentRepository : BaseRepository , IEstablishmentRepository
    {
        public EstablishmentRepository(DFDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Establishment>> ListAsync()
        {
            return await _context.establishments.ToListAsync();
        }

        public async Task AddAsync(Establishment establishment)
        {
            await _context.establishments.AddAsync(establishment);
        }

        public async Task<IEnumerable<Establishment>> searchAsync(string query, string cityFilter, string sortBy)
        {
            var establishments = from e in _context.establishments select e;
            establishments = _context.establishments
            .Where(e => e.Name.Contains(query) || e.establishemntDrinks.Any(ed => ed.drink.Name.Contains(query)))
            .Include(e => e.establishemntDrinks)
            .ThenInclude(ed => ed.drink)
            .Include(e => e.City);
            


            if (!string.IsNullOrEmpty(cityFilter))
            {
                establishments = establishments.Where(e => e.City.Name == cityFilter);
            }


            switch (sortBy)
            {
                case "name":
                    establishments.OrderBy(e => e.Name);
                    break;
                case "city":
                    establishments.OrderBy(e => e.City.Name);
                    break;
                case "address":
                    establishments.OrderBy(e => e.Address);
                    break;
                default:
                    break;
            }

            return await establishments.ToListAsync();
        }

        public async Task<IEnumerable<Establishment>> pageSearchAsync(int pageNumber, int pageSize, string query, string cityFilter, string sortBy)
        {
            var establishments = from e in _context.establishments select e;
            establishments = _context.establishments
            .Where(e => e.Name.Contains(query) || e.establishemntDrinks.Any(ed => ed.drink.Name.Contains(query)))
            .Include(e => e.establishemntDrinks)
            .ThenInclude(ed => ed.drink)
            .Include(e => e.City);


            if (!string.IsNullOrEmpty(cityFilter))
            {
                establishments = establishments.Where(e => e.City.Name == cityFilter);
            }


            switch (sortBy)
            {
                case "name":
                    establishments = establishments.OrderBy(e => e.Name);
                    break;
                case "city":
                    establishments = establishments.OrderBy(e => e.City.Name);
                    break;
                case "address":
                    establishments = establishments.OrderBy(e => e.Address);
                    break;
                default:
                    break;
            }

            return await establishments.ToListAsync();
        }
    }
}
