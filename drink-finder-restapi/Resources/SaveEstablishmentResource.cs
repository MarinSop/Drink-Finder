using drink_finder_restapi.Domain.Models;

namespace drink_finder_restapi.Resources
{
    public class SaveEstablishmentResource
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int CityId { get; set; }

    }
}
