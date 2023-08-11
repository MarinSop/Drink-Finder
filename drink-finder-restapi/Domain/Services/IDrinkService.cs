using drink_finder_restapi.Domain.Models;
using drink_finder_restapi.Domain.Services.Communication;

namespace drink_finder_restapi.Domain.Services
{
    public interface IDrinkService
    {
        Task<IEnumerable<Drink>> ListAsync();
        Task<SaveDrinkResponse> SaveAsync(Drink drink);
    }
}
