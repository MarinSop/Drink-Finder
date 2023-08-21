using drink_finder_restapi.Domain.Models;

namespace drink_finder_restapi.Resources
{
    public class EstablishmentResource
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public CityResource? city { get; set; }
    }
}
