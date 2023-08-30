using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Services.Communication;
using drink_finder_restapi.Resources;

namespace drink_finder_restapi.Domain.Services
{
    public interface IUserService
    {
        Task<string> authenticate(string username, string password);
        Task<SaveUserResponse> register(string username, string password);
        Task<Establishment> getEstablishmentByIdAndUserId(int establishmentId);
        Task<PageResource<EstablishmentResource>> getAllEstablishmentsPageAsync(int pageNumber, int pageSize, string query, string cityFilter, string sortBy, string sort);
        Task<SaveEstablishmentResponse> SaveEstablishmentAsync(Establishment establishment);
        Task<SaveEstablishmentDrinkResponse> SaveEstablishmentDrinkAsync(EstablishmentDrink establishmentDrink);
        Task<SaveEstablishmentResponse> UpdateEstablishmentAsync(int id, Establishment establishment);
        Task<SaveEstablishmentResponse> DeleteEstablishmentAsync(int id);
        Task<SaveEstablishmentDrinkResponse> DeleteEstablishmentDrinkAsync(int establishmentId, int drinkId);
    }
}
