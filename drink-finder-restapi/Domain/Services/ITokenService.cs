using drink_finder_restapi.Domain.Models;

namespace drink_finder_restapi.Domain.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
