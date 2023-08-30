using drink_finder_restapi.Domain.Models;

namespace drink_finder_restapi.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> getByUsernameAsync(string username);
        Task registerUser(User user);
        Task<Establishment> getEstablishmentByIdAndUserId(int establishmentId,int userId);
        Task<IEnumerable<Establishment>> getAllEstablishmentsPageAsync(int userId, string query, string cityFilter, string sortBy, string sort);
        Task AddEstablishmentAsync(Establishment establishment);
        Task AddEstablishmentDrinkAsync(EstablishmentDrink establishmentDrink);
        Task<Establishment> FindEstablishmentByIdAndUserIdAsync(int id, int userId);
        Task<EstablishmentDrink> FindEstablishmentDrinkByIdsAsync(int establishmentId,int drinkId);
        void UpdateEstablishment(Establishment establishment);
        void RemoveEstablishment(Establishment establishment);
        void RemoveEstablishmentDrink(EstablishmentDrink establishmentDrink);
    }
}
