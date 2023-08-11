using drink_finder_restapi.Domain.Models;

namespace drink_finder_restapi.Resources
{
    public class DrinkResource
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public double Volume { get; set; }
        public int drinkCategoryId { get; set; }

    }
}
