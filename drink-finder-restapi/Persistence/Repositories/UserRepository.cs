using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Repositories;
using drink_finder_restapi.Persistence.Contexts;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Globalization;

namespace drink_finder_restapi.Persistence.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(DFDbContext context) : base(context)
        {
        }

        public async Task<User> getByUsernameAsync(string username)
        {
            return await _context.users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task registerUser(User user)
        {
            await _context.users.AddAsync(user);
        }

        public async Task<Establishment> getEstablishmentByIdAndUserId(int establishmentId,int userId)
        {
            return await _context.establishments.FirstOrDefaultAsync(e => e.Id == establishmentId && e.UserId == userId);
        }

        public async Task<IEnumerable<Establishment>> getAllEstablishmentsPageAsync(int userId, string query, string cityFilter, string sortBy, string sort)
        {
            var establishments = from e in _context.establishments select e;
            establishments = _context.establishments
            .Where(e => (e.Name.Contains(query) || e.establishemntDrinks.Any(ed => ed.drink.Name.Contains(query))) && e.UserId == userId)
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
                    establishments = sort == "asc" ? establishments.OrderBy(e => e.Name) : establishments.OrderByDescending(e => e.Name);
                    break;
                case "city":
                    establishments = sort == "asc" ? establishments.OrderBy(e => e.City.Name) : establishments.OrderByDescending(e => e.City.Name);
                    break;
                case "address":
                    establishments = sort == "asc" ? establishments.OrderBy(e => e.Address) : establishments.OrderByDescending(e => e.Address);
                    break;
                default:
                    break;
            }

            return await establishments.ToListAsync();
        }

        public async Task AddEstablishmentAsync(Establishment establishment)
        {
            await _context.establishments.AddAsync(establishment);
        }

        public async Task AddEstablishmentDrinkAsync(EstablishmentDrink establishmentDrink)
        {
            await _context.establishmentDrinks.AddAsync(establishmentDrink);
        }
        public async Task<Establishment> FindEstablishmentByIdAndUserIdAsync(int id, int userId)
        {
            return await _context.establishments.FirstOrDefaultAsync(e => e.UserId == userId && e.Id == id);
        }

        public void UpdateEstablishment(Establishment establishment)
        {
            _context.establishments.Update(establishment);
        }
        public void RemoveEstablishment(Establishment establishment)
        {
            _context.establishments.Remove(establishment);
        }
        public void RemoveEstablishmentDrink(EstablishmentDrink establishmentDrink)
        {
            _context.establishmentDrinks.Remove(establishmentDrink);
        }

        public async Task<EstablishmentDrink> FindEstablishmentDrinkByIdsAsync(int establishmentId, int drinkId)
        {
            return await _context.establishmentDrinks.FindAsync(establishmentId, drinkId);
        }
    }
}
